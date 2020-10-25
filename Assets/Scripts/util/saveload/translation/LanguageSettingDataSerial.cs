
[System.Serializable]
public class LanguageSettingDataSerial {
    public string languageCode;

    public LanguageSettingDataSerial() {
        languageCode = LanguageSetting.languageCode;
    }
}