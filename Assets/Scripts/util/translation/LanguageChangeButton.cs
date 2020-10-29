using UnityEngine;
using UnityEngine.EventSystems;

public class LanguageChangeButton : MonoBehaviour, IPointerClickHandler {
    
    public void OnPointerClick(PointerEventData pointerEventData) {
        string buttonName = pointerEventData.pointerCurrentRaycast.gameObject.transform.name;
        SetNewGameLanguage(buttonName);
    }

    private static void SetNewGameLanguage(string lang) {
        string langCode = lang.Split('-')[0];

        LanguageSelect.SetLanguageCodeText(langCode);
        NotificationHandler.ShowNotification_String($"{langCode}: {I18nTextTranslator.SetTranslatedText("languagerestartnotification")}", Color.green);
        LanguageSetting.SaveLanguageCodeItem(langCode);

        // EVENT:: for new game language set
        DevEventHandler.CheckLanguageEvent($"{I18nTextTranslator.SetTranslatedText("eventlanguagegameset")} [{langCode}]");
    }
}
