using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class ProfileData : MonoBehaviour
{
    private Button _button;
    private bool _authorized = false;

    private void Start()
    {
        _button = GetComponent<Button>();
    }

    private void Authorize()
    {
        PlayerAccount.Authorize();
    }
    
    private void OnRequestPersonalProfileDataPermissionButtonClick()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
    }
    
    private void OnGetProfileDataButtonClick()
    {
        PlayerAccount.GetProfileData((result) =>
        {
            string name = result.publicName;
            if (string.IsNullOrEmpty(name))
                name = "Anonymous";
            Debug.Log($"My id = {result.uniqueID}, name = {name}");
        });
    }
}
