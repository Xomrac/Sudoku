using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class CellNotesController : SerializedMonoBehaviour
{

	[SerializeField] private Dictionary<int,GameObject> notes;
	[SerializeField] private float growSpeed=.2f;

	

	public List<int> GetActiveNotes()
	{
		return (from pair in notes where pair.Value.activeSelf select pair.Key).ToList();
	}

	private void Start()
	{
		foreach (KeyValuePair<int,GameObject> pair in notes)
		{
			pair.Value.SetActive(false);
		}
	}

	public void ToggleNote(int value)
	{
		var note = notes[value].gameObject;
		AnimateNote(note,!note.activeSelf);
	}

	private void AnimateNote(GameObject note, bool activated)
	{
		var sequence = DOTween.Sequence();
		if (!activated)
		{
			sequence.Append(note.transform.DOScale(0, growSpeed));
			sequence.onComplete += () => note.SetActive(false);
			sequence.Play();
		}
		else
		{
			note.SetActive(true);
			note.transform.DOScale(1, growSpeed);
		}
	}

	public void ActivatesNotes(List<int> notesToActivate)
	{
	

		if (notesToActivate.Count<=0) return;
		
		EraseAllNotes();
		foreach (int index in notesToActivate)
		{
			AnimateNote(notes[index],true);
		}
	}

	public void EraseAllNotes()
	{
		foreach (KeyValuePair<int,GameObject> pair in notes)
		{
			AnimateNote(pair.Value,false);
		}
	}
	
}