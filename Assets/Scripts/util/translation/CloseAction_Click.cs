using UnityEngine;
using UnityEngine.EventSystems;

using SomeAimGame.SFX;

public class CloseAction_Click : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData pointerEventData) {
        LanguageSelect.CheckCloseLanguageSelect();

        TargetSoundSelect.CheckCloseTargetSoundDropdowns();

        QuitGame.CheckCloseQuitConfirmation();

        CrosshairImportExport.CheckCloseCrosshairActions();
    }
}
