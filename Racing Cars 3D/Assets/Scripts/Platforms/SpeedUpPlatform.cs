using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpPlatform : MonoBehaviour
{
    [SerializeField] private float _force = 10;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rigidbody rigidbody))
        {
            if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
            {
                rigidbody.AddForce(Vector3.forward * _force, ForceMode.Impulse);
                Debug.Log("Rush!");
            }
        }
    }
}
