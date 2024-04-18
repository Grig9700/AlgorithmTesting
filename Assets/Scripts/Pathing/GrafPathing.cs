using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.Profiling;
using System.IO;

[ExecuteAlways]
public class GrafPathing : MonoBehaviour
{
    // private CustomSampler sampler;
    // private Recorder recorder;
    //
    //
    //
    // [HideInInspector]
    // public AlgorithmType algorithm;
    // [HideInInspector]
    // public TestGenerator tester;
    // [HideInInspector]
    // public List<Node> nodes;
    //
    // [HideInInspector]
    // public Node start = null;
    // [HideInInspector]
    // public Node end = null;
    // private List<Node> path = null;
    // private bool endWasReached = false;
    //
    //
    // private List<Node> explored = null;
    //
    //
    // private List<Node> pathToGoal;
    // private bool DStarSwitch;
    // private List<Node> openList;
    //
    //
    // //private List<dataEntry> dataEntries;
    // private List<dataEntry> dataEntriesBreadthFirst;
    // private List<dataEntry> dataEntriesDijkstra;
    // private List<dataEntry> dataEntriesA_Star;
    // private List<dataEntry> dataEntriesD_Star;
    //
    //
    // private void OnDrawGizmos()
    // {
    //     //Gizmos.color = Color.grey;
    //
    //     //for (int i = 0; i < nodes.Count; i++)
    //     //{
    //     //    nodes[i].drawn = false;
    //     //}
    //
    //     //for (int i = 0; i < nodes.Count; i++)
    //     //{
    //     //    nodes[i].drawn = true;
    //
    //     //    for (int j = 0; j < nodes[i].neighbours.Count; j++)
    //     //    {
    //     //        Gizmos.DrawLine(nodes[i].transform.position, nodes[i].neighbours[j].neoughbour.transform.position);
    //     //    }
    //     //}
    //
    //     Gizmos.color = Color.grey;
    //
    //     if (explored.Count > 0)
    //     {
    //         for (int i = 0; i < explored.Count; i++)
    //         {
    //             if (!explored[i].isStartNode && !explored[i].isEndNode)
    //             {
    //                 if (explored[i] != null && explored[i].cameFrom != null)
    //                 {
    //                     Gizmos.DrawLine(explored[i].transform.position, explored[i].cameFrom.transform.position);
    //                 }
    //                 else
    //                 {
    //                     //Debug.Log("Explored is Null" + explored.Count);
    //                 }
    //             }
    //             else
    //             {
    //
    //             }
    //         }
    //     }
    //
    //     if (path != null)
    //     {
    //
    //         Gizmos.color = Color.green;
    //
    //         for (int i = 0; i < path.Count; i++)
    //         {
    //             if (path[i] != null && path[i].cameFrom != null)
    //             {
    //                 Gizmos.DrawLine(path[i].transform.position, path[i].cameFrom.transform.position);
    //             }
    //         }
    //     }
    // }
    //
    // private void CreateDataSheet()
    // {
    //     string route = Application.persistentDataPath + "/DataSheet.csv";
    //
    //     if(File.Exists(route))
    //     {
    //         File.Delete(route);
    //     }
    //
    //     var dataSheet = File.CreateText(route);
    //
    //
    //     dataSheet.WriteLine(System.Environment.NewLine + System.Environment.NewLine + $"Breadth First" + System.Environment.NewLine);
    //     dataSheet.WriteLine($";Number of Nodes; Explored Nodes; %Nodes Explored; Time taken");
    //     for (int i = 0; i < dataEntriesBreadthFirst.Count; i += tester.samplesPerTest)
    //     {
    //         float nodeCountAverage  = 0;
    //         for (int j = 0; j < tester.samplesPerTest; j++)
    //         {
    //             nodeCountAverage += dataEntriesBreadthFirst[i + j].nodeCount;
    //         }
    //         nodeCountAverage = nodeCountAverage / tester.samplesPerTest;
    //
    //         float exploredNodeAverage = 0;
    //         for (int j = 0; j < tester.samplesPerTest; j++)
    //         {
    //             exploredNodeAverage += dataEntriesBreadthFirst[i + j].exploredNodes;
    //         }
    //         exploredNodeAverage = exploredNodeAverage / tester.samplesPerTest;
    //
    //         float frameTimeAverage = 0;
    //         for (int j = 0; j < tester.samplesPerTest; j++)
    //         {
    //             frameTimeAverage += dataEntriesBreadthFirst[i + j].frameTime;
    //         }
    //         frameTimeAverage = frameTimeAverage / tester.samplesPerTest;
    //
    //         int percentage = (int)(exploredNodeAverage / nodeCountAverage * 100f);
    //
    //         dataSheet.WriteLine($";{nodeCountAverage}; {exploredNodeAverage}; {percentage} ;{frameTimeAverage}");
    //     }
    //
    //     dataSheet.WriteLine(System.Environment.NewLine + System.Environment.NewLine + $"Dijkstra" + System.Environment.NewLine);
    //     dataSheet.WriteLine($";Number of Nodes; Explored Nodes; %Nodes Explored; Time taken");
    //     for (int i = 0; i < dataEntriesDijkstra.Count; i += tester.samplesPerTest)
    //     {
    //         float nodeCountAverage = 0;
    //         for (int j = 0; j < tester.samplesPerTest; j++)
    //         {
    //             nodeCountAverage += dataEntriesDijkstra[i + j].nodeCount;
    //         }
    //         nodeCountAverage = nodeCountAverage / tester.samplesPerTest;
    //
    //         float exploredNodeAverage = 0;
    //         for (int j = 0; j < tester.samplesPerTest; j++)
    //         {
    //             exploredNodeAverage += dataEntriesDijkstra[i + j].exploredNodes;
    //         }
    //         exploredNodeAverage = exploredNodeAverage / tester.samplesPerTest;
    //
    //         float frameTimeAverage = 0;
    //         for (int j = 0; j < tester.samplesPerTest; j++)
    //         {
    //             frameTimeAverage += dataEntriesDijkstra[i + j].frameTime;
    //         }
    //         frameTimeAverage = frameTimeAverage / tester.samplesPerTest;
    //
    //         int percentage = (int)(exploredNodeAverage / nodeCountAverage * 100f);
    //
    //         dataSheet.WriteLine($";{nodeCountAverage}; {exploredNodeAverage}; {percentage} ;{frameTimeAverage}");
    //     }
    //
    //     dataSheet.WriteLine(System.Environment.NewLine + System.Environment.NewLine + $"A_Star" + System.Environment.NewLine);
    //     dataSheet.WriteLine($";Number of Nodes; Explored Nodes; %Nodes Explored; Time taken");
    //     for (int i = 0; i < dataEntriesA_Star.Count; i += tester.samplesPerTest)
    //     {
    //         float nodeCountAverage = 0;
    //         for (int j = 0; j < tester.samplesPerTest; j++)
    //         {
    //             nodeCountAverage += dataEntriesA_Star[i + j].nodeCount;
    //         }
    //         nodeCountAverage = nodeCountAverage / tester.samplesPerTest;
    //
    //         float exploredNodeAverage = 0;
    //         for (int j = 0; j < tester.samplesPerTest; j++)
    //         {
    //             exploredNodeAverage += dataEntriesA_Star[i + j].exploredNodes;
    //         }
    //         exploredNodeAverage = exploredNodeAverage / tester.samplesPerTest;
    //
    //         float frameTimeAverage = 0;
    //         for (int j = 0; j < tester.samplesPerTest; j++)
    //         {
    //             frameTimeAverage += dataEntriesA_Star[i + j].frameTime;
    //         }
    //         frameTimeAverage = frameTimeAverage / tester.samplesPerTest;
    //
    //         int percentage = (int)(exploredNodeAverage / nodeCountAverage * 100f);
    //
    //         dataSheet.WriteLine($";{nodeCountAverage}; {exploredNodeAverage}; {percentage} ;{frameTimeAverage}");
    //     }
    //
    //     dataSheet.WriteLine(System.Environment.NewLine + System.Environment.NewLine + $"D_Star" + System.Environment.NewLine);
    //     dataSheet.WriteLine($";Number of Nodes; Explored Nodes; %Nodes Explored; Time taken");
    //     for (int i = 0; i < dataEntriesD_Star.Count; i += tester.samplesPerTest)
    //     {
    //         float nodeCountAverage = 0;
    //         for (int j = 0; j < tester.samplesPerTest; j++)
    //         {
    //             nodeCountAverage += dataEntriesD_Star[i + j].nodeCount;
    //         }
    //         nodeCountAverage = nodeCountAverage / tester.samplesPerTest;
    //
    //         float exploredNodeAverage = 0;
    //         for (int j = 0; j < tester.samplesPerTest; j++)
    //         {
    //             exploredNodeAverage += dataEntriesD_Star[i + j].exploredNodes;
    //         }
    //         exploredNodeAverage = exploredNodeAverage / tester.samplesPerTest;
    //
    //         float frameTimeAverage = 0;
    //         for (int j = 0; j < tester.samplesPerTest; j++)
    //         {
    //             frameTimeAverage += dataEntriesD_Star[i + j].frameTime;
    //         }
    //         frameTimeAverage = frameTimeAverage / tester.samplesPerTest;
    //
    //         int percentage = (int)(exploredNodeAverage / nodeCountAverage * 100f);
    //
    //         dataSheet.WriteLine($";{nodeCountAverage}; {exploredNodeAverage}; {percentage} ;{frameTimeAverage}");
    //     }
    //
    //
    //     dataSheet.Close();
    //
    //
    //
    //     Application.OpenURL(route);
    // }
    //
    // private void OnApplicationQuit()
    // {
    //     CreateDataSheet();
    // }
    //
    //
    // // Start is called before the first frame update
    // void Start()
    // {
    //     sampler = CustomSampler.Create("DataSheet");
    //     recorder = Recorder.Get("DataSheet");
    //     recorder.enabled = true;
    //     dataEntriesBreadthFirst = new List<dataEntry>();
    //     dataEntriesDijkstra = new List<dataEntry>();
    //     dataEntriesA_Star = new List<dataEntry>();
    //     dataEntriesD_Star = new List<dataEntry>();
    //
    //
    //     nodes = new List<Node>();
    //     path = new List<Node>();
    //     explored = new List<Node>(); 
    //     openList = new List<Node>();
    //
    //     GameObject[] points = GameObject.FindGameObjectsWithTag("Node");
    //
    //     foreach (GameObject point in points)
    //     {
    //         nodes.Add(point.GetComponent<Node>());
    //     }
    // }
    //
    // private void OnValidate()
    // {
    //     start = null;
    //     end = null;
    //
    //     foreach (Node node in nodes)
    //     {
    //         if (node.isStartNode)
    //         {
    //             if (start != null)
    //             {
    //                 Debug.LogError("Multiple Start Nodes");
    //             }
    //             start = node;
    //         }
    //         if (node.isEndNode)
    //         {
    //             if (end != null)
    //             {
    //                 Debug.LogError("Multiple End Nodes");
    //             }
    //             end = node;
    //         }
    //     }
    // }
    //
    // public void GenerateNeighbours()
    // {
    //     explored.Clear();
    //     endWasReached = false;
    //
    //     foreach (Node node in nodes)
    //     {
    //         node.neighbors.Clear();
    //         node.explored = false;
    //         node.totalCostToReach = float.PositiveInfinity;
    //     }
    //
    //     float dis;
    //     for (int i = 0; i < nodes.Count; i++)
    //     {
    //         new BoundingSphere(nodes[i].gameObject.transform.position, nodes[i].range);
    //
    //         for (int j = i + 1; j < nodes.Count; j++)
    //         {
    //             if (nodes[i] != nodes[j])
    //             {
    //                 dis = Vector3.Distance(nodes[i].gameObject.transform.position, nodes[j].gameObject.transform.position);
    //
    //                 if (dis < nodes[i].range)
    //                 {
    //                     var bridge = new bridge();
    //                     bridge.neighbour = nodes[j];
    //                     bridge.weight = dis;
    //
    //                     nodes[i].neighbors.Add(bridge);
    //
    //                     bridge.neighbour = nodes[i];
    //
    //                     nodes[j].neighbors.Add(bridge);
    //                 }
    //             }
    //         }
    //     }
    // }
    //
    // public void ReadyNewTest()
    // {
    //     path.Clear();
    //     explored.Clear();
    // }
    //
    // public void NewPathPrep()
    // {
    //     foreach (Node node in nodes)
    //     {
    //         node.explored = false;
    //         node.totalCostToReach = float.PositiveInfinity;
    //         node.UpdateWeight();
    //     }
    //
    //     explored.Clear();
    //     endWasReached = false;
    // }
    //
    // private void AStarLoop(List<Node> roamingPath)
    // {
    //     if (roamingPath.Count > tester.numberOfNodes)
    //     {
    //         Debug.LogError("We went over the max!");
    //     }
    //
    //     if (roamingPath.Count > 1 && roamingPath.Count < tester.numberOfNodes)
    //     {
    //         roamingPath.Sort((n1, n2) => n1.heuristicCost.CompareTo(n2.heuristicCost));
    //     }
    //
    //
    //     Node testing = roamingPath.First();
    //
    //
    //     testing.distanceToEnd = Vector2.Distance(testing.transform.position, end.transform.position);
    //
    //
    //     roamingPath.Remove(roamingPath.First());
    //
    //     testing.explored = true;
    //     explored.Add(testing);
    //
    //     if (!testing.isEndNode)
    //     {
    //         for (int i = 0; i < testing.neighbors.Count; i++)
    //         {
    //             if (!testing.neighbors[i].neighbour.explored && !roamingPath.Contains(testing.neighbors[i].neighbour))
    //             {
    //                 roamingPath.Add(testing.neighbors[i].neighbour);
    //             }
    //             if (testing.neighbors[i].neighbour.totalCostToReach > testing.totalCostToReach + testing.neighbors[i].weight)
    //             {
    //                 testing.neighbors[i].neighbour.totalCostToReach = testing.totalCostToReach + testing.neighbors[i].weight;
    //                 testing.neighbors[i].neighbour.heuristicCost = testing.neighbors[i].weight + Vector2.Distance(testing.neighbors[i].neighbour.transform.position, end.transform.position);
    //                 testing.neighbors[i].neighbour.cameFrom = testing;
    //             }
    //         }
    //
    //         if (roamingPath.Count > 0)
    //         {
    //             AStarLoop(roamingPath);
    //         }
    //     }
    //     else
    //     {
    //         endWasReached = true;
    //     }
    // }
    //
    // private List<Node> AStar()
    // {
    //     List<Node> roamingPath = new List<Node>();
    //     List<Node> shortestPath = new List<Node>();
    //     start.explored = true;
    //     explored.Add(start);
    //     start.totalCostToReach = 0;
    //
    //     if (start.neighbors.Count > 0)
    //     {
    //         for (int i = 0; i < start.neighbors.Count; i++)
    //         {
    //             roamingPath.Add(start.neighbors[i].neighbour);
    //             start.neighbors[i].neighbour.totalCostToReach = start.neighbors[i].weight;
    //             start.neighbors[i].neighbour.heuristicCost = start.neighbors[i].weight + Vector2.Distance(start.neighbors[i].neighbour.transform.position, end.transform.position);
    //             start.neighbors[i].neighbour.cameFrom = start;
    //         }
    //
    //         AStarLoop(roamingPath);
    //     }
    //
    //     if (endWasReached)
    //     {
    //         shortestPath = LoopBack(shortestPath, end);
    //
    //         return shortestPath;
    //     }
    //     return null;
    // }
    //
    // private List<Node> LoopBack(List<Node> shortestPath, Node retracePathFrom)
    // {
    //     
    //
    //     int iterator = 0;
    //     Node testing = retracePathFrom;
    //     if (shortestPath == null || retracePathFrom == null || testing == null)
    //     {
    //         return shortestPath;
    //     }
    //     while (iterator < tester.numberOfNodes && !testing.isStartNode && algorithm == AlgorithmType.BreadthFirst
    //         || iterator < tester.numberOfNodes && !testing.isStartNode && algorithm == AlgorithmType.Dijkstra
    //         || iterator < tester.numberOfNodes && !testing.isStartNode && algorithm == AlgorithmType.AStar
    //         || iterator < tester.numberOfNodes && !testing.isEndNode && algorithm == AlgorithmType.DStar)
    //     {
    //         iterator++;
    //         shortestPath.Add(testing);
    //         testing = testing.cameFrom;
    //         if (testing == null)
    //         {
    //             return shortestPath;
    //         }
    //     }
    //
    //     return shortestPath;
    // }
    //
    // private List<Node> GreedyLoopBack()
    // {
    //     /*
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      Do Me
    //      
    //      How?
    //      
    //      Well, basically make a version of Dijkstra that doesnet remember which node it came from
    //         then make it take the end node loop through the end nodes neighbours and pick the lowest value
    //         that will be the node it'll add to the return path
    //         loop this for the node added untill you reach start, that'll be your path
    //      
    //      it'll count as a different implementation and will probably be slower than the other option due to not remembering the path from the get go
    //      as such having to page through the neighbours ahead a time
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      
    //      */
    //
    //     return null;
    // }
    //
    // private void DijkstraLoop(List<Node> roamingPath)
    // {
    //     if (roamingPath.Count > tester.numberOfNodes)
    //     {
    //         Debug.LogError("We went over the max!");
    //     }
    //
    //     if (roamingPath.Count > 1 && roamingPath.Count < tester.numberOfNodes)
    //     {
    //         roamingPath.Sort((n1, n2) => n1.totalCostToReach.CompareTo(n2.totalCostToReach));
    //     }
    //
    //
    //     Node testing = roamingPath.First();
    //
    //     roamingPath.Remove(roamingPath.First());
    //
    //     testing.explored = true;
    //     explored.Add(testing);
    //
    //     if (!testing.isEndNode)
    //     {
    //         for (int i = 0; i < testing.neighbors.Count; i++)
    //         {
    //             if (!testing.neighbors[i].neighbour.explored && !roamingPath.Contains(testing.neighbors[i].neighbour))
    //             {
    //                 roamingPath.Add(testing.neighbors[i].neighbour);
    //             }
    //             if (testing.neighbors[i].neighbour.totalCostToReach > testing.totalCostToReach + testing.neighbors[i].weight)
    //             {
    //                 testing.neighbors[i].neighbour.totalCostToReach = testing.totalCostToReach + testing.neighbors[i].weight;
    //                 testing.neighbors[i].neighbour.cameFrom = testing;
    //             }
    //         }
    //
    //         if (roamingPath.Count > 0)
    //         {
    //             DijkstraLoop(roamingPath);
    //         }
    //     }
    //     else
    //     {
    //         endWasReached = true;
    //     }
    // }
    //
    // private List<Node> Dijkstra()
    // {
    //     List<Node> roamingPath = new List<Node>();
    //     List<Node> shortestPath = new List<Node>();
    //     start.explored = true;
    //     explored.Add(start);
    //     start.totalCostToReach = 0;
    //
    //     if (start.neighbors.Count > 0)
    //     {
    //         for (int i = 0; i < start.neighbors.Count; i++)
    //         {
    //             roamingPath.Add(start.neighbors[i].neighbour);
    //             start.neighbors[i].neighbour.totalCostToReach = start.neighbors[i].weight;
    //             start.neighbors[i].neighbour.cameFrom = start;
    //         }
    //
    //         DijkstraLoop(roamingPath);
    //     }
    //
    //     if (endWasReached)
    //     {
    //         shortestPath = LoopBack(shortestPath, end);
    //
    //         return shortestPath;
    //     }
    //     return null;
    // }
    //
    // private bool IsRaised(Node currentNode)
    // {
    //     if (currentNode.currentCost > currentNode.minimumCost)
    //     {
    //         for (int i = 0; i < currentNode.neighbors.Count; i++)
    //         {
    //             if (currentNode.neighbors[i].neighbour.cameFrom == currentNode)
    //             {
    //                 currentNode.neighbors[i].neighbour.currentCost = currentNode.neighbors[i].weight + currentNode.currentCost;
    //                 currentNode.neighbors[i].neighbour.heuristicCost = currentNode.neighbors[i].neighbour.currentCost + currentNode.neighbors[i].neighbour.distanceToStart;
    //
    //                 if (!openList.Contains(currentNode.neighbors[i].neighbour))
    //                 {
    //                     openList.Add(currentNode.neighbors[i].neighbour);
    //                 }
    //             }
    //         }
    //         return true;
    //     }
    //     return false;
    // }
    //
    // private bool IsLowered(Node currentNode)
    // {
    //     if (currentNode.currentCost < currentNode.minimumCost)
    //     {
    //         for (int i = 0; i < currentNode.neighbors.Count; i++)
    //         {
    //             if (currentNode.neighbors[i].neighbour.cameFrom == currentNode)
    //             {
    //                 currentNode.neighbors[i].neighbour.currentCost = currentNode.neighbors[i].weight + currentNode.currentCost;
    //                 currentNode.neighbors[i].neighbour.heuristicCost = currentNode.neighbors[i].neighbour.currentCost + currentNode.neighbors[i].neighbour.distanceToStart;
    //
    //                 if (!openList.Contains(currentNode.neighbors[i].neighbour))
    //                 {
    //                     openList.Add(currentNode.neighbors[i].neighbour);
    //                 }
    //             }
    //         }
    //         return true;
    //     }
    //     return false;
    // }
    //
    // private void ExpandDStar(Node currentNode, ref bool goalCouldBeReached)
    // {
    //     
    //     if (currentNode.isStartNode)
    //     {
    //         endWasReached = true;
    //         List<Node> shortestPath = new List<Node>();
    //         pathToGoal = LoopBack(shortestPath, currentNode);
    //         goalCouldBeReached = true;
    //     }
    //     else
    //     {
    //         //This one doesn't seem to happen
    //         for (int i = 0; i < currentNode.neighbors.Count; i++)
    //         {
    //             if (!currentNode.neighbors[i].neighbour.isEndNode && currentNode.neighbors[i].neighbour.cameFrom == null)
    //             {
    //                 currentNode.neighbors[i].neighbour.cameFrom = currentNode;
    //                 currentNode.neighbors[i].neighbour.currentCost = currentNode.neighbors[i].weight + currentNode.currentCost;
    //                 currentNode.neighbors[i].neighbour.heuristicCost = currentNode.neighbors[i].neighbour.currentCost + currentNode.neighbors[i].neighbour.distanceToStart;
    //
    //                 if (!openList.Contains(currentNode.neighbors[i].neighbour))
    //                 {
    //                     openList.Add(currentNode.neighbors[i].neighbour);
    //                 }
    //             }
    //         }
    //
    //         bool isRaised = IsRaised(currentNode);
    //         bool isLowered = IsLowered(currentNode);
    //
    //         if (!isRaised && !isLowered)
    //         {
    //
    //             if (endWasReached)
    //             {
    //                 for (int i = 0; i < pathToGoal.Count; i++)
    //                 {
    //                     if (currentNode == pathToGoal[i].cameFrom)
    //                     {
    //                         openList.Add(pathToGoal[i]);
    //                         break;
    //                     }
    //                 }
    //             }
    //             else
    //             {
    //                 for (int i = 0; i < currentNode.neighbors.Count; i++)
    //                 {
    //                     if (currentNode.neighbors[i].neighbour.cameFrom == currentNode)
    //                     {
    //                         openList.Add(currentNode.neighbors[i].neighbour);
    //                     }
    //                 }
    //
    //                 //int lowest = 0;
    //                 //float savedValue = float.PositiveInfinity;
    //                 //for (int i = 0; i < currentNode.neighbours.Count; i++)
    //                 //{
    //                 //    if (currentNode.neighbours[i].neighbour.currentCost + currentNode.neighbours[i].neighbour.distanceToStart < savedValue)
    //                 //    {
    //                 //        lowest = i;
    //                 //        savedValue = currentNode.neighbours[i].neighbour.currentCost + currentNode.neighbours[i].neighbour.distanceToStart;
    //                 //    }
    //                 //}
    //
    //                 //openList.Add(currentNode.neighbours[lowest].neighbour);
    //             }
    //         }
    //         else if (isLowered)
    //         {
    //
    //         }
    //         else if (isRaised)
    //         {
    //             //Searches for if there is a cheaper way to reach this one amongst it's neighbours
    //             for (int i = 0; i < currentNode.neighbors.Count && currentNode.neighbors[i].neighbour.cameFrom != currentNode; i++)
    //             {
    //                 if (currentNode.neighbors[i].neighbour.currentCost + currentNode.neighbors[i].weight < currentNode.currentCost)
    //                 {
    //                     currentNode.currentCost = currentNode.neighbors[i].neighbour.currentCost + currentNode.neighbors[i].weight;
    //                     currentNode.heuristicCost = currentNode.currentCost + currentNode.distanceToStart;
    //                     currentNode.cameFrom = currentNode.neighbors[i].neighbour;
    //                     if (!openList.Contains(currentNode.neighbors[i].neighbour))
    //                     {
    //                         openList.Add(currentNode.neighbors[i].neighbour);
    //
    //                     }
    //                 }
    //             }
    //         }
    //     }
    //     
    //
    //
    //
    //
    //     {
    //         //bool isRaised = currentNode.isRaised();
    //         //float cost;
    //
    //         //for (int i = 0; i < currentNode.neighbours.Count; i++)
    //         //{
    //         //    if (isRaised)
    //         //    {
    //         //        if (currentNode.neighbours[i].neighbour.cameFrom == null && !currentNode.neighbours[i].neighbour.isEndNode)
    //         //        {
    //         //            currentNode.neighbours[i].neighbour.cameFrom = currentNode;
    //         //        }
    //         //        else if (currentNode.neighbours[i].neighbour.cameFrom == currentNode && !currentNode.neighbours[i].neighbour.isEndNode)
    //         //        {
    //         //            currentNode.neighbours[i].neighbour.cameFrom = currentNode;
    //         //            currentNode.neighbours[i].neighbour.totalCostToReach = currentNode.totalCostToReach + currentNode.neighbours[i].weight;
    //
    //         //            if (!openList.Contains(currentNode.neighbours[i].neighbour))
    //         //            {
    //         //                openList.Add(currentNode.neighbours[i].neighbour);
    //         //            }
    //         //        }
    //         //        else
    //         //        {
    //         //            cost = currentNode.neighbours[i].neighbour.totalCostToReach + currentNode.neighbours[i].weight;
    //         //            if (cost < currentNode.totalCostToReach)
    //         //            {
    //         //                currentNode.minimumCost = cost; 
    //         //                if (!openList.Contains(currentNode))
    //         //                {
    //         //                    openList.Add(currentNode);
    //         //                }
    //         //            }
    //         //        }
    //         //    }
    //         //    else
    //         //    {
    //         //        cost = currentNode.neighbours[i].neighbour.totalCostToReach + currentNode.neighbours[i].weight;
    //         //        if (cost < currentNode.totalCostToReach)
    //         //        {
    //         //            currentNode.totalCostToReach = cost;
    //         //            if (!openList.Contains(currentNode.neighbours[i].neighbour))
    //         //            {
    //         //                openList.Add(currentNode.neighbours[i].neighbour);
    //         //            }
    //         //        }
    //         //    }
    //         //}
    //     }
    //
    // }
    //
    // private List<Node> DStar()
    // {
    //     List<Node> shortestPath = new List<Node>();
    //     int iterator = 0;
    //
    //     openList.Clear();
    //
    //     for (int i = 0; i < end.neighbors.Count; i++)
    //     {
    //         openList.Add(end.neighbors[i].neighbour);
    //         end.neighbors[i].neighbour.cameFrom = end;
    //         end.neighbors[i].neighbour.currentCost = end.neighbors[i].weight;
    //         end.neighbors[i].neighbour.heuristicCost = end.neighbors[i].neighbour.currentCost + end.neighbors[i].neighbour.distanceToStart;
    //     }
    //
    //     end.currentCost = float.PositiveInfinity;
    //     end.totalCostToReach = 0;
    //     bool goalCouldBeReached = false;
    //
    //
    //     while (openList.Count > 0 && iterator < nodes.Count)
    //     {
    //         iterator++;
    //
    //         openList.Sort((n1, n2) => n1.heuristicCost.CompareTo(n2.heuristicCost));
    //
    //         Node node = openList.First();
    //         openList.Remove(node);
    //
    //         if (!explored.Contains(node))
    //         {
    //             explored.Add(node);
    //             node.explored = true; 
    //         }
    //
    //         ExpandDStar(node, ref goalCouldBeReached);
    //
    //         node.minimumCost = node.currentCost;
    //         node.totalCostToReach = node.currentCost;
    //
    //         if (node.isStartNode)
    //         {
    //             break;
    //         }
    //     }
    //
    //     if (goalCouldBeReached)
    //     {
    //         shortestPath = pathToGoal;
    //     }
    //     else
    //     {
    //         endWasReached = false;
    //     }
    //
    //     return shortestPath;
    // }
    //
    // private List<Node> BreadthFirst()
    // {
    //     List<Node> shortestPath = new List<Node>();
    //     Queue<Node> explorationLine = new Queue<Node>();
    //
    //     explored.Add(start);
    //     start.explored = true;
    //     explorationLine.Enqueue(start);
    //     int iterator = 0;
    //
    //     while (iterator < nodes.Count && explorationLine.Count > 0 && !endWasReached)
    //     {
    //         Node node = explorationLine.Dequeue();
    //         if (node.isEndNode)
    //         {
    //             endWasReached = true;
    //             explored.Add(node);
    //         }
    //         else
    //         {
    //             for (int i = 0; i < node.neighbors.Count; i++)
    //             {
    //                 if (!node.neighbors[i].neighbour.explored)
    //                 {
    //                     node.neighbors[i].neighbour.explored = true;
    //                     explored.Add(node.neighbors[i].neighbour);
    //                     node.neighbors[i].neighbour.cameFrom = node;
    //                     explorationLine.Enqueue(node.neighbors[i].neighbour);
    //                 }
    //             }
    //         }
    //     }
    //
    //     if (endWasReached)
    //     {
    //         shortestPath = LoopBack(shortestPath, end);
    //     }
    //
    //     return shortestPath;
    // }
    //
    // private void GetPathToGoal()
    // {
    //     switch(algorithm)
    //     {
    //         case AlgorithmType.BreadthFirst:
    //             DStarSwitch = false;
    //             NewPathPrep();
    //             sampler.Begin();
    //             path = BreadthFirst();
    //             sampler.End();
    //             break;
    //         case AlgorithmType.Dijkstra:
    //             DStarSwitch = false;
    //             NewPathPrep();
    //             sampler.Begin();
    //             path = Dijkstra();
    //             sampler.End();
    //             break;
    //         case AlgorithmType.AStar:
    //             DStarSwitch = false;
    //             NewPathPrep();
    //             sampler.Begin();
    //             path = AStar();
    //             sampler.End();
    //             break;
    //         case AlgorithmType.DStar:
    //
    //             if (!DStarSwitch)
    //             {
    //                 DStarSwitch = true;
    //                 foreach (Node n in nodes)
    //                 {
    //                     n.totalCostToReach = float.PositiveInfinity;
    //                     n.currentCost = float.PositiveInfinity;
    //                 }
    //                 end.cameFrom = null;
    //             }
    //
    //             foreach(Node node in nodes)
    //             {
    //                 node.UpdateWeight();
    //                 node.distanceToStart = Vector2.Distance(node.transform.position, start.transform.position);
    //             }
    //             foreach (Node node in explored)
    //             {
    //                 node.explored = false;
    //             }
    //             explored.Clear();
    //             endWasReached = false;
    //
    //             sampler.Begin();
    //             path = DStar();
    //             sampler.End();
    //             break;
    //     }
    // }
    //
    // // Update is called once per frame
    // public void GetSample()
    // {
    //     //sampler.Begin();
    //     GetPathToGoal();
    //     //sampler.End();
    //
    //     dataEntry dataEntry = new dataEntry();
    //     dataEntry.frameTime = recorder.elapsedNanoseconds; //adjusts to milisecond
    //     dataEntry.exploredNodes = explored.Count;
    //     dataEntry.nodeCount = nodes.Count;
    //     switch (algorithm)
    //     {
    //         case AlgorithmType.BreadthFirst:
    //             dataEntriesBreadthFirst.Add(dataEntry);
    //             break;
    //         case AlgorithmType.Dijkstra:
    //             dataEntriesDijkstra.Add(dataEntry);
    //             break;
    //         case AlgorithmType.AStar:
    //             dataEntriesA_Star.Add(dataEntry);
    //             break;
    //         case AlgorithmType.DStar:
    //             dataEntriesD_Star.Add(dataEntry);
    //             break;
    //     }
    // }
}

//[CustomEditor(typeof(GrafPathing))]
//public class GrafPathingEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        GrafPathing gp = (GrafPathing)target;
//        if (GUILayout.Button("Generate"))
//        {
//            gp.GenerateNeighbours();
//        }
//    }
//}