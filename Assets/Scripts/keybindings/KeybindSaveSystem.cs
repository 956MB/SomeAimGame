using UnityEngine;
using TMPro;

using SomeAimGame.Utilities;

public class KeybindSaveSystem : MonoBehaviour {
    public TMP_Text shootKeybindText, toggleWidgetsKeybindText, gameRestartKeybindText, toggleAARKeybindText, toggleSettingsKeybindText, toggleConsoleKeybindText;
    public TMP_Text AARrestartButton, AARcloseButton;

    private static KeybindSaveSystem keybindSaveLoad;
    void Awake() { keybindSaveLoad = this; }

    /// <summary>
    /// Saves supplied keybinds object (KeybindSettings) to file.
    /// </summary>
    /// <param name="keybindSettings"></param>
    public static void SaveKeybindSettingsData() {
        KeybindDataSerial keybindData = new KeybindDataSerial();
        SaveLoadUtil.SaveDataSerial("/prefs", "/sag_keybinds.prefs", keybindData);
    }

    /// <summary>
    /// Loads keybinds data (KeybindDataSerial) from file.
    /// </summary>
    /// <returns></returns>
    public static KeybindDataSerial LoadKeybindSettingsData() {
        KeybindDataSerial keybindData = (KeybindDataSerial)SaveLoadUtil.LoadDataSerial("/prefs/sag_keybinds.prefs", SaveType.KEYBINDS);
        return keybindData;
    }

    /// <summary>
    /// Inits saved keybinds object and sets all keybind values.
    /// </summary>
    public static void InitSavedKeybindSettings() {
        KeybindDataSerial loadedKeybindData = LoadKeybindSettingsData();

        if (loadedKeybindData != null) {
            SetShootKeybind(loadedKeybindData.shoot);
            SetToggleWidgetsKeybind(loadedKeybindData.toggleWidgets);
            SetGameRestartKeybind(loadedKeybindData.gameRestart);
            SetToggleAARKeybind(loadedKeybindData.toggleAAR);
            SetToggleSettingsKeybind(loadedKeybindData.toggleSettings);
            SetAARButtons(loadedKeybindData.gameRestart, loadedKeybindData.toggleSettings);
            SetToggleConsoleKeybind(loadedKeybindData.toggleConsole);

            KeybindSettings.LoadKeybindSettings(loadedKeybindData);
        } else {
            InitKeybindSettingsDefaults();
        }
    }

    /// <summary>
    /// Inits default keybind values and saves to file on first launch.
    /// </summary>
    public static void InitKeybindSettingsDefaults() {
        SetShootKeybind(KeyCode.Mouse0);
        SetToggleWidgetsKeybind(KeyCode.H);
        SetGameRestartKeybind(KeyCode.R);
        SetToggleAARKeybind(KeyCode.Tab);
        SetToggleSettingsKeybind(KeyCode.Escape);
        SetAARButtons(KeyCode.R, KeyCode.Escape);
        SetToggleConsoleKeybind(KeyCode.C);

        // Saves defaults to new 'keybind.settings' file.
        KeybindSettings.SaveAllKeybindDefaults(KeyCode.Mouse0, KeyCode.H, KeyCode.R, KeyCode.Tab, KeyCode.Escape, KeyCode.C);
    }

    /// <summary>
    /// Sets 'shoot' keybind value text to supplied keycode (setShootKeycode), and sets white color.
    /// </summary>
    /// <param name="setShootKeycode"></param>
    public static void SetShootKeybind(KeyCode setShootKeycode) {                   SetKeybindText(keybindSaveLoad.shootKeybindText, KeybindsUtil.ReturnKeybindString(setShootKeycode)); }
    /// <summary>
    /// Sets 'toggleWidgets' keybind value text to supplied keycode (setToggleWidgetsKeycode), and sets white color.
    /// </summary>
    /// <param name="setToggleWidgetsKeycode"></param>
    public static void SetToggleWidgetsKeybind(KeyCode setToggleWidgetsKeycode) {   SetKeybindText(keybindSaveLoad.toggleWidgetsKeybindText, KeybindsUtil.ReturnKeybindString(setToggleWidgetsKeycode)); }
    /// <summary>
    /// Sets 'gameRestart' keybind value text to supplied keycode (setGameRestartKeycode), and sets white color.
    /// </summary>
    /// <param name="setGameRestartKeycode"></param>
    public static void SetGameRestartKeybind(KeyCode setGameRestartKeycode) {       SetKeybindText(keybindSaveLoad.gameRestartKeybindText, KeybindsUtil.ReturnKeybindString(setGameRestartKeycode)); }
    /// <summary>
    /// Sets 'toggleAAR' keybind value text to supplied keycode (setToggleAARKeycode), and sets white color.
    /// </summary>
    /// <param name="setToggleAARKeycode"></param>
    public static void SetToggleAARKeybind(KeyCode setToggleAARKeycode) {           SetKeybindText(keybindSaveLoad.toggleAARKeybindText, KeybindsUtil.ReturnKeybindString(setToggleAARKeycode)); }
    /// <summary>
    /// Sets 'toggleSettings' keybind value text to supplied keycode (setToggleSettingsKeycode), and sets white color.
    /// </summary>
    /// <param name="setToggleSettingsKeycode"></param>
    public static void SetToggleSettingsKeybind(KeyCode setToggleSettingsKeycode) { SetKeybindText(keybindSaveLoad.toggleSettingsKeybindText, KeybindsUtil.ReturnKeybindString(setToggleSettingsKeycode)); }
    /// <summary>
    /// Sets 'toggleConsole' keybind value text to supplied keycode (setToggleConsoleKeycode), and sets white color.
    /// </summary>
    /// <param name="setToggleConsoleKeycode"></param>
    public static void SetToggleConsoleKeybind(KeyCode setToggleConsoleKeycode) {   SetKeybindText(keybindSaveLoad.toggleConsoleKeybindText, KeybindsUtil.ReturnKeybindString(setToggleConsoleKeycode)); }

    /// <summary>
    /// Sets AAR restart and close button text with supplied keycodes (restartKeyCode, escapeKeyCode).
    /// </summary>
    /// <param name="restartKeyCode"></param>
    /// <param name="escapeKeyCode"></param>
    public static void SetAARButtons(KeyCode restartKeyCode, KeyCode escapeKeyCode) {
        keybindSaveLoad.AARrestartButton.SetText($"{I18nTextTranslator.SetTranslatedText("aarrestart")}  [{KeybindsUtil.ReturnKeybindString(restartKeyCode)}]");
        keybindSaveLoad.AARcloseButton.SetText($"{I18nTextTranslator.SetTranslatedText("aarclose")}  [{KeybindsUtil.ReturnKeybindString(escapeKeyCode)}]");
    }

    /// <summary>
    /// Set supplied keybind TMP_Text (keybindText) to supplied string (setText).
    /// </summary>
    /// <param name="keybindText"></param>
    /// <param name="setText"></param>
    private static void SetKeybindText(TMP_Text keybindText, string setText) {
        keybindText.SetText(setText);
        keybindText.color = InterfaceColors.selectedColor;
        keybindText.fontSize = setText == "`" ? 26f : 14f;
    }
}
