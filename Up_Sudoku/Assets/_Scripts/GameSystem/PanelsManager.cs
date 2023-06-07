using System;
using Audio;
using Grid;
using UnityEngine;

namespace GameSystem
{

	public class PanelsManager : MonoBehaviour
	{

		#region Fields

		[SerializeField] private Canvas loadingScreen;
		[SerializeField] private Canvas winCanvas;
		[SerializeField] private Canvas loseCanvas;

		#endregion

		#region LifeCycle
		private void OnEnable()
		{
			GameManager.GameEnded += OnGameEnded;
			GridBuilder.GridReady += OnGridReady;
			GameManager.NewGameStarted += OnNewGameStarted;
		}

		private void OnDisable()
		{
			GameManager.GameEnded -= OnGameEnded;
			GridBuilder.GridReady -= OnGridReady;
			GameManager.NewGameStarted -= OnNewGameStarted;
		}

		#endregion
		
		#region Callbacks
		private void OnGameEnded(bool win)
		{
			winCanvas.gameObject.SetActive(win);
			loseCanvas.gameObject.SetActive(!win);
		}

		private void OnNewGameStarted(bool newGrid)
		{
			winCanvas.gameObject.SetActive(false);
			loseCanvas.gameObject.SetActive(false);
		}

		private void OnGridReady()
		{
			loadingScreen.gameObject.SetActive(false);
		}
		#endregion

		#region Methods
		
		#endregion

	}

}