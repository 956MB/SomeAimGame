﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LanguageSaveSystem : MonoBehaviour {
    private static LanguageSaveSystem languageSave;
    void Awake() { languageSave = this; }

    public static void SaveLanguageSettingData(LanguageSetting languageSetting) {
        BinaryFormatter formatter = new BinaryFormatter();
        string dirPath            = Application.persistentDataPath + "/settings";
        string filePath           = dirPath + "/language.setting";

        DirectoryInfo dirInf = new DirectoryInfo(dirPath);
        if (!dirInf.Exists) { dirInf.Create(); }

        FileStream stream                      = new FileStream(filePath, FileMode.Create);
        LanguageSettingDataSerial languageData = new LanguageSettingDataSerial();
        formatter.Serialize(stream, languageData);
        stream.Close();
    }

    public static LanguageSettingDataSerial LoadLanguageSettingData() {
        string path = Application.persistentDataPath + "/settings/language.setting";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream         = new FileStream(path, FileMode.Open);

            LanguageSettingDataSerial languageData = formatter.Deserialize(stream) as LanguageSettingDataSerial;
            stream.Close();

            return languageData;
        } else {
            return null;
        }
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
