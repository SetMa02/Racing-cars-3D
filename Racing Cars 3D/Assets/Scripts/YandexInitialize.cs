using System.Collections;
using Agava.YandexGames;
using Agava.YandexGames.Samples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class YandexInitialize : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;  
    }

    private IEnumerator Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    yield return YandexGamesSdk.Initialize();
#endif
        yield return null;

    }
}

