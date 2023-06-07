using System;
using _Scripts.Grid;
using Grid;
using UnityEngine;
using XomracUtilities.Patterns;

namespace GameSystem
{

	public class TimeManager : MonoBehaviour
	{

		#region Fields

		public static event Action<float> TimeTicked;
		public static event Action<float> TimePaused;
		public static event Action<float> TimeResumed;

		private float elapsedTime;
		public float ElapsedTime => elapsedTime;
		
		private bool paused;
		
		#endregion

		#region LifeCycle

		private void OnEnable()
		{
			GameManager.NewGameStarted += OnNewGame;
			GameManager.GamePaused += Pause;
			GridBuilder.GridReady += Resume;
		}
		private void Update()
		{
			if (paused) return;
			elapsedTime += Time.deltaTime;
			TimeTicked?.Invoke(elapsedTime);
		}
		private void OnDisable()
		{
			GameManager.GamePaused -= Pause;
			GameManager.NewGameStarted -= OnNewGame;
			GridBuilder.GridReady -= Resume;
		}

		#endregion

		#region Callbacks

		private void OnNewGame(bool newGrid)
		{
			elapsedTime = 0;
		}

		#endregion

		#region Methods

		private void Pause()
		{
			paused = true;
			TimePaused?.Invoke(elapsedTime);
		}

		private void Resume()
		{
			paused = false;
			TimeResumed?.Invoke(elapsedTime);
		}
		
		#endregion
	}
}