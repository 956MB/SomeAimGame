using UnityEngine;
using UnityEngine.UI;

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

        switch (clickedToggleName) {
            case "TargetSoundToggle": // Toggles target sound.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                ExtraSettings.SaveTargetSoundItem(toggleClicked.isOn);
                break;

            case "UISoundToggle": // Toggles UI sound.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                ExtraSettings.SaveUISoundItem(toggleClicked.isOn);
                break;

            case "AARToggle": // Toggles showing 'AfterActionReport'.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                ExtraSettings.SaveShowAAR(toggleClicked.isOn);
                break;

            case "CenterDotToggle": // Toggles center dot for crosshair.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                simpleCrosshair.SetCenterDot(toggleClicked.isOn, true);
                CrosshairSettings.SaveCenterDot(toggleClicked.isOn);
                CrosshairOptionsObject.SaveCrosshairObject(true);
                break;

            case "TStyleToggle": // Toggles TStyle for crosshair.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                simpleCrosshair.SetTStyle(toggleClicked.isOn, true);
                CrosshairSettings.SaveTStyle(toggleClicked.isOn);
                CrosshairOptionsObject.SaveCrosshairObject(true);
                break;

            case "OutlineEnableToggle": // Toggles outline for crosshair.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                simpleCrosshair.SetOutlineEnabled(toggleClicked.isOn, true);
                CrosshairSettings.SaveOutlineEnabled(toggleClicked.isOn);
                CrosshairOptionsObject.SaveCrosshairObject(true);
                break;

            case "ShowExtraStatsToggle": // Toggles 'ExtraStats' panel in 'AfterActionReport'.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }
                if (toggleClicked.isOn) {
                    StatsManager.ShowExtraStatsPanel();
                    ExtraSettings.SaveShowExtraStats(true);
                } else {
                    StatsManager.HideExtraStatsPanel();
                    ExtraSettings.SaveShowExtraStats(false);
                }
                break;

            case "ShowExtraStatsBackgroundsToggle": // Toggles 'ExtraStats' backgrounds panel in 'AfterActionReport'.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }
                if (toggleClicked.isOn) {
                    StatsManager.SetExtraStatsBackgrounds();
                    ExtraSettings.SaveShowExtraStatsBackgrounds(true);
                } else {
                    StatsManager.ClearExtraStatsBackgrounds();
                    ExtraSettings.SaveShowExtraStatsBackgrounds(false);
                }
                break;

            case "QuickStartToggle": // Toggles quick start game in gamemode panel.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                CosmeticsSaveSystem.SetQuickStartGame(toggleClicked.isOn);
                CosmeticsSettings.SaveQuickStartGameItem(toggleClicked.isOn);
                break;

            case "ShowModeToggle": // Toggles Mode widget.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                modeWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowModeItem(toggleClicked.isOn);
                break;

            case "ShowFPSToggle": // Toggles FPS widget.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                fpsWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowFPSItem(toggleClicked.isOn);
                break;

            case "ShowTimeToggle": // Toggles time widget.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                timeWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowTimeItem(toggleClicked.isOn);
                break;

            case "ShowScoreToggle": // Toggles score widget.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                ScoreWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowScoreItem(toggleClicked.isOn);
                break;

            case "ShowAccuracyToggle": // Toggles accuracy widget.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                accuracyWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowAccuracyItem(toggleClicked.isOn);
                break;

            case "ShowStreakToggle": // Toggles streak widget.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                streakWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowStreakItem(toggleClicked.isOn);
                break;

            case "ShowTTKToggle": // Toggles ttk widget.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                ttkWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowTTKItem(toggleClicked.isOn);
                break;

            case "ShowKPSToggle": // Toggles kps widget.
                if (UISoundOn()) { UISound.PlayUISound_Click(); }

                kpsWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowKPSItem(toggleClicked.isOn);
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
