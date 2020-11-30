using UnityEngine;

using SomeAimGame.Utilities;

public class CrosshairPresets : MonoBehaviour {
    public GameObject presetsPanel, parentCrosshairGroup;
    public static bool crosshairPresetsPanelOpen = false;

    private static CrosshairPresets crosshairPresets;
    void Awake() { crosshairPresets = this; }

    public static void SetPresetCrosshair(string presetButtonName) {

    }

    /// <summary>
    /// Toggles crosshair presets panel open/closed.
    /// </summary>
    public void TriggerPresetsPanelOpen() {
        if (!crosshairPresetsPanelOpen) {
            OpenPresetsPanel_Static();
        } else {
            ClosePresetsPanel_Static();
        }

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Click(); }
    }

    /// <summary>
    /// Opens crosshair presets panel.
    /// </summary>
    public static void OpenPresetsPanel_Static() {
        if (!crosshairPresetsPanelOpen) {
            crosshairPresets.presetsPanel.SetActive(true);
            Util.RefreshRootLayoutGroup(crosshairPresets.parentCrosshairGroup);
            Util.RefreshRootLayoutGroup(crosshairPresets.presetsPanel);
            SubMenuHandler.ResetCrosshairScrollview();
            crosshairPresetsPanelOpen = true;
        }
    }

    /// <summary>
    /// Closes crosshair presets panel.
    /// </summary>
    public static void ClosePresetsPanel_Static() {
        crosshairPresets.presetsPanel.SetActive(false);
        Util.RefreshRootLayoutGroup(crosshairPresets.parentCrosshairGroup);
        ButtonHighlight_Hover.ResetPresetsButton_TextColor();
        crosshairPresetsPanelOpen = false;
    }

    public static void HidePresetsButton() {
        crosshairPresets.gameObject.transform.localScale = new Vector3(0f, 1f, 1f);
    }

    public static void ShowPresetsButton() {
        crosshairPresets.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
