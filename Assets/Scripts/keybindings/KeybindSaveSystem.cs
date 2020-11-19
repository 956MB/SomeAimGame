using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class KeybindSaveSystem : MonoBehaviour {
    public TMP_Text shootKeybindText, toggleWidgetsKeybindText, gameRestartKeybindText, toggleAARKeybindText, toggleSettingsKeybindText;

    private static KeybindSaveSystem keybindSaveLoad;
    void Awake() { keybindSaveLoad = this; }

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

    public static void InitSavedKeybindSettings() {
        KeybindDataSerial loadedKeybindData = LoadKeybindSettingsData();

        if (loadedKeybindData != null) {
            SetShootKeybind(loadedKeybindData.shoot);
            SetToggleWidgetsKeybind(loadedKeybindData.toggleWidgets);
            SetGameRestartKeybind(loadedKeybindData.gameRestart);
            SetToggleAARKeybind(loadedKeybindData.toggleAAR);
            SetToggleSettingsKeybind(loadedKeybindData.toggleSettings);

            KeybindSettings.LoadKeybindSettings(loadedKeybindData);
        } else {
            InitKeybindSettingsDefaults();
        }
    }

    public static void InitKeybindSettingsDefaults() {
        SetShootKeybind(KeyCode.Mouse0);
        SetToggleWidgetsKeybind(KeyCode.H);
        SetGameRestartKeybind(KeyCode.R);
        SetToggleAARKeybind(KeyCode.Tab);
        SetToggleSettingsKeybind(KeyCode.Escape);

        // Saves defaults to new 'keybind.settings' file.
        KeybindSettings.SaveAllKeybindDefaults(KeyCode.Mouse0, KeyCode.H, KeyCode.R, KeyCode.Tab, KeyCode.Escape);
    }

    public static void SetShootKeybind(KeyCode setShootKeycode) {
        keybindSaveLoad.shootKeybindText.SetText(KeybindsHandler.ReturnKeybindShort(setShootKeycode));
    }
    public static void SetToggleWidgetsKeybind(KeyCode setToggleWidgetsKeycode) {
        keybindSaveLoad.toggleWidgetsKeybindText.SetText(KeybindsHandler.ReturnKeybindShort(setToggleWidgetsKeycode));
    }
    public static void SetGameRestartKeybind(KeyCode setGameRestartKeycode) {
        keybindSaveLoad.gameRestartKeybindText.SetText(KeybindsHandler.ReturnKeybindShort(setGameRestartKeycode));
    }
    public static void SetToggleAARKeybind(KeyCode setToggleAARKeycode) {
        keybindSaveLoad.toggleAARKeybindText.SetText(KeybindsHandler.ReturnKeybindShort(setToggleAARKeycode));
    }
    public static void SetToggleSettingsKeybind(KeyCode setToggleSettingsKeycode) {
        keybindSaveLoad.toggleSettingsKeybindText.SetText(KeybindsHandler.ReturnKeybindShort(setToggleSettingsKeycode));
    }
}
