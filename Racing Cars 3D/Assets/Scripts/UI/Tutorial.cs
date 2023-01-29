using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]private PauseButton _pauseButton;
    private GroundDetection _groundDetection;
    private void Start()
    {
        _groundDetection = FindObjectOfType<GroundDetection>();
        _pauseButton.gameObject.SetActive(false);
        _groundDetection.gameObject.SetActive(false);
    }

    public void CompleteTutorial()
    {
        _groundDetection.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(true);
    }

}
