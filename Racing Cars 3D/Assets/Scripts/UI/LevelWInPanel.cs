using System;
using DefaultNamespace.Obstacles;
using DefaultNamespace.Player;
using UnityEngine;
using UnityEngine.UI;


namespace DefaultNamespace.Player
{
    public class LevelWInPanel : MonoBehaviour
    {
        [SerializeField]private Finish _finish;
        private CanvasGroup _winPanel;

        private void Start()
        {
            _winPanel = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            _finish.Victory += FinishOnVictory;
        }

        private void OnDisable()
        {
            _finish.Victory -= FinishOnVictory;
        }

        public void FinishOnVictory()
        {
            _winPanel.alpha = 1;
            _winPanel.interactable = true;
            _winPanel.blocksRaycasts = true;
            Time.timeScale = 0;
        }
    }
}