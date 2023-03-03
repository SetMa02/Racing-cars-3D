using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Obstacles;
using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    private TMP_Text _stopwatch;
    private int _minutes = 0;
    private int _seconds = 0;
    private float _realSeconds = 0;
    private StartSignal _startSignal;
    private bool _isRaceStart = false;
    private float _maxSeconds = 60;
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

        if (_startSignal == null || _finish == null)
        {
            throw new NullReferenceException("Components missing");
        }
    }

    private void FixedUpdate()
    {
        if (_isRaceStart == true)
        {
            _realSeconds += Time.deltaTime;
            _seconds = (int)Math.Round(_realSeconds, 2);
            
            if (_seconds >= _maxSeconds)
            {
                _minutes++;
                _seconds = 0;
            }
            
            _stopwatch.text = $"{$"{_minutes:d}"}:{$"{_seconds:d}"}";
        }
    }

    private void StartSignalOnRaceStart()
    {
        _isRaceStart = true;
    }
}
