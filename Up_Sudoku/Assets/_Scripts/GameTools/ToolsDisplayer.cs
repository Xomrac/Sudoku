using GameSystem;
using TMPro;
using UnityEngine;

namespace GameTools
{

	public class ToolsDisplayer : MonoBehaviour
	{

		#region Fields

		[SerializeField] private TextMeshProUGUI notesStatus;
		[SerializeField] private TextMeshProUGUI hintsAmount;

		#endregion

		#region LifeCycle

		private void Start()
		{
			OnNotesClicked(false);
			OnHintsClicked(GameManager.Instance.GameRules.MaxHints);
		}

		private void OnEnable()
		{
			ToolsManager.NotesClicked += OnNotesClicked;
			ToolsManager.HintClicked += OnHintsClicked;
		}

		private void OnDisable()
		{
			ToolsManager.NotesClicked -= OnNotesClicked;
			ToolsManager.HintClicked -= OnHintsClicked;
		}

		#endregion

		#region Callbacks

		private void OnNotesClicked(bool active)
		{
			notesStatus.text = active ? "ON" : "OFF";
		}

		private void OnHintsClicked(int amount)
		{
			hintsAmount.text = $"{amount}";
		}

		#endregion

	}

}