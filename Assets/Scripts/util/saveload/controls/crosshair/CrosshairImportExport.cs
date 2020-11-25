using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

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

    public void TriggerResetConfirmation() {
        if (!resetConfirmActive) {
            SetResetConfirm();
        } else {
            //CrosshairSaveSystem.InitCrosshairSettingsDefaults();
            SetResetDefault();
        }
    }

    public static void SetResetConfirm() {
        crosshairImportExport.resetConfirmBox.material = crosshairImportExport.crosshairboxResetConfirm;
        crosshairImportExport.resetCrosshairText.SetText($"{I18nTextTranslator.SetTranslatedText("resetcrosshairconfirm")}");
        crosshairImportExport.resetCrosshairText.color = InterfaceColors.notificationColorRed;
        resetConfirmActive = true;
    }

    public static void SetResetDefault() {
        crosshairImportExport.resetConfirmBox.material = crosshairImportExport.crosshairBoxDefault;
        crosshairImportExport.resetCrosshairText.SetText($"{I18nTextTranslator.SetTranslatedText("resetcrosshair")}");
        crosshairImportExport.resetCrosshairText.color = InterfaceColors.unselectedColor;
        resetConfirmActive = false;
    }

    public void TriggerImportExportPanelOpen() {
        if (!importExportPanelOpen) {
            OpenImportExportPanel_Static();
        } else {
            CloseImportExportPanel_Static();
        }
    }

    public static void OpenImportExportPanel_Static() {
        if (!importExportPanelOpen) {
            //crosshairImportExport.importExportPanel.transform.localScale = new Vector3(1f, 1f, 1f);
            crosshairImportExport.importExportPanel.SetActive(true);
            //Util.RefreshRootLayoutGroup(crosshairImportExport.importExportPanel);
            Util.RefreshRootLayoutGroup(crosshairImportExport.parentCrosshairGroup);
            Util.RefreshRootLayoutGroup(crosshairImportExport.importExportPanel);
            importExportPanelOpen = true;
        }
    }

    public static void CloseImportExportPanel_Static() {
        //crosshairImportExport.importExportPanel.transform.localScale = new Vector3(1f, 0f, 1f);
        crosshairImportExport.importExportPanel.SetActive(false);
        //Util.RefreshRootLayoutGroup(crosshairImportExport.importExportPanel);
        Util.RefreshRootLayoutGroup(crosshairImportExport.parentCrosshairGroup);
        //Util.RefreshRootLayoutGroup(crosshairImportExport.importExportPanel);
        ButtonHighlight_Hover.ResetImportExportButton_TextColor();
        importExportPanelOpen = false;
    }

    public void ImportCrosshairString() {
        crosshairStringInputField.Select();
        string importString = crosshairStringInputField.text.ToString();

        if (SimpleCrosshair.ValidateSetCrosshairString(importString, true)) {
            SetCrosshairNotification_Success();
            crosshairStringInputField.clear();
        } else {
            SetCrosshairNotification_Error();
        }
    }

    public void ExportCrosshairString() {
        Util.CopyToClipboard(SimpleCrosshair.ReturnExportedCrosshairString());
        SetCrosshairNotification_Exported();
    }

    private void SetCrosshairNotification_Error() {
        notificationText.SetText($"{I18nTextTranslator.SetTranslatedText("crosshairseterror")}");
        notificationText.color = InterfaceColors.notificationColorRed;
        Util.RefreshRootLayoutGroup(importExportPanel);
        crosshairImportExport.StartCoroutine(HideCrosshairNotification_Delay());
    }
    private void SetCrosshairNotification_Success() {
        notificationText.SetText($"{I18nTextTranslator.SetTranslatedText("crosshairsetsuccess")}");
        notificationText.color = InterfaceColors.notificationColorGreen;
        Util.RefreshRootLayoutGroup(importExportPanel);
        crosshairImportExport.StartCoroutine(HideCrosshairNotification_Delay());
    }
    private void SetCrosshairNotification_Exported() {
        notificationText.SetText($"{I18nTextTranslator.SetTranslatedText("crosshairsetexported")}");
        notificationText.color = InterfaceColors.hoveredColor;
        Util.RefreshRootLayoutGroup(importExportPanel);
        crosshairImportExport.StartCoroutine(HideCrosshairNotification_Delay());
    }

    private void HideCrosshairNotification() {
        notificationText.SetText("");
        notificationText.color = InterfaceColors.unselectedColor;
        Util.RefreshRootLayoutGroup(importExportPanel);
    }

    public static IEnumerator HideCrosshairNotification_Delay() {
        yield return notificationDestroyDelay;
        crosshairImportExport.HideCrosshairNotification();
    }
}

