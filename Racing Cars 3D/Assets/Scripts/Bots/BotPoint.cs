using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Bots
{
    public class BotPoint : MonoBehaviour
    {
        public event UnityAction Reached;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<BotController>(out BotController controller))
            {
                Reached.Invoke();
            }
        }
    }
}