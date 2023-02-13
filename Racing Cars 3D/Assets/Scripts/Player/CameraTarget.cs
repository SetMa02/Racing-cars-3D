using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private float _followSpeed = 0.5f;
    [SerializeField] private float _distanceToPlayer;
    private PlayerInput _playerInput;
    
    private void Start()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
        _distanceToPlayer = Vector3.Distance(_playerInput.transform.position, transform.position);
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (Vector3.Distance(_playerInput.gameObject.transform.position, transform.position) > _distanceToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerInput.transform.position, 
                _followSpeed * Time.fixedDeltaTime);
        }
    }
}
