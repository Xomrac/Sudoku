using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Grid;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XomracUtilities.Patterns;

public class CellController : ServiceLocator, IPointerDownHandler
{
	private int? currentValue;

	public static event Action<CellController> Clicked;

	public static event Action CellUpdated;
	public static event Action<bool> ValueInsterted;

	public int? CurrentValue
	{
		set{
			currentValue = value;
			valueText.text = value == null ? "" : $"{value}";
			if (!GameManager.Instance.IsZenMode)
			{
				currentlyCompleted = value == correctValue;
				valueText.color = currentlyCompleted ? Color.green : Color.red;
				ValueInsterted?.Invoke(currentValue == correctValue);
			}
		}
		get => currentValue;
	}

	[SerializeField] private int correctValue;
	public int CorrectValue => correctValue;

	public CellNode node;

	[SerializeField] private TextMeshProUGUI valueText;
	[SerializeField] private Image background;

	private bool completedAFirstTime;
	public bool CompletedAFirstTime => completedAFirstTime;

	private bool currentlyCompleted;
	public bool CurrentlyCompleted => currentlyCompleted;

#if UNITY_EDITOR
	public void Init(CellNode newNode)
	{
		node = newNode;
		gameObject.name = $"[{node.CellRow},{node.CellColumn},{node.CellSquare}]";
		correctValue = 0;
		EditorUtility.SetDirty(this);
		UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
	}
#endif

	private void Awake()
	{
		RemoveValue();
		PopulateDictionary();
	}



	public void SetInitialValue(int value)
	{
		correctValue = value;
		currentValue = correctValue;
		valueText.text = $"{value}";
		valueText.color = Color.black;
		currentlyCompleted = true;
	}
	
	public void RemoveValue()
	{
		currentValue = null;
		valueText.text = "";
	}

	public void SetBackgroundColor(Color backgroundColor)
	{
		background.color = backgroundColor;
	}

	public void InsertValue(int value)
	{
		CurrentValue = value;
		CellUpdated?.Invoke();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		NumberSelector.Selected = InsertValue;
		Debug.Log($"Clicked On cell {gameObject.name}");
		Clicked?.Invoke(this);
	}

	public override void PopulateDictionary()
	{
		services.Add(typeof(CellHighlighter), GetComponent<CellHighlighter>());
	}

	
}