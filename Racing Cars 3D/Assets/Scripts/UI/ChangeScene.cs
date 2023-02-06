using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.Player
{
    public class ChangeScene : MonoBehaviour
    {
        [SerializeField] private int _targetScene;
        private PlayerUI _playerUI;
        private float _minBrightness = 1;
        private WaitForSeconds _holdTime;
        private CurrentLevel _currentLevel;

        private void Awake()
        {
            _playerUI = FindObjectOfType<PlayerUI>();
            _holdTime = new WaitForSeconds(_playerUI.DarkSpeed);
            _currentLevel = FindObjectOfType<CurrentLevel>();
            if (_currentLevel == null)
            {
                throw new NullReferenceException("Нужен скрипт CurrentLevel");
            }
        }

        public void StartLoadingNextLevel()
        {
            Time.timeScale = 1;
            _playerUI.StartChangeScreenBrightness(_minBrightness + _minBrightness);
            _currentLevel.CheckCurrentLevel();
            StartCoroutine(LoadHold(_currentLevel.Level));
        }
        
        public void StartLoadingLevel()
        {
            Time.timeScale = 1;
            _playerUI.StartChangeScreenBrightness(_minBrightness + _minBrightness);
            StartCoroutine(LoadHold(null));
        }

        private IEnumerator LoadHold(int? targetScene)
        {
            yield return _holdTime;
            if (targetScene == null)
            {
                SceneManager.LoadScene(_targetScene);
            }
            else
            {
                SceneManager.LoadScene((int)targetScene);
            }
        }
    }
}