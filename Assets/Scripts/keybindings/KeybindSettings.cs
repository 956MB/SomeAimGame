using UnityEngine;

public class KeybindSettings : MonoBehaviour {
    public static KeyCode shoot          = KeyCode.Mouse0;
    public static KeyCode toggleWidgets  = KeyCode.H;
    public static KeyCode gameRestart    = KeyCode.R;
    public static KeyCode toggleAAR      = KeyCode.Tab;
    public static KeyCode toggleSettings = KeyCode.Escape;

    private static KeybindSettings keybindSettings;
    void Awake() { keybindSettings = this; }

    /// <summary>
    /// Saves supplied shoot keybind keycode (setShoot) to keybind settings object (KeybindSettings), then saves keybind settings object.
    /// </summary>
    /// <param name="setShoot"></param>
    public static void SaveShootKeybindItem(KeyCode setShoot) {
        shoot = setShoot;
        keybindSettings.SaveKeybindSettings();
    }

    /// <summary>
    /// Saves supplied toggle widgets keybind keycode (setToggleWidgets) to keybind settings object (KeybindSettings), then saves keybind settings object.
    /// </summary>
    /// <param name="setToggleWidgets"></param>
    public static void SaveToggleWidgetsKeybindItem(KeyCode setToggleWidgets) {
        toggleWidgets = setToggleWidgets;
        keybindSettings.SaveKeybindSettings();
    }

    /// <summary>
    /// Saves supplied game restart keybind keycode (setGameRestart) to keybind settings object (KeybindSettings), then saves keybind settings object.
    /// </summary>
    /// <param name="setGameRestart"></param>
    public static void SaveGameRestartKeybindItem(KeyCode setGameRestart) {
        gameRestart = setGameRestart;
        keybindSettings.SaveKeybindSettings();
    }

    /// <summary>
    /// Saves supplied toggle AAR keybind keycode (setToggleAAR) to keybind settings object (KeybindSettings), then saves keybind settings object.
    /// </summary>
    /// <param name="setToggleAAR"></param>
    public static void SaveToggleAARKeybindItem(KeyCode setToggleAAR) {
        toggleAAR = setToggleAAR;
        keybindSettings.SaveKeybindSettings();
    }

    /// <summary>
    /// Saves supplied toggle settings keybind keycode (setToggleSettings) to keybind settings object (KeybindSettings), then saves keybind settings object.
    /// </summary>
    /// <param name="setToggleSettings"></param>
    public static void SaveToggleSettingsKeybindItem(KeyCode setToggleSettings) {
        toggleSettings = setToggleSettings;
        keybindSettings.SaveKeybindSettings();
    }

    /// <summary>
    /// Calls 'KeybindSaveSystem.SaveKeybindSettingsData()' to save keybind settings object (KeybindSettings) to file.
    /// </summary>
    public void SaveKeybindSettings() { KeybindSaveSystem.SaveKeybindSettingsData(this); }

    /// <summary>
    /// Saves default keybind settings object (KeybindSettings).
    /// </summary>
    /// <param name="setShoot"></param>
    /// <param name="setToggleWidgets"></param>
    /// <param name="setGameRestart"></param>
    /// <param name="setToggleAAR"></param>
    /// <param name="setToggleSettings"></param>
    public static void SaveAllKeybindDefaults(KeyCode setShoot, KeyCode setToggleWidgets, KeyCode setGameRestart, KeyCode setToggleAAR, KeyCode setToggleSettings) {
        shoot          = setShoot;
        toggleWidgets  = setToggleWidgets;
        gameRestart    = setGameRestart;
        toggleAAR      = setToggleAAR;
        toggleSettings = setToggleSettings;

        Keybinds.keybindsLoaded = true;
        keybindSettings.SaveKeybindSettings();
    }

    /// <summary>
    /// Loads keybind data (KeybindDataSerial) and sets values to this keybind settings object (KeybindSettings).
    /// </summary>
    /// <param name="keybindData"></param>
    public static void LoadKeybindSettings(KeybindDataSerial keybindData) {
        shoot          = keybindData.shoot;
        toggleWidgets  = keybindData.toggleWidgets;
        gameRestart    = keybindData.gameRestart;
        toggleAAR      = keybindData.toggleAAR;
        toggleSettings = keybindData.toggleSettings;

        Keybinds.keybindsLoaded = true;
    }
}
