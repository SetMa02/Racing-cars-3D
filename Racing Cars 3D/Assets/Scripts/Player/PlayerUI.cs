using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private float _maxFov;
    [SerializeField] private float _fovTime;
    [SerializeField] private float _darkSpeed = 1;
    [SerializeField] private Image _actionEffectImage;
    private DarkImage _darkImage;
    private float _currentFov;
    private IEnumerator _showAimEffectCourutine;
    private IEnumerator _showDarkScreenEffect;
    private bool _isDark = false;

    public float DarkSpeed => _darkSpeed;

    public event UnityAction Loose;

    public float MaxFov => _maxFov;
    public float NormalFov => _currentFov;

    private void Start()
    {
        _currentFov = Camera.main.fieldOfView;
        _darkImage = FindObjectOfType<DarkImage>();
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
        var darkImageColor = _darkImage.DarkScreen.color;
        darkImageColor.a = 0;
        _darkImage.DarkScreen.color = darkImageColor;
    }

    public void ShowActionEffect()
    {
        var color = _actionEffectImage.color;
        color.a = 0;
        _actionEffectImage.color = color;
    }

    public void HideActionEffect()
    {
        var color = _actionEffectImage.color;
        color.a = 1;
        _actionEffectImage.color = color;
    }

    private IEnumerator ChangeScreenBrightness(float targetBrightness)
    {
        while (Math.Abs(_darkImage.DarkScreen.color.a - targetBrightness) > 0.01)
        {
            var darkImageColor = _darkImage.DarkScreen.color;
            darkImageColor.a = Mathf.Lerp(darkImageColor.a, targetBrightness, _darkSpeed * Time.deltaTime);
            _darkImage.DarkScreen.color = darkImageColor;
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
