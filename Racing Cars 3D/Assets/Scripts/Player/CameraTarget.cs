using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private float _followSpeed = 0.5f;
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private GameObject _moveTarget;
    private PlayerInput _playerInput;
    private LooseRespawn _respawn;
    private Vector3 _targetPosition;
    private Vector3 _positionDifference;
    private Vector3 _startPosition;
    private Quaternion _targetRotation;

    private void OnEnable()
    {
        _respawn.Respawned += RespawnOnRespawned;
    }

    private void OnDisable()
    {
        _respawn.Respawned -= RespawnOnRespawned;
    }

    private void Awake()
    {
        _playerInput = FindObjectOfType<PlayerInput>();

        if (_playerInput != null)
        {
            _respawn = _playerInput.GetComponent<LooseRespawn>();
        }
        
        _distanceToPlayer = Vector3.Distance(_playerInput.transform.position, transform.position);
        _positionDifference = transform.position - _playerInput.transform.position ;
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }
    
    private void RespawnOnRespawned()
    {
        transform.position = _startPosition;
    }

    private void FollowPlayer()
    {
        _targetPosition = _playerInput.transform.position + _positionDifference;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 
            _followSpeed * Time.fixedDeltaTime);
    }
}
