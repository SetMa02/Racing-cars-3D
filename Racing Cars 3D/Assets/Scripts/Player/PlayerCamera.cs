 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _speed;
    private Vector3 _position;

    private void Start()
    {
        _position = _targetTransform.InverseTransformPoint(transform.position);
    }

    private void FixedUpdate()
    {
        Vector3 currentPosition = _targetTransform.TransformPoint(_position);
        transform.position = Vector3.Lerp(transform.position, currentPosition, _speed * Time.deltaTime);
        Transform newRotation = transform;
        newRotation.LookAt(_targetTransform);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation.rotation, _speed * Time.deltaTime);
    }
}
