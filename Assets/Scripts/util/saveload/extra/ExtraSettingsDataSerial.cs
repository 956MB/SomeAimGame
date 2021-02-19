
[System.Serializable]
public class ExtraSettingsDataSerial {
    public int   gameTimer;
    public bool  showCountdown;
    public bool  showAAR;
    public float mouseSensitivity;
    public bool  showWidgets;
    public bool  showExtraStats;
    public bool  showExtraStatsBackgrounds;

    public ExtraSettingsDataSerial() {
        gameTimer                 = ExtraSettings.gameTimer;
        showCountdown             = ExtraSettings.showCountdown;
        showAAR                   = ExtraSettings.showAAR;
        mouseSensitivity          = ExtraSettings.mouseSensitivity;
        showWidgets               = ExtraSettings.showWidgets;
        showExtraStats            = ExtraSettings.showExtraStats;
        showExtraStatsBackgrounds = ExtraSettings.showExtraStatsBackgrounds;
    }
}
