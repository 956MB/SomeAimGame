using UnityEngine;

using SomeAimGame.Utilities;

public class LanguageSaveSystem : MonoBehaviour {
    private static LanguageSaveSystem languageSave;
    void Awake() { languageSave = this; }

    public static void SaveLanguageSettingData() {
        LanguageSettingDataSerial languageData = new LanguageSettingDataSerial();
        SaveLoadUtil.SaveDataSerial("/prefs", "/sag_language.prefs", languageData);
    }

    public static LanguageSettingDataSerial LoadLanguageSettingData() {
        LanguageSettingDataSerial languageData = (LanguageSettingDataSerial)SaveLoadUtil.LoadDataSerial("/prefs/sag_language.prefs", SaveType.LANGUAGE);
        return languageData;
    }

    public static void InitSavedLanguageSetting() {
        LanguageSettingDataSerial loadedLanguageData = LoadLanguageSettingData();
        if (loadedLanguageData != null) {
            LanguageSetting.LoadLanguageSetting(loadedLanguageData);
            I18n.LoadLanguage(loadedLanguageData.languageCode);

            // DevEventHandler moved into editor folder.
            //if (DevEventHandler.cardsOn) {
            //    I18n.CalculateLongestKey();
            //    DevEventHandler.PopulateExtraSpaces();
            //}

            // EVENT:: for saved language file loaded
            //DevEventHandler.CheckLanguageEvent($"[{loadedLanguageData.languageCode}] {I18nTextTranslator.SetTranslatedText("eventlanguagefileload")}");
            // EVENT:: for game language set
            //DevEventHandler.CheckLanguageEvent($"{I18nTextTranslator.SetTranslatedText("eventlanguagegameset")} [{loadedLanguageData.languageCode}]");
        }
        else {
            InitLanguageSettingDefault();

            // EVENT:: for default language file loaded
            //DevEventHandler.CheckLanguageEvent($"[ENG] {I18nTextTranslator.SetTranslatedText("eventlanguagefileloaddefault")}");
            // EVENT:: for game language set
            //DevEventHandler.CheckLanguageEvent($"{I18nTextTranslator.SetTranslatedText("eventlanguagegameset")} [ENG]");
        }
    }

    public static void InitLanguageSettingDefault() {
        string lang = I18n.Get2LetterISOCodeFromSystemLanguage();
        I18n.LoadLanguage(lang);
        
        //if (DevEventHandler.cardsOn) {
        //    I18n.CalculateLongestKey();
        //    DevEventHandler.PopulateExtraSpaces();
        //}
        
        LanguageSetting.SaveLanguageSettingDefault(lang);
    }
}
