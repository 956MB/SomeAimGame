using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using SomeAimGame.Utilities;
using SomeAimGame.Core;

namespace SomeAimGame.Console {
    public class SAGConsole : MonoBehaviour {
        public GameObject consoleCanvas;
        public TMP_InputField consoleInputField;
        public static bool consoleActive = false;

        private static SAGConsole console;
        void Awake() { console = this; }

        void Start() {
            CloseConsole();
        }

        public static void SubmitConsoleInput() {
            console.consoleInputField.Select();
            string consoleCommandString = console.consoleInputField.text.ToString();

            //Debug.Log($"command: {consoleCommandString}");
            CheckConsoleCommand(consoleCommandString);
            ClearConsole();
        }

        public static void CheckConsoleCommand(string fullCommandString) {
            CommandTrip<ConsoleErrorType, string, string> returnConsoleTrip = ConsoleUtil.SplitConsoleCommandString(fullCommandString);
            string commandKey   = returnConsoleTrip.Key;
            string commandValue = returnConsoleTrip.Value;

            switch (returnConsoleTrip.Type) {
                case ConsoleErrorType.PRINT_VALUE:   console.IdentifyAndRunGivenCommand(commandKey, commandValue, true);   break;
                case ConsoleErrorType.TOO_MANY_ARGS: ConsoleUtil.ThrowTooManyArgumentsError(commandKey, commandValue);     break;
                case ConsoleErrorType.VALID_COMMAND: console.IdentifyAndRunGivenCommand(commandKey, commandValue, false);  break;
            }
        }

        private void IdentifyAndRunGivenCommand(string commandKey, string commandValue, bool printNotSet = false) {
            // TODO: if command given without argument, print value
            switch (commandKey) {
                // UTIL //
                case "help":                ConsoleCommands.PrintCommandList();                           break;
                case "version":             ConsoleCommands.PrintGameVersion();                           break;

                // GAMEMODE //
                case "ga_gamemode":         ConsoleCommands.SetGamemode(commandKey, commandValue);        break;

                // TARGET //
                case "ta_target_color":     ConsoleCommands.SetTargetColor(commandKey, commandValue, printNotSet);     break;

                // SOUND //
                case "so_target_hitsound":  ConsoleCommands.SetTargetHitSound(commandKey, commandValue, printNotSet);  break;
                case "so_target_misssound": ConsoleCommands.SetTargetMissSound(commandKey, commandValue, printNotSet); break;
                case "so_ui_sound":         ConsoleCommands.SetUISound(commandKey, commandValue, printNotSet);         break;
                    
                // WIDGETS //
                case "wi_widget_mode":      ConsoleCommands.SetModeWidget(commandKey, commandValue, printNotSet);      break;
                case "wi_widget_fps":       ConsoleCommands.SetFPSWidget(commandKey, commandValue, printNotSet);       break;
                case "wi_widget_time":      ConsoleCommands.SetTimeWidget(commandKey, commandValue, printNotSet);      break;
                case "wi_widget_score":     ConsoleCommands.SetScoreWidget(commandKey, commandValue, printNotSet);     break;
                case "wi_widget_accuracy":  ConsoleCommands.SetAccuracyWidget(commandKey, commandValue, printNotSet);  break;
                case "wi_widget_streak":    ConsoleCommands.SetStreakWidget(commandKey, commandValue, printNotSet);    break;
                case "wi_widget_ttk":       ConsoleCommands.SetTTKWidget(commandKey, commandValue, printNotSet);       break;
                case "wi_widget_kps":       ConsoleCommands.SetKPSWidget(commandKey, commandValue, printNotSet);       break;
                    
                // CONTROLS //
                case "co_mouse_sensitivity":      break;

                //// KEYBINDS //
                //case "co_keybind_shoot":          break;
                //case "co_keybind_togglewidets":   break;
                //case "co_keybind_restart":        break;
                //case "co_keybind_toggleaar":      break;
                //case "co_keybind_togglesettings": break;
                    
                // CROSSHAIR //
                case "cr_crosshair_tstyle":       break;
                case "cr_crosshair_centerdot":    break;
                case "cr_crosshair_size":         break;
                case "cr_crosshair_thickness":    break;
                case "cr_crosshair_gap":          break;
                case "cr_crosshair_r":            break;
                case "cr_crosshair_g":            break;
                case "cr_crosshair_b":            break;
                case "cr_crosshair_a":            break;
                case "cr_crosshair_outline":      break;
                case "cr_crosshair_outline_r":    break;
                case "cr_crosshair_outline_g":    break;
                case "cr_crosshair_outline_b":    break;
                case "cr_crosshair_outline_a":    break;
                case "cr_crosshair_string":       break;
                case "cr_crosshair_reset":        break;
                case "cr_crosshair_import":       break;
                case "cr_crosshair_export":       break;

                default: break;
            }
        }

        public static void ToggleConsoleActive() {
            if (consoleActive) {
                CloseConsole();
            } else {
                OpenConsole();
            }
        }
        public static void OpenConsole() {
            consoleActive = true;
            Util.SetCursorState(CursorLockMode.None, true);
            //console.FocusConsole();
            console.consoleCanvas.SetActive(true);
        }
        public static void CloseConsole() {
            consoleActive = false;
            if (!SettingsPanel.settingsOpen && !SettingsPanel.afterActionReportOpen) {
                Util.SetCursorState(CursorLockMode.Locked, false);
            }
            ClearConsole();
            //console.UnfocusConsole();
            console.consoleCanvas.SetActive(false);
        }

        private void FocusConsole() {
            consoleInputField.Select();
        }
        private void UnfocusConsole() {
            consoleInputField.DeactivateInputField(true);
        }

        public static void ClearConsole() {
            console.consoleInputField.clear();
        }
    }
}