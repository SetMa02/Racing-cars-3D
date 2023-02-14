using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Player
{
    public class DarkImage : MonoBehaviour
    {
        public Image DarkScreen;

        private void Awake()
        {
            DarkScreen = GetComponent<Image>();
        }
    }
}