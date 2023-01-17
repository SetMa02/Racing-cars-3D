using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LanguageCombobox : MonoBehaviour
{
   private TMP_Dropdown _languageDropDown;
   private string _languagePref = "language";
   private string _textTipe = "text";

   private void Awake()
   {
       _languageDropDown = GetComponent<TMP_Dropdown>();

       if (PlayerPrefs.HasKey(_languagePref))
       {
           Debug.Log(PlayerPrefs.GetString(_languagePref));

           foreach (var option in _languageDropDown.options)
           {
               if (option.text == PlayerPrefs.GetString(_languagePref))
               {
                   _languageDropDown.value = _languageDropDown.options.IndexOf(option);
               }
           }
       }
       else
       {
           PlayerPrefs.SetString(_languagePref, _languageDropDown.options[_languageDropDown.value].text.ToLower());
       }
   }

   public void OnLanguageChanged()
   {
       PlayerPrefs.SetString(_languagePref, _languageDropDown.options[_languageDropDown.value].text.ToLower());
       ChangeAllLanguage();
   }

   private void ChangeAllLanguage()
   {
       LanguageText[] texts = GameObject.FindObjectsOfType<LanguageText>();

       foreach (var text in texts)
       {
           text.ChangeLanguage();
       }
   }
}
