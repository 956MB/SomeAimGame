using System;
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
                case "ToggleConsole-Button":  return KeybindSettings.toggleConsole;
                default:                      return KeyCode.None;
            }
        }

        /// <summary>
        /// Returns KeyCode from supplied string (inputString) if valid, otherwise KeyCode.None.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static KeyCode ReturnKeyCodeFromString(string inputString) {
            KeyCode newKeyCode = (KeyCode)Enum.Parse(typeof(KeyCode), inputString) ;
            if (Enum.IsDefined(typeof(KeyCode), newKeyCode) && newKeyCode != KeyCode.None) {
                return newKeyCode;
            }

            return KeyCode.None;
        }

        /// <summary>
        /// Returns short string version of supplied keycode (fullKeycode).
        /// </summary>
        /// <param name="fullKeycode"></param>
        /// <returns></returns>
        public static string ReturnKeybindString(KeyCode fullKeycode) {
            switch (fullKeycode) {
                case KeyCode.Escape:            return "ESC";
                case KeyCode.Mouse0:            return "MB1";
                case KeyCode.Mouse1:            return "MB2";
                case KeyCode.Mouse2:            return "MB3";
                case KeyCode.Mouse3:            return "MB4";
                case KeyCode.Mouse4:            return "MB5";
                case KeyCode.Space:             return "SPACE";
                case KeyCode.LeftBracket:       return "[";
                case KeyCode.RightBracket:      return "]";
                case KeyCode.LeftCurlyBracket:  return "}";
                case KeyCode.RightCurlyBracket: return "}";
                case KeyCode.Tilde:             return "~";
                case KeyCode.BackQuote:         return "`";
                default:                        return fullKeycode.ToString();
            }
        }
    }
}
