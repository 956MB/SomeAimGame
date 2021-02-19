using UnityEngine;

using SomeAimGame.Skybox;
using SomeAimGame.Gamemode;
using SomeAimGame.Targets;
using SomeAimGame.Utilities;

public class CosmeticsSettings : MonoBehaviour {
    public static GamemodeType gamemode                = GamemodeType.SCATTER;
    public static TargetType   targetColor             = TargetType.YELLOW;

    public static string       customColorNameStrings  = "";
    public static int          customColorIndex        = -1;
    public static string       customColorStrings      = "";

    public static SkyboxType   skybox                  = SkyboxType.SLATE;
    public static float        afterActionReportPanelX = 960f;
    public static float        afterActionReportPanelY = 540f;
    public static float        extraStatsPanelX        = 1455.711f;
    public static float        extraStatsPanelY        = 638.3904f;
    public static bool         quickStartGame          = false;

    static bool cosmeticsSettingsChangeReady = false;

    private static CosmeticsSettings cosmeticsSettings;
    void Awake() { cosmeticsSettings = this; }

    /// <summary>
    /// Saves supplied gamemode string (setGamemode) to cosmetics settings object (CosmeticsSettings), then saves cosmetics settings object.
    /// </summary>
    /// <param name="setGamemode"></param>
    public static void SaveGamemodeItem(GamemodeType setGamemode) {                              Util.RefSetSettingChange(ref cosmeticsSettingsChangeReady, ref gamemode, setGamemode); }
    /// <summary>
    /// Saves supplied target color string (setTargetColor) to cosmetics settings object (CosmeticsSettings), then saves cosmetics settings object.
    /// </summary>
    /// <param name="setTargetColor"></param>
    public static void SaveTargetColorItem(TargetType setTargetColor, int setCustomColorIndex) { Util.RefSetSettingChange(ref cosmeticsSettingsChangeReady, ref targetColor, setTargetColor, ref customColorIndex, setCustomColorIndex); }
    /// <summary>
    /// Saves supplied skybox string (setSkybox) to cosmetics settings object (CosmeticsSettings), then saves cosmetics settings object.
    /// </summary>
    /// <param name="setSkybox"></param>
    public static void SaveSkyboxItem(SkyboxType setSkybox) {                                    Util.RefSetSettingChange(ref cosmeticsSettingsChangeReady, ref skybox, setSkybox); }
    /// <summary>
    /// Saves supplied settings panel location floats (setPanelX)/(setPanelY) to cosmetics settings object (CosmeticsSettings), then saves cosmetics settings object.
    /// </summary>
    /// <param name="setPanelX"></param>
    /// <param name="setPanelY"></param>
    public static void SavePanelLocationXY(string panelName, float setPanelX, float setPanelY) {
        switch (panelName) {
            case "AfterActionReport":
                Util.RefSetSettingChange(ref cosmeticsSettingsChangeReady, ref afterActionReportPanelX, setPanelX);
                Util.RefSetSettingChange(ref cosmeticsSettingsChangeReady, ref afterActionReportPanelY, setPanelY);
                break;
            case "ExtraStats":
                Util.RefSetSettingChange(ref cosmeticsSettingsChangeReady, ref extraStatsPanelX, setPanelX);
                Util.RefSetSettingChange(ref cosmeticsSettingsChangeReady, ref extraStatsPanelY, setPanelY);
                break;
        }
    }
    public static void SaveCustomTargetIndex(int setCustomTargetIndex) {
        Util.RefSetSettingChange(ref cosmeticsSettingsChangeReady, ref customColorIndex, setCustomTargetIndex);
    }
    public static void SaveCustomTarget(string setCustomTargetName, string setCustomTargetColor) {
        string newCustomTargetNamesString, newCustomTargetColorString;

        if (customColorNameStrings == "" && customColorStrings == "") {
            newCustomTargetNamesString = $"{setCustomTargetName}";
            newCustomTargetColorString = $"#{setCustomTargetColor}"; // TODO: make sure 'Util.ColorToHex' returns color without "#", and change 'Util.HexToColor' to accept "#RRGGBBAA".
        } else {
            newCustomTargetNamesString = $"{customColorNameStrings},{setCustomTargetName}";
            newCustomTargetColorString = $"{customColorStrings},#{setCustomTargetColor}";
        }

        Util.RefSetSettingChange(ref cosmeticsSettingsChangeReady, ref customColorNameStrings, newCustomTargetNamesString);
        Util.RefSetSettingChange(ref cosmeticsSettingsChangeReady, ref customColorStrings, newCustomTargetColorString);
    }
    /// <summary>
    /// Saves supplied quick start game bool (setQuickStart) to cosmetics settings object (CosmeticsSettings), then saves cosmetics settings object.
    /// </summary>
    /// <param name="setQuickStart"></param>
    public static void SaveQuickStartGameItem(bool setQuickStart) { Util.RefSetSettingChange(ref cosmeticsSettingsChangeReady, ref quickStartGame, setQuickStart); }

    /// <summary>
    /// Calls 'CosmeticsSaveSystem.SaveCosmeticsItem()' to save cosmetics settings object (CosmeticsSettings) to file.
    /// </summary>
    public void SaveCosmeticsSettings() { CosmeticsSaveSystem.SaveCosmeticsSettingsData(); }

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
    public static void SaveAllCosmeticsToggleDefaults(GamemodeType setGamemode, TargetType setTargetColor, string setCustomNameStrings, int setCustomColorIndex, string setCustomColorStrings, SkyboxType setSkybox, float setPanelSettingsX, float setPanelSettingsY, float setPanelExtraStatsX, float setPanelExtraStatsY, bool setQuickStart) {
        gamemode                = setGamemode;
        targetColor             = setTargetColor;
        customColorNameStrings  = setCustomNameStrings;
        customColorIndex        = setCustomColorIndex;
        customColorStrings      = setCustomColorStrings;
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
        customColorNameStrings  = cometicsData.customColorNameStrings;
        customColorIndex        = cometicsData.customColorIndex;
        customColorStrings      = cometicsData.customColorStrings;
        skybox                  = cometicsData.skybox;
        afterActionReportPanelX = cometicsData.afterActionReportPanelX;
        afterActionReportPanelY = cometicsData.afterActionReportPanelY;
        extraStatsPanelX        = cometicsData.extraStatsPanelX;
        extraStatsPanelX        = cometicsData.extraStatsPanelY;
        quickStartGame          = cometicsData.quickStartGame;
    }

    public static void CheckSaveCosmeticsSettings() {
        if (cosmeticsSettingsChangeReady) {
            cosmeticsSettings.SaveCosmeticsSettings();
            cosmeticsSettingsChangeReady = false;
        }
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
