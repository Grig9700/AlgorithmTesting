using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AlgorithmObject : ScriptableObject, IAlgorithm
{
    public List<Datapoint> data;

    public virtual List<Datapoint> GetData()
    {
        return data;
    }

    public virtual string GetName()
    {
        return name;
    }

    public virtual void Initialize()
    {
        data = new List<Datapoint>();
    }

    public virtual void MakeDataEntry(Datapoint datapoint)
    {
        data.Add(datapoint);
    }
    public abstract bool Run(TestPacket packet);
}