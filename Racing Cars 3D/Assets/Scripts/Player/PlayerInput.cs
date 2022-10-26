using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerUI), typeof(CarAnimation))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private GroundDetection _groundDetection;
    private PlayerUI _playerUI;
    private bool _isPressed = false;
    private ParticleSystem[] _lights = new ParticleSystem[] {};
    private Arrow _arrow;
    private CarAnimation _carAnimation;
    private IEnumerator _buttonPressedCourutine;


    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _groundDetection = GetComponentInChildren<GroundDetection>();
        _playerUI = GetComponent<PlayerUI>();
        _lights = GetComponentsInChildren<ParticleSystem>();
        _arrow = GetComponentInChildren<Arrow>();
        _carAnimation = GetComponent<CarAnimation>();
        _buttonPressedCourutine = ButtonPressed();
        _arrow.gameObject.SetActive(false);

        if (_lights.Length == 0)
        {
            throw new NullReferenceException();
        }
    }

    private void OnEnable()
    {
        _groundDetection.RampJump += GroundDetectionOnRampJump;
        _groundDetection.Loose += GroundDetectionOnLoose;
    }

    private void OnDisable()
    {
        _groundDetection.RampJump -= GroundDetectionOnRampJump;
        _groundDetection.Loose -= GroundDetectionOnLoose;
    }
    
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && _isPressed == false && _groundDetection.IsGrounded == true)
        {
            StopCoroutine(ButtonPressed());
            StartCoroutine(ButtonPressed());
            _isPressed = true;
        }
    }

    private void SwichLightStatus(bool status)
    {
        foreach (ParticleSystem light in _lights)
        {
            light.gameObject.SetActive(status);
        }
    }
    
    private void GroundDetectionOnRampJump()
    {
        StartCoroutine(OnRampJump());
    }

    private void GroundDetectionOnLoose()
    {
        _playerUI.StartDarkScreen();
    }

    private IEnumerator ButtonPressed()
    {
        Vector3 playerAimPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        _playerMovement.StartX = playerAimPoint.x;
        _playerUI.ChangeFov(_playerUI.MaxFov);
        _carAnimation.StartLoad();
        _arrow.gameObject.SetActive(true);
        // SwichLightStatus( false);
        
        while (Input.GetMouseButton(0))
        {
            Vector3 newPlayerAimPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            _playerMovement.Aim(newPlayerAimPoint);
            yield return null;
        }
        
       // SwichLightStatus( true);
       _arrow.gameObject.SetActive(false);
       _carAnimation.Drop();
        _isPressed = false;
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
