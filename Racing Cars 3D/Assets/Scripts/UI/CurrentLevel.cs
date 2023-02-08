using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentLevel : MonoBehaviour
{
    public int Level = 0;
    private string _currentlevelString = "CurrentLevel";
    private int _tutorialLevel = 1;
    private void Start()
    {
       CheckCurrentLevel();
    }

    public void CheckCurrentLevel()
    {
        if (PlayerPrefs.HasKey(_currentlevelString))
        {
            Level = PlayerPrefs.GetInt(_currentlevelString);
        }
        else
        {
            Level = _tutorialLevel;
        }

        if (SceneManager.sceneCountInBuildSettings - 1 < Level)
        {
            Level = _tutorialLevel;
            Level++;
        }
    }

    public void SaveLevel(int level)
    {
        PlayerPrefs.SetInt(_currentlevelString, level);
    }
}
