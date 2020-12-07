using UnityEngine;
using UnityEngine.UI;

using SomeAimGame.Stats;
using SomeAimGame.SFX;

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
            case "TargetSoundToggle": // Toggles target hit sound.
                SFXManager.CheckPlayClick_Toggle();
                //TargetSoundSelect.ToggleTargetSoundSelectionContainer_Static();
                TargetSoundSelect.SetSoundSelectionContainerContainerState(toggleClicked.isOn);

                SFXSettings.SaveTargetSoundOn(toggleClicked.isOn);
                break;

            case "TargetMissSoundToggle": // Toggles target miss sound.
                SFXManager.CheckPlayClick_Toggle();

                SFXSettings.SaveTargetMissSoundOn(toggleClicked.isOn);
                break;

            case "UISoundToggle": // Toggles UI sound.
                SFXManager.CheckPlayClick_Toggle();

                SFXSettings.SaveUISoundOn(toggleClicked.isOn);
                break;

            case "AARToggle": // Toggles showing 'AfterActionReport'.
                SFXManager.CheckPlayClick_Toggle();

                ExtraSettings.SaveShowAAR(toggleClicked.isOn);
                break;

            case "CenterDotToggle": // Toggles center dot for crosshair.
                SFXManager.CheckPlayClick_Toggle();

                simpleCrosshair.SetCenterDot(toggleClicked.isOn, true);
                CrosshairSettings.SaveCenterDot(toggleClicked.isOn);
                CrosshairOptionsObject.SaveCrosshairObject(true);
                break;

            case "TStyleToggle": // Toggles TStyle for crosshair.
                SFXManager.CheckPlayClick_Toggle();

                simpleCrosshair.SetTStyle(toggleClicked.isOn, true);
                CrosshairSettings.SaveTStyle(toggleClicked.isOn);
                CrosshairOptionsObject.SaveCrosshairObject(true);
                break;

            case "OutlineEnableToggle": // Toggles outline for crosshair.
                SFXManager.CheckPlayClick_Toggle();

                simpleCrosshair.SetOutlineEnabled(toggleClicked.isOn, true);
                CrosshairSettings.SaveOutlineEnabled(toggleClicked.isOn);
                CrosshairOptionsObject.SetOutlineContainerState(toggleClicked.isOn);
                CrosshairOptionsObject.SaveCrosshairObject(true);
                break;

            case "ShowExtraStatsToggle": // Toggles 'ExtraStats' panel in 'AfterActionReport'.
                SFXManager.CheckPlayClick_Toggle();
                StatsManager.SetExtraStatsState(toggleClicked.isOn);
                ExtraSettings.SaveShowExtraStats(toggleClicked.isOn);
                break;

            case "ShowExtraStatsBackgroundsToggle": // Toggles 'ExtraStats' backgrounds panel in 'AfterActionReport'.
                SFXManager.CheckPlayClick_Toggle();
                StatsManager.SetExtraStatsBackgroundsState(toggleClicked.isOn);
                ExtraSettings.SaveShowExtraStatsBackgrounds(toggleClicked.isOn);
                break;

            case "QuickStartToggle": // Toggles quick start game in gamemode panel.
                SFXManager.CheckPlayClick_Toggle();

                CosmeticsSaveSystem.SetQuickStartGame(toggleClicked.isOn);
                CosmeticsSettings.SaveQuickStartGameItem(toggleClicked.isOn);
                break;

            case "ShowModeToggle": // Toggles Mode widget.
                SFXManager.CheckPlayClick_Toggle();

                modeWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowModeItem(toggleClicked.isOn);
                break;

            case "ShowFPSToggle": // Toggles FPS widget.
                SFXManager.CheckPlayClick_Toggle();

                fpsWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowFPSItem(toggleClicked.isOn);
                break;

            case "ShowTimeToggle": // Toggles time widget.
                SFXManager.CheckPlayClick_Toggle();

                timeWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowTimeItem(toggleClicked.isOn);
                break;

            case "ShowScoreToggle": // Toggles score widget.
                SFXManager.CheckPlayClick_Toggle();

                ScoreWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowScoreItem(toggleClicked.isOn);
                break;

            case "ShowAccuracyToggle": // Toggles accuracy widget.
                SFXManager.CheckPlayClick_Toggle();

                accuracyWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowAccuracyItem(toggleClicked.isOn);
                break;

            case "ShowStreakToggle": // Toggles streak widget.
                SFXManager.CheckPlayClick_Toggle();

                streakWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowStreakItem(toggleClicked.isOn);
                break;

            case "ShowTTKToggle": // Toggles ttk widget.
                SFXManager.CheckPlayClick_Toggle();

                ttkWidget.SetActive(toggleClicked.isOn);
                WidgetSettings.SaveShowTTKItem(toggleClicked.isOn);
                break;

            case "ShowKPSToggle": // Toggles kps widget.
                SFXManager.CheckPlayClick_Toggle();

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
    public static bool TargetSoundOn() { return true; }
    /// <summary>
    /// Returns ExtraSettings.uiSound value.
    /// </summary>
    /// <returns></returns>
    public static bool UISoundOn() { return true; }
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
