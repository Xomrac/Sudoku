using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{

    public class MenuFlowManager : MonoBehaviour
    {
        [SerializeField] private GameObject currentOpenTab;
        [SerializeField] private string gameSceneName;

        private void OnEnable()
        {
            GameRulesSetter.GameStarted += StartGame;
        }

        private void OnDisable()
        {
            GameRulesSetter.GameStarted -= StartGame;

        }

        public void ChangeTab(GameObject tabToOpen)
        {
            currentOpenTab.SetActive(false);
            currentOpenTab = tabToOpen;
            currentOpenTab.SetActive(true);
        }


        public void StartGame()
        {
            SceneManager.LoadScene(gameSceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }

}
