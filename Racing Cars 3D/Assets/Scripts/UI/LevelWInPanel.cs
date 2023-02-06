using System;
using DefaultNamespace.Obstacles;
using DefaultNamespace.Player;
using UnityEngine;
using UnityEngine.UI;


namespace DefaultNamespace.Player
{
    public class LevelWInPanel : MonoBehaviour
    {
        private Finish _finish;
        private CanvasGroup _winPanel;
        private CurrentLevel _currentLevel;

        private void Awake()
        {
            _currentLevel = FindObjectOfType<CurrentLevel>();
            _winPanel = GetComponent<CanvasGroup>();
            _finish = FindObjectOfType<Finish>();
            if (_finish == null)
            {
                throw new NullReferenceException("Необходимо расположить финишную арку");
            }
        }

        private void OnEnable()
        {
            _finish.Victory += FinishOnVictory;
        }

        private void OnDisable()
        {
            _finish.Victory -= FinishOnVictory;
        }

        private void FinishOnVictory()
        {
            _currentLevel.Level++;
            _currentLevel.SaveLevel(_currentLevel.Level);
            _currentLevel.CheckCurrentLevel();
            _winPanel.alpha = 1;
            _winPanel.interactable = true;
            _winPanel.blocksRaycasts = true;
            Time.timeScale = 0;
        }
    }
}