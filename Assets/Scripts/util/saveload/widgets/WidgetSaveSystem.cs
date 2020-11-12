using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class WidgetSaveSystem : MonoBehaviour {
    public Toggle showFPSToggle, showTimeToggle, showScoreToggle, showAccuracyToggle, showStreakToggle, showTTKToggle, showKPSToggle;
    public GameObject showFPSWidget, showTimeWidget, showScoreWidget, showAccuracyWidget, showStreakWidget, showTTKWidget, showKPSWidget;

    private static WidgetSaveSystem widgetSave;
    void Awake() { widgetSave = this; }

    /// <summary>
    /// Saves supplied widget settings object (WidgetSettings) to file.
    /// </summary>
    /// <param name="widgetSettings"></param>
    public static void SaveWidgetSettingsData(WidgetSettings widgetSettings) {
        BinaryFormatter formatter = new BinaryFormatter();
        string dirPath            = Application.persistentDataPath + "/settings";
        string filePath           = dirPath + "/widget.settings";

        DirectoryInfo dirInf = new DirectoryInfo(dirPath);
        if (!dirInf.Exists) { dirInf.Create(); }

        FileStream stream                   = new FileStream(filePath, FileMode.Create);
        WidgetSettingsDataSerial widgetData = new WidgetSettingsDataSerial();
        formatter.Serialize(stream, widgetData);
        stream.Close();
    }

    /// <summary>
    /// Loads widget settings data (WidgetSettingsDataSerial) from file.
    /// </summary>
    /// <returns></returns>
    public static WidgetSettingsDataSerial LoadWidgetSettingsData() {
        string path = Application.persistentDataPath + "/settings/widget.settings";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream         = new FileStream(path, FileMode.Open);

            WidgetSettingsDataSerial widgetData = formatter.Deserialize(stream) as WidgetSettingsDataSerial;
            stream.Close();

            return widgetData;
        } else {
            return null;
        }
    }

    /// <summary>
    /// Inits saved extra settings object and sets all extra settings values.
    /// </summary>
    public static void InitSavedWidgetSettings() {
        WidgetSettingsDataSerial loadedWidgetData = LoadWidgetSettingsData();
        if (loadedWidgetData != null) {
            WidgetSettings.LoadWidgetSettings(loadedWidgetData);

            SetWidget(widgetSave.showFPSToggle, widgetSave.showFPSWidget, loadedWidgetData.showFPS);
            SetWidget(widgetSave.showTimeToggle, widgetSave.showTimeWidget, loadedWidgetData.showTime);
            SetWidget(widgetSave.showScoreToggle, widgetSave.showScoreWidget, loadedWidgetData.showScore);
            SetWidget(widgetSave.showAccuracyToggle, widgetSave.showAccuracyWidget, loadedWidgetData.showAccuracy);
            SetWidget(widgetSave.showStreakToggle, widgetSave.showStreakWidget, loadedWidgetData.showStreak);
            SetWidget(widgetSave.showTTKToggle, widgetSave.showTTKWidget, loadedWidgetData.showTTK);
            SetWidget(widgetSave.showKPSToggle, widgetSave.showKPSWidget, loadedWidgetData.showKPS);
        } else {
            //Debug.Log("failed to init extra in 'initSavedSettings', extra: " + loadedExtraData);
            InitWidgetSettingsDefaults();
        }
    }

    /// <summary>
    /// Inits default widget settings values and saves to file on first launch.
    /// </summary>
    public static void InitWidgetSettingsDefaults() {
        SetWidget(widgetSave.showFPSToggle, widgetSave.showFPSWidget, false);
        SetWidget(widgetSave.showTimeToggle, widgetSave.showTimeWidget, true);
        SetWidget(widgetSave.showScoreToggle, widgetSave.showScoreWidget, true);
        SetWidget(widgetSave.showAccuracyToggle, widgetSave.showAccuracyWidget, true);
        SetWidget(widgetSave.showStreakToggle, widgetSave.showStreakWidget, false);
        SetWidget(widgetSave.showTTKToggle, widgetSave.showTTKWidget, false);
        SetWidget(widgetSave.showKPSToggle, widgetSave.showKPSWidget, false);

        WidgetSettings.SaveAllWidgetSettingsDefaults(false, true, true, true, false, false, false);
    }

    /// <summary>
    /// Sets show item value and toggle to supplied bool (setShowValue).
    /// </summary>
    /// <param name="showItemToggle"></param>
    /// <param name="showItemObject"></param>
    /// <param name="setShowValue"></param>
    private static void SetWidget(Toggle showItemToggle, GameObject showItemObject, bool setShowValue) {
        showItemToggle.isOn = setShowValue;
        showItemObject.SetActive(setShowValue);
    }
}
