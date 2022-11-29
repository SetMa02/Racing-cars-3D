using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Player
{
    [RequireComponent(typeof(Button))]
    public class ButtonCloseCanvasGroupe : MonoBehaviour
    {
        private CanvasGroup _targetCanvasGroup;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _targetCanvasGroup = transform.parent.gameObject.GetComponent<CanvasGroup>();
            _button.onClick.AddListener(CloseCanvasGroup);
        }

        private void CloseCanvasGroup()
        {
            _targetCanvasGroup.alpha = 0;
            _targetCanvasGroup.interactable = false;
            _targetCanvasGroup.blocksRaycasts = false;
        }
    }
}