using System;
using System.Collections.Generic;
using DefaultNamespace.Bots;
using UnityEngine;

namespace DefaultNamespace
{
    public class RaceStartSignal : MonoBehaviour
    {
        private StartSignal _startSignal;
        private BotMovement[] _groundDetections = new BotMovement[] {};
        private PlayerInput _playerInput;

        private void OnEnable()
        {
            _startSignal.RaceStart += StartSignalOnRaceStart;
        }
        
        private void OnDisable()
        {
            _startSignal.RaceStart -= StartSignalOnRaceStart;
        }

        private void Awake()
        {
            _startSignal = FindObjectOfType<StartSignal>();
            _groundDetections = FindObjectsOfType<BotMovement>();
            _playerInput = FindObjectOfType<PlayerInput>();

            if (_startSignal == null)
            {
                throw new NullReferenceException("No start signal");
            }
        }

        private void Start()
        {
            foreach (var _groundDetection in _groundDetections)
            {
                _groundDetection.enabled = false;
            }

            _playerInput.enabled = false;
            _startSignal.StartCountDown();
        }

        private void StartSignalOnRaceStart()
        {
            foreach (var _groundDetection in _groundDetections)
            {
                _groundDetection.enabled = true;
            }

            _playerInput.enabled = true;
        }
    }
}