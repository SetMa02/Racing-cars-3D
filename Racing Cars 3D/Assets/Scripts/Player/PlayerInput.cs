using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerUI), typeof(CarAnimation))]
[RequireComponent(typeof(LooseRespawn))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private GroundDetection _groundDetection;
    private PlayerUI _playerUI;
    private bool _isPressed = false;
    private ParticleSystem[] _lights = new ParticleSystem[] {};
    private Arrow _arrow;
    private CarAnimation _carAnimation;
    private LooseRespawn _looseRespawn;
    private float _maxBrightnes = 1;
    private WaitForSeconds _carRespawnCooldown;
    private float _secSlowmo = 1.5f;

    public bool IsPressed => _isPressed;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _groundDetection = GetComponentInChildren<GroundDetection>();
        _playerUI = GetComponent<PlayerUI>();
        _lights = GetComponentsInChildren<ParticleSystem>();
        _arrow = GetComponentInChildren<Arrow>();
        _carAnimation = GetComponent<CarAnimation>();
        _arrow.gameObject.SetActive(false);
        _looseRespawn = GetComponent<LooseRespawn>();
        _carRespawnCooldown = new WaitForSeconds(1);

        if (_lights.Length == 0)
        {
            throw new NullReferenceException();
        }
    }

    private void OnEnable()
    {
        _groundDetection.RampJump += GroundDetectionOnRampJump;
        _groundDetection.Loose += GroundDetectionOnLoose;
        _looseRespawn.Respawned += LooseRespawnOnRespawned;
        _playerUI.Loose+= PlayerUIOnLoose;
    }

    private void OnDisable()
    {
        _groundDetection.RampJump -= GroundDetectionOnRampJump;
        _groundDetection.Loose -= GroundDetectionOnLoose;
        _looseRespawn.Respawned -= LooseRespawnOnRespawned;
        _playerUI.Loose -= PlayerUIOnLoose;
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
    private void LooseRespawnOnRespawned()
    {
        _playerMovement.StopCar();
        StartCoroutine(RespawnCarCooldown());
    }
    
    private void PlayerUIOnLoose()
    {
        _looseRespawn.Respawn();
    }

    private void GroundDetectionOnRampJump()
    {
        StartCoroutine(OnRampJump());
    }

    public void GroundDetectionOnLoose()
    {
        _playerUI.StartChangeScreenBrightness(_maxBrightnes);
    }

    private IEnumerator ButtonPressed()
    {
        Vector3 playerAimPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        _playerMovement.StartX = playerAimPoint.x;
        _playerUI.ChangeFov(_playerUI.MaxFov);
        _carAnimation.StartLoad();
        _arrow.gameObject.SetActive(true);

        while (Input.GetMouseButton(0))
        {
            Vector3 newPlayerAimPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            _playerMovement.Aim(newPlayerAimPoint);
            yield return null;
        }
        
       _arrow.gameObject.SetActive(false);
       _carAnimation.Drop();
        _isPressed = false;
        _playerUI.ChangeFov(_playerUI.NormalFov);
        _playerMovement.Drop();
        yield return null;
    }

    private IEnumerator RespawnCarCooldown()
    {
        yield return _carRespawnCooldown;
        
        _playerUI.ShowScreen();
    }

    private IEnumerator OnRampJump()
    {
        _playerMovement.ActivateSlowMo();
        float secSlowMo = _secSlowmo;
        while (_groundDetection.IsGrounded == false || 0 >= secSlowMo)
        {
            secSlowMo -= Time.deltaTime;
            
            yield return null;
        }
        Time.timeScale = 1;
    }
}
