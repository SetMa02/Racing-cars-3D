using System;
using System.Collections;
using System.Collections.Generic;
using Platforms;
using UnityEngine;
using UnityEngine.Events;

public class GroundDetection : MonoBehaviour
{
    private float _secUnitilLoose = 3;
    private IEnumerator _looseCountDownCourutine;
    public event UnityAction RampJump;
    public event UnityAction Loose;
    public bool IsGrounded { get; private set; }
    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            IsGrounded = true;
            
            if (_looseCountDownCourutine != null)
            {
                StopCoroutine(_looseCountDownCourutine);
                _looseCountDownCourutine = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            IsGrounded = false;
            if (other.gameObject.TryGetComponent<RampPlatform>(out RampPlatform rampPlatform))
            {
             
                RampJump?.Invoke();
            }

            if (_looseCountDownCourutine == null)
            {
                _looseCountDownCourutine = LooseCountDown();
                StartCoroutine(_looseCountDownCourutine);
            }
        }
    }

    private IEnumerator LooseCountDown()
    {
        float count = _secUnitilLoose;
        while (_secUnitilLoose > 0 &&  IsGrounded == false)
        {
            count -= Time.deltaTime;

            if (count <= 0)
            {
                Loose?.Invoke();
                count = _secUnitilLoose;
            }

            yield return null;
        }
    }
}
