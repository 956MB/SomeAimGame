using UnityEngine;

public class WidgetSettings : MonoBehaviour {
    public static bool showFPS      = false;
    public static bool showTime     = true;
    public static bool showScore    = true;
    public static bool showAccuracy = true;
    public static bool showStreak   = false;
    public static bool showTTK      = false;
    public static bool showKPS      = false;

    private static WidgetSettings widgetSettings;
    void Awake() { widgetSettings = this; }

    /// <summary>
    /// Saves supplied showFPS bool (setShowFPS) to widget settings object (WidgetSettings), then saves widget settings object.
    /// </summary>
    /// <param name="setShowFPS"></param>
    public static void SaveShowFPSItem(bool setShowFPS) {
        showFPS = setShowFPS;
        widgetSettings.SaveWidgetSettings();
    }
    /// <summary>
    /// Saves supplied showTime bool (setShowTime) to widget settings object (WidgetSettings), then saves widget settings object.
    /// </summary>
    /// <param name="setShowTime"></param>
    public static void SaveShowTimeItem(bool setShowTime) {
        showTime = setShowTime;
        widgetSettings.SaveWidgetSettings();
    }
    /// <summary>
    /// Saves supplied showScore bool (setShowScore) to widget settings object (WidgetSettings), then saves widget settings object.
    /// </summary>
    /// <param name="setShowScore"></param>
    public static void SaveShowScoreItem(bool setShowScore) {
        showScore = setShowScore;
        widgetSettings.SaveWidgetSettings();
    }
    /// <summary>
    /// Saves supplied showAccuracy bool (setShowAccuracy) to widget settings object (WidgetSettings), then saves widget settings object.
    /// </summary>
    /// <param name="setShowAccuracy"></param>
    public static void SaveShowAccuracyItem(bool setShowAccuracy) {
        showAccuracy = setShowAccuracy;
        widgetSettings.SaveWidgetSettings();
    }
    /// <summary>
    /// Saves supplied showStreak bool (setShowStreak) to widget settings object (WidgetSettings), then saves widget settings object.
    /// </summary>
    /// <param name="setShowStreak"></param>
    public static void SaveShowStreakItem(bool setShowStreak) {
        showStreak = setShowStreak;
        widgetSettings.SaveWidgetSettings();
    }
    /// <summary>
    /// Saves supplied showTTK bool (setShowTTK) to widget settings object (WidgetSettings), then saves widget settings object.
    /// </summary>
    /// <param name="setShowTTK"></param>
    public static void SaveShowTTKItem(bool setShowTTK) {
        showTTK = setShowTTK;
        widgetSettings.SaveWidgetSettings();
    }
    /// <summary>
    /// Saves supplied showKPS float (setShowKPS) to widget settings object (WidgetSettings), then saves widget settings object.
    /// </summary>
    /// <param name="setShowKPS"></param>
    public static void SaveShowKPSItem(bool setShowKPS) {
        showKPS = setShowKPS;
        widgetSettings.SaveWidgetSettings();
    }

    /// <summary>
    /// Calls 'WidgetSaveSystem.SaveWidgetSettingsData()' to save widget settings object (WidgetSettings) to file.
    /// </summary>
    public void SaveWidgetSettings() { WidgetSaveSystem.SaveWidgetSettingsData(this); }

    /// <summary>
    /// Saves default widget settings object (WidgetSettings).
    /// </summary>
    /// <param name="setShowFPS"></param>
    /// <param name="setShowTime"></param>
    /// <param name="setShowScore"></param>
    /// <param name="setShowAccuracy"></param>
    /// <param name="setShowStreak"></param>
    /// <param name="setShowTTK"></param>
    /// <param name="setShowKPS"></param>
    public static void SaveAllWidgetSettingsDefaults(bool setShowFPS, bool setShowTime, bool setShowScore, bool setShowAccuracy, bool setShowStreak, bool setShowTTK, bool setShowKPS) {
        showFPS      = setShowFPS;
        showTime     = setShowTime;
        showScore    = setShowScore;
        showAccuracy = setShowAccuracy;
        showStreak   = setShowStreak;
        showTTK      = setShowTTK;
        showKPS      = setShowKPS;

        widgetSettings.SaveWidgetSettings();
    }

    /// <summary>
    /// Loads widget settings data (WidgetSettingsDataSerial) and sets values to this widget settings object (WidgetSettings).
    /// </summary>
    /// <param name="widgetData"></param>
    public static void LoadWidgetSettings(WidgetSettingsDataSerial widgetData) {
        showFPS      = widgetData.showFPS;
        showTime     = widgetData.showTime;
        showScore    = widgetData.showScore;
        showAccuracy = widgetData.showAccuracy;
        showStreak   = widgetData.showStreak;
        showTTK      = widgetData.showTTK;
        showKPS      = widgetData.showKPS;
    }
}
