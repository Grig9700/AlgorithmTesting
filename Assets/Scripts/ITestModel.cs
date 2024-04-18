using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface ITestModel
{
    void Start();
    void NewTest();
    void BeforeReset(bool samplingCompleted);
    /// <summary>
    /// So that multiple samples can be made a reset for the current test is needed
    /// </summary>
    void ResetTest();
    void BeforeSample();
    TestPacket GetTestPacket();
    void AfterSample();
    Datapoint CreateDataEntry();
    bool TestCompleteCheck();
    void WriteEntries(IAlgorithm algorithm, StreamWriter dataSheet, int samplesPerTest);
}
