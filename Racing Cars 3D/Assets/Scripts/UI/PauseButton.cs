using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseButton : MonoBehaviour
{
   private Button _button;

   private void Start()
   {
      _button = GetComponent<Button>();
      
      _button.onClick.AddListener(PauseGame);
   }

   private void PauseGame()
   {
      if (Time.timeScale == 0)
      {
         Time.timeScale = 1;
      }
      else
      {
         Time.timeScale = 0;
      }
   }
}
