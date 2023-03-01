using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class RaceStartSignal : MonoBehaviour
    {
        private StartSignal _startSignal;
        private GroundDetection[] _groundDetections = new GroundDetection[] {};

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
            _groundDetections = FindObjectsOfType<GroundDetection>();

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
        }

        private void StartSignalOnRaceStart()
        {
            foreach (var _groundDetection in _groundDetections)
            {
                _groundDetection.enabled = true;
            }
        }
    }
}