using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Obstacles
{
    public class Finish : MonoBehaviour
    {
        public event UnityAction Victory;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerInput _player))
            {
                Victory?.Invoke();
            }
        }
    }
}