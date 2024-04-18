using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MergeAStar", menuName = "Tests/Pathing/MergeAStar", order = 1)]
public class MergeAStar : AlgorithmObject
{
    private static List<Node> MergeAStarPath(Node selected, Node target, int iteratorLimit)
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

        roamingPath = ImprovedTDMergeSort.TdMergeSort(roamingPath.ToArray()).ToList();
        int iterator = 0;
        List<Node> temp = new List<Node>();
        List<Node> combiner = new List<Node>();
        while (iterator < iteratorLimit && !endWasReached && roamingPath.Count > 0)
        {
            iterator++;
            if (temp.Count > 0)
            {
                temp = ImprovedTDMergeSort.TdMergeSort(temp.ToArray()).ToList();
                int iStart = 0;
                int iDivide = roamingPath.Count;
                int starty = iStart;
                int dividy = iDivide;
                int iStop = roamingPath.Count + temp.Count;

                combiner.AddRange(roamingPath);
                combiner.AddRange(temp);
                roamingPath.Clear();
                
                for (int i = iStart; i < iStop; i++)
                {
                    if (starty < iDivide && (dividy >= iStop || combiner[starty].heuristicCost <= combiner[dividy].heuristicCost))
                    {
                        roamingPath.Add(combiner[starty]);
                        starty++;
                    }
                    else
                    {
                        roamingPath.Add(combiner[dividy]);
                        dividy++;
                    }
                }
                temp.Clear();
                combiner.Clear();
            }
                
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
                if (!roamingPath.Contains(node) && !explored.Contains(node) && !temp.Contains(node))
                    temp.Add(node);
                        
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
        return MergeAStarPath(packet.selected, packet.target, packet.iteratorLimit) == null;
    }
}