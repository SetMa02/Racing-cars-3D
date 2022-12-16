using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Obstacle))]
public class Saw : MonoBehaviour
{
    [SerializeField] private float _power;
    private ParticleSystem _sparks;

    private void Start()
    {
        _sparks = GetComponentInChildren<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<LooseRespawn>(out LooseRespawn looseRespawn))
        {
            if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(transform.forward * _power, ForceMode.Impulse);
                Debug.Log("aaa");
            }


            if (_sparks.isPlaying == true)
            {
                _sparks.Stop();
            }

            _sparks.Play();

            if (collision.gameObject.TryGetComponent<PlayerUI>(out PlayerUI playerUI))
            {
                playerUI.StartChangeScreenBrightness(1f);
            }
        }
    }
}
