 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PlayerInput _player;
    private Vector3 _position;
    private CameraTarget _targetTransform;

    private void Start()
    {
        _player = FindObjectOfType<PlayerInput>();
        _targetTransform = FindObjectOfType<CameraTarget>();
        _position = _targetTransform.transform.InverseTransformPoint(transform.position);
    }

    private void FixedUpdate()
    {
        if (_player.IsPressed == false)
        {
            Vector3 currentPosition = _targetTransform.transform.TransformPoint(_position);
            transform.position = Vector3.Lerp(transform.position, currentPosition, _speed * Time.deltaTime);
            Transform newRotation = transform;
            newRotation.LookAt(_targetTransform.transform);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation.rotation, _speed * Time.deltaTime);
        }
       
    }
}
