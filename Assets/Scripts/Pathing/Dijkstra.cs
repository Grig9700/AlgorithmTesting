using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Dijkstra", menuName = "Tests/Pathing/Dijkstra", order = 1)]
public class Dijkstra : AlgorithmObject
{
    private static List<Node> DijkstraPath(Node start, Node end, int iteratorLimit)
    {
        List<Node> roamingPath = new List<Node>();
        List<Node> shortestPath = new List<Node>();
        HashSet<Node> explored = new HashSet<Node>();
        start.explored = true;
        explored.Add(start);
        start.totalCostToReach = 0;
        bool endWasReached = false;
    
        if (start.neighbors.Count <= 0)
        {
            return null;
        }
    
        foreach (var node in start.neighbors)
        {
            roamingPath.Add(node);
            node.totalCostToReach = Vector3.Distance(start.transform.position, node.transform.position);
            node.cameFrom = start;
        }

        int iterator = 0;
        
        while (!endWasReached && iterator < iteratorLimit && roamingPath.Count > 0)
        {
            iterator++;
            if (roamingPath.Count > 1)
                roamingPath.Sort((n1, n2) => n1.totalCostToReach.CompareTo(n2.totalCostToReach));

            Node testing = roamingPath.First();

            roamingPath.Remove(roamingPath.First());

            explored.Add(testing);

            if (testing == end)
            {
                endWasReached = true;
                break;
            }
            
            foreach (var node in testing.neighbors)
            {
                if (!explored.Contains(node) && !roamingPath.Contains(node))
                    roamingPath.Add(node);
                    
                float weight = Vector3.Distance(testing.transform.position, node.transform.position);

                if (node.totalCostToReach <= testing.totalCostToReach + weight) 
                    continue;
                    
                node.totalCostToReach = testing.totalCostToReach + weight;
                node.cameFrom = testing;
            }
        }
        
        if (explored.Count == iteratorLimit)
            Debug.Log($"Limit was reached");

        foreach (var node in explored)
        {
            node.explored = true;
        }
        
        if (!endWasReached) 
            return null;
        
        Node nextPos = end;
        iterator = 0;
        while (iterator < iteratorLimit && nextPos != start)
        {
            iterator++;
            shortestPath.Add(nextPos);
            nextPos = nextPos.cameFrom;
        }
        
        shortestPath.Add(start);
        shortestPath.Reverse();

        return shortestPath;
    }

    public override bool Run(TestPacket packet)
    {
        return DijkstraPath(packet.selected, packet.target, packet.iteratorLimit) == null;
    }
}