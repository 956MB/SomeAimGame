using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class ExtraSaveSystem : MonoBehaviour {
    public Toggle targetSoundToggleObject, UISoundToggleObject, movemetnLockToggleObject, FPSCounterToggleObject, ShowAARToggleObject, ShowExtraStatsToggleObject;
    public GameObject fpsCounterContainer;

    private static ExtraSaveSystem extraSave;
    void Awake() { extraSave = this; }

    /// <summary>
    /// Saves supplied extra settings object (ExtraSettings) to file.
    /// </summary>
    /// <param name="extraSettings"></param>
    public static void SaveExtraSettingsData(ExtraSettings extraSettings) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/extra.settings";
        FileStream stream = new FileStream(path, FileMode.Create);

        ExtraSettingsDataSerial extraData = new ExtraSettingsDataSerial();
        formatter.Serialize(stream, extraData);
        stream.Close();
    }

    /// <summary>
    /// Loads extra settings data (ExtraSettingsDataSerial) from file.
    /// </summary>
    /// <returns></returns>
    public static ExtraSettingsDataSerial LoadExtraSettingsData() {
        string path = Application.persistentDataPath + "/extra.settings";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ExtraSettingsDataSerial extraData = formatter.Deserialize(stream) as ExtraSettingsDataSerial;
            stream.Close();

            return extraData;
        }
        else {
            //Debug.Log("Save file not found in " + path);
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
            SetTargetSoundToggle(loadedExtraData.targetSound);
            SetUISoundToggle(loadedExtraData.uiSound);
            //setMovementLockToggle(loadedExtraData.movementLock);
            SetFPSCounterToggle(loadedExtraData.fpsCounter);
            SetShowAARToggle(loadedExtraData.showAAR);
            SetMouseSensitivity(loadedExtraData.mouseSensitivity);
            SetHideUI(loadedExtraData.hideUI);
            SetShowExtraStatsToggle(loadedExtraData.showExtraStats);
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

        extraSave.targetSoundToggleObject.isOn    = true;
        extraSave.UISoundToggleObject.isOn        = true;
        extraSave.movemetnLockToggleObject.isOn   = true;
        extraSave.FPSCounterToggleObject.isOn     = false;
        extraSave.ShowAARToggleObject.isOn        = true;
        extraSave.ShowExtraStatsToggleObject.isOn = false;
        SetFPSCounterToggle(false);

        MouseSensitivitySlider.SetMouseSensitivityValueText(2.0f);
        MouseSensitivitySlider.SetMouseSensitivitySlider(2.0f);

        MouseLook.mouseSensitivity = 2.0f;

        ExtraSettings.SaveAllExtraSettingsDefaults(60, true, true, true, false, true, 2.0f, true, false);
        GameUI.ShowUI();
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
        ExtraSettings.targetSound = targetSoundToggle;
    }
    /// <summary>
    /// Sets UI sound toggle to supplied bool (UISoundToggle), and saves in 'ExtraSettings' object.
    /// </summary>
    /// <param name="UISoundToggle"></param>
    private static void SetUISoundToggle(bool UISoundToggle) {
        extraSave.UISoundToggleObject.isOn = UISoundToggle;
        ExtraSettings.uiSound = UISoundToggle;
    }
    /// <summary>
    /// Sets movement lock toggle to supplied bool (movementLockToggle), and saves in 'ExtraSettings' object (currently not being used).
    /// </summary>
    /// <param name="movementLockToggle"></param>
    private static void SetMovementLockToggle(bool movementLockToggle) {
        extraSave.movemetnLockToggleObject.isOn = true;
        ExtraSettings.movementLock = true;
    }
    /// <summary>
    /// Sets FPS counter toggle to supplied bool (FPSCounterToggle), and saves in 'ExtraSettings' object (currently not being used).
    /// </summary>
    /// <param name="FPSCounterToggle"></param>
    private static void SetFPSCounterToggle(bool FPSCounterToggle) {
        extraSave.FPSCounterToggleObject.isOn = FPSCounterToggle;
        ExtraSettings.fpsCounter = FPSCounterToggle;
        extraSave.fpsCounterContainer.gameObject.SetActive(FPSCounterToggle);
    }
    /// <summary>
    /// Sets show 'AfterActionReport' panel toggle to supplied bool (showAARToggle), and saves in 'ExtraSettings' object.
    /// </summary>
    /// <param name="showAARToggle"></param>
    private static void SetShowAARToggle(bool showAARToggle) {
        extraSave.ShowAARToggleObject.isOn = showAARToggle;
        ExtraSettings.showAAR = showAARToggle;
    }
    /// <summary>
    /// Sets show 'ExtraStats' panel toggle to supplied bool (showExtraStatsToggle), and saves in 'ExtraSettings' object.
    /// </summary>
    /// <param name="showExtraStatsToggle"></param>
    private static void SetShowExtraStatsToggle(bool showExtraStatsToggle) {
        extraSave.ShowExtraStatsToggleObject.isOn = showExtraStatsToggle;
        ExtraSettings.showExtraStats = showExtraStatsToggle;
    }
    /// <summary>
    /// Sets mouse sensivity value, slider and text to supplied float (mouseSens), and saves in 'ExtraSettings' object.
    /// </summary>
    /// <param name="mouseSens"></param>
    private static void SetMouseSensitivity(float mouseSens) {
        ExtraSettings.mouseSensitivity = mouseSens;
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
            GameUI.ShowUI();
            ExtraSettings.SaveHideUI(true);
        } else {
            GameUI.HideUI();
            ExtraSettings.SaveHideUI(false);
        }
    }
}
