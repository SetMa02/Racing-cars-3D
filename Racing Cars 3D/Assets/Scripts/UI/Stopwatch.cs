using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Obstacles;
using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    private TMP_Text _stopwatch;
    private float _seconds = 0;
    private float _realSeconds = 0;
    private StartSignal _startSignal;
    private bool _isRaceStart = false;
    private float _maxSeconds;
    private Finish _finish;

    private void OnEnable()
    {
        _startSignal.RaceStart += StartSignalOnRaceStart;
        _finish.Victory += FinishOnVictory;
    }
    
    private void OnDisable()
    {
        _startSignal.RaceStart -= StartSignalOnRaceStart;
        _finish.Victory -= FinishOnVictory;
    }

    private void FinishOnVictory()
    {
        _isRaceStart = false;
    }

    private void Awake()
    {
        _startSignal = FindObjectOfType<StartSignal>();
        _stopwatch = GetComponent<TMP_Text>();
        _finish = FindObjectOfType<Finish>();
        _maxSeconds = 60f;

        if (_startSignal == null || _finish == null)
        {
            throw new NullReferenceException("Components missing");
        }
    }

    private void FixedUpdate()
    {
        if (_isRaceStart == true)
        {
            _seconds += Time.deltaTime;
            _stopwatch.text = $"{$"{_seconds:F}"}";
        }
    }

    private void StartSignalOnRaceStart()
    {
        _isRaceStart = true;
    }
}
