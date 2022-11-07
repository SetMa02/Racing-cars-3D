using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hammer : MonoBehaviour
{
    [SerializeField] private float _attackDuration = 2;
    [SerializeField] private float _recoveryDuration = 2;
    private int _rotateDegree = -90;
    
    private void Start()
    {
        RotateHammer();
    }

    private void RotateHammer()
    {
        transform.DORotate(new Vector3(0, 0, _rotateDegree), _attackDuration).OnComplete(() =>
        {
            transform.DORotate(new Vector3(0, 0, 0), _recoveryDuration).OnComplete(RotateHammer);
        });
    }
}
