using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

using SomeAimGame.Utilities;

public class KeybindSaveSystem : MonoBehaviour {
    public TMP_Text shootKeybindText, toggleWidgetsKeybindText, gameRestartKeybindText, toggleAARKeybindText, toggleSettingsKeybindText;
    public TMP_Text AARrestartButton, AARcloseButton;

    private static KeybindSaveSystem keybindSaveLoad;
    void Awake() { keybindSaveLoad = this; }

    /// <summary>
    /// Saves supplied keybinds object (KeybindSettings) to file.
    /// </summary>
    /// <param name="keybindSettings"></param>
    public static void SaveKeybindSettingsData(KeybindSettings keybindSettings) {
        BinaryFormatter formatter = new BinaryFormatter();
        string dirPath = Application.persistentDataPath + "/settings";
        string filePath = dirPath + "/keybind.settings";

        DirectoryInfo dirInf = new DirectoryInfo(dirPath);
        if (!dirInf.Exists) { dirInf.Create(); }

        FileStream stream = new FileStream(filePath, FileMode.Create);
        KeybindDataSerial keybindData = new KeybindDataSerial();
        formatter.Serialize(stream, keybindData);
        stream.Close();
    }

    /// <summary>
    /// Loads keybinds data (KeybindDataSerial) from file.
    /// </summary>
    /// <returns></returns>
    public static KeybindDataSerial LoadKeybindSettingsData() {
        string path = Application.persistentDataPath + "/settings/keybind.settings";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            KeybindDataSerial keybindData = formatter.Deserialize(stream) as KeybindDataSerial;
            stream.Close();

            return keybindData;
        } else {
            return null;
        }
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

        // Saves defaults to new 'keybind.settings' file.
        KeybindSettings.SaveAllKeybindDefaults(KeyCode.Mouse0, KeyCode.H, KeyCode.R, KeyCode.Tab, KeyCode.Escape);
    }

    /// <summary>
    /// Sets 'shoot' keybind value text to supplied keycode (setShootKeycode), and sets white color.
    /// </summary>
    /// <param name="setShootKeycode"></param>
    public static void SetShootKeybind(KeyCode setShootKeycode) {
        keybindSaveLoad.shootKeybindText.SetText(KeybindsHandler.ReturnKeybindString(setShootKeycode));
        keybindSaveLoad.shootKeybindText.color = InterfaceColors.selectedColor;
    }
    /// <summary>
    /// Sets 'toggleWidgets' keybind value text to supplied keycode (setToggleWidgetsKeycode), and sets white color.
    /// </summary>
    /// <param name="setToggleWidgetsKeycode"></param>
    public static void SetToggleWidgetsKeybind(KeyCode setToggleWidgetsKeycode) {
        keybindSaveLoad.toggleWidgetsKeybindText.SetText(KeybindsHandler.ReturnKeybindString(setToggleWidgetsKeycode));
        keybindSaveLoad.toggleWidgetsKeybindText.color = InterfaceColors.selectedColor;
    }
    /// <summary>
    /// Sets 'gameRestart' keybind value text to supplied keycode (setGameRestartKeycode), and sets white color.
    /// </summary>
    /// <param name="setGameRestartKeycode"></param>
    public static void SetGameRestartKeybind(KeyCode setGameRestartKeycode) {
        keybindSaveLoad.gameRestartKeybindText.SetText(KeybindsHandler.ReturnKeybindString(setGameRestartKeycode));
        keybindSaveLoad.gameRestartKeybindText.color = InterfaceColors.selectedColor;
    }
    /// <summary>
    /// Sets 'toggleAAR' keybind value text to supplied keycode (setToggleAARKeycode), and sets white color.
    /// </summary>
    /// <param name="setToggleAARKeycode"></param>
    public static void SetToggleAARKeybind(KeyCode setToggleAARKeycode) {
        keybindSaveLoad.toggleAARKeybindText.SetText(KeybindsHandler.ReturnKeybindString(setToggleAARKeycode));
        keybindSaveLoad.toggleAARKeybindText.color = InterfaceColors.selectedColor;
    }
    /// <summary>
    /// Sets 'toggleSettings' keybind value text to supplied keycode (setToggleSettingsKeycode), and sets white color.
    /// </summary>
    /// <param name="setToggleSettingsKeycode"></param>
    public static void SetToggleSettingsKeybind(KeyCode setToggleSettingsKeycode) {
        keybindSaveLoad.toggleSettingsKeybindText.SetText(KeybindsHandler.ReturnKeybindString(setToggleSettingsKeycode));
        keybindSaveLoad.toggleSettingsKeybindText.color = InterfaceColors.selectedColor;
    }
    /// <summary>
    /// Sets AAR restart and close button text with supplied keycodes (restartKeyCode, escapeKeyCode).
    /// </summary>
    /// <param name="restartKeyCode"></param>
    /// <param name="escapeKeyCode"></param>
    public static void SetAARButtons(KeyCode restartKeyCode, KeyCode escapeKeyCode) {
        keybindSaveLoad.AARrestartButton.SetText($"[{KeybindsHandler.ReturnKeybindString(restartKeyCode)}]  {I18nTextTranslator.SetTranslatedText("aarrestart")}");
        keybindSaveLoad.AARcloseButton.SetText($"[{KeybindsHandler.ReturnKeybindString(escapeKeyCode)}]  {I18nTextTranslator.SetTranslatedText("aarclose")}");
    }
}
