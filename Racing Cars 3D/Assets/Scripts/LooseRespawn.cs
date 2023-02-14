using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LooseRespawn : MonoBehaviour
{
    [SerializeField] private float _stepUp = 2;
     private CheckPoint _checkPoint;
     public event UnityAction Respawned;

     private void Awake()
     {
         _checkPoint = FindObjectOfType<CheckPoint>();
     }

     public void Respawn()
    {
        Vector3 newPosition = new Vector3(_checkPoint.GetRandomPositionOnSpawn().x, _checkPoint.transform.position.y + _stepUp,
            _checkPoint.transform.position.z);
        gameObject.transform.rotation = _checkPoint.transform.rotation;
        transform.position = newPosition;
        Respawned?.Invoke();
    }
}
