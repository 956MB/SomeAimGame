using UnityEngine;
using UnityEngine.EventSystems;

public class LanguageChangeButton : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData pointerEventData) {
        string buttonName = pointerEventData.pointerCurrentRaycast.gameObject.transform.name;
        SetNewGameLanguage(buttonName);
    }

    /// <summary>
    /// Sets new game language from supplied language string (lang).
    /// </summary>
    /// <param name="lang"></param>
    private static void SetNewGameLanguage(string lang) {
        string newLangCode = lang.Split('-')[0];

        if (newLangCode != LanguageSetting.activeLanguageCode) {
            LanguageSelect.SetLanguageCodeText(newLangCode);
            NotificationHandler.ShowTimedNotification_String($"{newLangCode}: {I18nTextTranslator.SetTranslatedText("languagerestartnotification")}", InterfaceColors.notificationColorGreen);
            LanguageSetting.SaveLanguageCodeItem(newLangCode);

            // EVENT:: for new game language set
            //DevEventHandler.CheckLanguageEvent($"{I18nTextTranslator.SetTranslatedText("eventlanguagegameset")} [{newLangCode}]");
        } else {
            NotificationHandler.ShowTimedNotification_String($"{newLangCode}: {I18nTextTranslator.SetTranslatedText("languageactive")}", InterfaceColors.notificationColorRed);
        }
    }
}
