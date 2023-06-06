using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Grid;
using DG.Tweening;
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

	[SerializeField] private ScriptableAudio neutralInput;
	[SerializeField] private ScriptableAudio correctInput;
	[SerializeField] private ScriptableAudio wrongInput;
	[SerializeField] private ScriptableAudio eraseInput;
	[SerializeField] private ScriptableAudio hintInput;

	public static event Action<CellController> Clicked;

	[SerializeField] private CellNotesController notesController;
	public CellNotesController NotesController => notesController;

	public static event Action<CellController> CellUpdated;
	public static event Action<bool> ValueInsterted;

	private bool editable = false;

	[SerializeField] private float growEndValue = 1.1f;
	[SerializeField] private float growSpeed = .2f;
	[SerializeField] private Ease ease;

	public int? CurrentValue
	{
		set{
			currentValue = value;
			valueText.text = value == null ? "" : $"{value}";
			currentlyCompleted = value == correctValue;
			valueText.color = GetService<CellThemer>().Theme.GetElementColor(ElementsNames.neutralValuesColor);
			var sequence = DOTween.Sequence();
			if (!GameManager.Instance.IsZenMode)
			{
				if (currentlyCompleted)
				{
					valueText.color = GetService<CellThemer>().Theme.GetElementColor(ElementsNames.correctValuesColor);
					sequence.Append(valueText.transform.DOPunchPosition(new Vector3(0, 10, 0), growSpeed));
					editable = false;
					AudioManager.Instance.PlayEffect(correctInput);
				}
				else
				{
					sequence.Append(valueText.transform.DOPunchRotation(new Vector3(0, 0, 30), growSpeed));
					sequence.Append(valueText.transform.DOPunchRotation(new Vector3(0, 0, -30), growSpeed));
					valueText.color = GetService<CellThemer>().Theme.GetElementColor(ElementsNames.wrongValuesColor);
					editable = true;
					AudioManager.Instance.PlayEffect(wrongInput);
				}
				ValueInsterted?.Invoke(currentValue == correctValue);
			}

			sequence.Append(valueText.transform.DOScale(growEndValue, growSpeed));
			sequence.Append(valueText.transform.DOScale(1, growSpeed));
			sequence.Play();
			CellUpdated?.Invoke(this);
		}
		get => currentValue;
	}

	[SerializeField] private int correctValue;
	public int CorrectValue => correctValue;

	public CellNode node;

	[SerializeField] private TextMeshProUGUI valueText;

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
		PopulateDictionary();
	}

	public void ReplaceInitialValue(int? value)
	{
		currentValue = value;
		valueText.text = $"{value}";
		valueText.color = GetService<CellThemer>().Theme.GetElementColor(ElementsNames.initialValuesColor);
		currentlyCompleted = currentValue == correctValue;
		editable = !value.HasValue;
		notesController?.DisableAllNotes();
	}

	public void SetInitialValue(int value)
	{
		correctValue = value;
		currentValue = correctValue;
		valueText.text = $"{value}";
		valueText.color = GetService<CellThemer>().Theme.GetElementColor(ElementsNames.initialValuesColor);
		currentlyCompleted = true;
	}

	public void AutoResolve()
	{
		AudioManager.Instance.PlayEffect(hintInput);
		CurrentValue = correctValue;
		valueText.color = GetService<CellThemer>().Theme.GetElementColor(ElementsNames.correctValuesColor);
		editable = false;
	}

	public void RemoveValue()
	{
		if (!editable) return;

		AudioManager.Instance.PlayEffect(eraseInput);

		currentValue = null;
		currentlyCompleted = false;
		notesController?.EraseAllNotes();
		var sequence = DOTween.Sequence();
		sequence.Append(valueText.transform.DOScale(0, growSpeed));
		sequence.onComplete += () => valueText.text = "";
		CellUpdated?.Invoke(this);
	}

	public void EraseValue()
	{
		currentValue = null;
		valueText.text = "";
		currentlyCompleted = false;
		notesController?.EraseAllNotes();
		editable = true;
	}

	public void ReInsertPreviousValue(int? value)
	{
		if (!GameManager.Instance.IsZenMode && currentlyCompleted) return;

		currentValue = value;
		valueText.text = value == null ? "" : $"{value}";
		currentlyCompleted = value == correctValue;
		valueText.color = GetService<CellThemer>().Theme.GetElementColor(ElementsNames.neutralValuesColor);
		if (!GameManager.Instance.IsZenMode)
		{
			valueText.color = currentlyCompleted ? GetService<CellThemer>().Theme.GetElementColor(ElementsNames.correctValuesColor) : GetService<CellThemer>().Theme.GetElementColor(ElementsNames.wrongValuesColor);
		}
		CellUpdated?.Invoke(this);
	}

	private void Animate()
	{
		var sequence = DOTween.Sequence();
		sequence.Append(transform.DOScale(growEndValue, growSpeed).SetEase(ease));
		sequence.Append(transform.DOScale(1f, growSpeed).SetEase(ease));
		sequence.Play();
	}

	public void OnValuePressed(int value, bool notes)
	{
		if (!editable) return;

		AudioManager.Instance.PlayEffect(neutralInput);
		if (!notes)
		{
			ActionRecorder.Instance.Record(new InputValueAction(this, currentValue, value));
		}
		else
		{
			ActionRecorder.Instance.Record(new InputNoteAction(this, value));
		}
	}

	public void OnEraserPressed()
	{
		ActionRecorder.Instance.Record(new EraseAction(this, currentValue, notesController?.GetActiveNotes()));
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!editable) return;

		NumberSelector.Selected = OnValuePressed;
		ToolsManager.EraserClicked = OnEraserPressed;
		Animate();
		Clicked?.Invoke(this);
	}

	public override void PopulateDictionary()
	{
		services.Add(typeof(CellHighlighter), GetComponent<CellHighlighter>());
		services.Add(typeof(CellThemer), GetComponent<CellThemer>());
	}

}