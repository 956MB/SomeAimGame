using UnityEngine;
using UnityEngine.EventSystems;

using SomeAimGame.Utilities;
using SomeAimGame.SFX;
using System;

namespace SomeAimGame.Core {
    public class TimerChangeButton : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData pointerEventData) {
            string buttonName = pointerEventData.pointerCurrentRaycast.gameObject.transform.name;
            SetNewTimer(buttonName);
            LanguageSelect.CheckCloseLanguageSelect();
        }

        private void SetNewTimer(string timerName) {
            int newTimerNum = int.Parse(timerName.Split('-')[0]);

            if (newTimerNum != ExtraSettings.gameTimer) {
                ExtraSettings.SaveGameTimerItem(newTimerNum);
                TimerSelect.SetTimerDropdownText(newTimerNum);
                TimerSelect.CloseTimerSelect_Static();
                TimerSelect.EnableTimerRestartButton();
                //NotificationHandler.ShowTimedNotification_String($"{newLangCode}: {I18nTextTranslator.SetTranslatedText("languagerestartnotification")}", InterfaceColors.notificationColorGreen);
            } else {
                //NotificationHandler.ShowTimedNotification_String($"{newLangCode}: {I18nTextTranslator.SetTranslatedText("languageactive")}", InterfaceColors.notificationColorRed);
                SFXManager.CheckPlayError();
                TimerSelect.DisableTimerRestartButton();
            }
        }
    }
}
