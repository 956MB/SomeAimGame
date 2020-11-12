using UnityEngine;
using UnityEngine.EventSystems;

public class LanguageChangeButton : MonoBehaviour, IPointerClickHandler {
    
    public void OnPointerClick(PointerEventData pointerEventData) {
        string buttonName = pointerEventData.pointerCurrentRaycast.gameObject.transform.name;
        SetNewGameLanguage(buttonName);
    }

    private static void SetNewGameLanguage(string lang) {
        string langCode = lang.Split('-')[0];

        if (!LanguageSelect.languageSelectDisabled) {
            LanguageSelect.SetLanguageCodeText(langCode);
            NotificationHandler.ShowTimedNotification_String($"{langCode}: {I18nTextTranslator.SetTranslatedText("languagerestartnotification")}", NotificationHandler.notificationColorGreen);
            LanguageSetting.SaveLanguageCodeItem(langCode);

            // EVENT:: for new game language set
            DevEventHandler.CheckLanguageEvent($"{I18nTextTranslator.SetTranslatedText("eventlanguagegameset")} [{langCode}]");
        } else {
            NotificationHandler.ShowTimedNotification_String($"{langCode}: {I18nTextTranslator.SetTranslatedText("languagetemporarilydisabled")}", NotificationHandler.notificationColorYellow);

            // Close language select after notification if langauge select disabled
            LanguageSelect.CloseLanguageSelect_Static();
        }
    }
}
