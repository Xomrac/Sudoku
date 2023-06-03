using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public class CellNode
{
    [SerializeField]  private int cellIndex=10;
    public int CellIndex => cellIndex;
    
    [SerializeField] private int cellRow=20;
    public int CellRow => cellRow;

    [SerializeField] private int cellColumn=30;
    public int CellColumn => cellColumn;
    
    [SerializeField] private int cellSquare=40;
    public int CellSquare => cellSquare;

    public CellNode(int index, int row, int column, int square)
    {
        cellIndex = index;
        cellRow = row;
        cellColumn = column;
        cellSquare = square;
    }
}
