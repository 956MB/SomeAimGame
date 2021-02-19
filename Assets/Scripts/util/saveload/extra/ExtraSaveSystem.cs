using UnityEngine;
using UnityEngine.UI;

using SomeAimGame.Stats;
using SomeAimGame.Utilities;
using SomeAimGame.Core;

public class ExtraSaveSystem : MonoBehaviour {
    public Toggle targetSoundToggleObject, UISoundToggleObject, ShowAARToggleObject, ShowExtraStatsToggleObject, ShowExtraStatsBackgroundsToggleObject;

    private static ExtraSaveSystem extraSave;
    void Awake() { extraSave = this; }

    /// <summary>
    /// Saves supplied extra settings object (ExtraSettings) to file.
    /// </summary>
    /// <param name="extraSettings"></param>
    public static void SaveExtraSettingsData() {
        ExtraSettingsDataSerial extraData = new ExtraSettingsDataSerial();
        SaveLoadUtil.SaveDataSerial("/prefs", "/sag_extra.prefs", extraData);
    }

    /// <summary>
    /// Loads extra settings data (ExtraSettingsDataSerial) from file.
    /// </summary>
    /// <returns></returns>
    public static ExtraSettingsDataSerial LoadExtraSettingsData() {
        ExtraSettingsDataSerial extraData = (ExtraSettingsDataSerial)SaveLoadUtil.LoadDataSerial("/prefs/sag_extra.prefs", SaveType.EXTRA);
        return extraData;
    }

    /// <summary>
    /// Inits saved extra settings object and sets all extra settings values.
    /// </summary>
    public static void InitSavedExtraSettings() {
        ExtraSettingsDataSerial loadedExtraData = LoadExtraSettingsData();
        if (loadedExtraData != null) {
            SetGameTimerDropdown(loadedExtraData.gameTimer);
            SetShowAARToggle(loadedExtraData.showAAR);
            SetMouseSensitivity(loadedExtraData.mouseSensitivity);
            SetShowWidgets(loadedExtraData.showWidgets);
            SetShowExtraStatsToggle(loadedExtraData.showExtraStats);
            SetShowExtraStatsBackgroundsToggle(loadedExtraData.showExtraStatsBackgrounds);

            ExtraSettings.LoadExtraSettings(loadedExtraData);
        } else {
            //Debug.Log("failed to init extra in 'initSavedSettings', extra: " + loadedExtraData);
            InitExtraSettingsDefaults();
        }
    }

    /// <summary>
    /// Inits default extra settings values and saves to file on first launch.
    /// </summary>
    public static void InitExtraSettingsDefaults() {
        SetGameTimerDropdown(60);

        extraSave.ShowAARToggleObject.isOn                   = true;
        extraSave.ShowExtraStatsToggleObject.isOn            = false;
        extraSave.ShowExtraStatsBackgroundsToggleObject.isOn = true;

        MouseSensitivitySlider.SetMouseSensitivityValueText(2.0f);
        MouseSensitivitySlider.SetMouseSensitivitySlider(2.0f);

        MouseLook.mouseSensitivity   = 2.0f;
        StatsManager.showBackgrounds = true;

        ExtraSettings.SaveAllExtraSettingsDefaults(60, true, true, 2.0f, true, false, true);
        GameUI.ShowWidgetsUI();
    }

    /// <summary>
    /// Inits game timer from saved extra settings file (if exists), or sets default 60.
    /// </summary>
    /// <returns></returns>
    public static int InitGameTimer() {
        ExtraSettingsDataSerial loadedExtraData = LoadExtraSettingsData();
        if (loadedExtraData != null) {
            return loadedExtraData.gameTimer + 1;
        } else {
            return 61;
        }
    }
    /// <summary>
    /// Inits show countdown timer from saved extra settings file (if exists), or sets default true.
    /// </summary>
    /// <returns></returns>
    public static bool InitShowCountdown() {
        ExtraSettingsDataSerial loadedExtraData = LoadExtraSettingsData();
        if (loadedExtraData != null) {
            return loadedExtraData.showCountdown;
        } else {
            return true;
        }
    }

    /// <summary>
    /// Sets game timer value and button to supplied int (gameTimerValue).
    /// </summary>
    /// <param name="gameTimerValue"></param>
    private static void SetGameTimerDropdown(int gameTimerValue) {           TimerSelect.SetTimerDropdownText(gameTimerValue); }
    /// <summary>
    /// Sets show 'AfterActionReport' panel toggle to supplied bool (showAARToggle), and saves in 'ExtraSettings' object.
    /// </summary>
    /// <param name="showAARToggle"></param>
    private static void SetShowAARToggle(bool showAARToggle) {               extraSave.ShowAARToggleObject.isOn = showAARToggle; }
    /// <summary>
    /// Sets show 'ExtraStats' panel toggle to supplied bool (showExtraStatsToggle), and saves in 'ExtraSettings' object.
    /// </summary>
    /// <param name="showExtraStatsToggle"></param>
    private static void SetShowExtraStatsToggle(bool showExtraStatsToggle) { extraSave.ShowExtraStatsToggleObject.isOn = showExtraStatsToggle; }
    /// <summary>
    /// Sets state of extra stats panel to supplied bool (showExtraStatsBackgroundsToggle).
    /// </summary>
    /// <param name="showExtraStatsBackgroundsToggle"></param>
    private static void SetShowExtraStatsBackgroundsToggle(bool showExtraStatsBackgroundsToggle) {
        StatsManager.showBackgrounds                         = showExtraStatsBackgroundsToggle;
        extraSave.ShowExtraStatsBackgroundsToggleObject.isOn = showExtraStatsBackgroundsToggle;
    }
    /// <summary>
    /// Sets mouse sensivity value, slider and text to supplied float (mouseSens), and saves in 'ExtraSettings' object.
    /// </summary>
    /// <param name="mouseSens"></param>
    private static void SetMouseSensitivity(float mouseSens) {
        MouseSensitivitySlider.SetMouseSensitivityValueText(mouseSens);
        MouseSensitivitySlider.SetMouseSensitivitySlider(mouseSens);
        MouseLook.mouseSensitivity = mouseSens;
        //MouseLook.mouseSensitivity = mouseSens * 100f;
    }
    /// <summary>
    /// Sets hideUI value to supplied bool (setHideUIValue), and saves in 'ExtraSettings' object.
    /// </summary>
    /// <param name="setHideUIValue"></param>
    private static void SetShowWidgets(bool setShowWidgets) {
        if (setShowWidgets) {
            GameUI.ShowWidgetsUI();
            ExtraSettings.SaveHideUI(true);
        } else {
            GameUI.HideWidgetsUI();
            ExtraSettings.SaveHideUI(false);
        }
    }
}
