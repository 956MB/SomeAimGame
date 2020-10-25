using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class LanguageSaveSystem : MonoBehaviour {
    private static LanguageSaveSystem languageSave;
    void Awake() { languageSave = this; }

    public static void SaveLanguageSettingData(LanguageSetting languageSetting) {
        BinaryFormatter formatter = new BinaryFormatter();
        string dirPath = Application.persistentDataPath + "/settings";
        string filePath = dirPath + "/language.setting";

        DirectoryInfo dirInf = new DirectoryInfo(dirPath);
        if (!dirInf.Exists) { dirInf.Create(); }

        FileStream stream = new FileStream(filePath, FileMode.Create);

        LanguageSettingDataSerial languageData = new LanguageSettingDataSerial();
        formatter.Serialize(stream, languageData);
        stream.Close();
    }

    public static LanguageSettingDataSerial LoadLanguageSettingData() {
        string path = Application.persistentDataPath + "/settings/language.setting";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

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
        } else {
            InitLanguageSettingDefault();
        }
    }

    public static void InitLanguageSettingDefault() {
        string lang = I18n.Get2LetterISOCodeFromSystemLanguage();

        I18n.LoadLanguage(lang);
        LanguageSetting.SaveLanguageSettingDefault(lang);
    }
}
