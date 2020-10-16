
[System.Serializable]
public class ExtraSettingsDataSerial {
    public int gameTimer;
    public bool targetSound;
    public bool uiSound;
    public bool movementLock;
    public bool fpsCounter;
    public bool showAAR;
    public float mouseSensitivity;
    public bool hideUI;
    public bool showExtraStats;

    public ExtraSettingsDataSerial() {
        gameTimer        = ExtraSettings.gameTimer;
        targetSound      = ExtraSettings.targetSound;
        uiSound          = ExtraSettings.uiSound;
        movementLock     = ExtraSettings.movementLock;
        fpsCounter       = ExtraSettings.fpsCounter;
        showAAR          = ExtraSettings.showAAR;
        mouseSensitivity = ExtraSettings.mouseSensitivity;
        hideUI           = ExtraSettings.hideUI;
        showExtraStats   = ExtraSettings.showExtraStats;
    }
}
