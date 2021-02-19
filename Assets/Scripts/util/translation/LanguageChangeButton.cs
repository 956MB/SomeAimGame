using UnityEngine;
using UnityEngine.EventSystems;

using SomeAimGame.Utilities;
using SomeAimGame.SFX;

namespace SomeAimGame.Core {
    public class LanguageChangeButton : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData pointerEventData) {
            string buttonName = pointerEventData.pointerCurrentRaycast.gameObject.transform.name;
            SetNewGameLanguage(buttonName);
            LanguageSelect.CheckCloseLanguageSelect();
        }

        /// <summary>
        /// Sets new game language from supplied language string (lang).
        /// </summary>
        /// <param name="lang"></param>
        private static void SetNewGameLanguage(string lang) {
            string newLangCode = lang.Split('-')[0];

            if (newLangCode != LanguageSetting.activeLanguageCode) {
                LanguageSelect.SetLanguageCodeText(newLangCode);
                LanguageSelect.CloseLanguageSelect_Static();
                //NotificationHandler.ShowTimedNotification_String($"{newLangCode}: {I18nTextTranslator.SetTranslatedText("languagerestartnotification")}", InterfaceColors.notificationColorGreen);
                LanguageSetting.SaveLanguageCodeItem(newLangCode);
                LanguageSelect.EnableLanguageRestartButton();
            } else {
                //NotificationHandler.ShowTimedNotification_String($"{newLangCode}: {I18nTextTranslator.SetTranslatedText("languageactive")}", InterfaceColors.notificationColorRed);
                SFXManager.CheckPlayError();
                LanguageSelect.DisableLanguageRestartButton();
            }
        }
    }
}
