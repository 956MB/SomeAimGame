using UnityEngine;
using UnityEngine.UI;

using SomeAimGame.Stats;
using SomeAimGame.SFX;
using SomeAimGame.Core.Video;

public class ToggleHandler : MonoBehaviour {
    public Toggle checkToggle;
    private string clickedToggleName;
    public GameObject modeWidget, fpsWidget, timeWidget, ScoreWidget, accuracyWidget, streakWidget, ttkWidget, kpsWidget;

    public SimpleCrosshair simpleCrosshair;

    private static ToggleHandler toggleHandle;
    void Awake() { toggleHandle = this; }

    void Start() {
        if (simpleCrosshair == null) {
            Debug.LogError("You have not set the target SimpleCrosshair. Disabling!");
            enabled = false;
        }
    }

    /// <summary>
    /// Handles which actions to be taken based on toggle clicked (toggleClicked).
    /// </summary>
    /// <param name="toggleClicked"></param>
    public void HandleToggle(Toggle toggleClicked) {
        clickedToggleName = toggleClicked.name;
        SFXManager.CheckPlayClick_Toggle();

        switch (clickedToggleName) {
            // SFX //

            case "TargetSoundToggle":     // Toggles target hit sound.
                TargetSoundSelect.SetSoundSelectionContainerState(toggleClicked.isOn);
                SFXSettings.SaveTargetSoundOn(toggleClicked.isOn);
                break;
            case "TargetMissSoundToggle": // Toggles target miss sound.
                TargetSoundSelect.SetTargetMissSoundContainerState(toggleClicked.isOn);
                SFXSettings.SaveTargetMissSoundOn(toggleClicked.isOn);
                break;
            case "UISoundToggle":         // Toggles UI sound.
                SFXSettings.SaveUISoundOn(toggleClicked.isOn);
                break;
            case "AARToggle":             // Toggles showing 'AfterActionReport'.
                ExtraSettings.SaveShowAAR(toggleClicked.isOn);
                break;

            // Crosshair //

            case "CenterDotToggle":            // Toggles center dot for crosshair.
                SetCrosshairCenterDotToggle(toggleClicked.isOn);
                CrosshairSettings.SaveCenterDot(toggleClicked.isOn);
                CrosshairOptionsObject.SaveCrosshairObject(true);
                break;
            case "TStyleToggle":               // Toggles TStyle for crosshair.
                SetCrosshairTStyleToggle(toggleClicked.isOn);
                CrosshairSettings.SaveTStyle(toggleClicked.isOn);
                CrosshairOptionsObject.SaveCrosshairObject(true);
                break;
            case "OutlineEnableToggle":        // Toggles outline for crosshair.
                SetCrosshairOutlineToggle(toggleClicked.isOn);
                CrosshairSettings.SaveOutlineEnabled(toggleClicked.isOn);
                CrosshairOptionsObject.SetOutlineContainerState(toggleClicked.isOn);
                CrosshairOptionsObject.SaveCrosshairObject(true);
                break;
            case "ShowExtraStatsToggle":       // Toggles 'ExtraStats' panel in 'AfterActionReport'.
                StatsManager.SetExtraStatsState(toggleClicked.isOn);
                ExtraSettings.SaveShowExtraStats(toggleClicked.isOn);
                break;
            case "ShowStatsBackgroundsToggle": // Toggles stats backgrounds in 'AfterActionReport'.
                StatsManager.SetStatsBackgroundState(toggleClicked.isOn);
                ExtraSettings.SaveShowStatsBackgrounds(toggleClicked.isOn);
                break;
            case "QuickStartToggle":           // Toggles quick start game in gamemode panel.
                CosmeticsSaveSystem.SetQuickStartGame(toggleClicked.isOn);
                CosmeticsSettings.SaveQuickStartGameItem(toggleClicked.isOn);
                break;

            // Widgets //

            case "ShowModeToggle":     // Toggles Mode widget.
                SetShowModeWidgetToggle(toggleClicked.isOn);
                WidgetSettings.SaveShowModeItem(toggleClicked.isOn);
                break;
            case "ShowFPSToggle":      // Toggles FPS widget.
                SetShowFPSWidgetToggle(toggleClicked.isOn);
                WidgetSettings.SaveShowFPSItem(toggleClicked.isOn);
                break;
            case "ShowTimeToggle":     // Toggles time widget.
                SetShowTimeWidgetToggle(toggleClicked.isOn);
                WidgetSettings.SaveShowTimeItem(toggleClicked.isOn);
                break;
            case "ShowScoreToggle":    // Toggles score widget.
                SetShowScoreWidgetToggle(toggleClicked.isOn);
                WidgetSettings.SaveShowScoreItem(toggleClicked.isOn);
                break;
            case "ShowAccuracyToggle": // Toggles accuracy widget.
                SetShowAccuracyWidgetToggle(toggleClicked.isOn);
                WidgetSettings.SaveShowAccuracyItem(toggleClicked.isOn);
                break;
            case "ShowStreakToggle":   // Toggles streak widget.
                SetShowStreakWidgetToggle(toggleClicked.isOn);
                WidgetSettings.SaveShowStreakItem(toggleClicked.isOn);
                break;
            case "ShowTTKToggle":      // Toggles ttk widget.
                SetShowTTKWidgetToggle(toggleClicked.isOn);
                WidgetSettings.SaveShowTTKItem(toggleClicked.isOn);
                break;
            case "ShowKPSToggle":      // Toggles kps widget.
                SetShowKPSWidgetToggle(toggleClicked.isOn);
                WidgetSettings.SaveShowKPSItem(toggleClicked.isOn);
                break;

            // Video //

            case "VSyncToggle":               // Toggles VSync in video settings.
                ApplyVideoSettings.ApplyVSync(toggleClicked.isOn);
                break;
            case "VignetteToggle":            // Toggles vignette in video settings.
                ApplyVideoSettings.ApplyVignette(toggleClicked.isOn);
                break;
            case "ChromaticAberrationToggle": // Toggles cromatic aberration in video settings.
                ApplyVideoSettings.ApplyChromaticAberration(toggleClicked.isOn);
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

    #region utils

    public static void SetShowModeWidgetToggle(bool setMode) { toggleHandle.modeWidget.SetActive(setMode); }
    public static void SetShowFPSWidgetToggle(bool setFPS) { toggleHandle.fpsWidget.SetActive(setFPS); }
    public static void SetShowTimeWidgetToggle(bool setTime) { toggleHandle.timeWidget.SetActive(setTime); }
    public static void SetShowScoreWidgetToggle(bool setScore) { toggleHandle.ScoreWidget.SetActive(setScore); }
    public static void SetShowAccuracyWidgetToggle(bool setAccuracy) { toggleHandle.accuracyWidget.SetActive(setAccuracy); }
    public static void SetShowStreakWidgetToggle(bool setStreak) { toggleHandle.streakWidget.SetActive(setStreak); }
    public static void SetShowTTKWidgetToggle(bool setTTK) { toggleHandle.ttkWidget.SetActive(setTTK); }
    public static void SetShowKPSWidgetToggle(bool setKPS) { toggleHandle.kpsWidget.SetActive(setKPS); }

    public static void SetCrosshairCenterDotToggle(bool setCenterDot) { toggleHandle.simpleCrosshair.SetCenterDot(setCenterDot, true); }
    public static void SetCrosshairTStyleToggle(bool setTstyle) { toggleHandle.simpleCrosshair.SetTStyle(setTstyle, true); }
    public static void SetCrosshairOutlineToggle(bool setOutline) { toggleHandle.simpleCrosshair.SetOutlineEnabled(setOutline, true); }

    #endregion
}