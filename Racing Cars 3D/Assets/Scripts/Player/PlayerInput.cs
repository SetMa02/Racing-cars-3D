using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private bool _playerAim = false;
    private GroundDetection _groundDetection;
    
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _groundDetection = GetComponentInChildren<GroundDetection>();
    }

    public void OnButtonPressed()
    {
        if (_playerAim == false && _groundDetection.IsGrounded == true)
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
        _playerMovement.Drop();
        yield return null;
    }
}
