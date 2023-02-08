using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject _leftBorder;
    [SerializeField] private GameObject _rightBorder;
    private Vector3 _newPosition;

    private void Start()
    {
        if (_leftBorder == null|| _rightBorder == null)
        {
            throw new NullReferenceException("Spawn border missing");
        }
    }

    public Vector3 GetRandomPositionOnSpawn()
    {
        _newPosition.x = Random.Range(_leftBorder.transform.position.x, _rightBorder.transform.position.x);
        return _newPosition;
    }
}
