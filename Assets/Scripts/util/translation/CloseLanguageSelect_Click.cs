using UnityEngine;
using UnityEngine.EventSystems;

public class CloseLanguageSelect_Click : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData pointerEventData) {
        // Closes language select if selection panel open.
        if (LanguageSelect.languageSelectOpen) { LanguageSelect.CloseLanguageSelect_Static(); }

        // Closes quit game section if confirmation not open.
        if (!QuitGame.gameQuitConfirmationOpen && QuitGame.gameQuitButtonOpen) { QuitGame.CloseQuitConfirmation(); }

        // Closes crosshair import/export panel if open.
        if (CrosshairImportExport.importExportPanelOpen) { CrosshairImportExport.CloseImportExportPanel_Static(); }
        // Disables crosshair reset confirmation if active.
        if (CrosshairImportExport.resetConfirmActive) { CrosshairImportExport.SetResetDefault(); }
    }
}
