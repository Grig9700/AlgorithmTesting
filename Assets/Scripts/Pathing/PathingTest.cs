using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "PathingTest", menuName = "Tests/PathingTest", order = 1)]
public class PathingTest : TestModelObject
{
    [SerializeField] [Range(0, 1000)]
    public int numberOfNodes;
    [SerializeField] [Tooltip("How many nodes exist at start")]
    private int startingNodeCount;
    [SerializeField] [Tooltip("How many nodes are added each time the sample size changes")]
    private int nodeIncrease = 1;
    [SerializeField] [Tooltip("Displays the current number of nodes tested. Don't write values here")]
    private int currentNodeCount;
    
    [SerializeField]
    private GameObject startPrefab;
    [SerializeField]
    private GameObject endPrefab;
    [SerializeField]
    private GameObject nodePrefab;
    
    private List<GameObject> _nodeObjects;
    private List<Node> _nodes;
    private List<Node> _explored;
    private List<Node> _path;
    private Node _startNode;
    private Node _endNode;
    
    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;
    
    public override void Start()
    {
        _nodeObjects = new List<GameObject>();
        _nodes = new List<Node>();
        _explored = new List<Node>();
        _path = new List<Node>();
        
        GetWorldBounds();
    }
    
    private void GetWorldBounds()
    {
        var vertExtent = Camera.main!.GetComponent<Camera>().orthographicSize;
        var horizExtent = vertExtent * Screen.width / Screen.height;

        // Calculations assume map is position at the origin
        _minX = -horizExtent;
        _maxX = horizExtent;
        _minY = -vertExtent;
        _maxY = vertExtent;
    }

    public override void NewTest()
    {
        if (_nodeObjects.Count > 0)
        {
            foreach (var point in _nodeObjects)
            {
                DestroyImmediate(point);
            }
        }
        _nodeObjects.Clear();
        _nodes.Clear();
        
        GameObject[] points = GameObject.FindGameObjectsWithTag("Node");

        if (points.Length > 0)
        {
            foreach (var point in points)
            {
                DestroyImmediate(point);
            }
        }
        
        var sNode = Instantiate(startPrefab);
        _startNode = sNode.GetComponent<Node>();
        _nodeObjects.Add(sNode.gameObject);
        _nodes.Add(_startNode);
        
        var eNode = Instantiate(endPrefab);
        _endNode = eNode.GetComponent<Node>();
        _nodeObjects.Add(eNode.gameObject);
        _nodes.Add(_endNode);
        
        for (var i = _nodes.Count; i < startingNodeCount; i++)
        {
            SpawnNode();
        }
    }

    public override void BeforeReset(bool samplingCompleted)
    {
        if (_nodes.Count < numberOfNodes && samplingCompleted)
        {
            for (int i = 0; i < nodeIncrease; i++)
            {
                SpawnNode();
            }
        }
        currentNodeCount = _nodes.Count;
    }

    private void SpawnNode()
    {
        var nod = Instantiate(nodePrefab);
        var noodle = nod.GetComponent<Node>();
        _nodeObjects.Add(nod.gameObject);
        _nodes.Add(noodle);
        noodle.transform.position = new Vector2(Random.Range(_minX, _maxX), Random.Range(_minY, _maxY));
    }

    public override void ResetTest()
    {
        foreach (var node in _nodes)
        {
            node.explored = false;
            node.cameFrom = null;
            node.totalCostToReach = Single.PositiveInfinity;
        }
        _explored.Clear();
        _path?.Clear();
    }

    public override void BeforeSample()
    {
        //Nothing for this test
    }

    public override TestPacket GetTestPacket()
    {
        return new TestPacket()
        {
            selected = _startNode,
            target = _endNode,
            iteratorLimit = _nodes.Count
        };
    }

    public override void AfterSample()
    {
        foreach (var node in _nodes.Where(node => node.explored))
        {
            _explored.Add(node);
        }
    }

    public override Datapoint CreateDataEntry()
    {
        return new Datapoint
        {
            nodeCount = currentNodeCount,
            exploredNodes = _explored.Count
        };
    }

    public override bool TestCompleteCheck()
    {
        return _nodes.Count != numberOfNodes;
    }

    public override void WriteEntries(IAlgorithm algorithm, StreamWriter dataSheet, int samplesPerTest)
    {
        dataSheet.WriteLine(Environment.NewLine + Environment.NewLine + $"{algorithm.GetName()}" + Environment.NewLine);
        dataSheet.WriteLine($";Number of Nodes; Explored Nodes; %Nodes Explored; Time taken");
        var data = algorithm.GetData();
        for (var i = 0; i < data.Count; i += samplesPerTest)
        {
            float nodeCountAverage  = 0;
            for (var j = 0; j < samplesPerTest; j++)
            {
                nodeCountAverage += data[i + j].nodeCount;
            }
            nodeCountAverage = nodeCountAverage / samplesPerTest;

            float exploredNodeAverage = 0;
            for (var j = 0; j < samplesPerTest; j++)
            {
                exploredNodeAverage += data[i + j].exploredNodes;
            }
            exploredNodeAverage = exploredNodeAverage / samplesPerTest;

            float frameTimeAverage = 0;
            for (var j = 0; j < samplesPerTest; j++)
            {
                frameTimeAverage += data[i + j].sampleTime;
            }
            frameTimeAverage = frameTimeAverage / samplesPerTest;

            var percentage = (int)(exploredNodeAverage / nodeCountAverage * 100f);

            dataSheet.WriteLine($";{nodeCountAverage}; {exploredNodeAverage}; {percentage} ;{frameTimeAverage}");
        }
    }
}

[Serializable]
public struct Datapoint
{
    public float sampleTime;
    public int nodeCount;
    public int exploredNodes;
}

[Serializable]
public struct TestPacket
{
    public Node selected;
    public Node target;
    public int iteratorLimit;
}





// private void OnDrawGizmos()
// {
//     if (_explored == null)
//         return;
//     
//     Gizmos.color = Color.grey;
//
//     if (_explored.Count > 0)
//     {
//         foreach (var node in _explored.Where(node => node != null).Where(node => !node.isEndNode && !node.isStartNode && node.cameFrom != null))
//         {
//             Gizmos.DrawLine(node.transform.position, node.cameFrom.transform.position);
//         }
//     }
//
//     if (_path is not { Count: > 0 })
//         return;
//
//     Gizmos.color = Color.green;
//
//     foreach (var node in _path.Where(node => node != null).Where(node=> node.cameFrom != null))
//     {
//         Gizmos.DrawLine(node.transform.position, node.cameFrom.transform.position);
//     }
// }