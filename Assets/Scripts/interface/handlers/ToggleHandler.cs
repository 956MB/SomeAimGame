using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleHandler : MonoBehaviour {
    public Toggle extraToggle;
    private string clickedToggleName;
    public GameObject fpsCounter;

    public SimpleCrosshair simpleCrosshair;

    void Start() {
        if (simpleCrosshair == null) {
            Debug.LogError("You have not set the target SimpleCrosshair. Disabling!");
            enabled = false;
        }

        SettingsPanel.CloseSettingsPanel();
        extraToggle = GetComponent<Toggle>();
        //extraToggleName = extraToggle.name;
        extraToggle.onValueChanged.AddListener(delegate {
            //Debug.Log($"extraToggle '{extraToggleName}' : {extraToggle.isOn}");
            HandleToggle(extraToggle);
        });
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

            case "MovementLockToggle":
                // Toggles movement lock.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                //if (extra.isOn) {
                //    ExtraSettings.saveMovementLockItem(true);
                //    GameUI.restartGame(CosmeticsSettings.gamemode);
                //} else {
                //    ExtraSettings.saveMovementLockItem(false);
                //}
                ExtraSettings.SaveMovementLockItem(true);
                break;

            case "FPSCounterToggle":
                // Toggles FPS Counter.
                if (UISoundOn()) { UISound.PlayUISound02(); }
                if (toggleClicked.isOn) {
                    ExtraSettings.SaveFPSCounter(true);
                    fpsCounter.gameObject.SetActive(true);
                    //Debug.Log("after fps turn on");
                } else {
                    ExtraSettings.SaveFPSCounter(false);
                    fpsCounter.gameObject.SetActive(false);
                    //Debug.Log("after fps turn off");
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
    /// Returns ExtraSettings.movementLock value.
    /// </summary>
    /// <returns></returns>
    public static bool MovementLockOn() { return ExtraSettings.movementLock; }
    /// <summary>
    /// Returns ExtraSettings.fpsCounter value.
    /// </summary>
    /// <returns></returns>
    public static bool FPSCounterOn() { return ExtraSettings.fpsCounter; }
    /// <summary>
    /// Returns ExtraSettings.showAAR value.
    /// </summary>
    /// <returns></returns>
    public static bool ShowAAROn() { return ExtraSettings.showAAR; }
}
