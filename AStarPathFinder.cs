using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinder
{
    static public List<Vector2> FindPath(Vector2 startCell, Vector2 endCell, List<Vector2> emptyCells)
    {
        var Open = new Dictionary<Vector2, Vector2>();//Vector2 (G, H)
        var Close = new Dictionary<Vector2, Vector2>();
        var Relations = new Dictionary<Vector2, Vector2>();//Son - parent 

        Close.Add(startCell, new Vector2(0, 0));
        Vector2 Current = startCell;
        float GValue = 0;
        List<Vector2> NeighborCell = new List<Vector2>();
        while (!Relations.ContainsKey(endCell))
        {
            NeighborCell.Add(Current + new Vector2(-1, 0));
            NeighborCell.Add(Current + new Vector2(1, 0));
            NeighborCell.Add(Current + new Vector2(0, 1));
            NeighborCell.Add(Current + new Vector2(0, -1));

            foreach (Vector2 EachCell in NeighborCell)
            {
                if (emptyCells.Contains(EachCell))
                {
                    if (Close.ContainsKey(EachCell))
                    {
                        continue;
                    }
                    else if (Open.ContainsKey(EachCell))
                    {
                        Vector2 Tmp = new Vector2(GetGValue(Current, EachCell, GValue), GetHValue(EachCell, endCell));

                        if ((Open[EachCell].x + Open[EachCell].y) > (Tmp.x + Tmp.y))
                        {
                            Open[EachCell] = Tmp;
                            Relations[EachCell] = Current;
                        }
                    }
                    else
                    {
                        Open.Add(EachCell, new Vector2(GetGValue(Current, EachCell, GValue), GetHValue(EachCell, endCell)));
                        Relations.Add(EachCell, Current);
                    }
                }
            }
            NeighborCell.Clear();
            if (0 == Open.Count)
            {
                break;
            }
            Vector2 SmallestCell = GetSmallestFValueCell(Open);

            GValue = Open[SmallestCell].x;
            Open.Remove(SmallestCell);
            Close.Add(SmallestCell, new Vector2(GValue, GetHValue(SmallestCell, endCell)));

            Current = SmallestCell;


        }

        var tempPath = new List<Vector2>();
        Vector2 TmpCell = endCell;
        if (Relations.ContainsValue(startCell) && Relations.ContainsKey(endCell))
        {
            while (TmpCell != startCell)
            {
                if (Relations.ContainsKey(TmpCell))
                {
                    tempPath.Add(TmpCell);
                    TmpCell = Relations[TmpCell];
                }
            }
            tempPath.Add(startCell);
        }
        else
        {
            Debug.Log("No Path");
            return null;
        }

        List<Vector2> RealPath = new List<Vector2>();
        for (int i = tempPath.Count - 1; i >= 0; i--)
        {
            RealPath.Add(tempPath[i]);
        }

        return RealPath;
    }

    static public float GetHValue(Vector2 PosSrc, Vector2 PosDes)
    {
        Vector2 Dis = PosSrc - PosDes;
        return Math.Abs(Dis.x) + Math.Abs(Dis.y);
    }
    static public float GetGValue(Vector2 Current, Vector2 Cell, float GValue)
    {
        return GValue + 1;
    }

    static public Vector2 GetSmallestFValueCell(Dictionary<Vector2, Vector2> Open)
    {
        float SmallestF = 1000;
        Vector2 SmallestCell = new Vector2(0, 0);
        foreach (KeyValuePair<Vector2, Vector2> kvp in Open)
        {
            if (SmallestF > (kvp.Value.x + kvp.Value.y))
            {
                SmallestF = (kvp.Value.x + kvp.Value.y);
                SmallestCell = kvp.Key;
            }
        }
        return SmallestCell;
    }
}
