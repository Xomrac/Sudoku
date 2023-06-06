using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using XomracUtilities.Patterns;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private ScriptableAudio sceneBackgroundMusic;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private float fadeDuration;

    private void Awake()
    {
        base.Awake();
        sceneBackgroundMusic.Play(musicSource);
        musicSource.playOnAwake = false;
        musicSource.volume = 0;
        musicSource.Play();
        musicSource.DOFade(sceneBackgroundMusic.Volume, fadeDuration);
    }

    public void PlayEffect(ScriptableAudio effect)
    {
        effect.PlayOneShot(effectsSource);
    }
    

}
