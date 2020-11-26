using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
        crosshairImportExport.resetConfirmBox.material = crosshairImportExport.crosshairboxResetConfirm;
        crosshairImportExport.resetCrosshairText.SetText($"{I18nTextTranslator.SetTranslatedText("resetcrosshairconfirm")}");
        crosshairImportExport.resetCrosshairText.color = InterfaceColors.notificationColorRed;
        resetConfirmActive = true;
    }

    /// <summary>
    /// Sets reset crosshair confirmation to inactive (default).
    /// </summary>
    public static void SetResetDefault() {
        crosshairImportExport.resetConfirmBox.material = crosshairImportExport.crosshairBoxDefault;
        crosshairImportExport.resetCrosshairText.SetText($"{I18nTextTranslator.SetTranslatedText("resetcrosshair")}");
        crosshairImportExport.resetCrosshairText.color = InterfaceColors.unselectedColor;
        resetConfirmActive = false;
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

    /// <summary>
    /// Opens import/export crosshair panel.
    /// </summary>
    public static void OpenImportExportPanel_Static() {
        if (!importExportPanelOpen) {
            crosshairImportExport.importExportPanel.SetActive(true);
            //Util.RefreshRootLayoutGroup(crosshairImportExport.importExportPanel);
            Util.RefreshRootLayoutGroup(crosshairImportExport.parentCrosshairGroup);
            Util.RefreshRootLayoutGroup(crosshairImportExport.importExportPanel);
            importExportPanelOpen = true;
        }
    }

    /// <summary>
    /// Closes import/export crosshair panel.
    /// </summary>
    public static void CloseImportExportPanel_Static() {
        crosshairImportExport.importExportPanel.SetActive(false);
        //Util.RefreshRootLayoutGroup(crosshairImportExport.importExportPanel);
        Util.RefreshRootLayoutGroup(crosshairImportExport.parentCrosshairGroup);
        //Util.RefreshRootLayoutGroup(crosshairImportExport.importExportPanel);
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
            SetCrosshairNotification_Delay(I18nTextTranslator.SetTranslatedText("crosshairsetsuccess"), InterfaceColors.notificationColorGreen);
            crosshairStringInputField.clear();

            if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Click(); }
        } else {
            SetCrosshairNotification_Delay(I18nTextTranslator.SetTranslatedText("crosshairseterror"), InterfaceColors.notificationColorRed);
            
            if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Error(); }
        }
    }

    /// <summary>
    /// Exports (copies) current crosshair string to clipboard.
    /// </summary>
    public void ExportCrosshairString() {
        Util.CopyToClipboard(SimpleCrosshair.ReturnExportedCrosshairString());
        SetCrosshairNotification_Delay(I18nTextTranslator.SetTranslatedText("crosshairsetexported"), InterfaceColors.hoveredColor);

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Click(); }
    }

    /// <summary>
    /// Sets import/export crosshair notification text to 'error'.
    /// </summary>
    private void SetCrosshairNotification_Delay(string translationMessage, Color32 notificationColor) {
        notificationText.SetText($"{translationMessage}");
        notificationText.color = notificationColor;
        Util.RefreshRootLayoutGroup(importExportPanel);
        crosshairImportExport.StartCoroutine(HideCrosshairNotification_Delay());
    }

    /// <summary>
    /// Sets active crosshair notification text to "", and resets color (hides).
    /// </summary>
    private void HideCrosshairNotification() {
        notificationText.SetText("");
        notificationText.color = InterfaceColors.unselectedColor;
        Util.RefreshRootLayoutGroup(importExportPanel);
    }

    /// <summary>
    /// Hides current crosshair notification text after delay (3.5s).
    /// </summary>
    /// <returns></returns>
    public static IEnumerator HideCrosshairNotification_Delay() {
        yield return notificationDestroyDelay;
        crosshairImportExport.HideCrosshairNotification();
    }
}

