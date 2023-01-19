using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using DeviceType = Agava.YandexGames.DeviceType;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]public float StartX;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _timeSlowScale;
    private float _slowSpeed;
    private Rigidbody _rigidbody;
    private float _desktopRotateSpeed = 0.2f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (Device.Type == DeviceType.Desktop)
        {
            _rotateSpeed = _desktopRotateSpeed;
        }
    }

    public void StopCar()
    {
        _rigidbody.velocity = Vector3.zero;
    }
    
    public void Aim(Vector3 _inputPosition)
    {
        float newRotationY = _inputPosition.x - StartX;
        newRotationY /= _rotateSpeed;
        var transformRotation = transform.eulerAngles;
        transformRotation.y -= newRotationY;
        transform.eulerAngles = transformRotation;
        _rigidbody.velocity = new Vector3(0,0,0);
    }

    public void Drop()
    {
        _rigidbody.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    public void ActivateSlowMo()
    {
        Time.timeScale = _timeSlowScale;
    }
}
