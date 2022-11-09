using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Platforms;

[RequireComponent(typeof(Obstacle))]
public class Hammer : MonoBehaviour
{
    [SerializeField] private float _attackDuration = 2;
    [SerializeField] private float _recoveryDuration = 2;
    private int _rotateDegree = -90;
    private ParticleSystem _boomEffect;
    private Sequence _sequence;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Start()
    {
        _sequence = DOTween.Sequence();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        Attack();
        _boomEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_boomEffect.isPlaying == true)
        {
            _boomEffect.Stop();
        }

        _boomEffect.transform.rotation = collision.gameObject.transform.rotation;
        _boomEffect.transform.position = collision.contacts[0].point;
        _boomEffect.Play();

        if (collision.gameObject.TryGetComponent<LooseRespawn>(out LooseRespawn looseRespawn))
        {
            looseRespawn.Respawn();
        }

        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            transform.DOKill();
            Reset();
        }
    }

    private void Update()
    {
        transform.position = _startPosition;
    }

    private void Attack()
    {
        transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, _rotateDegree), _attackDuration
            , RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(Reset);
    }

    private void Reset()
    {
        transform.DORotate(new Vector3(_startPosition.x, _startPosition.y, _startPosition.z), 
            _recoveryDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(Attack);
    }
}
