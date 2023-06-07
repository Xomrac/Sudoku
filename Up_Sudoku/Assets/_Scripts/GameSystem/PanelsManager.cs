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
			GameManager.GameEnded += DisplayEndgamePanel;
			GridBuilder.GridReady += RemoveLoadingPanel;
		}

		private void OnDisable()
		{
			GameManager.GameEnded -= DisplayEndgamePanel;
			GridBuilder.GridReady -= RemoveLoadingPanel;
		}

		#endregion

		#region Methods

		private void DisplayEndgamePanel(bool win)
		{
			winCanvas.gameObject.SetActive(win);
			loseCanvas.gameObject.SetActive(!win);
		}

		private void RemoveLoadingPanel()
		{
			loadingScreen.gameObject.SetActive(false);
		}
		

		#endregion

	}

}