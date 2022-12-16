using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Platforms;

[RequireComponent(typeof(Obstacle), typeof(Animator))]
public class Hammer : MonoBehaviour
{
    [SerializeField] private float _attackDuration = 2;
    [SerializeField] private float _recoveryDuration = 2;
    [SerializeField]private ParticleSystem _boomEffect;
    [SerializeField] private ParticleSystem _crackEffect;
    private Animator _animator;
    private string _smashed = "Smashed";

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if (_boomEffect == null && _crackEffect == null)
        {
            throw new Exception();
        }
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        _animator.SetTrigger(_smashed);

        if (_boomEffect.isPlaying == true || _crackEffect.isPlaying == true)
        {
            _boomEffect.Stop();
            _crackEffect.Stop();
        }

        _boomEffect.transform.position = collision.contacts[0].point;
        _crackEffect.transform.position = _boomEffect.transform.position;
        
        _crackEffect.Play();
        _boomEffect.Play();

        if (collision.gameObject.TryGetComponent<PlayerUI>(out PlayerUI player))
        {
            player.StartChangeScreenBrightness(1f);
        }
        
        else if (collision.gameObject.TryGetComponent<LooseRespawn>(out LooseRespawn looseRespawn))
        {
            looseRespawn.Respawn();
        }
    }
}
