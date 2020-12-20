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

            case "TargetSoundToggle": // Toggles target hit sound.
                TargetSoundSelect.SetSoundSelectionContainerState(toggleClicked.isOn);
                SFXSettings.SaveTargetSoundOn(toggleClicked.isOn);
                break;

            case "TargetMissSoundToggle": // Toggles target miss sound.
                SFXSettings.SaveTargetMissSoundOn(toggleClicked.isOn);
                break;

            case "UISoundToggle": // Toggles UI sound.
                SFXSettings.SaveUISoundOn(toggleClicked.isOn);
                break;

            case "AARToggle": // Toggles showing 'AfterActionReport'.
                ExtraSettings.SaveShowAAR(toggleClicked.isOn);
                break;

            // Crosshair //

            case "CenterDotToggle": // Toggles center dot for crosshair.
                simpleCrosshair.SetCenterDot(toggleClicked.isOn, true);
                CrosshairSettings.SaveCenterDot(toggleClicked.isOn);
                CrosshairOptionsObject.SaveCrosshairObject(true);
                break;

            case "TStyleToggle": // Toggles TStyle for crosshair.
                simpleCrosshair.SetTStyle(toggleClicked.isOn, true);
                CrosshairSettings.SaveTStyle(toggleClicked.isOn);
                CrosshairOptionsObject.SaveCrosshairObject(true);
                break;

            case "OutlineEnableToggle": // Toggles outline for crosshair.
                simpleCrosshair.SetOutlineEnabled(toggleClicked.isOn, true);
                CrosshairSettings.SaveOutlineEnabled(toggleClicked.isOn);
                CrosshairOptionsObject.SetOutlineContainerState(toggleClicked.isOn);
                CrosshairOptionsObject.SaveCrosshairObject(true);
                break;

            case "ShowExtraStatsToggle": // Toggles 'ExtraStats' panel in 'AfterActionReport'.
                StatsManager.SetExtraStatsState(toggleClicked.isOn);
                ExtraSettings.SaveShowExtraStats(toggleClicked.isOn);
                break;

            case "ShowStatsBackgroundsToggle": // Toggles stats backgrounds in 'AfterActionReport'.
                StatsManager.SetStatsBackgroundState(toggleClicked.isOn);
                ExtraSettings.SaveShowStatsBackgrounds(toggleClicked.isOn);
                break;

            case "QuickStartToggle": // Toggles quick start game in gamemode panel.
                CosmeticsSaveSystem.SetQuickStartGame(toggleClicked.isOn);
                CosmeticsSettings.SaveQuickStartGameItem(toggleClicked.isOn);
                break;

            // Widgets //

            case "ShowModeToggle": // Toggles Mode widget.
                modeWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowModeItem(toggleClicked.isOn);
                break;

            case "ShowFPSToggle": // Toggles FPS widget.
                fpsWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowFPSItem(toggleClicked.isOn);
                break;

            case "ShowTimeToggle": // Toggles time widget.
                timeWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowTimeItem(toggleClicked.isOn);
                break;

            case "ShowScoreToggle": // Toggles score widget.
                ScoreWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowScoreItem(toggleClicked.isOn);
                break;

            case "ShowAccuracyToggle": // Toggles accuracy widget.
                accuracyWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowAccuracyItem(toggleClicked.isOn);
                break;

            case "ShowStreakToggle": // Toggles streak widget.
                streakWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowStreakItem(toggleClicked.isOn);
                break;

            case "ShowTTKToggle": // Toggles ttk widget.
                ttkWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowTTKItem(toggleClicked.isOn);
                break;

            case "ShowKPSToggle": // Toggles kps widget.
                kpsWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowKPSItem(toggleClicked.isOn);
                break;

            // Video //

            case "VSyncToggle": // Toggles VSync in video settings.
                ApplyVideoSettings.ApplyVSync(toggleClicked.isOn);
                break;

            case "VignetteToggle": // Toggles vignette in video settings.
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
}