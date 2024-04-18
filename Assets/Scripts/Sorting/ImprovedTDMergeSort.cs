using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedTDMergeSort : MonoBehaviour
{
    private static void CopyToArray(Node[] noodles, int iStart, int iStop, Node[] workNodes)
    {
        for (int i = iStart; i < iStop; i++)
        {
            workNodes[i] = noodles[i];
        }
    }

    private static void TdMerge(Node[] noodles, int iStart, int iDivide, int iStop, Node[] workNodes)
    {
        int starty = iStart;
        int dividy = iDivide;

        for (int i = iStart; i < iStop; i++)
        {
            if (starty < iDivide && (dividy >= iStop || noodles[starty].heuristicCost <= noodles[dividy].heuristicCost))
            {
                workNodes[i] = noodles[starty];
                starty++;
            }
            else
            {
                workNodes[i] = noodles[dividy];
                dividy++;
            }
        }
    }

    private static void TdSplitMerge(Node[] workNodes, int iStart, int iStop, Node[] noodles)
    {
        if (iStop - iStart <= 1)
        {
            return;
        }

        int iDivide = (iStart + iStop) / 2;

        TdSplitMerge(noodles, iStart, iDivide, workNodes);
        TdSplitMerge(noodles, iDivide, iStop, workNodes);
        TdMerge(workNodes, iStart, iDivide, iStop, noodles);
    }

    public static Node[] TdMergeSort(Node[] noodles)
    {
        Node[] workNodes = new Node[noodles.Length];
        CopyToArray(noodles, 0, noodles.Length, workNodes);
        TdSplitMerge(workNodes, 0, noodles.Length, noodles);
        return noodles;
    }
}
