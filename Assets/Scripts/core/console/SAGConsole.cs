using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using SomeAimGame.Utilities;
using SomeAimGame.Core;
using System;

namespace SomeAimGame.Console {
    public class SAGConsole : MonoBehaviour {
        public GameObject consoleCanvas;
        public TMP_InputField consoleInputField;
        public static bool consoleActive = false;
        public static bool caretHidden   = false;

        List<string> commandHistory = new List<string>();
        public static int consoleIndex = 1;
        //public static int commandAutoCompleteIndex = 0;

        private static SAGConsole console;
        void Awake() { console = this; }

        void Start() {
            // TODO: add console command auto complete and options in dropdown
            // TODO: add arrow up/down navigation for suggested commands and previously used commands
            OpenConsole();
            CloseConsole();
        }

        /// <summary>
        /// Submits command in console input and clears console.
        /// </summary>
        public static void SubmitConsoleInput() {
            console.consoleInputField.Select();
            string consoleCommandString = console.consoleInputField.text.ToString();

            //Debug.Log($"command: {consoleCommandString}");
            CheckConsoleCommand(consoleCommandString);
            ClearConsole();
            console.FocusConsole();
        }

        /// <summary>
        /// Checks supplied console command string (fullCommandString) and sens key/value to IdentifyAndRunGivenCommand().
        /// </summary>
        /// <param name="fullCommandString"></param>
        public static void CheckConsoleCommand(string fullCommandString) {
            CommandTrip<CommandReturnType, string, string> returnConsoleTrip = ConsoleUtil.SplitConsoleCommandString(fullCommandString);
            string commandKey   = returnConsoleTrip.Key;
            string commandValue = returnConsoleTrip.Value;

            switch (returnConsoleTrip.Type) {
                case CommandReturnType.PRINT_VALUE:   console.IdentifyAndRunGivenCommand(commandKey, commandValue, true);   break;
                case CommandReturnType.TOO_MANY_ARGS: ConsoleUtil.ThrowTooManyArgumentsError(commandKey, commandValue);     break;
                case CommandReturnType.VALID_COMMAND: console.IdentifyAndRunGivenCommand(commandKey, commandValue, false);  break;
            }

            console.CheckAddCommandToHistory(commandKey);
            SettingsPanel.CheckSaveSettings();
        }

        /// <summary>
        /// Identifies given command key and runs appropriate command method.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="printNotset"></param>
        private void IdentifyAndRunGivenCommand(string key, string value, bool printNotset = false) {
            switch (key) {
                // UTIL //
                case "help":                      ConsoleCommands.PrintCommandList(key, value);                      break;
                case "version":                   ConsoleCommands.PrintGameVersion(key, value);                      break;
                case "restart":                   ConsoleCommands.RestartCurrentGame(key, value);                    break;
                case "quit":                      ConsoleCommands.QuitCurrentGame(key, value);                       break;
                // GAMEMODE //
                case "ga_gamemode":               ConsoleCommands.SetGamemode(key, value, printNotset);              break;
                // TARGET //
                case "ta_target_color":           ConsoleCommands.SetTargetColor(key, value, printNotset);           break;
                // SOUND //
                case "so_target_hitsound":        ConsoleCommands.SetTargetHitSound(key, value, printNotset);        break;
                case "so_target_misssound":       ConsoleCommands.SetTargetMissSound(key, value, printNotset);       break;
                case "so_ui_sound":               ConsoleCommands.SetUISound(key, value, printNotset);               break;
                // WIDGETS //
                case "wi_widgets_shown":          ConsoleCommands.SetWidgetsShown(key, value, printNotset);          break;
                case "wi_widget_mode":            ConsoleCommands.SetModeWidget(key, value, printNotset);            break;
                case "wi_widget_fps":             ConsoleCommands.SetFPSWidget(key, value, printNotset);             break;
                case "wi_widget_time":            ConsoleCommands.SetTimeWidget(key, value, printNotset);            break;
                case "wi_widget_score":           ConsoleCommands.SetScoreWidget(key, value, printNotset);           break;
                case "wi_widget_accuracy":        ConsoleCommands.SetAccuracyWidget(key, value, printNotset);        break;
                case "wi_widget_streak":          ConsoleCommands.SetStreakWidget(key, value, printNotset);          break;
                case "wi_widget_ttk":             ConsoleCommands.SetTTKWidget(key, value, printNotset);             break;
                case "wi_widget_kps":             ConsoleCommands.SetKPSWidget(key, value, printNotset);             break;
                // CONTROLS //
                case "co_mouse_sensitivity":      ConsoleCommands.SetMouseSensitivity(key, value, printNotset);      break;
                // KEYBINDS //
                case "ke_keybind_list":           ConsoleCommands.PrintKeybindsList(key, value, printNotset);        break;
                case "ke_keybind_shoot":          ConsoleCommands.SetShootKeybind(key, value, printNotset);          break;
                case "ke_keybind_togglewidets":   ConsoleCommands.SetToggleWidgetsKeybind(key, value, printNotset);  break;
                case "ke_keybind_togglesettings": ConsoleCommands.SetToggleSettingsKeybind(key, value, printNotset); break;
                case "ke_keybind_toggleaar":      ConsoleCommands.SetToggleAARKeybind(key, value, printNotset);      break;
                case "ke_keybind_restart":        ConsoleCommands.SetRestartKeybind(key, value, printNotset);        break;
                // CROSSHAIR //
                case "cr_crosshair_centerdot":    ConsoleCommands.SetCrosshairCenterDot(key, value, printNotset);    break;
                case "cr_crosshair_tstyle":       ConsoleCommands.SetCrosshairTStyle(key, value, printNotset);       break;
                case "cr_crosshair_size":         ConsoleCommands.SetCrosshairSize(key, value, printNotset);         break;
                case "cr_crosshair_thickness":    ConsoleCommands.SetCrosshairThickness(key, value, printNotset);    break;
                case "cr_crosshair_gap":          ConsoleCommands.SetCrosshairGap(key, value, printNotset);          break;
                case "cr_crosshair_r":            ConsoleCommands.SetCrosshairRed(key, value, printNotset);          break;
                case "cr_crosshair_g":            ConsoleCommands.SetCrosshairGreen(key, value, printNotset);        break;
                case "cr_crosshair_b":            ConsoleCommands.SetCrosshairBlue(key, value, printNotset);         break;
                case "cr_crosshair_a":            ConsoleCommands.SetCrosshairAlpha(key, value, printNotset);        break;
                case "cr_crosshair_outline":      ConsoleCommands.SetCrosshairOutline(key, value, printNotset);      break;
                case "cr_crosshair_outline_r":    ConsoleCommands.SetCrosshairRedOutline(key, value, printNotset);   break;
                case "cr_crosshair_outline_g":    ConsoleCommands.SetCrosshairGreenOutline(key, value, printNotset); break;
                case "cr_crosshair_outline_b":    ConsoleCommands.SetCrosshairBlueOutline(key, value, printNotset);  break;
                case "cr_crosshair_outline_a":    ConsoleCommands.SetCrosshairAlphaOutline(key, value, printNotset); break;
                case "cr_crosshair_string":       ConsoleCommands.PrintCrosshairString(key, value, printNotset);     break;
                case "cr_crosshair_reset":        ConsoleCommands.ResetCrosshair(key, value, printNotset);           break;
                case "cr_crosshair_import":       ConsoleCommands.ImportCrossahir(key, value, printNotset);          break;
                case "cr_crosshair_import_csgo":  ConsoleCommands.ImportCrossahirCSGO(key, value, printNotset);      break;
                case "cr_crosshair_export":       ConsoleCommands.ExportCrossahir(key, value, printNotset);          break;
                case "cr_crosshair_export_csgo":  ConsoleCommands.ExportCrossahirCSGO(key, value, printNotset);      break;

                default:                          ConsoleUtil.ThrowInvalidCommandKeyError(key, value);               break;
            }
        }

        /// <summary>
        /// Toggles console on/off.
        /// </summary>
        public static void ToggleConsoleActive() {
            if (consoleActive) { CloseConsole(); } else { OpenConsole(); }
        }
        /// <summary>
        /// Opens console.
        /// </summary>
        public static void OpenConsole() {
            Util.SetCursorState(CursorLockMode.None, true);
            console.consoleCanvas.SetActive(consoleActive = true);
            console.FocusConsole();
        }
        /// <summary>
        /// CLoses console.
        /// </summary>
        public static void CloseConsole() {
            if (!SettingsPanel.settingsOpen && !SettingsPanel.afterActionReportOpen)                                { Util.SetCursorState(CursorLockMode.Locked, false); }
            if (!SettingsPanel.settingsOpen && !SettingsPanel.afterActionReportOpen && ExtraSettings.showWidgets)   { GameUI.ShowWidgetsUICanvas(); }
            if (!SettingsPanel.settingsOpen && (!ExtraSettings.showWidgets || SettingsPanel.afterActionReportOpen)) { GameUI.HideWidgetsUICanvas(); }
            ClearConsole();
            console.UnfocusConsole();
            console.consoleCanvas.SetActive(consoleActive = false);
        }

        /// <summary>
        /// Gives console focus.
        /// </summary>
        private void FocusConsole() {
            // TODO: not working
            consoleInputField.ActivateInputField();
        }
        /// <summary>
        /// Unfocuses console.
        /// </summary>
        private void UnfocusConsole() {
            // TODO: not working
            consoleInputField.DeactivateInputField(true);
        }

        /// <summary>
        /// Clears text content from console input.
        /// </summary>
        public static void ClearConsole() {
            // TODO: not working
            console.consoleInputField.clear();
        }

        #region command history

        public void CheckAddCommandToHistory(string addCommandKey) {
            if (!commandHistory.Contains(addCommandKey)) { commandHistory.Add(addCommandKey); }
        }

        public static void NavigateConsoleIndexUp() {
            if (console.commandHistory.Count >= 1) {
                consoleIndex = consoleIndex < console.commandHistory.Count - 1 ? console.commandHistory.Count - 1 : 0;
                console.StartCoroutine(FillRecentCommandInConsole(consoleIndex));
            } else {
                ShowCaretWhileNavigatingConsoleIndex();
            }
        }
        public static void NavigateConsoleIndexDown() {
            if (console.commandHistory.Count >= 1) {
                consoleIndex = consoleIndex > 0 ? 0 : console.commandHistory.Count - 1;
                console.StartCoroutine(FillRecentCommandInConsole(consoleIndex));
            } else {
                ShowCaretWhileNavigatingConsoleIndex();
            }
        }

        public static void HideCaretWhileNavigatingConsoleIndex() {
            if (!caretHidden) { ConsoleUtil.SetCaretState(console.consoleInputField, ref caretHidden, true); }
        }
        public static void ShowCaretWhileNavigatingConsoleIndex() {
            if (caretHidden) { ConsoleUtil.SetCaretState(console.consoleInputField, ref caretHidden, false); }
        }

        public static IEnumerator FillRecentCommandInConsole(int consoleIndex) {
            yield return new WaitForEndOfFrame();

            try {
                console.consoleInputField.text = $"{console.commandHistory[consoleIndex]}";
                console.consoleInputField.MoveToEndOfLine(false, false);
                ShowCaretWhileNavigatingConsoleIndex();
            } catch (ArgumentOutOfRangeException) { }
        }

        public static void TabCompleteCommandKey() {
            if (ConsoleUtil.CheckValidCommandValue_NotEmpty(console.consoleInputField.text)) {

            }
        }

        #endregion
    }
}