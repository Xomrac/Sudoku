using System;
using _Scripts.Grid;
using _Scripts.Grid.Themes;
using Audio;
using Command;
using DG.Tweening;
using GameSystem;
using UnityEngine;
using XomracUtilities.Patterns;

namespace Grid.Cells
{

	public class CellInput : ServiceComponent<CellController>
	{

		#region Fields

		private int? currentValue;
		public int? CurrentValue => currentValue;

		[SerializeField] private ScriptableAudio neutralInput;
		[SerializeField] private ScriptableAudio correctInput;
		[SerializeField] private ScriptableAudio wrongInput;
		[SerializeField] private ScriptableAudio eraseInput;
		[SerializeField] private ScriptableAudio hintInput;

		public static event Action<bool> ValueInsterted;

		#endregion

		#region Callbacks

		public void OnValuePressed(int value, bool notes)
		{
			if (!ServiceLocator.Editable) return;

			AudioManager.Instance.PlayEffect(neutralInput);
			if (!notes)
			{
				ActionRecorder.Instance.Record(new InputValueAction(ServiceLocator, currentValue, value));
			}
			else
			{
				ActionRecorder.Instance.Record(new InputNoteAction(ServiceLocator, value));
			}
		}

		public void OnEraserPressed()
		{
			ActionRecorder.Instance.Record(new EraseAction(ServiceLocator, currentValue, ServiceLocator.GetService<CellNotesController>()?.GetActiveNotes()));
		}

		#endregion

		#region Methods

		public void InputValue(int value)
		{
			currentValue = value;
			bool correctValue = value == ServiceLocator.CorrectValue;
			
			if (!GameManager.Instance.IsZenMode)
			{
				if (correctValue)
				{
					var textColor = ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.correctValuesColor);
					ServiceLocator.GetService<CellDisplayer>().DisplayValue(value, textColor, true);
					AudioManager.Instance.PlayEffect(correctInput);
				}
				else
				{
					var textColor = ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.wrongValuesColor);
					ServiceLocator.GetService<CellDisplayer>().DisplayValue(value, textColor, false);
					AudioManager.Instance.PlayEffect(wrongInput);
				}
				ValueInsterted?.Invoke(correctValue);
			}
			else
			{
				var textColor = ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.neutralValuesColor);
				ServiceLocator.GetService<CellDisplayer>().DisplayValue(value, textColor, true);
			}
			ServiceLocator.SetCompletition(correctValue);
		}

		public void SetInitialValue(int value)
		{
			currentValue = value;
			var textColor = ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.initialValuesColor);
			ServiceLocator.GetService<CellDisplayer>().SetText(value, textColor);
		}

		public void SetCorrectvalue()
		{
			InputValue(ServiceLocator.CorrectValue);
			AudioManager.Instance.PlayEffect(hintInput);
		}

		public void RestorePreviousValue(int? value)
		{
			currentValue = value;
			bool correctValue = value == ServiceLocator.CorrectValue;
			
			if (!GameManager.Instance.IsZenMode)
			{
				if (correctValue)
				{
					var textColor = ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.correctValuesColor);
					ServiceLocator.GetService<CellDisplayer>().DisplayValue(value, textColor, true);
					// AudioManager.Instance.PlayEffect(correctInput);
				}
				else
				{
					var textColor = ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.wrongValuesColor);
					ServiceLocator.GetService<CellDisplayer>().DisplayValue(value, textColor, false);
					// AudioManager.Instance.PlayEffect(wrongInput);
				}
			}
			else
			{
				var textColor = ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.neutralValuesColor);
				ServiceLocator.GetService<CellDisplayer>().DisplayValue(value, textColor, false);
			}
			ServiceLocator.SetCompletition(correctValue);
		}

		public void RestoreInitialValue(int? value)
		{
			currentValue = value;
			bool correctValue = value == ServiceLocator.CorrectValue;
			var textColor = ServiceLocator.GetService<CellThemer>().Theme.GetElementColor(ElementsNames.initialValuesColor);
			ServiceLocator.GetService<CellDisplayer>().DisplayValue(value, textColor);
			ServiceLocator.SetCompletition(correctValue,false);
		}

		public void EraseValue()
		{
			if (!ServiceLocator.Editable) return;
			AudioManager.Instance.PlayEffect(eraseInput);
			currentValue = null;
			ServiceLocator.GetService<CellNotesController>()?.EraseAllNotes();
			ServiceLocator.GetService<CellDisplayer>().HideValue();
			ServiceLocator.SetCompletition(false);
		}

		public void RemoveValue()
		{
			currentValue = null;
			ServiceLocator.GetService<CellDisplayer>().RemoveText();
			ServiceLocator.SetCompletition(false,false);
		}

		#endregion

	}

}