using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Obstacles;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBer : MonoBehaviour
{
    [SerializeField] private PlayerInput _player;
    [SerializeField] private Finish _finish;
    private Vector3 _startPosition;
    
    private Scrollbar _scrollbar;

    private void Start()
    {
        _scrollbar = GetComponent<Scrollbar>();

        if (_player == null || _finish == null)
        {
            throw new NullReferenceException();
        }
    }

    private void SetProgressBar()
    {
        _startPosition = _player.transform.position;
    }

    private void CheckPlayerProgress()
    {
        
    }
}
