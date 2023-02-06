using System;
using System.Collections;
using System.Collections.Generic;
using Platforms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    private PlayerUI _playerUI;
    private float _minBrightness = 1;
    private WaitForSeconds _holdTime;
    private CurrentLevel _currentLevel;

    private void Awake()
    {
        _playerUI = FindObjectOfType<PlayerUI>();
        _holdTime = new WaitForSeconds(_playerUI.DarkSpeed);
        _currentLevel = FindObjectOfType<CurrentLevel>();
        if (_currentLevel == null)
        {
            throw new NullReferenceException("Нужен скрипт CurrentLevel");
        }
    }

    public void StartLoadingNextLevel()
    {
        _playerUI.StartChangeScreenBrightness(1);
        StartCoroutine(LoadHold());
    }

    private IEnumerator LoadHold()
    {
        yield return _holdTime;
        int nextLevel = _currentLevel.Level;
        nextLevel++;
        SceneManager.LoadScene(nextLevel);
    }
}
