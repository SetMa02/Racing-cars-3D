using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    private Button _button;
    private float _scaleValue = 0.9f;
    private float _scaleTime = 0.1f;
    private Transform _startScale;

    private void Start()
    {
        _button = GetComponent<Button>();
        _startScale = transform;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            transform.DOKill();
            transform.DOScale(_scaleValue, _scaleTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            transform.DOKill();
            transform.DOScale(1, _scaleTime);
        }
    }
}