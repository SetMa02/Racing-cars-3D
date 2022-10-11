using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]public float StartX;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _timeSlowScale;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Aim(Vector3 _inputPosition)
    {
        Time.timeScale = _timeSlowScale;
        float newRotationY = _inputPosition.x - StartX;
        newRotationY /= _rotateSpeed;
        var transformRotation = transform.eulerAngles;
        transformRotation.y -= newRotationY;
        transform.eulerAngles = transformRotation;
    }

    public void Drop()
    {
        Time.timeScale = 1;
        _rigidbody.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }
}
