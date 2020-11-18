
[System.Serializable]
public class WidgetSettingsDataSerial {
    public bool showMode;
    public bool showFPS;
    public bool showTime;
    public bool showScore;
    public bool showAccuracy;
    public bool showStreak;
    public bool showTTK;
    public bool showKPS;

    public WidgetSettingsDataSerial() {
        showMode     = WidgetSettings.showMode;
        showFPS      = WidgetSettings.showFPS;
        showTime     = WidgetSettings.showTime;
        showScore    = WidgetSettings.showScore;
        showAccuracy = WidgetSettings.showAccuracy;
        showStreak   = WidgetSettings.showStreak;
        showTTK      = WidgetSettings.showTTK;
        showKPS      = WidgetSettings.showKPS;
    }
}
