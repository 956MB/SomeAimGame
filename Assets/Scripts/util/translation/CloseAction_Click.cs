using UnityEngine;
using UnityEngine.EventSystems;

using SomeAimGame.SFX;
using SomeAimGame.Core;
using SomeAimGame.Core.Video;

public class CloseAction_Click : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData pointerEventData) {
        LanguageSelect.CheckCloseLanguageSelect();

        TimerSelect.CheckCloseTimerSelect();

        TargetSoundSelect.CheckCloseTargetSoundDropdowns();

        VideoSettingSelect.CheckCloseVideoSettingsDropdowns();

        QuitGame.CheckCloseQuitConfirmation();

        CrosshairImportExport.CheckCloseCrosshairActions();
    }
}
