
[System.Serializable]
public class ExtraSettingsDataSerial {
    public int gameTimer;
    public bool targetSound;
    public bool uiSound;
    public bool showAAR;
    public float mouseSensitivity;
    public bool hideUI;
    public bool showExtraStats;
    public bool showExtraStatsBackgrounds;

    public ExtraSettingsDataSerial() {
        gameTimer        = ExtraSettings.gameTimer;
        targetSound      = ExtraSettings.targetSound;
        uiSound          = ExtraSettings.uiSound;
        showAAR          = ExtraSettings.showAAR;
        mouseSensitivity = ExtraSettings.mouseSensitivity;
        hideUI           = ExtraSettings.hideUI;
        showExtraStats   = ExtraSettings.showExtraStats;
        showExtraStatsBackgrounds = ExtraSettings.showExtraStatsBackgrounds;
    }
}
