using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

using SomeAimGame.Utilities;

/// <summary>
/// Extension for TMP_InputField that clears text box.
/// </summary>
public static class Extension {
    public static void clear(this TMP_InputField inputfield) {
        inputfield.Select();
        inputfield.text = "";
    }
}

public class CrosshairImportExport : MonoBehaviour {
    public GameObject importExportPanel, parentCrosshairGroup;
    public Image resetConfirmBox;
    public TMP_InputField crosshairStringInputField;
    public TMP_Text notificationText, resetCrosshairText;

    public Material crosshairBoxDefault, crosshairboxResetConfirm;

    public static bool importExportPanelOpen               = false;
    public static bool resetConfirmActive                  = false;
    private static WaitForSeconds notificationDestroyDelay = new WaitForSeconds(3.5f);

    private static CrosshairImportExport crosshairImportExport;
    void Awake() { crosshairImportExport = this; }

    /// <summary>
    /// Toggles reset crosshair confirmation.
    /// </summary>
    public void TriggerResetConfirmation() {
        if (!resetConfirmActive) {
            SetResetConfirm();
        } else {
            CrosshairSaveSystem.InitCrosshairSettingsDefaults();
            NotificationHandler.ShowTimedNotification_String($"{I18nTextTranslator.SetTranslatedText("crosshairresetdefault")}", InterfaceColors.notificationColorGreen);
            SetResetDefault();
        }
    }

    /// <summary>
    /// Sets reset crosshair confirmation to active.
    /// </summary>
    public static void SetResetConfirm() {
        SetResetButtonValues(crosshairImportExport.crosshairboxResetConfirm, I18nTextTranslator.SetTranslatedText("resetcrosshairconfirm"), InterfaceColors.notificationColorRed, true);
    }

    /// <summary>
    /// Sets reset crosshair confirmation to inactive (default).
    /// </summary>
    public static void SetResetDefault() {
        SetResetButtonValues(crosshairImportExport.crosshairBoxDefault, I18nTextTranslator.SetTranslatedText("resetcrosshair"), InterfaceColors.unselectedColor, false);
    }

    /// <summary>
    /// Sets reset crosshair button values to supplied Material (boxMat), text string (resetText), text color (resetTextColor), and reset confirm active (resetConfirm).
    /// </summary>
    /// <param name="boxMat"></param>
    /// <param name="resetText"></param>
    /// <param name="resetTextColor"></param>
    /// <param name="resetConfirm"></param>
    public static void SetResetButtonValues(Material boxMat, string resetText, Color32 resetTextColor, bool resetConfirm) {
        crosshairImportExport.resetCrosshairText.SetText($"{resetText}");
        crosshairImportExport.resetConfirmBox.material = boxMat;
        crosshairImportExport.resetCrosshairText.color = resetTextColor;
        resetConfirmActive                             = resetConfirm;
    }

    /// <summary>
    /// Toggles opened/closed the import/export crosshair panel.
    /// </summary>
    public void TriggerImportExportPanelOpen() {
        if (!importExportPanelOpen) {
            OpenImportExportPanel_Static();
        } else {
            CloseImportExportPanel_Static();
        }

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Click(); }
    }

    public void CancelCloseImportExport() {
        CloseImportExportPanel_Static();
    }

    /// <summary>
    /// Opens import/export crosshair panel.
    /// </summary>
    public static void OpenImportExportPanel_Static() {
        if (!importExportPanelOpen) {
            crosshairImportExport.importExportPanel.SetActive(true);
            Util.RefreshRootLayoutGroup(crosshairImportExport.parentCrosshairGroup);
            Util.RefreshRootLayoutGroup(crosshairImportExport.importExportPanel);
            SubMenuHandler.ResetCrosshairScrollview();
            importExportPanelOpen = true;
        }
    }

    /// <summary>
    /// Closes import/export crosshair panel.
    /// </summary>
    public static void CloseImportExportPanel_Static() {
        crosshairImportExport.importExportPanel.SetActive(false);
        Util.RefreshRootLayoutGroup(crosshairImportExport.parentCrosshairGroup);
        crosshairImportExport.crosshairStringInputField.clear();
        ButtonHighlight_Hover.ResetImportExportButton_TextColor();
        importExportPanelOpen = false;
    }

    /// <summary>
    /// Imports given crosshair string from input (importString) if valid.
    /// </summary>
    public void ImportCrosshairString() {
        crosshairStringInputField.Select();
        string importString = crosshairStringInputField.text.ToString();

        if (SimpleCrosshair.ValidateSetCrosshairString(importString, true)) {
            SetCrosshairNotification_Delay(I18nTextTranslator.SetTranslatedText("crosshairsetsuccess"), InterfaceColors.notificationColorGreen, true);
            crosshairStringInputField.clear();
            CrosshairOptionsObject.SaveCrosshairObject(true);

            if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Click(); }
        } else {
            SetCrosshairNotification_Delay(I18nTextTranslator.SetTranslatedText("crosshairseterror"), InterfaceColors.notificationColorRed, true);
            
            if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Error(); }
        }
    }

    /// <summary>
    /// Exports (copies) current crosshair string to clipboard.
    /// </summary>
    public void ExportCrosshairString() {
        // Save crosshair string if any changes made before export.
        CrosshairOptionsObject.SaveCrosshairObject();
        //Debug.Log($"updated string?? {SimpleCrosshair.SetCrosshairString_Static()}");
        Util.CopyToClipboard(SimpleCrosshair.ReturnExportedCrosshairString());
        SetCrosshairNotification_Delay(I18nTextTranslator.SetTranslatedText("crosshairsetexported"), InterfaceColors.hoveredColor, true);

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Click(); }
    }

    /// <summary>
    /// Sets import/export crosshair notification text to 'error'.
    /// </summary>
    private void SetCrosshairNotification_Delay(string translationMessage, Color32 notificationColor, bool show) {
        notificationText.SetText($"{translationMessage}");
        notificationText.color = notificationColor;
        Util.RefreshRootLayoutGroup(importExportPanel);

        if (show) { crosshairImportExport.StartCoroutine(HideCrosshairNotification_Delay()); }
    }

    /// <summary>
    /// Hides current crosshair notification text after delay (3.5s).
    /// </summary>
    /// <returns></returns>
    public static IEnumerator HideCrosshairNotification_Delay() {
        yield return notificationDestroyDelay;
        crosshairImportExport.SetCrosshairNotification_Delay("", InterfaceColors.unselectedColor, false);
    }

    public static void CheckCloseCrosshairActions() {
        // Closes crosshair import/export panel if open.
        //if (importExportPanelOpen) { CloseImportExportPanel_Static(); }

        // Disables crosshair reset confirmation if active.
        if (resetConfirmActive) { SetResetDefault(); }
        // Closes crosshair presets panel if open.
        if (CrosshairPresets.crosshairPresetsPanelOpen) { CrosshairPresets.ClosePresetsPanel_Static(); }
    }
}

