using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Button _movementButton;
        public event UnityAction ButtonPressed;
        public event UnityAction ButtonDrop;
        

        public void OnButtonPressed()
        {
            ButtonPressed?.Invoke();
        }

        public void OnButtonDrop()
        {
            ButtonDrop?.Invoke();
        }

    }
}