using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{
    private bool active = false;
    private const string LOCALE_SAFE_STRING = "LocaleKey";

    private void Start()
    {
        int ID = PlayerPrefs.GetInt(LOCALE_SAFE_STRING, 0);
        ChangeLocale(ID);
    }

    public void ChangeLocale(int localeID)
    {
        if(active) return;
        StartCoroutine(SetLocale(localeID));
    }

    private IEnumerator SetLocale(int localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt(LOCALE_SAFE_STRING,localeID);
        active = false;
    }
}
