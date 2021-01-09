using UnityEngine;

using SomeAimGame.Utilities;

public class KeybindSettings : MonoBehaviour {
    public static KeyCode shoot          = KeyCode.Mouse0;
    public static KeyCode toggleWidgets  = KeyCode.H;
    public static KeyCode gameRestart    = KeyCode.R;
    public static KeyCode toggleAAR      = KeyCode.Tab;
    public static KeyCode toggleSettings = KeyCode.Escape;

    static bool keybindsSettingsChangeReady = false;

    private static KeybindSettings keybindSettings;
    void Awake() { keybindSettings = this; }

    /// <summary>
    /// Saves supplied shoot keybind keycode (setShoot) to keybind settings object (KeybindSettings), then saves keybind settings object.
    /// </summary>
    /// <param name="setShoot"></param>
    public static void SaveShootKeybindItem(KeyCode setShoot) { Util.RefSetSettingChange(ref keybindsSettingsChangeReady, ref shoot, setShoot); }
    /// <summary>
    /// Saves supplied toggle widgets keybind keycode (setToggleWidgets) to keybind settings object (KeybindSettings), then saves keybind settings object.
    /// </summary>
    /// <param name="setToggleWidgets"></param>
    public static void SaveToggleWidgetsKeybindItem(KeyCode setToggleWidgets) { Util.RefSetSettingChange(ref keybindsSettingsChangeReady, ref toggleWidgets, setToggleWidgets); }
    /// <summary>
    /// Saves supplied game restart keybind keycode (setGameRestart) to keybind settings object (KeybindSettings), then saves keybind settings object.
    /// </summary>
    /// <param name="setGameRestart"></param>
    public static void SaveGameRestartKeybindItem(KeyCode setGameRestart) { Util.RefSetSettingChange(ref keybindsSettingsChangeReady, ref gameRestart, setGameRestart); }
    /// <summary>
    /// Saves supplied toggle AAR keybind keycode (setToggleAAR) to keybind settings object (KeybindSettings), then saves keybind settings object.
    /// </summary>
    /// <param name="setToggleAAR"></param>
    public static void SaveToggleAARKeybindItem(KeyCode setToggleAAR) { Util.RefSetSettingChange(ref keybindsSettingsChangeReady, ref toggleAAR, setToggleAAR); }
    /// <summary>
    /// Saves supplied toggle settings keybind keycode (setToggleSettings) to keybind settings object (KeybindSettings), then saves keybind settings object.
    /// </summary>
    /// <param name="setToggleSettings"></param>
    public static void SaveToggleSettingsKeybindItem(KeyCode setToggleSettings) { Util.RefSetSettingChange(ref keybindsSettingsChangeReady, ref toggleSettings, setToggleSettings); }
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

    public static void CheckSaveKeybindSettings() {
        if (keybindsSettingsChangeReady) {
            keybindSettings.SaveKeybindSettings();
            keybindsSettingsChangeReady = false;
        }
    }
}
