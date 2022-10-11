using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerUI))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerUI _UI;
    private bool _playerAim = false;
    
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _UI = GetComponent<PlayerUI>();
        
    }

    public void OnButtonPressed()
    {
        if (_playerAim == false)
        {
            StartCoroutine(ButtonPressed());
        }   
    }

    private IEnumerator ButtonPressed()
    {
        _playerAim = true;
        Vector3 playerAimPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        _playerMovement.StartX = playerAimPoint.x;
        while (Input.GetMouseButton(0))
        {
            Vector3 newPlayerAimPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            _playerMovement.Aim(newPlayerAimPoint);
            yield return null;
        }

        _playerAim = false;
        yield return null;
    }
}
