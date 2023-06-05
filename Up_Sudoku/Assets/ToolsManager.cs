using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolsManager : MonoBehaviour
{
    [SerializeField] private Button undoButton;
    [SerializeField] private Button eraserButton;
    [SerializeField] private Button hintButton;
    [SerializeField] private Button notesButton;

    public static Action EraserClicked;
    public static event Action HintClicked;
    public static event Action NotesClicked;

    [SerializeField] private TextMeshProUGUI notesStatus;
    [SerializeField] private TextMeshProUGUI hintAmount;

    private bool notesOn;
    
    private void Start()
    {
        undoButton.onClick.RemoveAllListeners();
        undoButton.onClick.AddListener(() => {ActionRecorder.Instance.Undo();});
        
        eraserButton.onClick.RemoveAllListeners();
        eraserButton.onClick.AddListener(()=>{EraserClicked?.Invoke();});
        
        hintButton.onClick.RemoveAllListeners();
        hintButton.onClick.AddListener(()=>{HintClicked?.Invoke();});
        
        notesButton.onClick.RemoveAllListeners();
        notesButton.onClick.AddListener(() =>
        {
            NotesClicked?.Invoke();
            notesOn = !notesOn;
            notesStatus.text = notesOn ? "ON" : "OFF";
        });

        notesStatus.text = notesOn ? "ON" : "OFF";
        hintAmount.text = $"{GameManager.Instance.gameSettings.MaxHints}";
    }

}
