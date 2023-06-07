using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameTools
{

   public class NumberSelector : MonoBehaviour
   {

      #region Fields

      public static Action<int, bool> Selected;

      [SerializeField] private int selectableNumber;

      [SerializeField] private TextMeshProUGUI numberText;
      [SerializeField] private Button button;

      private bool notesActive;

      #endregion

      #region LifeCycle

      private void Start()
      {
         numberText.text = $"{selectableNumber}";
         button.onClick.AddListener(() => { Selected?.Invoke(selectableNumber, notesActive); });
      }

      private void OnEnable()
      {
         ToolsManager.NotesClicked += OnNotesClicked;
      }

      private void OnDisable()
      {
         ToolsManager.NotesClicked -= OnNotesClicked;
      }

      #endregion

      #region Callbacks

      private void OnNotesClicked(bool active)
      {
         notesActive = active;
      }

      #endregion
     
   }
}
