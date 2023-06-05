using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Grid;
using TMPro;
using UnityEngine;
using XomracUtilities.Extensions;

public class StatsDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI modeText;
    [SerializeField] private TextMeshProUGUI errorsText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;


    private GameSettings Settings;
    private void Start()
    {
        Settings = GameManager.Instance.gameSettings;
        modeText.text = Settings.GameMode.GetEnumName();
        errorsText.text = Settings.GameMode == GameMode.Classic ? $"0/{Settings.MaxErrors}" : "∞";
        scoreText.text = Settings.GameMode == GameMode.Classic ? "0" : "∞";
        timeText.text = "00:00";
    }

    private void OnEnable()
    {
        TimeManager.Tick += UpdateTime;
        ErrorsManager.ErrorsChanged += UpdateErrors;
        ScoreManager.ScoreChanged += UpdateScore;
        GameManager.GameResetted += OnGameReset;
    }

    private void OnGameReset()
    {
        modeText.text = Settings.GameMode.GetEnumName();
        errorsText.text = Settings.GameMode == GameMode.Classic ? $"0/{Settings.MaxErrors}" : "∞";
        scoreText.text = Settings.GameMode == GameMode.Classic ? "0" : "∞";
        timeText.text = "00:00";
    }

    private void OnDisable()
    {
        TimeManager.Tick -= UpdateTime;
        ErrorsManager.ErrorsChanged -= UpdateErrors;
        ScoreManager.ScoreChanged -= UpdateScore;
        GameManager.GameResetted -= OnGameReset;

    }

    private void UpdateScore(int score)
    {
        scoreText.text = $"{score}";
    }

    private void UpdateErrors(int errors)
    {
        errorsText.text = $"{errors}/{Settings.MaxErrors}";
    }

    private void UpdateTime(float elapsedSeconds)
    {
        int minutes = Mathf.FloorToInt(elapsedSeconds / 60F);
        int seconds = Mathf.FloorToInt(elapsedSeconds - minutes * 60);
        timeText.text = $"{minutes:00}:{seconds:00}";
    }

}
