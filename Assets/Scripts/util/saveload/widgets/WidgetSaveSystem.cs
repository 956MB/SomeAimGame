using UnityEngine;
using UnityEngine.UI;

using SomeAimGame.Utilities;

public class WidgetSaveSystem : MonoBehaviour {
    public Toggle showModeToggle, showFPSToggle, showTimeToggle, showScoreToggle, showAccuracyToggle, showStreakToggle, showTTKToggle, showKPSToggle;
    public GameObject showModeWidget, showFPSWidget, showTimeWidget, showScoreWidget, showAccuracyWidget, showStreakWidget, showTTKWidget, showKPSWidget;

    private static WidgetSaveSystem widgetSave;
    void Awake() { widgetSave = this; }

    /// <summary>
    /// Saves supplied widget settings object (WidgetSettings) to file.
    /// </summary>
    /// <param name="widgetSettings"></param>
    public static void SaveWidgetSettingsData() {
        WidgetSettingsDataSerial widgetData = new WidgetSettingsDataSerial();
        SaveLoadUtil.SaveDataSerial("/prefs", "/sag_widgets.prefs", widgetData);
    }

    /// <summary>
    /// Loads widget settings data (WidgetSettingsDataSerial) from file.
    /// </summary>
    /// <returns></returns>
    public static WidgetSettingsDataSerial LoadWidgetSettingsData() {
        WidgetSettingsDataSerial widgetData = (WidgetSettingsDataSerial)SaveLoadUtil.LoadDataSerial("/prefs/sag_widgets.prefs", SaveType.WIDGET);
        return widgetData;
    }

    /// <summary>
    /// Inits saved extra settings object and sets all extra settings values.
    /// </summary>
    public static void InitSavedWidgetSettings() {
        WidgetSettingsDataSerial loadedWidgetData = LoadWidgetSettingsData();
        if (loadedWidgetData != null) {
            WidgetSettings.LoadWidgetSettings(loadedWidgetData);

            SetWidget(widgetSave.showModeToggle, widgetSave.showModeWidget, loadedWidgetData.showMode);
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
        SetWidget(widgetSave.showModeToggle, widgetSave.showModeWidget, true);
        SetWidget(widgetSave.showFPSToggle, widgetSave.showFPSWidget, false);
        SetWidget(widgetSave.showTimeToggle, widgetSave.showTimeWidget, true);
        SetWidget(widgetSave.showScoreToggle, widgetSave.showScoreWidget, true);
        SetWidget(widgetSave.showAccuracyToggle, widgetSave.showAccuracyWidget, true);
        SetWidget(widgetSave.showStreakToggle, widgetSave.showStreakWidget, false);
        SetWidget(widgetSave.showTTKToggle, widgetSave.showTTKWidget, false);
        SetWidget(widgetSave.showKPSToggle, widgetSave.showKPSWidget, false);

        WidgetSettings.SaveAllWidgetSettingsDefaults(true, false, true, true, true, false, false, false);
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
