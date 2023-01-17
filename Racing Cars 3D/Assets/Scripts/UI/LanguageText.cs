using System;
using System.Collections;
using System.Collections.Generic;
using Platforms;
using TMPro;
using UnityEngine;

public class LanguageText : MonoBehaviour
{
    [SerializeField] private List<string> _languageVariants;
    private TMP_Text _text;
    private Dictionary<string, int> _languageKey;
    private TMP_Text _tmpText;
    private string _languagePref = "language";
    private string _textTag = "text";

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        
        if (_languageVariants.Count == 0)
        {
            throw new NullReferenceException();
        }

        gameObject.tag = _textTag;
        
        _languageKey = new Dictionary<string, int>
        {
            {"english", 0},
            {"русский", 1},
            {"turk", 2}
        };

    }

    private void Start()
    {
        ChangeLanguage();
    }

    public void ChangeLanguage()
    {
        _text.text = _languageVariants[_languageKey[PlayerPrefs.GetString(_languagePref)]];
    }
}
