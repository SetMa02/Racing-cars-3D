using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerInput>(out PlayerInput playerInput))
        {
           playerInput.GroundDetectionOnLoose();
        }
    }
}
