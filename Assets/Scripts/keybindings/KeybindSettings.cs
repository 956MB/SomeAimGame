using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindSettings : MonoBehaviour {
    public static KeyCode shoot          = KeyCode.Mouse0;
    public static KeyCode toggleWidgets  = KeyCode.H;
    public static KeyCode gameRestart    = KeyCode.R;
    public static KeyCode toggleAAR      = KeyCode.Tab;
    public static KeyCode toggleSettings = KeyCode.Escape;

    private static KeybindSettings keybindSettings;
    void Awake() { keybindSettings = this; }

    public static void SaveShootKeybindItem(KeyCode setShoot) {
        shoot = setShoot;
        keybindSettings.SaveKeybindSettings();
    }

    public static void SaveToggleWidgetsKeybindItem(KeyCode setToggleWidgets) {
        toggleWidgets = setToggleWidgets;
        keybindSettings.SaveKeybindSettings();
    }

    public static void SaveGameRestartKeybindItem(KeyCode setGameRestart) {
        gameRestart = setGameRestart;
        keybindSettings.SaveKeybindSettings();
    }

    public static void SaveToggleAARKeybindItem(KeyCode setToggleAAR) {
        toggleAAR = setToggleAAR;
        keybindSettings.SaveKeybindSettings();
    }

    public static void SaveToggleSettingsKeybindItem(KeyCode setToggleSettings) {
        toggleSettings = setToggleSettings;
        keybindSettings.SaveKeybindSettings();
    }

    public void SaveKeybindSettings() { KeybindSaveSystem.SaveKeybindSettingsData(this); }

    public static void SaveAllKeybindDefaults(KeyCode setShoot, KeyCode setToggleWidgets, KeyCode setGameRestart, KeyCode setToggleAAR, KeyCode setToggleSettings) {
        shoot          = setShoot;
        toggleWidgets  = setToggleWidgets;
        gameRestart    = setGameRestart;
        toggleAAR      = setToggleAAR;
        toggleSettings = setToggleSettings;

        Keybinds.keybindsLoaded = true;
        keybindSettings.SaveKeybindSettings();
    }

    public static void LoadKeybindSettings(KeybindDataSerial keybindData) {
        shoot          = keybindData.shoot;
        toggleWidgets  = keybindData.toggleWidgets;
        gameRestart    = keybindData.gameRestart;
        toggleAAR      = keybindData.toggleAAR;
        toggleSettings = keybindData.toggleSettings;

        Keybinds.keybindsLoaded = true;
    }
}
