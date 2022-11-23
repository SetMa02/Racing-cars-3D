using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LooseRespawn : MonoBehaviour
{
    [SerializeField] private CheckPoint _checkPoint;
    [SerializeField] private float _stepUp = 2;
    public event UnityAction Respawned; 

    public void Respawn()
    {
        Vector3 newPosition = new Vector3(_checkPoint.transform.position.x, _checkPoint.transform.position.y + _stepUp,
            _checkPoint.transform.position.z);
        gameObject.transform.rotation = _checkPoint.transform.rotation;
        transform.position = newPosition;
        Respawned?.Invoke();
    }
}
