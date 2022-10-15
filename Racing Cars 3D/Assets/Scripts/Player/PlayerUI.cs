using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private float _maxFov;
    [SerializeField] private float _fovTime;
    private float _currentFov;
    private IEnumerator _showAimEffectCourutine;

    public float MaxFov => _maxFov;
    public float NormalFov => _currentFov;
    
    private void Start()
    {
        _currentFov = Camera.main.fieldOfView;
    }

    /*
    private void OnValidate()
    {
        if (_maxFov == 0 || _fovTime == 0)
        {
            throw new NullReferenceException();
        }
    }
    */

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
