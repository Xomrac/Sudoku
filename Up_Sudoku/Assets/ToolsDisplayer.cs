using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid;
using TMPro;
using UnityEngine;

public class ToolsDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notesStatus;
    [SerializeField] private TextMeshProUGUI hintsAmount;
    

    private void OnEnable()
    {
        ToolsManager.NotesClicked += UpdateNotes;
        ToolsManager.HintClicked += UpdateHints;
    }
    
    private void UpdateNotes(bool active)
    {
        notesStatus.text = active ? "ON" : "OFF";
    }

    private void UpdateHints(int amount)
    {
        hintsAmount.text = $"{amount}";
    }
    
    
    

    
    
    
}
