using UnityEngine;

public class LanguageSetting : MonoBehaviour {
    public static string activeLanguageCode = "EN";

    private static LanguageSetting languageSetting;
    void Awake() { languageSetting = this; }

    /// <summary>
    /// Saves supplied language code string (setLanguageCode) to language settings object (LanguageSetting).
    /// </summary>
    /// <param name="setLanguageCode"></param>
    public static void SaveLanguageCodeItem(string setLanguageCode) {
        activeLanguageCode = setLanguageCode;
        languageSetting.SaveLanguageSetting();
    }

    /// <summary>
    /// Calls 'LanguageSaveSystem.SaveLanguageSettingData()' to save language setting object (LanguageSetting) to file.
    /// </summary>
    public void SaveLanguageSetting() { LanguageSaveSystem.SaveLanguageSettingData(this); }

    /// <summary>
    /// Saves default language setting in object (LanguageSetting).
    /// </summary>
    /// <param name="setLanguageCode"></param>
    public static void SaveLanguageSettingDefault(string setLanguageCode) {
        activeLanguageCode = setLanguageCode;
        languageSetting.SaveLanguageSetting();
    }

    /// <summary>
    /// Loads language setting data (LanguageSettingDataSerial) and sets value to this language setting object (LanguageSetting).
    /// </summary>
    /// <param name="languageData"></param>
    public static void LoadLanguageSetting(LanguageSettingDataSerial languageData) {
        activeLanguageCode = languageData.languageCode;
    }
}
