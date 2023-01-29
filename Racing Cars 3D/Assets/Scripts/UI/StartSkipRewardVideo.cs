using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using DefaultNamespace.Obstacles;
using DefaultNamespace.Player;
using UnityEngine;

public class StartSkipRewardVideo : MonoBehaviour
{
   [SerializeField] private ChangeScene _nextLevel;
   private LevelWInPanel _levelWIn;
   
   private void Start()
   {
      _levelWIn = FindObjectOfType<LevelWInPanel>();
   
   }

   public void ShowVideo()
   {
      VideoAd.Show(null, ShowAd);
   }

   private void ShowAd()
   {
      _nextLevel.StartLoadingLevel();
   }
}


 
