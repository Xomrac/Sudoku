using System;
using _Scripts.Grid;
using UnityEngine;
using XomracUtilities.Patterns;

public class TimeManager : MonoBehaviour
{

	public static event Action<float> Tick;
	public static event Action<float> Paused;
	public static event Action<float> Resumed;

	private bool paused;

	private float elapsedTime;
	public float ElapsedTime => elapsedTime;
	
	private void OnEnable()
	{
		GameManager.GameResetted += OnGameReset;
		ErrorsManager.GameLost += Pause;
		GridChecker.GameWon += Pause;
		GridBuilder.GridReady += Resume;
	}

	private void OnDisable()
	{
		GameManager.GameResetted -= OnGameReset;
		ErrorsManager.GameLost -= Pause;
		GridChecker.GameWon -= Pause;
		GridBuilder.GridReady -= Resume;

	}
	
	private void OnGameReset()
	{
		elapsedTime = 0;
	}
	

	private void Update()
	{
		if (paused) return;
		elapsedTime += Time.deltaTime;
		Tick?.Invoke(elapsedTime);
	}

	private void Pause()
	{
		paused = true;
		Paused?.Invoke(elapsedTime);
	}

	private void Resume()
	{
		paused = false;
		Resumed?.Invoke(elapsedTime);
	}
	
	
}