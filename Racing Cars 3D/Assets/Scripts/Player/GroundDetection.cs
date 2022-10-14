using System;
using System.Collections;
using System.Collections.Generic;
using Platforms;
using UnityEngine;
using UnityEngine.Events;

public class GroundDetection : MonoBehaviour
{
    public event UnityAction RampJump;
    public bool IsGrounded { get; private set; }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            IsGrounded = true;
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
        }
    }
}
