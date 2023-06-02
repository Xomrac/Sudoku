using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    [SerializeField] private int? currentValue;
    
    public int? CurrentValue
    {
        set{
            currentValue = value;
            valueText.text = value == null ? "" : $"{value}";
            completed = value == correctValue;
            valueText.color = completed ? Color.green : Color.red;
        }
        get => currentValue;
    }

    [SerializeField] private int correctValue;
    public int CorrectValue => correctValue;

    [SerializeField] private CellNode node;
    public CellNode Node => node;

    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Image background;

    private bool completed;

    public void Init(CellNode newNode)
    {
        node = newNode;
        gameObject.name = $"[{node.CellRow},{node.CellColumn},{node.CellSquare}]";
        correctValue = 0;
    }

    private void Awake()
    {
        EraseValue();
    }

    public void EraseValue()
    {
        CurrentValue = null;
    }

    public void SetInitialValue(int value)
    {
        correctValue = value;
        CurrentValue = correctValue;
        valueText.color = Color.black;
        completed = true;
    }
    

    
    
}
