using UnityEngine;
using UnityEngine.EventSystems;

using SomeAimGame.SFX;
using SomeAimGame.Core.Video;

public class CloseAction_Click : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData pointerEventData) {
        LanguageSelect.CheckCloseLanguageSelect();

        TargetSoundSelect.CheckCloseTargetSoundDropdowns();

        VideoSettingSelect.CheckCloseVideoSettingsDropdowns();

        QuitGame.CheckCloseQuitConfirmation();

        CrosshairImportExport.CheckCloseCrosshairActions();
    }
}
