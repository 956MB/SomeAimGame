using UnityEngine;
using TMPro;

public class LanguageSelect : MonoBehaviour {
    public TMP_Text langText;
    public GameObject languageCodeSelectObject;
    public static bool languageSelectOpen     = false;
    public static bool languageSelectDisabled = false;

    public static LanguageSelect langSelect;
    private void Awake() { langSelect = this; }

    void Start() {
        CloseLanguageSelect_Static();
    }

    /// <summary>
    /// Sets language code text in settings UI.
    /// </summary>
    /// <param name="langCode"></param>
    public static void SetLanguageCodeText(string langCode) { langSelect.langText.SetText($"{langCode}"); }

    /// <summary>
    /// Non statiic method that toggles language select scrollview on button press.
    /// </summary>
    public void ToggleLanguageSelect() { ToggleLanguageSelect_Static(); }

    /// <summary>
    /// Toggles language select scrollview.
    /// </summary>
    public static void ToggleLanguageSelect_Static() {
        if (!languageSelectDisabled) {
            langSelect.languageCodeSelectObject.SetActive(!languageSelectOpen);
            languageSelectOpen = !languageSelectOpen;
        } else {
            NotificationHandler.ShowTimedNotification_String($"{I18nTextTranslator.SetTranslatedText("languagetemporarilydisabled")}", NotificationHandler.notificationColorYellow);
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
        langSelect.languageCodeSelectObject.SetActive(true);
        languageSelectOpen = true;

        // EVENT:: for language select panel opened
        //DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacelanguageselectopened")}");
    }

    /// <summary>
    /// Closes language select scrollview.
    /// </summary>
    public static void CloseLanguageSelect_Static() {
        langSelect.languageCodeSelectObject.SetActive(false);
        languageSelectOpen = false;

        // EVENT:: for language select panel closed
        //DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacelanguageselectclosed")}");
    }
}
