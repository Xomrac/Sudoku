using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

	public static event Action<float> Tick;
	public static event Action<float> Paused;
	public static event Action<float> Resumed;

	private bool paused;

	private float elapsedTime;
	public float ElapsedTime => elapsedTime;

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