using UnityEngine;
using UnityEngine.EventSystems;

namespace SomeAimGame.Core {
    public class SaveRestartLanguage : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData pointerEventData) {
            TimerSelect.CloseTimerSelect_Static();
            //TimerSelect.DisableTimerRestartButton();
            LanguageSelect.CloseLanguageSelect_Static();
            //LanguageSelect.DisableLanguageRestartButton();
            LanguageSetting.CheckSaveLanguageSetting();
            //ExtraSettings.CheckSaveExtraSettings(); // maybe save timer at same time?

            ButtonClickHandler.RestartCurrentGame_Static();
        }
    }
}