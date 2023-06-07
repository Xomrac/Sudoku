using GameSettings;
using GameSystem;
using UnityEngine;
using XomracUtilities.Extensions;

namespace GameStats
{

    public class StatsDisplayer : MonoBehaviour
    {
        #region Fields

        [SerializeField] private StatField modeField;
        [SerializeField] private StatField errorsField;
        [SerializeField] private StatField scoreField;
        [SerializeField] private StatField timeField;
        
        private GameRules Settings;
        
        #endregion

        #region LifeCycle

        private void Start()
        {
            Settings = GameManager.Instance.GameRules;
            SetInitialValues();
        }
        
        private void OnEnable()
        {
            TimeManager.TimeTicked += OnTimeTicked;
            ErrorsTracker.ErrorsChanged += OnErrorsChanged;
            ScoreTracker.ScoreChanged += OnScoreChanged;
            GameManager.NewGameStarted += OnNewGameStarted;

        }
        
        private void OnDisable()
        {
            TimeManager.TimeTicked -= OnTimeTicked;
            ErrorsTracker.ErrorsChanged -= OnErrorsChanged;
            ScoreTracker.ScoreChanged -= OnScoreChanged;
            GameManager.NewGameStarted -= OnNewGameStarted;
        }

        #endregion

        #region Callbacks

        private void OnNewGameStarted(bool newGame)
        {
            SetInitialValues();
        }
        
        private void OnScoreChanged(int score)
        {
            scoreField.SetValue($"{score}",true);
        }

        private void OnErrorsChanged(int errors)
        {
            errorsField.SetValue($"{errors}/{GameManager.Instance.GameRules.MaxErrors}",true);
        }

        private void OnTimeTicked(float elapsedSeconds)
        {
            int minutes = Mathf.FloorToInt(elapsedSeconds / 60F);
            int seconds = Mathf.FloorToInt(elapsedSeconds - minutes * 60);
            timeField.SetValue($"{minutes:00}:{seconds:00}", seconds == 0);
        }

        #endregion

        #region Methods
        
        private void SetInitialValues()
        {
            modeField.SetValue(Settings.GameMode.GetEnumName(),false);
            errorsField.SetValue(Settings.GameMode == GameMode.Classic ? $"0/{Settings.MaxErrors}" : "∞",false);
            scoreField.SetValue(Settings.GameMode == GameMode.Classic ? "0" : "∞",false);
            timeField.SetValue("00:00",false);
        }
        
        #endregion
    }

}
