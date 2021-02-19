using UnityEngine;
using UnityEngine.EventSystems;

namespace SomeAimGame.Core {
    public class SaveRestartTimer : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData pointerEventData) {
            TimerSelect.CloseTimerSelect_Static();
            //TimerSelect.DisableTimerRestartButton();
            LanguageSelect.CloseLanguageSelect_Static();
            //LanguageSelect.DisableLanguageRestartButton();
            ExtraSettings.CheckSaveExtraSettings();
            //LanguageSetting.CheckSaveLanguageSetting(); // maybe save language at same time?

            ButtonClickHandler.RestartCurrentGame_Static();
        }
    }
}
