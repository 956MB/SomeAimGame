using UnityEngine;
using TMPro;

using SomeAimGame.Utilities;
using SomeAimGame.SFX;

public class LanguageSelect : MonoBehaviour {
    public TMP_Text langText, arrowText;
    public GameObject languageCodeSelectObject;
    public static bool languageSelectOpen     = false;
    public static bool languageSelectDisabled = false;

    public static LanguageSelect langSelect;
    private void Awake() { langSelect = this; }

    void Start() {
        SetLanguageCodeText(LanguageSetting.activeLanguageCode);
        CloseLanguageSelect_Static();
    }

    /// <summary>
    /// Sets language code text in settings UI.
    /// </summary>
    /// <param name="langCode"></param>
    public static void SetLanguageCodeText(string langCode) {
        langSelect.langText.SetText($"{ReturnLanguageTextFull(langCode)}");
    }

    /// <summary>
    /// Non statiic method that toggles language select scrollview on button press.
    /// </summary>
    public void ToggleLanguageSelect() { ToggleLanguageSelect_Static(); }

    /// <summary>
    /// Toggles language select scrollview.
    /// </summary>
    public static void ToggleLanguageSelect_Static() {
        TargetSoundSelect.CheckCloseTargetSoundDropdowns();

        if (!languageSelectOpen) {
            OpenLanguageSelect_Static();
            SFXManager.CheckPlayClick_Button();
        } else {
            CloseLanguageSelect_Static();
        }

        //if (languageSelectOpen) {
        //    // EVENT:: for language select panel opened
        //    DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacelanguageselectopened")}");
        //} else {
        //    // EVENT:: for language select panel closed
        //    DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacelanguageselectclosed")}");
        //}
    }

    /// <summary>
    /// Opens language select scrollview.
    /// </summary>
    public static void OpenLanguageSelect_Static() {
        //langSelect.languageCodeSelectObject.SetActive(true);
        langSelect.languageCodeSelectObject.transform.localScale = DropdownUtils.dropdownBodyOpenScale;
        Util.RefreshRootLayoutGroup(langSelect.languageCodeSelectObject);
        langSelect.arrowText.transform.localScale = DropdownUtils.arrowupScale;
        languageSelectOpen = true;

        // EVENT:: for language select panel opened
        //DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacelanguageselectopened")}");
    }

    /// <summary>
    /// Closes language select scrollview.
    /// </summary>
    public static void CloseLanguageSelect_Static() {
        //langSelect.languageCodeSelectObject.SetActive(false);
        langSelect.languageCodeSelectObject.transform.localScale = DropdownUtils.dropdownBodyClosedScale;
        langSelect.arrowText.transform.localScale = DropdownUtils.arrowDownScale;
        languageSelectOpen = false;

        // EVENT:: for language select panel closed
        //DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacelanguageselectclosed")}");
    }

    public static void CheckCloseLanguageSelect() {
        // Closes language select if selection panel open.
        if (languageSelectOpen) { CloseLanguageSelect_Static(); }
    }

    public static string ReturnLanguageTextFull(string langCode) {
        switch (langCode) {
            case "EN": return "English (United States)";
            case "RU": return "Pусский (Россия)";
            case "FI": return "Suomalainen (Suomi)";
            case "KO": return "한국어 (대한민국)";
            case "JA": return "日本人 (日本)";
            default:   return "English (United States)";
        }
    }
}
