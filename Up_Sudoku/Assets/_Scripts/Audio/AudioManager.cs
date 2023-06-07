using DG.Tweening;
using GameSystem;
using UnityEngine;
using XomracUtilities.Patterns;

namespace Audio
{
    public class AudioManager : Singleton<AudioManager>
    {

        #region Fields
        		
        [SerializeField] private ScriptableAudio gameWin;
        [SerializeField] private ScriptableAudio gameLost;
        [SerializeField] private ScriptableAudio sceneBackgroundMusic;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource effectsSource;
        [SerializeField] private float fadeDuration;
        
        #endregion

        #region LifeCycle
        
        private void Awake()
        {
            base.Awake();
            sceneBackgroundMusic.Play(musicSource);
            musicSource.playOnAwake = false;
            musicSource.volume = 0;
            musicSource.Play();
            musicSource.DOFade(sceneBackgroundMusic.Volume, fadeDuration);
        }

        private void OnEnable()
        {
            GameManager.GameEnded += OnGameEnded;
        }
        private void OnDisable()
        {
            GameManager.GameEnded -= OnGameEnded;
        }

        #endregion

        #region Callbacks

        private void OnGameEnded(bool win)
        {
            ScriptableAudio effectToPlay = win ? gameWin : gameLost;
            PlayEffect(effectToPlay);
        }

        #endregion
        
        #region Methods

        public void PlayEffect(ScriptableAudio effect)
        {
            effect.PlayOneShot(effectsSource);
        }

       
        #endregion
       
    }
}
