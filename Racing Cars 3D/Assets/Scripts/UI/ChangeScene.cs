using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.Player
{
    public class ChangeScene : MonoBehaviour
    {
        [SerializeField] private int _targetScene;
        [SerializeField]private PlayerUI _playerUI;
        private float _minBrightness = 1;
        private WaitForSeconds _holdTime;

        private void Awake()
        {
            _holdTime = new WaitForSeconds(0.5f);
        }

        public void StartLoadingLevel()
        {
            Time.timeScale = 1;
            _playerUI.StartChangeScreenBrightness(1);
            StartCoroutine(LoadHold());
        }

        private IEnumerator LoadHold()
        {
            yield return _holdTime;
            SceneManager.LoadScene(_targetScene);
        }
    }
}