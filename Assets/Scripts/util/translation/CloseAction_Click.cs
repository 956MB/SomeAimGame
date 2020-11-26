using UnityEngine;
using UnityEngine.EventSystems;

public class CloseAction_Click : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData pointerEventData) {
        LanguageSelect.CheckCloseLanguageSelect();

        QuitGame.CheckCloseQuitConfirmation();

        CrosshairImportExport.CheckCloseCrosshairActions();
    }
}
