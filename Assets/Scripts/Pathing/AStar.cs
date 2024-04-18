using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AStar", menuName = "Tests/Pathing/AStar", order = 1)]
public class AStar : AlgorithmObject
{
    private static List<Node> AStarPath(Node selected, Node target, int iteratorLimit)
    {
        List<Node> roamingPath = new List<Node>();
        List<Node> shortestPath = new List<Node>();
        HashSet<Node> explored = new HashSet<Node>();

        bool endWasReached = false;
        
        if (selected.neighbors.Count <= 0)
            return null;

        selected.totalCostToReach = 0;
        explored.Add(selected);
        foreach (var node in selected.neighbors)
        {
            roamingPath.Add(node);
            float weight = Vector3.Distance(selected.transform.position, node.transform.position);
            node.totalCostToReach = weight;
            node.heuristicCost = weight + Vector3.Distance(node.transform.position, target.transform.position);
            node.cameFrom = selected;
        }

        int iterator = 0;
        while (iterator < iteratorLimit && !endWasReached && roamingPath.Count > 0)
        {
            iterator++;
                
            if (roamingPath.Count > 1)
                roamingPath.Sort((n1, n2) => n1.heuristicCost.CompareTo(n2.heuristicCost));

            Node testing = roamingPath.First();
                
            roamingPath.Remove(roamingPath.First());

            explored.Add(testing);

            if (testing == target)
            {
                endWasReached = true;
                break;
            }
            
            foreach (var node in testing.neighbors)
            {
                if (!roamingPath.Contains(node) && !explored.Contains(node))
                    roamingPath.Add(node);
                    
                float weight = Vector3.Distance(testing.transform.position, node.transform.position);

                if (node.totalCostToReach <= testing.totalCostToReach + weight) 
                    continue;
                
                node.totalCostToReach = testing.totalCostToReach + weight;
                node.heuristicCost = weight + Vector2.Distance(node.transform.position, target.transform.position);
                node.cameFrom = testing;
            }
        }

        if (iterator == iteratorLimit)
            Debug.Log($"Limit was reached");

        foreach (var node in explored)
        {
            node.explored = true;
        }
        
        if (!endWasReached) 
            return null;
        
        Node nextPos = target;
        iterator = 0;
        while (iterator < iteratorLimit && nextPos != selected)
        {
            iterator++;
            shortestPath.Add(nextPos);
            nextPos = nextPos.cameFrom;
        }
        
        shortestPath.Add(selected);
        shortestPath.Reverse();

        return shortestPath;
    }

    public override bool Run(TestPacket packet)
    {
        return AStarPath(packet.selected, packet.target, packet.iteratorLimit) == null;
    }
}
