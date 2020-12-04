/*
 * Internationalization 
 * 
 * Author: Daniel Erdmann
 * 
 * 1. Add this File to you Project
 * 
 * 2. Add the language files to the folder Assets/Resources/I18n. (Filesnames: en.txt, es.txt, pt.txt, de.txt, and so on)
 *    Format: en.txt:           es.txt:
 *           =============== =================
 *           |hello=Hello  | |hello=Hola     |
 *           |world=World  | |world=Mundo    |
 *           |...          | |...            |
 *           =============== =================
 *           
 * 3. Use it! 
 *    Debug.Log(I18n.Fields["hello"] + " " + I18n.Fields["world"]); //"Hello World" or "Hola Mundo"
 * 
 * Use \n for new lines. Fallback language is "en"
 */

using System;
using System.Collections.Generic;
using UnityEngine;

using SomeAimGame.Gamemode;

public class I18n : MonoBehaviour {
    /// <summary>
    /// Text Fields
    /// Useage: Fields[key]
    /// Example: I18n.Fields["world"]
    /// </summary>
    public static Dictionary<String, String> Fields { get; private set; }
    public static string lang;
    private static bool randomLang   = true;
    private static string[] langList = new string[] { "EN", "FI", "JA", "KO", "RU" };

    public static I18n i18n;
    private void Awake() {
        i18n = this;
        //DevEventHandler.ClearDevEventLayoutGroup();
        LanguageSaveSystem.InitSavedLanguageSetting();
    }

    /// <summary>
    /// Loads language files from ressources and sets keys/values in Fields dict.
    /// </summary>
    public static void LoadLanguage(string langCode) {
        if (Fields == null) { Fields = new Dictionary<string, string>(); }
        Fields.Clear();

        lang = langCode;
        string allTexts, key, value;

        // Language testing
        //lang = "KO"; // "JA" "AR" "ZH" "KO" "RU" "EN" "FI"
        //if (randomLang) { lang = langList[UnityEngine.Random.Range(0, langList.Length)]; }

        var textAsset = Resources.Load(@"I18n/" + lang.ToLower()); //no .txt needed

        if (textAsset == null) { textAsset = Resources.Load(@"I18n/en") as TextAsset; } //no .txt needed
        if (textAsset == null) { Debug.LogError("File not found for I18n: Assets/Resources/I18n/" + lang + ".txt"); }

        allTexts = (textAsset as TextAsset).text;
        string[] lines = allTexts.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

        for (int i = 0; i < lines.Length; i++) {
            if (lines[i].IndexOf("=") >= 0 && !lines[i].StartsWith("#")) {
                key   = lines[i].Substring(0, lines[i].IndexOf("="));
                value = lines[i].Substring(lines[i].IndexOf("=") + 1, lines[i].Length - lines[i].IndexOf("=") - 1).Replace("\\n", Environment.NewLine);
                Fields.Add(key, value);
            }
        }
    }

    /// <summary>
    /// Gets the current language code.
    /// </summary>
    /// <returns></returns>
    public static string GetLanguage() { return Get2LetterISOCodeFromSystemLanguage().ToLower(); }

    public static void CalculateLongestKey() {
        string[] eventCardKeys = new string[] { Fields["cardtypegamemode"], Fields["cardtypetime"], Fields["cardtypecrosshair"], Fields["cardtypetargets"], Fields["cardtypeinterface"], Fields["cardtypesave"], Fields["cardtypeskybox"], Fields["cardtypelanguage"], Fields["cardtypekeybind"], Fields["cardtypesound"], Fields["cardtypenotification"] };
        string res = "";

        for (int i = 0; i < eventCardKeys.Length; i++) {
            string currentKey = eventCardKeys[i];
            if (currentKey.Length > res.Length) { res = currentKey; }
        }

        //DevEventHandler.longestCardTypeText = res;
    }

    /// <summary>
    /// Helps to convert Unity's Application.systemLanguage to a 
    /// 2 letter ISO country code. There is unfortunately not more
    /// countries available as Unity's enum does not enclose all
    /// countries.
    /// </summary>
    /// <returns>The 2-letter ISO code from system language.</returns>
    public static string Get2LetterISOCodeFromSystemLanguage() {
        SystemLanguage lang = Application.systemLanguage;
        string langCode = "EN"; // "ENG";

        switch (lang) {
            //case SystemLanguage.Afrikaans:          langCode = "AF"; break; // "AF";
            //case SystemLanguage.Arabic:             langCode = "AR"; break; // "AR";
            //case SystemLanguage.Basque:             langCode = "EU"; break; // "EU";
            //case SystemLanguage.Belarusian:         langCode = "BY"; break; // "BY";
            //case SystemLanguage.Bulgarian:          langCode = "BG"; break; // "BG";
            //case SystemLanguage.Catalan:            langCode = "CA"; break; // "CA";
            case SystemLanguage.Chinese:              langCode = "ZH"; break; // "CHI";
            case SystemLanguage.ChineseSimplified:    langCode = "ZH"; break; // "CHI";
            case SystemLanguage.ChineseTraditional:   langCode = "ZH"; break; // "CHI";
            //case SystemLanguage.Czech:              langCode = "CS"; break; // "CS";
            //case SystemLanguage.Danish:             langCode = "DA"; break; // "DA";
            //case SystemLanguage.Dutch:              langCode = "NL"; break; // "NL";
            case SystemLanguage.English:              langCode = "EN"; break; // "ENG";
            //case SystemLanguage.Estonian:           langCode = "ET"; break; // "ET";
            //case SystemLanguage.Faroese:            langCode = "FO"; break; // "FO";
            case SystemLanguage.Finnish:              langCode = "FI"; break; // "FIN";
            //case SystemLanguage.French:             langCode = "FR"; break; // "FR";
            //case SystemLanguage.German:             langCode = "DE"; break; // "DE";
            //case SystemLanguage.Greek:              langCode = "EL"; break; // "EL";
            //case SystemLanguage.Hebrew:             langCode = "IW"; break; // "IW";
            //case SystemLanguage.Hungarian:          langCode = "HU"; break; // "HU";
            //case SystemLanguage.Icelandic:          langCode = "IS"; break; // "IS";
            //case SystemLanguage.Indonesian:         langCode = "IN"; break; // "IN";
            //case SystemLanguage.Italian:            langCode = "IT"; break; // "IT";
            case SystemLanguage.Japanese:             langCode = "JA"; break; // "JPN";
            case SystemLanguage.Korean:               langCode = "KO"; break; // "KOR";
            //case SystemLanguage.Latvian:            langCode = "LV"; break; // "LV";
            //case SystemLanguage.Lithuanian:         langCode = "LT"; break; // "LT";
            //case SystemLanguage.Norwegian:          langCode = "NO"; break; // "NO";
            //case SystemLanguage.Polish:             langCode = "PL"; break; // "PL";
            //case SystemLanguage.Portuguese:         langCode = "PT"; break; // "PT";
            //case SystemLanguage.Romanian:           langCode = "RO"; break; // "RO";
            case SystemLanguage.Russian:              langCode = "RU"; break; // "RUS";
            //case SystemLanguage.SerboCroatian:      langCode = "SH"; break; // "SH";
            //case SystemLanguage.Slovak:             langCode = "SK"; break; // "SK";
            //case SystemLanguage.Slovenian:          langCode = "SL"; break; // "SL";
            //case SystemLanguage.Spanish:            langCode = "ES"; break; // "ES";
            //case SystemLanguage.Swedish:            langCode = "SV"; break; // "SV";
            //case SystemLanguage.Thai:               langCode = "TH"; break; // "TH";
            //case SystemLanguage.Turkish:            langCode = "TR"; break; // "TR";
            //case SystemLanguage.Ukrainian:          langCode = "UK"; break; // "UK";
            case SystemLanguage.Unknown:              langCode = "EN"; break; // "ENG";
            //case SystemLanguage.Vietnamese:         langCode = "VI"; break; // "VI";
        }

        return langCode;
    }
}