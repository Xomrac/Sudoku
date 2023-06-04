using System;
using UnityEngine;

public class ErrorsManager : MonoBehaviour
{
	[SerializeField] private int currentErrors;
	public static event Action<int> ErrorMade;

	private void OnEnable()
	{
		CellController.ValueInsterted += CheckForError;
	}

	private void OnDisable()
	{
		CellController.ValueInsterted -= CheckForError;

	}

	private void CheckForError(bool cellHasCorrectValue)
	{
		if (cellHasCorrectValue) return;
		
		currentErrors++;
		ErrorMade?.Invoke(currentErrors);
	}
}