using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button _button;
    private float _scaleValue = 0.9f;
    private float _scaleTime = 0.1f;
    private Transform _startScale;
    private bool _isDown = false;

    private void Start()
    {
        _button = GetComponent<Button>();
        _startScale = transform;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(_scaleValue, _scaleTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(1, _scaleTime);
    }
}