using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerUI))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private bool _playerAim = false;
    private GroundDetection _groundDetection;
    private PlayerUI _playerUI;
    
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _groundDetection = GetComponentInChildren<GroundDetection>();
        _playerUI = GetComponent<PlayerUI>();
    }

    private void OnValidate()
    {
        if (_groundDetection == null)
        {
           
        }
    }

    private void OnEnable()
    {
        _groundDetection.RampJump += GroundDetectionOnRampJump;
    }

    private void OnDisable()
    {
        _groundDetection.RampJump -= GroundDetectionOnRampJump;
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
        _playerUI.ChangeFov(_playerUI.MaxFov);
        
        while (Input.GetMouseButton(0))
        {
            Vector3 newPlayerAimPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            _playerMovement.Aim(newPlayerAimPoint);
            yield return null;
        }

        _playerAim = false;
        _playerUI.ChangeFov(_playerUI.NormalFov);
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
