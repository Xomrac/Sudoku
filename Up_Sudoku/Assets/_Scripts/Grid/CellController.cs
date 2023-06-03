using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XomracUtilities.Patterns;

public class CellController : ServiceLocator, IPointerDownHandler
{
    [SerializeField] private int? currentValue;

    public static event Action<CellController> Clicked; 

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

    public CellNode node;

    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Image background;

    private bool completed;
    public bool Completed => completed;

    #if UNITY_EDITOR
    public void Init(CellNode newNode)
    {
        node = newNode;
        gameObject.name = $"[{node.CellRow},{node.CellColumn},{node.CellSquare}]";
        correctValue = 0;
        EditorUtility.SetDirty(this);
        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
    }
    #endif

    private void Awake()
    {
        EraseValue();
        PopulateDictionary();
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

    public void SetBackgroundColor(Color backgroundColor)
    {
        background.color = backgroundColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"clicked cell {gameObject.name}");
        Clicked?.Invoke(this);
    }

    public override void PopulateDictionary()
    {
        services.Add(typeof(CellHighlighter),GetComponent<CellHighlighter>());
    }
    
    
}
