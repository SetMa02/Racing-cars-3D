using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartSignal : MonoBehaviour
{
    [SerializeField] private List<Image> _lights = new List<Image>();
    [SerializeField] private float _secBeforeStart;
    [SerializeField] private float _holdTime;
    private WaitForSeconds _waitDelay;
    private WaitForSeconds _hold;

    public event UnityAction RaceStart;
    private void Start()
    {
        if (_lights.Count <= 0 || _secBeforeStart <= 0)
        {
            throw new NullReferenceException();
        }
        
        _waitDelay = new WaitForSeconds(_secBeforeStart / _lights.Count);
        _hold = new WaitForSeconds(_holdTime);
    }

    public void StartCountDown()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        foreach (var light in _lights)
        {
            yield return _waitDelay;
            
            if (_lights.IndexOf(light) == (_lights.Count-2))
            {
                light.color = Color.yellow;
            }
            else if(_lights.IndexOf(light) == _lights.Count-1)
            {
                light.color = Color.green;
            }
            else
            {
                light.color = Color.red;
            }
        }

        yield return _hold;
        RaceStart?.Invoke();
        gameObject.SetActive(false);
        yield return null;
    }
}
