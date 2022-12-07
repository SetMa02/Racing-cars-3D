using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Bots;
using UnityEngine;

[RequireComponent(typeof(BotMovement), typeof(LooseRespawn))]
public class BotController : MonoBehaviour
{
    [SerializeField]private BotPoint[] _points;
    private BotPoint _targetPoint;
    private BotMovement _botMovement;
    private int _currentPointIndex = 0;
    private GroundDetection _groundDetection;
    private LooseRespawn _looseRespawn;

    private void OnEnable()
    {
        _botMovement.ReadyToShoot += BotMovementOnReadyToShoot;
        _groundDetection.Loose += GroundDetectionOnLoose;

        foreach (var point in _points)
        {
            point.Reached += PointOnReached;
        }
    }

    private void OnDisable()
    {
        _botMovement.ReadyToShoot -= BotMovementOnReadyToShoot;
        
        foreach (var point in _points)
        {
            point.Reached -= PointOnReached;
        }
    }

    private void Awake()
    {
        _groundDetection = GetComponentInChildren<GroundDetection>();
        _botMovement = GetComponent<BotMovement>();
        _looseRespawn = GetComponent<LooseRespawn>();
        _targetPoint = _points[0];
    }

    private void GroundDetectionOnLoose()
    {
        _currentPointIndex = 0;
        _targetPoint = _points[_currentPointIndex];
        _looseRespawn.Respawn();
    }
    
    private void BotMovementOnReadyToShoot()
    {
        if (_groundDetection.IsGrounded == true && _botMovement.IsAim == false)
        {
            _botMovement.StartAim(_targetPoint);
        }
    }

    private void PointOnReached()
    {
        _currentPointIndex++;
        if (_currentPointIndex >= _points.Length)
        {
            Debug.Log("bot finish");
        }
        else
        {
            _targetPoint = _points[_currentPointIndex];
        }
    }
}
