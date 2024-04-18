using System.Collections.Generic;

public class BreadthFirst : AlgorithmObject
{
    private static List<Node> BreadthFirstPath(Node start, int iteratorLimit)
    {
        List<Node> rangedTiles = new List<Node>();
        HashSet<Node> explored = new HashSet<Node>();
        List<Node> expandNext = new List<Node>();

        expandNext.Add(start);
        
        for (int i = 0; i < iteratorLimit + 1; i++)
        {
            List<Node> temp = new List<Node>();
            foreach (Node node in expandNext)
            {
                explored.Add(node);
                node.totalCostToReach = i;
                foreach(Node neighbor in node.neighbors)
                {
                    if (explored.Contains(neighbor) || expandNext.Contains(neighbor))
                        continue;
                    temp.Add(neighbor);
                    neighbor.cameFrom = node;
                }
            }
            expandNext = temp;
        }
        rangedTiles.AddRange(explored);
        return rangedTiles;
    }

    public override bool Run(TestPacket packet)
    {
        return BreadthFirstPath(packet.selected, packet.iteratorLimit) == null;
    }
}
