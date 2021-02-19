using UnityEngine;

using SomeAimGame.Utilities;

public class LanguageSaveSystem : MonoBehaviour {
    private static LanguageSaveSystem languageSave;
    void Awake() { languageSave = this; }

    /// <summary>
    /// Saves supplied language object (LanguageSetting) to file.
    /// </summary>
    public static void SaveLanguageSettingData() {
        LanguageSettingDataSerial languageData = new LanguageSettingDataSerial();
        SaveLoadUtil.SaveDataSerial("/prefs", "/sag_language.prefs", languageData);
    }

    /// <summary>
    /// Loads language setting data (LanguageSettingDataSerial) from file.
    /// </summary>
    /// <returns></returns>
    public static LanguageSettingDataSerial LoadLanguageSettingData() {
        LanguageSettingDataSerial languageData = (LanguageSettingDataSerial)SaveLoadUtil.LoadDataSerial("/prefs/sag_language.prefs", SaveType.LANGUAGE);
        return languageData;
    }

    /// <summary>
    /// Inits saved language object and sets language setting value.
    /// </summary>
    public static void InitSavedLanguageSetting() {
        LanguageSettingDataSerial loadedLanguageData = LoadLanguageSettingData();

        if (loadedLanguageData != null) {
            LanguageSetting.LoadLanguageSetting(loadedLanguageData);
            I18n.LoadLanguage(loadedLanguageData.languageCode);
        } else {
            InitLanguageSettingDefault();
        }
    }

    /// <summary>
    /// Inits default language value (EN) and saves to file on first launch.
    /// </summary>
    public static void InitLanguageSettingDefault() {
        string lang = I18n.Get2LetterISOCodeFromSystemLanguage();

        I18n.LoadLanguage(lang);
        LanguageSetting.SaveLanguageSettingDefault(lang);
    }
}
