using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHighlight_Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public TMP_Text highlightText, importExportText, resetCrosshairText;
    private static string currentHoveredCrosshairButton;

    private static ButtonHighlight_Hover buttonHighlight;
    void Awake() { buttonHighlight = this; }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        string buttonName = pointerEventData.pointerCurrentRaycast.gameObject.name;
        currentHoveredCrosshairButton = buttonName;

        if (buttonName == "ResetCrosshairButton") {
            if (!CrosshairImportExport.resetConfirmActive) { highlightText.color = InterfaceColors.selectedColor; }
        } else {
            if (!CrosshairImportExport.importExportPanelOpen) { highlightText.color = InterfaceColors.selectedColor; }
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        //string buttonName = pointerEventData.pointerCurrentRaycast.gameObject.name;
        if (currentHoveredCrosshairButton == "ResetCrosshairButton") {
            if (!CrosshairImportExport.resetConfirmActive) { highlightText.color = InterfaceColors.unselectedColor; }
        } else {
            if (!CrosshairImportExport.importExportPanelOpen) { highlightText.color = InterfaceColors.unselectedColor; }
        }
    }

    public static void ResetImportExportButton_TextColor() { buttonHighlight.importExportText.color = InterfaceColors.unselectedColor; }
}
