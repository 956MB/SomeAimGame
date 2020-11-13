using UnityEngine;
using UnityEngine.EventSystems;

public class CloseLanguageSelect_Click : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData pointerEventData) {
        if (LanguageSelect.languageSelectOpen) { LanguageSelect.CloseLanguageSelect_Static(); }
        //if (QuitGame.gameQuitConfirmationOpen) { QuitGame.CloseQuitConfirmation(); }
    }
}
