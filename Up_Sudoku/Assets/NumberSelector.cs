using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberSelector : MonoBehaviour
{
   public static event Action<int> Selected;

   [SerializeField] private int selectableNumber;

   [SerializeField] private TextMeshProUGUI numberText;
   [SerializeField] private Button button;

   

   private void Start()
   {
      button.onClick.RemoveAllListeners();
      numberText.text = $"{selectableNumber}";
      button.onClick.AddListener(() => { Selected?.Invoke(selectableNumber);});
   }
}
