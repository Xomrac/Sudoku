using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberSelector : MonoBehaviour
{
   public static  Action<int,bool> Selected;

   [SerializeField] private int selectableNumber;

   [SerializeField] private TextMeshProUGUI numberText;
   [SerializeField] private Button button;

   private bool notesActive;

   private void Start()
   {
      button.onClick.RemoveAllListeners();
      numberText.text = $"{selectableNumber}";
      button.onClick.AddListener(() => { Selected?.Invoke(selectableNumber,notesActive);});
   }

   private void OnEnable()
   {
      ToolsManager.NotesClicked += ToggleNotes;
   }
   
   private void OnDisable()
   {
      ToolsManager.NotesClicked -= ToggleNotes;
   }
   
   private void ToggleNotes(bool active)
   {
      notesActive = active;
   }
}
