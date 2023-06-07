using System;
using GameSettings;
using GameStats;
using Grid;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using XomracUtilities.Patterns;

namespace GameSystem
{

	public class GameManager : Singleton<GameManager>
	{

		#region Fields

		[SerializeField] private GameRules gameRules;
		public GameRules GameRules => gameRules;

		[SerializeField] private string menuScene;

		public static event Action<bool> NewGameStarted;
		public static event Action GameStarted;
		public static event Action GamePaused;
		public static event Action<bool> GameEnded;

		#endregion

		#region LifeCycle

		public override void Awake()
		{
			base.Awake();
			gameRules = new GameRules(GameData.gameRules.GameMode, GameData.gameRules.BlankCells, GameData.gameRules.MaxErrors, GameData.gameRules.MaxHints);
		}

		private void Start()
		{
			GameStarted?.Invoke();
		}

		private void OnEnable()
		{
			ErrorsTracker.GameLost += OnGameLost;
			GridChecker.GameWon += OnGameWon;
		}

		private void OnDisable()
		{
			ErrorsTracker.GameLost -= OnGameLost;
			GridChecker.GameWon -= OnGameWon;
		}

		#endregion

		#region Callbacks

		private void OnGameWon()
		{
			GamePaused?.Invoke();
			GameEnded?.Invoke(true);
		}

		private void OnGameLost()
		{
			GamePaused?.Invoke();
			GameEnded?.Invoke(false);
		}

		#endregion

		#region Methods

		public bool IsZenMode => gameRules.GameMode == GameMode.Zen;
		
		public void ResetGame()
		{
			NewGameStarted?.Invoke(false);
		}

		public void RestartGame()
		{
			NewGameStarted?.Invoke(true);
		}

		public void ReturnToMenu()
		{
			SceneManager.LoadScene(menuScene);
		}

		#endregion

	}

}