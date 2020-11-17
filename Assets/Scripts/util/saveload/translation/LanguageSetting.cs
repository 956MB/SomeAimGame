using UnityEngine;

public class LanguageSetting : MonoBehaviour {
    public static string activeLanguageCode = "EN";

    private static LanguageSetting languageSetting;
    void Awake() { languageSetting = this; }

    public static void SaveLanguageCodeItem(string setLanguageCode) {
        activeLanguageCode = setLanguageCode;
        languageSetting.SaveLanguageSetting();
    }

    public void SaveLanguageSetting() { LanguageSaveSystem.SaveLanguageSettingData(this); }

    public static void SaveLanguageSettingDefault(string setLanguageCode) {
        activeLanguageCode = setLanguageCode;
        languageSetting.SaveLanguageSetting();
    }

    public static void LoadLanguageSetting(LanguageSettingDataSerial languageData) {
        activeLanguageCode = languageData.languageCode;
    }
}
