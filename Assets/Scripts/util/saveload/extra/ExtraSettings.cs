using UnityEngine;

using SomeAimGame.Utilities;

public class ExtraSettings : MonoBehaviour {
    public static int   gameTimer                 = 60;
    public static bool  showCountdown             = true;
    public static bool  showAAR                   = true;
    public static float mouseSensitivity          = 2.0f;
    public static bool  showWidgets               = true;
    public static bool  showExtraStats            = false;
    public static bool  showExtraStatsBackgrounds = true;

    static bool extraSettingsChangeReady = false;

    private static ExtraSettings extraSettings;
    void Awake() { extraSettings = this; }

    /// <summary>
    /// Saves supplied game timer int (setGameTimer) to extra settings object (ExtraSettings), then saves extra settings object.
    /// </summary>
    /// <param name="setGameTimer"></param>
    public static void SaveGameTimerItem(int setGameTimer) {                         Util.RefSetSettingChange(ref extraSettingsChangeReady, ref gameTimer, setGameTimer); }
    /// <summary>
    /// Saves supplied show countdown timer bool (setShowCountdown) to extra settings object (ExtraSettings), then saves extra settings object.
    /// </summary>
    /// <param name="setShowCountdown"></param>
    public static void SaveShowCountdownItem(bool setShowCountdown) {                Util.RefSetSettingChange(ref extraSettingsChangeReady, ref showCountdown, setShowCountdown); }
    /// <summary>
    /// Saves supplied show 'AfterActionReport' bool (setShowAAR) to extra settings object (ExtraSettings), then saves extra settings object.
    /// </summary>
    /// <param name="setShowAAR"></param>
    public static void SaveShowAAR(bool setShowAAR) {                                Util.RefSetSettingChange(ref extraSettingsChangeReady, ref showAAR, setShowAAR); }
    /// <summary>
    /// Saves supplied mouse sensitivity float (setMouseSens) to extra settings object (ExtraSettings), then saves extra settings object.
    /// </summary>
    /// <param name="setMouseSens"></param>
    public static void SaveMouseSensItem(float setMouseSens) {                       Util.RefSetSettingChange(ref extraSettingsChangeReady, ref mouseSensitivity, setMouseSens); }
    /// <summary>
    /// Saves supplied hide UI bool (setHideUI) to extra settings object (ExtraSettings), then saves extra settings object.
    /// </summary>
    /// <param name="setHideUI"></param>
    public static void SaveHideUI(bool setHideUI) {                                  Util.RefSetSettingChange(ref extraSettingsChangeReady, ref showWidgets, setHideUI); }
    /// <summary>
    /// Saves supplied show 'ExtraStats' bool (setShowExtraStats) to extra settings object (ExtraSettings), then saves extra settings object.
    /// </summary>
    /// <param name="setShowExtraStats"></param>
    public static void SaveShowExtraStats(bool setShowExtraStats) {                  Util.RefSetSettingChange(ref extraSettingsChangeReady, ref showExtraStats, setShowExtraStats); }
    /// <summary>
    /// Saves supplied show stats backgrounds bool (setShowExtraStatsBackgrounds) to extra settings object (ExtraSettings), then saves extra settings object.
    /// </summary>
    /// <param name="setShowExtraStatsBackgrounds"></param>
    public static void SaveShowStatsBackgrounds(bool setShowExtraStatsBackgrounds) { Util.RefSetSettingChange(ref extraSettingsChangeReady, ref showExtraStatsBackgrounds, setShowExtraStatsBackgrounds); }

    /// <summary>
    /// Calls 'ExtraSaveSystem.SaveExtraSettingsData()' to save extra settings object (ExtraSettings) to file.
    /// </summary>
    public void SaveExtraSettings() { ExtraSaveSystem.SaveExtraSettingsData(); }

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
    public static void SaveAllExtraSettingsDefaults(int setGameTimer, bool setShowCountdown, bool setShowAAR, float setMouseSens, bool setShowWidgets, bool setShowExtraStats, bool setShowExtraStatsBackgrounds) {
        gameTimer                 = setGameTimer;
        showCountdown             = setShowCountdown;
        showAAR                   = setShowAAR;
        mouseSensitivity          = setMouseSens;
        showWidgets               = setShowWidgets;
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
        showCountdown             = extraData.showCountdown;
        showAAR                   = extraData.showAAR;
        mouseSensitivity          = extraData.mouseSensitivity;
        showWidgets               = extraData.showWidgets;
        showExtraStats            = extraData.showExtraStats;
        showExtraStatsBackgrounds = extraData.showExtraStatsBackgrounds;
    }

    public static void CheckSaveExtraSettings() {
        if (extraSettingsChangeReady) {
            extraSettings.SaveExtraSettings();
            extraSettingsChangeReady = false;
        }
    }
}