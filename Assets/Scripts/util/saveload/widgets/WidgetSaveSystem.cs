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
            SetAllWidgets(loadedWidgetData.showMode, loadedWidgetData.showFPS, loadedWidgetData.showTime, loadedWidgetData.showScore, loadedWidgetData.showAccuracy, loadedWidgetData.showStreak, loadedWidgetData.showTTK, loadedWidgetData.showKPS);

            WidgetSettings.LoadWidgetSettings(loadedWidgetData);
        } else {
            //Debug.Log("failed to init extra in 'initSavedSettings', extra: " + loadedExtraData);
            InitWidgetSettingsDefaults();
        }
    }

    /// <summary>
    /// Inits default widget settings values and saves to file on first launch.
    /// </summary>
    public static void InitWidgetSettingsDefaults() {
        SetAllWidgets(true, false, true, true, true, false, false, false);

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

    /// <summary>
    /// Sets all widgets to supplied bool values.
    /// </summary>
    /// <param name="setShowMode"></param>
    /// <param name="setShowFPS"></param>
    /// <param name="setShowTime"></param>
    /// <param name="setShowScore"></param>
    /// <param name="setShowAccuracy"></param>
    /// <param name="setShowStreak"></param>
    /// <param name="setShowTTK"></param>
    /// <param name="setShowKPS"></param>
    private static void SetAllWidgets(bool setShowMode, bool setShowFPS, bool setShowTime, bool setShowScore, bool setShowAccuracy, bool setShowStreak, bool setShowTTK, bool setShowKPS) {
        SetWidget(widgetSave.showModeToggle, widgetSave.showModeWidget, setShowMode);
        SetWidget(widgetSave.showFPSToggle, widgetSave.showFPSWidget, setShowFPS);
        SetWidget(widgetSave.showTimeToggle, widgetSave.showTimeWidget, setShowTime);
        SetWidget(widgetSave.showScoreToggle, widgetSave.showScoreWidget, setShowScore);
        SetWidget(widgetSave.showAccuracyToggle, widgetSave.showAccuracyWidget, setShowAccuracy);
        SetWidget(widgetSave.showStreakToggle, widgetSave.showStreakWidget, setShowStreak);
        SetWidget(widgetSave.showTTKToggle, widgetSave.showTTKWidget, setShowTTK);
        SetWidget(widgetSave.showKPSToggle, widgetSave.showKPSWidget, setShowKPS);
    }
}
