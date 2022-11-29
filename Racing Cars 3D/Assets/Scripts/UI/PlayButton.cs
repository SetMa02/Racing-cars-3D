using System;
using System.Collections;
using System.Collections.Generic;
using IJunior.TypedScenes;
using Platforms;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private PlayerUI _playerUI;
    private float _minBrightness = 1;
    private WaitForSeconds _holdTime;

    private void Awake()
    {
        _playerUI = Camera.main.GetComponent<PlayerUI>();
        _playerUI = Camera.main.GetComponent<PlayerUI>();
        _holdTime = new WaitForSeconds(_playerUI.DarkSpeed);
    }

    public void StartLoadingLevel()
    {
        _playerUI.StartChangeScreenBrightness(1);
        StartCoroutine(LoadHold());
    }

    private IEnumerator LoadHold()
    {
        yield return _holdTime;
        SampleScene.Load();
    }
}
