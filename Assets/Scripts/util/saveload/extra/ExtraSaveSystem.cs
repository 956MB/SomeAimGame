using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using SomeAimGame.Stats;

public class ExtraSaveSystem : MonoBehaviour {
    public Toggle targetSoundToggleObject, UISoundToggleObject, ShowAARToggleObject, ShowExtraStatsToggleObject, ShowExtraStatsBackgroundsToggleObject;

    private static ExtraSaveSystem extraSave;
    void Awake() { extraSave = this; }

    /// <summary>
    /// Saves supplied extra settings object (ExtraSettings) to file.
    /// </summary>
    /// <param name="extraSettings"></param>
    public static void SaveExtraSettingsData(ExtraSettings extraSettings) {
        BinaryFormatter formatter = new BinaryFormatter();
        string dirPath            = Application.persistentDataPath + "/prefs";
        string filePath           = dirPath + "/sag_extra.prefs";

        DirectoryInfo dirInf = new DirectoryInfo(dirPath);
        if (!dirInf.Exists) { dirInf.Create(); }

        FileStream stream                 = new FileStream(filePath, FileMode.Create);
        ExtraSettingsDataSerial extraData = new ExtraSettingsDataSerial();
        formatter.Serialize(stream, extraData);
        stream.Close();
    }

    /// <summary>
    /// Loads extra settings data (ExtraSettingsDataSerial) from file.
    /// </summary>
    /// <returns></returns>
    public static ExtraSettingsDataSerial LoadExtraSettingsData() {
        string path = Application.persistentDataPath + "/prefs/sag_extra.prefs";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream         = new FileStream(path, FileMode.Open);

            ExtraSettingsDataSerial extraData = formatter.Deserialize(stream) as ExtraSettingsDataSerial;
            stream.Close();

            return extraData;
        } else {
            return null;
        }
    }

    /// <summary>
    /// Inits saved extra settings object and sets all extra settings values.
    /// </summary>
    public static void InitSavedExtraSettings() {
        ExtraSettingsDataSerial loadedExtraData = LoadExtraSettingsData();
        if (loadedExtraData != null) {
            ExtraSettings.LoadExtraSettings(loadedExtraData);

            SetGameTimerButtons(loadedExtraData.gameTimer);
            SetShowAARToggle(loadedExtraData.showAAR);
            SetMouseSensitivity(loadedExtraData.mouseSensitivity);
            SetHideUI(loadedExtraData.hideUI);
            SetShowExtraStatsToggle(loadedExtraData.showExtraStats);
            SetShowExtraStatsBackgroundsToggle(loadedExtraData.showExtraStatsBackgrounds);
        } else {
            //Debug.Log("failed to init extra in 'initSavedSettings', extra: " + loadedExtraData);
            InitExtraSettingsDefaults();
        }
    }

    /// <summary>
    /// Inits default extra settings values and saves to file on first launch.
    /// </summary>
    public static void InitExtraSettingsDefaults() {
        SetGameTimerButtons(60);

        extraSave.ShowAARToggleObject.isOn                   = true;
        extraSave.ShowExtraStatsToggleObject.isOn            = false;
        extraSave.ShowExtraStatsBackgroundsToggleObject.isOn = true;

        MouseSensitivitySlider.SetMouseSensitivityValueText(2.0f);
        MouseSensitivitySlider.SetMouseSensitivitySlider(2.0f);

        MouseLook.mouseSensitivity   = 2.0f;
        StatsManager.showBackgrounds = true;

        ExtraSettings.SaveAllExtraSettingsDefaults(60, true, 2.0f, true, false, true);
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
    /// Sets game timer value and button to supplied int (gameTimerValue).
    /// </summary>
    /// <param name="gameTimerValue"></param>
    private static void SetGameTimerButtons(int gameTimerValue) {
        ChangeGameTimer.SetNewGameTimer(gameTimerValue, false);
        ChangeGameTimer.SetGameTimerButton(gameTimerValue);
    }
    /// <summary>
    /// Sets target sound toggle to supplied bool (targetSoundToggle), and saves in 'ExtraSettings' object.
    /// </summary>
    /// <param name="targetSoundToggle"></param>
    private static void SetTargetSoundToggle(bool targetSoundToggle) {
        extraSave.targetSoundToggleObject.isOn = targetSoundToggle;
    }
    /// <summary>
    /// Sets UI sound toggle to supplied bool (UISoundToggle), and saves in 'ExtraSettings' object.
    /// </summary>
    /// <param name="UISoundToggle"></param>
    private static void SetUISoundToggle(bool UISoundToggle) {
        extraSave.UISoundToggleObject.isOn = UISoundToggle;
    }
    /// <summary>
    /// Sets show 'AfterActionReport' panel toggle to supplied bool (showAARToggle), and saves in 'ExtraSettings' object.
    /// </summary>
    /// <param name="showAARToggle"></param>
    private static void SetShowAARToggle(bool showAARToggle) {
        extraSave.ShowAARToggleObject.isOn = showAARToggle;
    }
    /// <summary>
    /// Sets show 'ExtraStats' panel toggle to supplied bool (showExtraStatsToggle), and saves in 'ExtraSettings' object.
    /// </summary>
    /// <param name="showExtraStatsToggle"></param>
    private static void SetShowExtraStatsToggle(bool showExtraStatsToggle) {
        extraSave.ShowExtraStatsToggleObject.isOn = showExtraStatsToggle;
    }

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
    private static void SetHideUI(bool setHideUIValue) {
        if (setHideUIValue) {
            GameUI.ShowWidgetsUI();
            ExtraSettings.SaveHideUI(true);
        } else {
            GameUI.HideWidgetsUI();
            ExtraSettings.SaveHideUI(false);
        }
    }
}
