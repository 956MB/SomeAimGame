using UnityEngine;

namespace SomeAimGame.Utilities {
    public class KeybindsUtil : MonoBehaviour {
        /// <summary>
        /// Returns saved keycode for supplied keybind button name (buttonName).
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        public static KeyCode GetButtonKeybind(string buttonName) {
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
                case KeyCode.Mouse2:            return "MOUSE 3";
                case KeyCode.Mouse3:            return "MOUSE 4";
                case KeyCode.Mouse4:            return "MOUSE 5";
                case KeyCode.Space:             return "SPACE";
                case KeyCode.LeftBracket:       return "[";
                case KeyCode.RightBracket:      return "]";
                case KeyCode.LeftCurlyBracket:  return "}";
                case KeyCode.RightCurlyBracket: return "}";
                default:                        return fullKeycode.ToString();
            }
        }
    }
}
