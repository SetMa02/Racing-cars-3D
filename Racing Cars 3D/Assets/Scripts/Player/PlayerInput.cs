using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private bool _playerAim = false;
    private GroundDetection _groundDetection;

    private void OnEnable()
    {
        _groundDetection.RampJump += GroundDetectionOnRampJump;
    }

    private void OnDisable()
    {
        _groundDetection.RampJump -= GroundDetectionOnRampJump;
    }

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _groundDetection = GetComponentInChildren<GroundDetection>();
    }

    private void Update()
    {
        if (_groundDetection == false)
        {
            _playerMovement.ActivateSlowMo();
        }
    }

    public void OnButtonPressed()
    {
        if (_playerAim == false && _groundDetection.IsGrounded == true)
        {
            StartCoroutine(ButtonPressed());
        }   
    }
    
    private void GroundDetectionOnRampJump()
    {
        StartCoroutine(OnRampJump());
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

    private IEnumerator OnRampJump()
    {
        _playerMovement.ActivateSlowMo();
        yield return new WaitUntil(() => _groundDetection.IsGrounded == true);
        // метод возвращение в нормальное состояние
        yield return null;
    }
}
