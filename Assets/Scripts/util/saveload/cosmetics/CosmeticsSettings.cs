using TMPro;
using UnityEngine;

public class CosmeticsSettings : MonoBehaviour {
    public static Gamemode gamemode             = Gamemode.Scatter;
    public static TargetColor targetColor       = TargetColor.Yellow;
    public static string skybox                 = "Skybox-Slate";
    public static float afterActionReportPanelX = 960f;
    public static float afterActionReportPanelY = 540f;
    public static float extraStatsPanelX        = 1455.711f;
    public static float extraStatsPanelY        = 638.3904f;
    
    public static bool quickStartGame    = false;

    private static CosmeticsSettings cosmeticsSettings;
    void Awake() { cosmeticsSettings = this; }

    /// <summary>
    /// Saves supplied gamemode string (setGamemode) to cosmetics settings object (CosmeticsSettings), then saves cosmetics settings object.
    /// </summary>
    /// <param name="setGamemode"></param>
    public static void SaveGamemodeItem(Gamemode setGamemode) {
        gamemode = setGamemode;
        cosmeticsSettings.SaveCosmeticsSettings();
    }

    /// <summary>
    /// Saves supplied target color string (setTargetColor) to cosmetics settings object (CosmeticsSettings), then saves cosmetics settings object.
    /// </summary>
    /// <param name="setTargetColor"></param>
    public static void SaveTargetColorItem(TargetColor setTargetColor) {
        //Debug.Log("save target color here????" + setTargetColor);
        targetColor = setTargetColor;
        cosmeticsSettings.SaveCosmeticsSettings();
    }

    /// <summary>
    /// Saves supplied skybox string (setSkybox) to cosmetics settings object (CosmeticsSettings), then saves cosmetics settings object.
    /// </summary>
    /// <param name="setSkybox"></param>
    public static void SaveSkyboxItem(string setSkybox) {
        skybox = setSkybox;
        cosmeticsSettings.SaveCosmeticsSettings();
    }

    /// <summary>
    /// Saves supplied settings panel location floats (setPanelX)/(setPanelY) to cosmetics settings object (CosmeticsSettings), then saves cosmetics settings object.
    /// </summary>
    /// <param name="setPanelX"></param>
    /// <param name="setPanelY"></param>
    public static void SavePanelLocationXY(string panelName, float setPanelX, float setPanelY) {
        switch (panelName) {
            case "AfterActionReport":
                afterActionReportPanelX = setPanelX;
                afterActionReportPanelY = setPanelY;
                break;
            case "ExtraStats":
                extraStatsPanelX = setPanelX;
                extraStatsPanelY = setPanelY;
                break;
        }

        cosmeticsSettings.SaveCosmeticsSettings();
    }

    /// <summary>
    /// Saves supplied quick start game bool (setQuickStart) to cosmetics settings object (CosmeticsSettings), then saves cosmetics settings object.
    /// </summary>
    /// <param name="setQuickStart"></param>
    public static void SaveQuickStartGameItem(bool setQuickStart) {
        quickStartGame = setQuickStart;
        cosmeticsSettings.SaveCosmeticsSettings();
    }

    /// <summary>
    /// Calls 'CosmeticsSaveSystem.SaveCosmeticsItem()' to save cosmetics settings object (CosmeticsSettings) to file.
    /// </summary>
    public void SaveCosmeticsSettings() { CosmeticsSaveSystem.SaveCosmeticsSettingsData(this); }

    /// <summary>
    /// Saves default cosmetics settings object (CosmeticsSettings).
    /// </summary>
    /// <param name="setGamemode"></param>
    /// <param name="setTargetColor"></param>
    /// <param name="setSkybox"></param>
    /// <param name="setPanelSettingsX"></param>
    /// <param name="setPanelSettingsY"></param>
    /// <param name="setPanelExtraStatsX"></param>
    /// <param name="setPanelExtraStatsY"></param>
    public static void SaveAllCosmeticsToggleDefaults(Gamemode setGamemode, TargetColor setTargetColor, string setSkybox, float setPanelSettingsX, float setPanelSettingsY, float setPanelExtraStatsX, float setPanelExtraStatsY, bool setQuickStart) {
        gamemode                = setGamemode;
        targetColor             = setTargetColor;
        skybox                  = setSkybox;
        afterActionReportPanelX = setPanelSettingsX;
        afterActionReportPanelY = setPanelSettingsY;
        extraStatsPanelX        = setPanelExtraStatsX;
        extraStatsPanelY        = setPanelExtraStatsY;
        quickStartGame          = setQuickStart;

        cosmeticsSettings.SaveCosmeticsSettings();
    }

    /// <summary>
    /// Loads cosmetics data (CosmeticsDataSerial) and sets values to this cosmetics settings object (CosmeticsSettings).
    /// </summary>
    /// <param name="cometicsData"></param>
    public static void LoadCosmeticsSettings(CosmeticsDataSerial cometicsData) {
        gamemode                = cometicsData.gamemode;
        targetColor             = cometicsData.targetColor;
        skybox                  = cometicsData.skybox;
        afterActionReportPanelX = cometicsData.afterActionReportPanelX;
        afterActionReportPanelY = cometicsData.afterActionReportPanelY;
        extraStatsPanelX        = cometicsData.extraStatsPanelX;
        extraStatsPanelX        = cometicsData.extraStatsPanelY;
        quickStartGame          = cometicsData.quickStartGame;
    }

    /// <summary>
    /// Sets 'AfterActionReport' panel location (X/Y) back to center, then saves cosmetics settings.
    /// </summary>
    public static void resetAfterActionReportPanelCenter() {
        afterActionReportPanelX = 960f;
        afterActionReportPanelY = 540f;
        cosmeticsSettings.SaveCosmeticsSettings();
    }

    /// <summary>
    /// Sets 'ExtraStats' panel location (X/Y) back to default location, then saves cosmetics settings.
    /// </summary>
    public static void resetExtraStatsPanelCenter() {
        extraStatsPanelX = 1455.711f;
        extraStatsPanelY = 638.3904f;
        cosmeticsSettings.SaveCosmeticsSettings();
    }
}
