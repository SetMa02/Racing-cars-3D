using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonOpenCanvasGroup : MonoBehaviour
{
    [SerializeField] private CanvasGroup _targetCAnvasGroup;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OpenCanvasGroup);
    }

    private void OpenCanvasGroup()
    {
        _targetCAnvasGroup.alpha = 1;
        _targetCAnvasGroup.interactable = true;
        _targetCAnvasGroup.blocksRaycasts = true;
    }
}
