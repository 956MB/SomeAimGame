using UnityEngine;

[System.Serializable]
public class KeybindDataSerial {
    public KeyCode shoot;
    public KeyCode toggleWidgets;
    public KeyCode gameRestart;
    public KeyCode toggleAAR;
    public KeyCode toggleSettings;

    public KeybindDataSerial() {
        shoot          = KeybindSettings.shoot;
        toggleWidgets  = KeybindSettings.toggleWidgets;
        gameRestart    = KeybindSettings.gameRestart;
        toggleAAR      = KeybindSettings.toggleAAR;
        toggleSettings = KeybindSettings.toggleSettings;
    }
}
