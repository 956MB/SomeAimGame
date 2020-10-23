using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ToggleHandler : MonoBehaviour {
    public Toggle checkToggle;
    private string clickedToggleName;
    public GameObject fpsWidget, timeWidget, ScoreWidget, accuracyWidget, streakWidget, ttkWidget, kpsWidget;

    public SimpleCrosshair simpleCrosshair;

    void Start() {
        if (simpleCrosshair == null) {
            Debug.LogError("You have not set the target SimpleCrosshair. Disabling!");
            enabled = false;
        }

        SettingsPanel.CloseSettingsPanel();
        checkToggle = GetComponent<Toggle>();
        //extraToggleName = extraToggle.name;
        try {
            checkToggle.onValueChanged.AddListener(delegate {
                //Debug.Log($"extraToggle '{extraToggleName}' : {extraToggle.isOn}");
                HandleToggle(checkToggle);
            });
        } catch (NullReferenceException NRE) {
            //Debug.Log("Null reference exception here: " + NRE);
        }
    }

    /// <summary>
    /// Handles which actions to be taken based on toggle clicked (toggleClicked).
    /// </summary>
    /// <param name="toggleClicked"></param>
    public void HandleToggle(Toggle toggleClicked) {
        clickedToggleName = toggleClicked.name;
        //Debug.Log("hhfiefbsifbsanbfgsubfgib" + extraToggleName);

        switch (clickedToggleName) {
            case "TargetSoundToggle":
                // Toggles target sound.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    ExtraSettings.SaveTargetSoundItem(true);
                } else {
                    ExtraSettings.SaveTargetSoundItem(false);
                }
                break;

            case "UISoundToggle":
                // Toggles UI sound.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    ExtraSettings.SaveUISoundItem(true);
                } else {
                    ExtraSettings.SaveUISoundItem(false);
                }
                break;

            case "AARToggle":
                // Toggles showing 'AfterActionReport'.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    ExtraSettings.SaveShowAAR(true);
                } else {
                    ExtraSettings.SaveShowAAR(false);
                }
                break;

            case "CenterDotToggle":
                // Toggles center dot for crosshair.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    CrosshairSettings.SaveCenterDot(true);
                    simpleCrosshair.SetCenterDot(true, true);
                } else {
                    CrosshairSettings.SaveCenterDot(false);
                    simpleCrosshair.SetCenterDot(false, true);
                }
                break;

            case "TStyleToggle":
                // Toggles TStyle for crosshair.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    CrosshairSettings.SaveTStyle(true);
                    simpleCrosshair.SetTStyle(true, true);
                } else {
                    CrosshairSettings.SaveTStyle(false);
                    simpleCrosshair.SetTStyle(false, true);
                }
                break;

            case "OutlineEnableToggle":
                // Toggles outline for crosshair.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    CrosshairSettings.SaveOutlineEnabled(true);
                    simpleCrosshair.SetOutlineEnabled(true, true);
                } else {
                    CrosshairSettings.SaveOutlineEnabled(false);
                    simpleCrosshair.SetOutlineEnabled(false, true);
                }
                break;

            case "ShowExtraStatsToggle":
                // Toggles 'ExtraStats' panel in 'AfterActionReport'.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    StatsManager.ShowExtraStatsPanel();
                    ExtraSettings.SaveShowExtraStats(true);
                } else {
                    StatsManager.HideExtraStatsPanel();
                    ExtraSettings.SaveShowExtraStats(false);
                }
                break;

            case "ShowExtraStatsBackgroundsToggle":
                // Toggles 'ExtraStats' backgrounds panel in 'AfterActionReport'.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    StatsManager.SetExtraStatsBackgrounds();
                    ExtraSettings.SaveShowExtraStatsBackgrounds(true);
                } else {
                    StatsManager.ClearExtraStatsBackgrounds();
                    ExtraSettings.SaveShowExtraStatsBackgrounds(false);
                }
                break;

            case "QuickStartToggle":
                // Toggles quick start game in gamemode panel.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    CosmeticsSaveSystem.SetQuickStartGame(true);
                    CosmeticsSettings.SaveQuickStartGameItem(true);
                } else {
                    CosmeticsSaveSystem.SetQuickStartGame(false);
                    CosmeticsSettings.SaveQuickStartGameItem(false);
                }
                break;

            case "ShowFPSToggle":
                // Toggles FPS widget.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    fpsWidget.SetActive(true);
                    WidgetSettings.SaveShowFPSItem(true);
                } else {
                    fpsWidget.SetActive(false);
                    WidgetSettings.SaveShowFPSItem(false);
                }
                break;

            case "ShowTimeToggle":
                // Toggles time widget.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    timeWidget.SetActive(true);
                    WidgetSettings.SaveShowTimeItem(true);
                } else {
                    timeWidget.SetActive(false);
                    WidgetSettings.SaveShowTimeItem(false);
                }
                break;

            case "ShowScoreToggle":
                // Toggles score widget.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    ScoreWidget.SetActive(true);
                    WidgetSettings.SaveShowScoreItem(true);
                } else {
                    ScoreWidget.SetActive(false);
                    WidgetSettings.SaveShowScoreItem(false);
                }
                break;

            case "ShowAccuracyToggle":
                // Toggles accuracy widget.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    accuracyWidget.SetActive(true);
                    WidgetSettings.SaveShowAccuracyItem(true);
                } else {
                    accuracyWidget.SetActive(false);
                    WidgetSettings.SaveShowAccuracyItem(false);
                }
                break;

            case "ShowStreakToggle":
                // Toggles streak widget.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    streakWidget.SetActive(true);
                    WidgetSettings.SaveShowStreakItem(true);
                } else {
                    streakWidget.SetActive(false);
                    WidgetSettings.SaveShowStreakItem(false);
                }
                break;

            case "ShowTTKToggle":
                // Toggles ttk widget.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    ttkWidget.SetActive(true);
                    WidgetSettings.SaveShowTTKItem(true);
                } else {
                    ttkWidget.SetActive(false);
                    WidgetSettings.SaveShowTTKItem(false);
                }
                break;

            case "ShowKPSToggle":
                // Toggles kps widget.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    kpsWidget.SetActive(true);
                    WidgetSettings.SaveShowKPSItem(true);
                } else {
                    kpsWidget.SetActive(false);
                    WidgetSettings.SaveShowKPSItem(false);
                }
                break;
        }
    }

    /// <summary>
    /// Handles clicks on background part of toggle.
    /// </summary>
    /// <param name="backgroundToggle"></param>
    public void HandleBackgroundToggle(Toggle backgroundToggle) {
        backgroundToggle.isOn = !backgroundToggle.isOn;
        HandleToggle(backgroundToggle);
    }

    /// <summary>
    /// Returns ExtraSettings.targetSound value.
    /// </summary>
    /// <returns></returns>
    public static bool TargetSoundOn() { return ExtraSettings.targetSound; }
    /// <summary>
    /// Returns ExtraSettings.uiSound value.
    /// </summary>
    /// <returns></returns>
    public static bool UISoundOn() { return ExtraSettings.uiSound; }
    /// <summary>
    /// Returns WidgetSettings.showFPS value.
    /// </summary>
    /// <returns></returns>
    public static bool FPSCounterOn() { return WidgetSettings.showFPS; }
    /// <summary>
    /// Returns ExtraSettings.showAAR value.
    /// </summary>
    /// <returns></returns>
    public static bool ShowAAROn() { return ExtraSettings.showAAR; }
}
