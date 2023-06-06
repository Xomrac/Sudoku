using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XomracUtilities.Extensions;

public class GameRulesSetter : MonoBehaviour
{
    [SerializeField] private GameSettings defaultSettings;

    [SerializeField] private Slider blankCellsSlider;
    [SerializeField] private TMP_InputField blankCellsInput;
    
    [SerializeField] private Slider hintsSlider;
    [SerializeField] private TMP_InputField hintsInput;
    
    [SerializeField] private Button gameModeButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    

    [SerializeField] private GameObject errorsSettingsGroup;

    
    [SerializeField] private Slider ErrorsSlider;
    [SerializeField] private TMP_InputField errorsInput;
    [SerializeField] private Button startGameButton;

    public static event Action GameStarted;
    
    private void SetupValues()
    {
        blankCellsSlider.value = defaultSettings.BlankCells;
        blankCellsInput.text = $"{defaultSettings.BlankCells}";
        
        hintsSlider.value = defaultSettings.MaxHints;
        hintsInput.text = $"{defaultSettings.MaxHints}";
        
        ErrorsSlider.value = defaultSettings.MaxErrors;
        errorsInput.text = $"{defaultSettings.MaxErrors}";

        buttonText.text = defaultSettings.GameMode.GetEnumName();
        errorsSettingsGroup.SetActive(defaultSettings.GameMode==GameMode.Classic);

    }

    private void SetupElements()
    {
        SetupCells();
        SetupHints();
        SetupErrors();
        gameModeButton.onClick.AddListener(() =>
        {
            defaultSettings.GameMode = defaultSettings.GameMode==GameMode.Zen ? GameMode.Classic : GameMode.Zen;
            buttonText.text = defaultSettings.GameMode.GetEnumName();
            errorsSettingsGroup.SetActive(defaultSettings.GameMode==GameMode.Classic);
        });
        
        startGameButton.onClick.AddListener(() =>
        {
            GameRules.gameRules = new GameSettings(defaultSettings.GameMode,defaultSettings.BlankCells,defaultSettings.MaxErrors,defaultSettings.MaxHints);
            GameStarted?.Invoke();
        });

        void SetupHints() 
        {
            hintsSlider.onValueChanged.AddListener((value) =>
            {
                hintsInput.text = $"{value}";
                defaultSettings.MaxHints = (int)value;
            });

            hintsInput.onValueChanged.AddListener((stringValue) =>
            {
                var intValue = int.Parse(stringValue);
                intValue = Mathf.Clamp(intValue, 0, 80);
                defaultSettings.MaxHints = intValue;
                hintsSlider.value = intValue;
            });
        }
        
        void SetupCells() 
        {
            blankCellsSlider.onValueChanged.AddListener((value) =>
            {
                blankCellsInput.text = $"{value}";
                defaultSettings.BlankCells = (int)value;
            });

            blankCellsInput.onValueChanged.AddListener((stringValue) =>
            {
                var intValue = int.Parse(stringValue);
                intValue = Mathf.Clamp(intValue, 1, 80);
                defaultSettings.BlankCells = intValue;
                blankCellsSlider.value = intValue;
            });
        }

        void SetupErrors()
        {
            ErrorsSlider.onValueChanged.AddListener((value) =>
                {
                    errorsInput.text = $"{value}";
                    defaultSettings.MaxErrors = (int)value;
                });
            errorsInput.onValueChanged.AddListener((stringValue) =>
                {
                    var intValue = int.Parse(stringValue);
                    intValue = Mathf.Clamp(intValue, 1, 80);
                    defaultSettings.MaxErrors = intValue;
                    ErrorsSlider.value = intValue;
                });
        }
        
    }
    private void Start()
    {
        SetupValues();
        SetupElements();
    }

}
