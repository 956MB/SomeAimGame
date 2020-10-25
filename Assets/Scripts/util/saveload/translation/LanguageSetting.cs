﻿using UnityEngine;

public class LanguageSetting : MonoBehaviour {
    public static string languageCode = "ENG";

    private static LanguageSetting languageSetting;
    void Awake() { languageSetting = this; }

    public static void SaveLanguageCodeItem(string setLanguageCode) {
        languageCode = setLanguageCode;
        languageSetting.SaveLanguageSetting();
    }

    public void SaveLanguageSetting() { LanguageSaveSystem.SaveLanguageSettingData(this); }

    public static void SaveLanguageSettingDefault(string setLanguageCode) {
        languageCode = setLanguageCode;
        languageSetting.SaveLanguageSetting();
    }

    public static void LoadLanguageSetting(LanguageSettingDataSerial languageData) {
        languageCode = languageData.languageCode;
    }
}