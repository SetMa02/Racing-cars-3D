using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using DefaultNamespace.Obstacles;
using DefaultNamespace.Player;
using UnityEngine;

public class StartSkipRewardVideo : MonoBehaviour
{
   private LevelWInPanel _levelWIn;
   private ButtonCloseCanvasGroupe _buttonClose;

   private void Start()
   {
      _levelWIn = FindObjectOfType<LevelWInPanel>();
      _buttonClose = GetComponentInParent<ButtonCloseCanvasGroupe>();
   }

   public void ShowVideo()
   {
      VideoAd.Show(null, ShowAd);
   }

   private void ShowAd()
   {
      _buttonClose.CloseCanvasGroup();
      _levelWIn.FinishOnVictory();
   }
}


 
