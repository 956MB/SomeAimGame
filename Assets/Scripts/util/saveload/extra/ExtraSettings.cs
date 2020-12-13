using UnityEngine;

public class ExtraSettings : MonoBehaviour {
    public static int   gameTimer                 = 60;
    public static bool  showAAR                   = true;
    public static float mouseSensitivity          = 2.0f;
    public static bool  hideUI                    = false;
    public static bool  showExtraStats            = false;
    public static bool  showExtraStatsBackgrounds = true;

    private static ExtraSettings extraSettings;
    void Awake() { extraSettings = this; }

    /// <summary>
    /// Saves supplied game timer int (setGameTimer) to extra settings object (ExtraSettings), then saves extra settings object.
    /// </summary>
    /// <param name="setGameTimer"></param>
    public static void SaveGameTimerItem(int setGameTimer) {
        gameTimer = setGameTimer;
        extraSettings.SaveExtraSettings();
    }
    /// <summary>
    /// Saves supplied show 'AfterActionReport' bool (setShowAAR) to extra settings object (ExtraSettings), then saves extra settings object.
    /// </summary>
    /// <param name="setShowAAR"></param>
    public static void SaveShowAAR(bool setShowAAR) {
        showAAR = setShowAAR;
        extraSettings.SaveExtraSettings();
    }
    /// <summary>
    /// Saves supplied mouse sensitivity float (setMouseSens) to extra settings object (ExtraSettings), then saves extra settings object.
    /// </summary>
    /// <param name="setMouseSens"></param>
    public static void SaveMouseSensItem(float setMouseSens) {
        mouseSensitivity = setMouseSens;
        extraSettings.SaveExtraSettings();
    }
    /// <summary>
    /// Saves supplied hide UI bool (setHideUI) to extra settings object (ExtraSettings), then saves extra settings object.
    /// </summary>
    /// <param name="setHideUI"></param>
    public static void SaveHideUI(bool setHideUI) {
        hideUI = setHideUI;
        extraSettings.SaveExtraSettings();
    }
    /// <summary>
    /// Saves supplied show 'ExtraStats' bool (setShowExtraStats) to extra settings object (ExtraSettings), then saves extra settings object.
    /// </summary>
    /// <param name="setShowExtraStats"></param>
    public static void SaveShowExtraStats(bool setShowExtraStats) {
        showExtraStats = setShowExtraStats;
        extraSettings.SaveExtraSettings();
    }
    /// <summary>
    /// Saves supplied show stats backgrounds bool (setShowExtraStatsBackgrounds) to extra settings object (ExtraSettings), then saves extra settings object.
    /// </summary>
    /// <param name="setShowExtraStatsBackgrounds"></param>
    public static void SaveShowStatsBackgrounds(bool setShowExtraStatsBackgrounds) {
        showExtraStatsBackgrounds = setShowExtraStatsBackgrounds;
        extraSettings.SaveExtraSettings();
    }

    /// <summary>
    /// Calls 'ExtraSaveSystem.SaveExtraSettingsData()' to save extra settings object (ExtraSettings) to file.
    /// </summary>
    public void SaveExtraSettings() { ExtraSaveSystem.SaveExtraSettingsData(this); }

    /// <summary>
    /// Saves default extra settings object (ExtraSettings).
    /// </summary>
    /// <param name="setGameTimer"></param>
    /// <param name="setTargetSound"></param>
    /// <param name="setUISound"></param>
    /// <param name="setShowAAR"></param>
    /// <param name="setMouseSens"></param>
    /// <param name="setHideUI"></param>
    /// <param name="setShowExtraStats"></param>
    public static void SaveAllExtraSettingsDefaults(int setGameTimer, bool setShowAAR, float setMouseSens, bool setHideUI, bool setShowExtraStats, bool setShowExtraStatsBackgrounds) {
        gameTimer                 = setGameTimer;
        showAAR                   = setShowAAR;
        mouseSensitivity          = setMouseSens;
        hideUI                    = setHideUI;
        showExtraStats            = setShowExtraStats;
        showExtraStatsBackgrounds = setShowExtraStatsBackgrounds;

        extraSettings.SaveExtraSettings();
    }

    /// <summary>
    /// Loads extra settings data (ExtraSettingsDataSerial) and sets values to this extra settings object (ExtraSettings).
    /// </summary>
    /// <param name="extraData"></param>
    public static void LoadExtraSettings(ExtraSettingsDataSerial extraData) {
        gameTimer                 = extraData.gameTimer;
        showAAR                   = extraData.showAAR;
        mouseSensitivity          = extraData.mouseSensitivity;
        hideUI                    = extraData.hideUI;
        showExtraStats            = extraData.showExtraStats;
        showExtraStatsBackgrounds = extraData.showExtraStatsBackgrounds;
    }
}