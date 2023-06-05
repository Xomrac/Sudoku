using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using XomracUtilities.Patterns;

namespace _Scripts.Grid
{

	public class GameManager : Singleton<GameManager>
	{
		public GameSettings gameSettings;
		public static event Action GameResetted;
		public static event Action GameStarted;

		[SerializeField] private GameObject loadingScreen;
		[SerializeField] private Canvas winCanvas;
		[SerializeField] private Canvas loseCanvas;
		[SerializeField] private string menuScene;

		

		public bool IsZenMode => gameSettings.GameMode == GameMode.Zen;

		private void OnEnable()
		{
			ErrorsManager.GameLost+= DisplayLoseCanvas;
			GridManager.GridReady += RemoveLoading;
			GridManager.GameWon += DisplayWinCanvas;
		}

		public override void Awake()
		{
			base.Awake();
			gameSettings = new GameSettings(GameRules.gameRules.GameMode,GameRules.gameRules.BlankCells,GameRules.gameRules.MaxErrors,GameRules.gameRules.MaxHints);
		}

		private void OnDisable()
		{
			ErrorsManager.GameLost-= DisplayLoseCanvas;
			GridManager.GridReady -= RemoveLoading;
			GridManager.GameWon -= DisplayWinCanvas;

		}

		private void DisplayWinCanvas()
		{
			winCanvas.gameObject.SetActive(true);
		}
		private void DisplayLoseCanvas()
		{
			loseCanvas.gameObject.SetActive(true);
		}

		public void ResetGame()
		{
			GameResetted?.Invoke();
			loseCanvas.gameObject.SetActive(false);
			winCanvas.gameObject.SetActive(false);
		}

		public void ReturnToMenu()
		{
			SceneManager.LoadScene(menuScene);
		}
		

		private void Start()
		{
			GameStarted?.Invoke();
		}

		private void RemoveLoading()
		{
			loadingScreen.SetActive(false);
		}

	}

}