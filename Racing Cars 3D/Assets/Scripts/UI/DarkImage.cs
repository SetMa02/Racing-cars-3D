using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Player
{
    public class DarkImage : MonoBehaviour
    {
        [HideInInspector]public Image DarkScreen;

        private void Awake()
        {
            DarkScreen = GetComponent<Image>();
        }
    }
}