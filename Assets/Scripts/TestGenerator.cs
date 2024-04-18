using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.Profiling;

public class TestGenerator : MonoBehaviour
{
    [SerializeField] [Tooltip("Check this box if you want to overwrite the previous test result document")]
    private bool deletePreviousTestResult;
    [SerializeField] [Tooltip("The type of test you want to run")]
    private TestModelObject testModel;
    [SerializeField] 
    private List<AlgorithmObject> algorithms;
    [SerializeField] [Tooltip("Amount of samples gathered before moving on to next test")]
    private int samplesPerTest;
    
    private int _currentAlgorithm;
    private int _samplesThisTest;
    
    private CustomSampler _sampler;
    private Recorder _recorder;
    
    private void CreateDataSheet()
    {
        var route = Application.persistentDataPath + "/DataSheet.csv";

        var iterator = 1;
        while (File.Exists(route) && !deletePreviousTestResult)
        {
            route = Application.persistentDataPath + $"/DataSheet_{iterator}.csv";
            if(!File.Exists(route))
                break;
            iterator++;
        }
        
        var dataSheet = File.CreateText(route);

        foreach (var algorithm in algorithms)
        {
            testModel.WriteEntries(algorithm, dataSheet, samplesPerTest);
        }

        dataSheet.Close();

        Application.OpenURL(route);
    }

    private void OnApplicationQuit()
    {
        CreateDataSheet();
    }

    private void Start()
    {
        _sampler = CustomSampler.Create("DataSheet");
        _recorder = Recorder.Get("DataSheet");
        _recorder.enabled = true;
        
        testModel.Start();

        foreach (var algorithm in algorithms)
        {
            algorithm.Initialize();
        }
        
        NewTest();
    }

    private void NewTest()
    {
        _samplesThisTest = 0;
        testModel.NewTest();
    }

    private void RunTest()
    {
        var samplingCompleted = _samplesThisTest == samplesPerTest;
        if (samplingCompleted)
            _samplesThisTest = 0;
        
        testModel.BeforeReset(samplingCompleted);
        
        testModel.ResetTest();

        if (_samplesThisTest < samplesPerTest)
        {
            testModel.BeforeSample();
            
            _samplesThisTest++;
            
            _sampler.Begin();
            Debug.Log(algorithms[_currentAlgorithm].Run(testModel.GetTestPacket()) ? "Success" : "Failure");
            _sampler.End();
            
            testModel.AfterSample();

            var datapoint = testModel.CreateDataEntry();
            datapoint.sampleTime = _recorder.elapsedNanoseconds;

            algorithms[_currentAlgorithm].MakeDataEntry(datapoint);
        }

        if (testModel.TestCompleteCheck() || _samplesThisTest != samplesPerTest) 
            return;

        _currentAlgorithm++;
        if (_currentAlgorithm >= algorithms.Count || algorithms[_currentAlgorithm] == null)
        {
            EditorApplication.isPlaying = false;
            return;
        }
        
        NewTest();
    }
    
    private void Update()
    {
        RunTest();
    }
}