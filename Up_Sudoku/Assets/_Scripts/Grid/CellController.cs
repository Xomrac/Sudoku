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

	[SerializeField] private ThemePalette theme;

	
	private int? currentValue;

	public static event Action<CellController> Clicked;

	[SerializeField] private CellNotesController notesController;
	public CellNotesController NotesController => notesController;

	public static event Action<CellController> CellUpdated;
	public static event Action<bool> ValueInsterted;
	
	

	public int? CurrentValue
	{
		set{
			currentValue = value;
			valueText.text = value == null ? "" : $"{value}";
			currentlyCompleted = value == correctValue;
			valueText.color = theme.NeutralValuesColor;
			if (!GameManager.Instance.IsZenMode)
			{
				valueText.color = currentlyCompleted ? theme.CorrectValuesColor : theme.WrongValuesColor;
				ValueInsterted?.Invoke(currentValue == correctValue);
			}
			CellUpdated?.Invoke(this);
			
		}
		get => currentValue;
	}

	[SerializeField] private int correctValue;
	public int CorrectValue => correctValue;

	public CellNode node;

	[SerializeField] private TextMeshProUGUI valueText;
	[SerializeField] private Image background;
	

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
		valueText.color = theme.InitialValuesColor;
		currentlyCompleted = true;
	}

	public void AutoResolve()
	{
		CurrentValue = correctValue;
	}
	
	public void RemoveValue()
	{
		currentValue = null;
		valueText.text = "";
		currentlyCompleted = false;
		notesController?.EraseAllNotes();
		CellUpdated?.Invoke(this);
	}

	public void ReInsertPreviousValue(int? value)
	{
		if (!GameManager.Instance.IsZenMode && currentlyCompleted) return;
		
		currentValue = value;
		valueText.text = value == null ? "" : $"{value}";
		currentlyCompleted = value == correctValue;
		valueText.color = theme.NeutralValuesColor;
		if (!GameManager.Instance.IsZenMode)
		{
			valueText.color = currentlyCompleted ? theme.CorrectValuesColor : theme.WrongValuesColor;
		}
		CellUpdated?.Invoke(this);
	}

	

	public void SetBackgroundColor(Color backgroundColor)
	{
		background.color = backgroundColor;
	}

	public void OnValuePressed(int value, bool notes)
	{
		if (!notes)
		{
			ActionRecorder.Instance.Record(new InputValueAction(this,currentValue,value));
		}
		else
		{
			ActionRecorder.Instance.Record(new InputNoteAction(this,value));
		}
	}
	
	public void OnEraserPressed()
	{
		ActionRecorder.Instance.Record(new EraseAction(this,currentValue,notesController?.GetActiveNotes()));
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		NumberSelector.Selected = OnValuePressed;
		ToolsManager.EraserClicked = OnEraserPressed;
		Clicked?.Invoke(this);
	}

	public override void PopulateDictionary()
	{
		services.Add(typeof(CellHighlighter), GetComponent<CellHighlighter>());
	}

	
}