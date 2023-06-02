using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public struct CellNode
{
    [SerializeField] [ReadOnly] private int cellIndex;
    public int CellIndex => cellIndex;
    
    [SerializeField][ReadOnly] private int cellRow;
    public int CellRow => cellRow;

    [SerializeField][ReadOnly] private int cellColumn;
    public int CellColumn => cellColumn;
    
    [SerializeField][ReadOnly] private int cellSquare;
    public int CellSquare => cellSquare;

    public CellNode(int index, int row, int column, int square)
    {
        cellIndex = index;
        cellRow = row;
        cellColumn = column;
        cellSquare = square;
    }
}
