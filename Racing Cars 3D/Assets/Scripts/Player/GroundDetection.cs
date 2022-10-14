using System;
using System.Collections;
using System.Collections.Generic;
using Platforms;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            IsGrounded = true;
        }
        Debug.Log(other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            IsGrounded = false;
        }
    }
}
