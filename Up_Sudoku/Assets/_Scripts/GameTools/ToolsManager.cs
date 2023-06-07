using System;
using Audio;
using Command;
using GameSystem;
using UnityEngine;
using UnityEngine.UI;

namespace GameTools
{

	public class ToolsManager : MonoBehaviour
	{

		#region Fields

		[SerializeField] private ScriptableAudio noteToggle;

		[SerializeField] private Button undoButton;
		[SerializeField] private Button eraserButton;
		[SerializeField] private Button hintButton;
		[SerializeField] private Button notesButton;

		public static Action EraserClicked;
		public static event Action<int> HintClicked;
		public static event Action HintUsed;
		public static event Action<bool> NotesClicked;

		private int startingHintsAmount;
		private bool notesOn;

		#endregion

		#region LifeCycle

		private void Start()
		{
			SetupTools();

			undoButton.onClick.AddListener(() => { ActionRecorder.Instance.Undo(); });


			eraserButton.onClick.AddListener(() => { EraserClicked?.Invoke(); });


			hintButton.onClick.AddListener(() =>
			{
				if (startingHintsAmount <= 0) return;
				startingHintsAmount--;
				HintClicked?.Invoke(startingHintsAmount);
				HintUsed?.Invoke();
			});


			notesButton.onClick.AddListener(() =>
			{
				notesOn = !notesOn;
				AudioManager.Instance.PlayEffect(noteToggle);
				NotesClicked?.Invoke(notesOn);
			});
		}

		private void OnEnable()
		{
			GameManager.NewGameStarted += OnNewGameStarted;
		}

		private void OnDisable()
		{
			GameManager.NewGameStarted -= OnNewGameStarted;
		}

		#endregion

		#region Callbacks

		private void OnNewGameStarted(bool newGrid)
		{
			SetupTools();
		}

		#endregion

		#region Methods

		private void SetupTools()
		{
			startingHintsAmount = GameManager.Instance.GameRules.MaxHints;
			notesOn = false;
			HintClicked?.Invoke(startingHintsAmount);
			NotesClicked?.Invoke(notesOn);
		}

		#endregion

	}

}