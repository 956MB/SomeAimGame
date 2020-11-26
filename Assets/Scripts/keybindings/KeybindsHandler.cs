using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class KeybindsHandler : MonoBehaviour, IPointerEnterHandler {
    private static KeyCode clickedKeycode;
    public static GameObject currentKey;

    private static KeybindsHandler keybindHandler;
    void Awake() { keybindHandler = this; }

    /// <summary>
    /// Sets new keybind to keybind being changed if current key is set.
    /// </summary>
    void OnGUI() {
        if (currentKey != null) {
            Event e = Event.current;
            if (e.isKey) {
                if (e.keyCode != clickedKeycode && e.keyCode != KeyCode.Escape) {
                    HandleNewKeybindSet(clickedKeycode, e.keyCode);
                } else {
                    currentKey.transform.GetChild(0).GetComponent<TMP_Text>().text  = ReturnKeybindString(clickedKeycode);
                    currentKey.transform.GetChild(0).GetComponent<TMP_Text>().color = InterfaceColors.selectedColor;
                }
                
                currentKey = null;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_HoverInner(); }
    }

    /// <summary>
    /// Sets clicked keybind button as current keybind to be changed.
    /// </summary>
    /// <param name="clickedButton"></param>
    public void ChangeKeybind(GameObject clickedButton) {
        currentKey     = clickedButton;
        clickedKeycode = GetButtonKeybind(currentKey.name);
        currentKey.transform.GetChild(0).GetComponent<TMP_Text>().text = "-";
        currentKey.transform.GetChild(0).GetComponent<TMP_Text>().color = InterfaceColors.unselectedColor;
    }

    /// <summary>
    /// Sets all UI keybind values for supplied clicked keycode (clickedKeycode) to new keycode (newKeycode), saves new keycode to keybind settings object, then shows notification.
    /// </summary>
    /// <param name="clickedKeycode"></param>
    /// <param name="newKeycode"></param>
    private void HandleNewKeybindSet(KeyCode clickedKeycode, KeyCode newKeycode) {
        if (clickedKeycode == KeybindSettings.shoot) {
            KeybindSaveSystem.SetShootKeybind(newKeycode);
            KeybindSettings.SaveShootKeybindItem(newKeycode);

            NotificationHandler.ShowTimedNotification_String($"{I18nTextTranslator.SetTranslatedText("keybindshoot")} \"{clickedKeycode}\" {I18nTextTranslator.SetTranslatedText("keybindrebinded")} \"{newKeycode}\".", InterfaceColors.notificationColorGreen);
        } else if (clickedKeycode == KeybindSettings.toggleSettings) {
            KeybindSaveSystem.SetToggleSettingsKeybind(newKeycode);
            KeybindSettings.SaveToggleSettingsKeybindItem(newKeycode);

            NotificationHandler.ShowTimedNotification_String($"{I18nTextTranslator.SetTranslatedText("keybindtogglesettings")} \"{clickedKeycode}\" {I18nTextTranslator.SetTranslatedText("keybindrebinded")} \"{newKeycode}\".", InterfaceColors.notificationColorGreen);
        } else if (clickedKeycode == KeybindSettings.toggleAAR) {
            KeybindSaveSystem.SetToggleAARKeybind(newKeycode);
            KeybindSettings.SaveToggleAARKeybindItem(newKeycode);

            NotificationHandler.ShowTimedNotification_String($"{I18nTextTranslator.SetTranslatedText("keybindtoggleaar")} \"{clickedKeycode}\" {I18nTextTranslator.SetTranslatedText("keybindrebinded")} \"{newKeycode}\".", InterfaceColors.notificationColorGreen);
        } else if (clickedKeycode == KeybindSettings.toggleWidgets) {
            KeybindSaveSystem.SetToggleWidgetsKeybind(newKeycode);
            KeybindSettings.SaveToggleWidgetsKeybindItem(newKeycode);

            NotificationHandler.ShowTimedNotification_String($"{I18nTextTranslator.SetTranslatedText("keybindtogglewidgets")} \"{clickedKeycode}\" {I18nTextTranslator.SetTranslatedText("keybindrebinded")} \"{newKeycode}\".", InterfaceColors.notificationColorGreen);
        } else if (clickedKeycode == KeybindSettings.gameRestart) {
            KeybindSaveSystem.SetGameRestartKeybind(newKeycode);
            KeybindSettings.SaveGameRestartKeybindItem(newKeycode);

            NotificationHandler.ShowTimedNotification_String($"{I18nTextTranslator.SetTranslatedText("keybindgamerestart")} \"{clickedKeycode}\" {I18nTextTranslator.SetTranslatedText("keybindrebinded")} \"{newKeycode}\".", InterfaceColors.notificationColorGreen);
        }
    }

    /// <summary>
    /// Returns saved keycode for supplied keybind button name (buttonName).
    /// </summary>
    /// <param name="buttonName"></param>
    /// <returns></returns>
    private static KeyCode GetButtonKeybind(string buttonName) {
        switch (buttonName) {
            case "Shoot-Button":          return KeybindSettings.shoot;
            case "ToggleSettings-Button": return KeybindSettings.toggleSettings;
            case "ToggleAAR-Button":      return KeybindSettings.toggleAAR;
            case "ToggleWidgets-Button":  return KeybindSettings.toggleWidgets;
            case "GameRestart-Button":    return KeybindSettings.gameRestart;
            default:                      return KeyCode.None;
        }
    }

    /// <summary>
    /// Returns short string version of supplied keycode (fullKeycode).
    /// </summary>
    /// <param name="fullKeycode"></param>
    /// <returns></returns>
    public static string ReturnKeybindString(KeyCode fullKeycode) {
        switch (fullKeycode) {
            case KeyCode.Escape:            return "ESCAPE";
            case KeyCode.Mouse0:            return "MOUSE 1";
            case KeyCode.Mouse1:            return "MOUSE 2";
            case KeyCode.Space:             return "SPACE";
            case KeyCode.LeftBracket:       return "[";
            case KeyCode.RightBracket:      return "]";
            case KeyCode.LeftCurlyBracket:  return "}";
            case KeyCode.RightCurlyBracket: return "}";
            default:                        return fullKeycode.ToString();
        }
    }
}
