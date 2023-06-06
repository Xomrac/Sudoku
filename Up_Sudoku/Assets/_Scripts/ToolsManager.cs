using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolsManager : MonoBehaviour
{
    [SerializeField] private ScriptableAudio noteToggle;

    
    [SerializeField] private Button undoButton;
    [SerializeField] private Button eraserButton;
    [SerializeField] private Button hintButton;
    [SerializeField] private Button notesButton;


    private bool notesOn;
    [SerializeField]private int startingHintsAmount;

    public static Action EraserClicked;
    public static event Action<int> HintClicked;
    public static event Action HintUsed;
    public static event Action<bool> NotesClicked;

    private void OnEnable()
    {
        GameManager.GameResetted += OnGameReset;
        GameManager.GameRestarted += OnGameReset;
    }

    private void OnDisable()
    {
        GameManager.GameResetted -= OnGameReset;
        GameManager.GameRestarted -= OnGameReset;
    }

    private void OnGameReset()
    {
        SetupTools();
    }
    
    private void SetupTools()
    {
        startingHintsAmount = GameManager.Instance.gameSettings.MaxHints;
        notesOn = false;
        HintClicked?.Invoke(startingHintsAmount);
        NotesClicked?.Invoke(notesOn);
    }
    private void Start()
    {
        startingHintsAmount = GameManager.Instance.gameSettings.MaxHints;
        notesOn = false;
        HintClicked?.Invoke(startingHintsAmount);
        NotesClicked?.Invoke(notesOn);


        undoButton.onClick.AddListener(() => { ActionRecorder.Instance.Undo(); });


        eraserButton.onClick.AddListener(() => { EraserClicked?.Invoke(); });


        hintButton.onClick.AddListener(() =>
        {
            if (startingHintsAmount <= 0) return;
            startingHintsAmount--;
            HintClicked?.Invoke(startingHintsAmount);
            HintUsed?.Invoke();
        });


        notesButton.onClick.AddListener(() =>
        {
            notesOn = !notesOn;
            AudioManager.Instance.PlayEffect(noteToggle);
            NotesClicked?.Invoke(notesOn);
        });


    }

}

