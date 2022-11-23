 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _speed;
    [SerializeField] private PlayerInput _player;
    private Vector3 _position;

    private void Start()
    {
        _position = _targetTransform.InverseTransformPoint(transform.position);

        if (_player == null)
        {
            throw new NullReferenceException();
        }
    }

    private void FixedUpdate()
    {
        if (_player.IsPressed == false)
        {
            Vector3 currentPosition = _targetTransform.TransformPoint(_position);
            transform.position = Vector3.Lerp(transform.position, currentPosition, _speed * Time.deltaTime);
            Transform newRotation = transform;
            newRotation.LookAt(_targetTransform);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation.rotation, _speed * Time.deltaTime);
        }
       
    }
}
