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
    private bool _isDark = false;

    public event UnityAction Loose;

    public float MaxFov => _maxFov;
    public float NormalFov => _currentFov;

    private void Start()
    {
        _currentFov = Camera.main.fieldOfView;

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

    public void StartChangeScreenBrightness(float targetBrightness)
    {
        if (_isDark == false)
        {
            _showDarkScreenEffect = ChangeScreenBrightness(targetBrightness);
            StartCoroutine(_showDarkScreenEffect);
            _isDark = true;
        }
    }

    public void ShowScreen()
    {
        var darkImageColor = _darkImage.color;
        darkImageColor.a = 0;
        _darkImage.color = darkImageColor;
    }

    private IEnumerator ChangeScreenBrightness(float targetBrightness)
    {
        while (Math.Abs(_darkImage.color.a - targetBrightness) > 0.01)
        {
            var darkImageColor = _darkImage.color;
            darkImageColor.a = Mathf.Lerp(darkImageColor.a, targetBrightness, _darkSpeed * Time.deltaTime);
            _darkImage.color = darkImageColor;
            yield return null;
        }
        Loose?.Invoke();
        _isDark = false;
        yield return null;
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
