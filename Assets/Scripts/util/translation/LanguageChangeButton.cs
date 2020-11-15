using UnityEngine;
using UnityEngine.EventSystems;

public class LanguageChangeButton : MonoBehaviour, IPointerClickHandler {
    
    public void OnPointerClick(PointerEventData pointerEventData) {
        string buttonName = pointerEventData.pointerCurrentRaycast.gameObject.transform.name;
        SetNewGameLanguage(buttonName);
    }

    private static void SetNewGameLanguage(string lang) {
        string newLangCode = lang.Split('-')[0];

        if (!LanguageSelect.languageSelectDisabled) {
            if (newLangCode != LanguageSetting.activeLanguageCode) {
                LanguageSelect.SetLanguageCodeText(newLangCode);
                NotificationHandler.ShowTimedNotification_String($"{newLangCode}: {I18nTextTranslator.SetTranslatedText("languagerestartnotification")}", NotificationHandler.notificationColorGreen);
                LanguageSetting.SaveLanguageCodeItem(newLangCode);

                // EVENT:: for new game language set
                DevEventHandler.CheckLanguageEvent($"{I18nTextTranslator.SetTranslatedText("eventlanguagegameset")} [{newLangCode}]");
            } else {
                NotificationHandler.ShowTimedNotification_String($"{newLangCode}: {I18nTextTranslator.SetTranslatedText("languageactive")}", NotificationHandler.notificationColorRed);
            }
        } else {
            NotificationHandler.ShowTimedNotification_String($"{newLangCode}: {I18nTextTranslator.SetTranslatedText("languagetemporarilydisabled")}", NotificationHandler.notificationColorYellow);

            // Close language select after notification if langauge select disabled
            LanguageSelect.CloseLanguageSelect_Static();
        }
    }
}
