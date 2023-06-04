using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CellNotesController : SerializedMonoBehaviour
{

	[SerializeField] private Dictionary<int,GameObject> notes;

	private void Start()
	{
		EraseAllNotes();
	}

	public void ToggleNote(int value)
	{
		notes[value].gameObject.SetActive(!notes[value].gameObject.activeSelf);
	}

	public void EraseAllNotes()
	{
		foreach (KeyValuePair<int,GameObject> valuePair in notes)
		{
			valuePair.Value.SetActive(false);
		}
	}
	
}