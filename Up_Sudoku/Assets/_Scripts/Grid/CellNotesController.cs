using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class CellNotesController : SerializedMonoBehaviour
{

	[SerializeField] private Dictionary<int,GameObject> notes;
	
	
	public List<int> GetActiveNotes()
	{
		return (from pair in notes where pair.Value.activeSelf select pair.Key).ToList();
	}

	private void Start()
	{
		EraseAllNotes();
	}

	public void ToggleNote(int value)
	{
		notes[value].gameObject.SetActive(!notes[value].gameObject.activeSelf);
	}

	public void ActivatesNotes(List<int> notesToActivate)
	{
		Debug.Log("EO");

		if (notesToActivate.Count<=0) return;
		Debug.Log("OA");
		foreach (KeyValuePair<int,GameObject> pair in notes)
		{
			pair.Value.SetActive(false);
		}
		foreach (int index in notesToActivate)
		{
			Debug.Log("GABIBBO");
			notes[index].gameObject.SetActive(true);
		}
	}

	public void EraseAllNotes()
	{
		foreach (KeyValuePair<int,GameObject> valuePair in notes)
		{
			valuePair.Value.SetActive(false);
		}
	}
	
}