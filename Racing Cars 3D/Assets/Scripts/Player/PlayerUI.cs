using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private float _maxFov;
    [SerializeField] private float _fovTime;
    [SerializeField] private Image _darkImage;
    [SerializeField] private float _darkSpeed = 1;
    private float _currentFov;
    private IEnumerator _showAimEffectCourutine;
    private IEnumerator _showDarkScreenEffect;

    public float MaxFov => _maxFov;
    public float NormalFov => _currentFov;

    private void Start()
    {
        _currentFov = Camera.main.fieldOfView;
        _showDarkScreenEffect = DarkScreenEffect();
        
        if (_maxFov == 0 || _fovTime == 0)
        {
            throw new NullReferenceException();
        }
    }

    public void ChangeFov(float targetFov)
    {
        if (_showAimEffectCourutine != null)
        {
            StopCoroutine(_showAimEffectCourutine);
            _showAimEffectCourutine = null;
        }

        _showAimEffectCourutine = ShowAimEffect(targetFov);
        StartCoroutine(_showAimEffectCourutine);
    }

    public void StartDarkScreen()
    {
        StopCoroutine(_showDarkScreenEffect);
        StartCoroutine(_showDarkScreenEffect);
    }

    private IEnumerator DarkScreenEffect()
    {
        while (_darkImage.color.a < 1)
        {
            var darkImageColor = _darkImage.color;
            darkImageColor.a = Mathf.Lerp(darkImageColor.a, 1, _darkSpeed * Time.deltaTime);
            _darkImage.color = darkImageColor;
            yield return null;
        }
    }

    private IEnumerator ShowAimEffect(float targetFov)
    {
        while (Camera.main.fieldOfView != targetFov)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFov, _fovTime * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }
}
