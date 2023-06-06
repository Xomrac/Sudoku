using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Grid;
using DG.Tweening;
using TMPro;
using UnityEngine;
using XomracUtilities.Extensions;

public class StatsDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI modeText;
    [SerializeField] private TextMeshProUGUI errorsText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private Vector3 punchForce;
    [SerializeField] private float punchSpeed;

    

    
    [SerializeField] private float growEndValue=1.1f;
    [SerializeField] private float growSpeed=.2f;
    [SerializeField] private Ease ease=Ease.Linear;


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
        GameManager.GameRestarted += OnGameReset;

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
        GameManager.GameRestarted -= OnGameReset;


    }

    private void UpdateScore(int score)
    {
        scoreText.text = $"{score}";
        var sequence = DOTween.Sequence();
        sequence.Append(scoreText.transform.DOScale(growEndValue, growSpeed).SetEase(ease));
        sequence.Append(scoreText.transform.DOScale(1, growSpeed).SetEase(ease));
        sequence.Play();

    }

    private void UpdateErrors(int errors)
    {
        var sequence = DOTween.Sequence();

        errorsText.text = $"{errors}/{Settings.MaxErrors}";
        sequence.Append(errorsText.transform.DOPunchPosition(punchForce, punchSpeed));
        sequence.Append(errorsText.transform.DOPunchPosition(-punchForce, punchSpeed));
        sequence.Play();
    }

    private void UpdateTime(float elapsedSeconds)
    {
        int minutes = Mathf.FloorToInt(elapsedSeconds / 60F);
        int seconds = Mathf.FloorToInt(elapsedSeconds - minutes * 60);
        var sequence = DOTween.Sequence();
        if (seconds==0)
        {
            sequence.Append(timeText.transform.DOScale(growEndValue, growSpeed).SetEase(ease));
            sequence.Append(timeText.transform.DOScale(1, growSpeed).SetEase(ease));
            sequence.Play();
        }
        
        timeText.text = $"{minutes:00}:{seconds:00}";
    }

}
