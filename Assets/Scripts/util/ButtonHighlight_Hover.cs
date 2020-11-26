﻿using UnityEngine;
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

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_HoverInner(); }
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        if (currentHoveredCrosshairButton == "ResetCrosshairButton") {
            if (!CrosshairImportExport.resetConfirmActive) { highlightText.color = InterfaceColors.unselectedColor; }
        } else {
            if (!CrosshairImportExport.importExportPanelOpen) { highlightText.color = InterfaceColors.unselectedColor; }
        }
    }

    /// <summary>
    /// Sets import/export button text color back to normal (unselectedColor).
    /// </summary>
    public static void ResetImportExportButton_TextColor() { buttonHighlight.importExportText.color = InterfaceColors.unselectedColor; }
}