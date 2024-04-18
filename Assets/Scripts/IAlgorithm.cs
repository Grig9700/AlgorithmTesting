using System.Collections.Generic;

public interface IAlgorithm
{
    List<Datapoint> GetData();

    string GetName();
    
    void Initialize();

    void MakeDataEntry(Datapoint datapoint);

    bool Run(TestPacket packet);
}
