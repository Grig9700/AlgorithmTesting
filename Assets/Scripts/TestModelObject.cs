using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class TestModelObject : ScriptableObject, ITestModel
{
    public abstract void Start();
    public abstract void NewTest();
    public abstract void BeforeReset(bool samplingCompleted);
    public abstract void ResetTest();
    public abstract void BeforeSample();
    public abstract TestPacket GetTestPacket();
    public abstract void AfterSample();
    public abstract Datapoint CreateDataEntry();
    public abstract bool TestCompleteCheck();
    public abstract void WriteEntries(IAlgorithm algorithm, StreamWriter dataSheet, int samplesPerTest);
}
