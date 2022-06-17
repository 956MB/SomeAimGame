using UnityEngine;

using SomeAimGame.Targets;
using SomeAimGame.Gamemode;
using SomeAimGame.SFX;
using SomeAimGame.Utilities;

namespace SomeAimGame.Console {
    public class ConsoleCommands : MonoBehaviour {

        #region util commands

        /// <summary>
        /// Help command: Prints full command list in console.
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="commandValue"></param>
        public static void PrintCommandList(string commandKey, string commandValue) {
            if (commandValue == "") {
                // print command list
            } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, commandValue); }
        }
        /// <summary>
        /// Version command: Prints version of game
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="commandValue"></param>
        public static void PrintGameVersion(string commandKey, string commandValue) {
            if (commandValue == "") {
                // print game version
            } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, commandValue); }
        }
        /// <summary>
        /// Restart command: Restarts current running game.
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="commandValue"></param>
        public static void RestartCurrentGame(string commandKey, string commandValue) {
            if (commandValue == "") {
                SAGConsole.CloseConsole();
                GameUI.RestartGame(CosmeticsSettings.gamemode, true);
            } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, commandValue); }
        }
        /// <summary>
        /// Quit command: Quits game.
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="commandValue"></param>
        public static void QuitCurrentGame(string commandKey, string commandValue) {
            if (commandValue == "") {
                if (!Application.isEditor) { QuitGame.QuitCurrentGame_Static(); }
            } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, commandValue); }
        }

        #endregion

        #region gamemode commands

        /// <summary>
        /// Gamemode command: Sets gamemode
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setGamemode"></param>
        /// <param name="printNotset"></param>
        public static void SetGamemode(string commandKey, string setGamemode, bool printNotset = false) {
            if (printNotset) {
                Debug.Log($"command: {commandKey}");
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Int(setGamemode)) {
                    int gamemodeValue = int.Parse(setGamemode);
                    if (gamemodeValue >= 0 && gamemodeValue <= 5) {
                        GamemodeSelect.PopulateGamemodeSelectText((GamemodeType)gamemodeValue, true);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setGamemode); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setGamemode); }
            }
        }

        #endregion

        #region targets commands

        /// <summary>
        /// Target color command: Sets new target color
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setTargetColor"></param>
        /// <param name="printNotset"></param>
        public static void SetTargetColor(string commandKey, string setTargetColor, bool printNotset = false) {
            if (printNotset) {
                // TODO: print method for console
                //Debug.Log($"command: {commandKey}");
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Int(setTargetColor)) {
                    int targetColorValue = int.Parse(setTargetColor);
                    if (targetColorValue >= 0 && targetColorValue <= 8) {
                        TargetType targetColor = (TargetType)targetColorValue;
                        if (!ButtonClickHandler.SetTargetColor(targetColor)) { return; }
                        CosmeticsSaveSystem.SetTargetColorBorder(targetColor);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setTargetColor); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setTargetColor); }
            }
        }

        #endregion

        #region sound commands

        /// <summary>
        /// Target hit command: Toggles hit sound effect on/off
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setTargetHit"></param>
        /// <param name="printNotset"></param>
        public static void SetTargetHitSound(string commandKey, string setTargetHit, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setTargetHit)) {
                    int targetHitSoundValue = int.Parse(setTargetHit);
                    TargetSoundSelect.SetSoundSelectionContainerState(targetHitSoundValue != 0);
                    SFXSettings.SaveTargetSoundOn(targetHitSoundValue != 0);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setTargetHit); }
            }
        }
        /// <summary>
        /// Target miss command: Toggles miss sound effect on/off
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setTargetMiss"></param>
        /// <param name="printNotset"></param>
        public static void SetTargetMissSound(string commandKey, string setTargetMiss, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setTargetMiss)) {
                    int targetMissSoundValue = int.Parse(setTargetMiss);
                    TargetSoundSelect.SetTargetMissSoundContainerState(targetMissSoundValue != 0);
                    SFXSettings.SaveTargetMissSoundOn(targetMissSoundValue != 0);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setTargetMiss); }
            }
        }
        /// <summary>
        /// UI sound command: Toggles ui sound effect on/off
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setUISound"></param>
        /// <param name="printNotset"></param>
        public static void SetUISound(string commandKey, string setUISound, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setUISound)) {
                    int uiSoundValue = int.Parse(setUISound);
                    SFXSettings.SaveUISoundOn(uiSoundValue != 0);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setUISound); }
            }
        }

        #endregion

        #region widgets commands

        /// <summary>
        /// Widgets hidden command: Sets widget layer hidden on/off (0,1).
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setWidgetsShown"></param>
        /// <param name="printNotset"></param>
        public static void SetWidgetsShown(string commandKey, string setWidgetsShown, bool printNotset = false) {
            if (printNotset) {
                // print if widgets ui enabled
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setWidgetsShown)) {
                    int widgetsShownValue = int.Parse(setWidgetsShown);
                    ExtraSettings.SaveHideUI(widgetsShownValue != 0);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setWidgetsShown); }
            }
        }
        /// <summary>
        /// Mode widget command: Sets mode widget on/off (0,1)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setModeWidget"></param>
        /// <param name="printNotset"></param>
        public static void SetModeWidget(string commandKey, string setModeWidget, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setModeWidget)) {
                    int modeWidgetValue = int.Parse(setModeWidget);
                    ToggleHandler.SetShowModeWidgetToggle(modeWidgetValue != 0);
                    WidgetSettings.SaveShowModeItem(modeWidgetValue != 0);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setModeWidget); }
            }
        }
        /// <summary>
        /// FPS widget command: Sets fps widget on/off (0,1)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setFPSWidget"></param>
        /// <param name="printNotset"></param>
        public static void SetFPSWidget(string commandKey, string setFPSWidget, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setFPSWidget)) {
                    int fpsWidgetValue = int.Parse(setFPSWidget);
                    ToggleHandler.SetShowFPSWidgetToggle(fpsWidgetValue != 0);
                    WidgetSettings.SaveShowFPSItem(fpsWidgetValue != 0);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setFPSWidget); }
            }
        }
        /// <summary>
        /// Time widget command: Sets time widget on/off (0,1)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setTimeWidget"></param>
        /// <param name="printNotset"></param>
        public static void SetTimeWidget(string commandKey, string setTimeWidget, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setTimeWidget)) {
                    int timeWidgetValue = int.Parse(setTimeWidget);
                    ToggleHandler.SetShowTimeWidgetToggle(timeWidgetValue != 0);
                    WidgetSettings.SaveShowTimeItem(timeWidgetValue != 0);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setTimeWidget); }
            }
        }
        /// <summary>
        /// Score widget command: Sets score widget on/off (0,1)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setScoreWidget"></param>
        /// <param name="printNotset"></param>
        public static void SetScoreWidget(string commandKey, string setScoreWidget, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setScoreWidget)) {
                    int scoreWidgetValue = int.Parse(setScoreWidget);
                    ToggleHandler.SetShowScoreWidgetToggle(scoreWidgetValue != 0);
                    WidgetSettings.SaveShowScoreItem(scoreWidgetValue != 0);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setScoreWidget); }
            }
        }
        /// <summary>
        /// Accuracy widget command: Sets accuracy widget on/off (0,1)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setAccuracyWidget"></param>
        /// <param name="printNotset"></param>
        public static void SetAccuracyWidget(string commandKey, string setAccuracyWidget, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setAccuracyWidget)) {
                    int accuracyWidgetValue = int.Parse(setAccuracyWidget);
                    ToggleHandler.SetShowAccuracyWidgetToggle(accuracyWidgetValue != 0);
                    WidgetSettings.SaveShowAccuracyItem(accuracyWidgetValue != 0);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setAccuracyWidget); }
            }
        }
        /// <summary>
        /// Streak widget command: Sets streak widget on/off (0,1)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setStreakWidget"></param>
        /// <param name="printNotset"></param>
        public static void SetStreakWidget(string commandKey, string setStreakWidget, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setStreakWidget)) {
                    int streakWidgetValue = int.Parse(setStreakWidget);
                    ToggleHandler.SetShowStreakWidgetToggle(streakWidgetValue != 0);
                    WidgetSettings.SaveShowStreakItem(streakWidgetValue != 0);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setStreakWidget); }
            }
        }
        /// <summary>
        /// TTK widget command: Sets ttk widget on/off (0,1)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setTTKWIdget"></param>
        /// <param name="printNotset"></param>
        public static void SetTTKWidget(string commandKey, string setTTKWIdget, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setTTKWIdget)) {
                    int ttkWidgetValue = int.Parse(setTTKWIdget);
                    ToggleHandler.SetShowTTKWidgetToggle(ttkWidgetValue != 0);
                    WidgetSettings.SaveShowTTKItem(ttkWidgetValue != 0);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setTTKWIdget); }
            }
        }
        /// <summary>
        /// KPS widget command: Sets kps widget on/off (0,1)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setKPSWidget"></param>
        /// <param name="printNotset"></param>
        public static void SetKPSWidget(string commandKey, string setKPSWidget, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setKPSWidget)) {
                    int kpsWidgetValue = int.Parse(setKPSWidget);
                    ToggleHandler.SetShowKPSWidgetToggle(kpsWidgetValue != 0);
                    WidgetSettings.SaveShowKPSItem(kpsWidgetValue != 0);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setKPSWidget); }
            }
        }

        #endregion

        #region controls commands

        /// <summary>
        /// Mouse sensitivity command: Sets mouse sensitivity (float 0-10)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setSensitivity"></param>
        /// <param name="printNotset"></param>
        public static void SetMouseSensitivity(string commandKey, string setSensitivity, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Float(setSensitivity)) {
                    float sensValue = float.Parse(setSensitivity);
                    if (sensValue >= 0f && sensValue <= 10f) {
                        MouseLook.mouseSensitivity = sensValue;
                        MouseSensitivitySlider.SetMouseSensitivityValueText(sensValue);
                        MouseSensitivitySlider.SetMouseSensitivitySlider(sensValue);
                        ExtraSettings.SaveMouseSensItem(sensValue);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setSensitivity); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setSensitivity); }
            }
        }

        #endregion

        #region keybinds commands

        /// <summary>
        /// Keybinds list command: Prints full keybinds list in console.
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="commandValue"></param>
        /// <param name="printNotset"></param>
        public static void PrintKeybindsList(string commandKey, string commandValue, bool printNotset = false) {
            if (commandValue == "") {
                // print keybinds list
            } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, commandValue); }
        }
        /// <summary>
        /// Keybind shoot command: Sets shoot keybind (string)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setShoot"></param>
        /// <param name="printNotset"></param>
        public static void SetShootKeybind(string commandKey, string setShoot, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                KeyCode newShootKeyCode = KeybindsUtil.ReturnKeyCodeFromString(setShoot);
                if (newShootKeyCode != KeyCode.None) {
                    KeybindSaveSystem.SetShootKeybind(newShootKeyCode);
                    KeybindSettings.SaveShootKeybindItem(newShootKeyCode);
                } else { ConsoleUtil.ThrowInvalidCommandValueKeybindError(commandKey, setShoot); }
            }
        }
        /// <summary>
        /// Keybind toggle widgets command: Sets toggle widgets keybind (string)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setToggleWidgets"></param>
        /// <param name="printNotset"></param>
        public static void SetToggleWidgetsKeybind(string commandKey, string setToggleWidgets, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                KeyCode newToggleWidgetsKeyCode = KeybindsUtil.ReturnKeyCodeFromString(setToggleWidgets);
                if (newToggleWidgetsKeyCode != KeyCode.None) {
                    KeybindSaveSystem.SetToggleWidgetsKeybind(newToggleWidgetsKeyCode);
                    KeybindSettings.SaveToggleWidgetsKeybindItem(newToggleWidgetsKeyCode);
                } else { ConsoleUtil.ThrowInvalidCommandValueKeybindError(commandKey, setToggleWidgets); }
            }
        }
        /// <summary>
        /// Keybind toggle settings command: Sets toggle settings keybind (string)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setSettings"></param>
        /// <param name="printNotset"></param>
        public static void SetToggleSettingsKeybind(string commandKey, string setSettings, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                KeyCode newToggleSettingsKeyCode = KeybindsUtil.ReturnKeyCodeFromString(setSettings);
                if (newToggleSettingsKeyCode != KeyCode.None) {
                    KeybindSaveSystem.SetToggleSettingsKeybind(newToggleSettingsKeyCode);
                    KeybindSettings.SaveToggleSettingsKeybindItem(newToggleSettingsKeyCode);
                    KeybindSaveSystem.SetAARButtons(KeybindSettings.gameRestart, newToggleSettingsKeyCode);
                } else { ConsoleUtil.ThrowInvalidCommandValueKeybindError(commandKey, setSettings); }
            }
        }
        /// <summary>
        /// Keybind AAR command: Sets aar keybind (string)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setAAR"></param>
        /// <param name="printNotset"></param>
        public static void SetToggleAARKeybind(string commandKey, string setAAR, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                KeyCode newToggleAARKeyCode = KeybindsUtil.ReturnKeyCodeFromString(setAAR);
                if (newToggleAARKeyCode != KeyCode.None) {
                    KeybindSaveSystem.SetToggleAARKeybind(newToggleAARKeyCode);
                    KeybindSettings.SaveToggleAARKeybindItem(newToggleAARKeyCode);
                } else { ConsoleUtil.ThrowInvalidCommandValueKeybindError(commandKey, setAAR); }
            }
        }
        /// <summary>
        /// Keybind restart command: Sets restart keybind (string)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setRestart"></param>
        /// <param name="printNotset"></param>
        public static void SetRestartKeybind(string commandKey, string setRestart, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                KeyCode newRestartKeyCode = KeybindsUtil.ReturnKeyCodeFromString(setRestart);
                if (newRestartKeyCode != KeyCode.None) {
                    KeybindSaveSystem.SetGameRestartKeybind(newRestartKeyCode);
                    KeybindSettings.SaveGameRestartKeybindItem(newRestartKeyCode);
                    KeybindSaveSystem.SetAARButtons(newRestartKeyCode, KeybindSettings.toggleSettings);
                } else { ConsoleUtil.ThrowInvalidCommandValueKeybindError(commandKey, setRestart); }
            }
        }

        #endregion

        #region crosshair commands

        /// <summary>
        /// Crosshair center dot command: Sets crosshairs center dot on/off (0,1)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setCenterDot"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairCenterDot(string commandKey, string setCenterDot, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setCenterDot)) {
                    int centerDotValue = int.Parse(setCenterDot);
                    ToggleHandler.SetCrosshairTStyleToggle(centerDotValue != 0);
                    CrosshairSettings.SaveTStyle(centerDotValue != 0);
                    CrosshairOptionsObject.SaveCrosshairObject(true);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setCenterDot); }
            }
        }
        /// <summary>
        /// Crosshair tstyle command: Sets crosshairs tstyle on/off (0,1)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setTStyle"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairTStyle(string commandKey, string setTStyle, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setTStyle)) {
                    int tstyleValue = int.Parse(setTStyle);
                    ToggleHandler.SetCrosshairTStyleToggle(tstyleValue != 0);
                    CrosshairSettings.SaveTStyle(tstyleValue != 0);
                    CrosshairOptionsObject.SaveCrosshairObject(true);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setTStyle); }
            }
        }
        /// <summary>
        /// Crosshair size command: Sets crosshairs size (int 0-45)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setSize"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairSize(string commandKey, string setSize, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Int(setSize)) {
                    int sizeValue = int.Parse(setSize);
                    if (sizeValue >= 0 && sizeValue <= 45) {
                        CrosshairOptionsObject.ReturnCrosshairSizeSlider().value = sizeValue;
                        CrosshairOptionsObject.SetCrosshairSizeValue();
                        CrosshairOptionsObject.SaveCrosshairObject(true);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setSize); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setSize); }
            }
        }
        /// <summary>
        /// Crosshair thickness command: Sets crosshairs thickness (int 1-15)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setThickness"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairThickness(string commandKey, string setThickness, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Int(setThickness)) {
                    int thicknessValue = int.Parse(setThickness);
                    if (thicknessValue >= 1 && thicknessValue <= 15) {
                        CrosshairOptionsObject.ReturnCrosshairThicknessSlider().value = thicknessValue;
                        CrosshairOptionsObject.SetThicknessSizeValue();
                        CrosshairOptionsObject.SaveCrosshairObject(true);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setThickness); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setThickness); }
            }
        }
        /// <summary>
        /// Crosshair gap command: Sets crosshairs gap (int 0-25)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setGap"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairGap(string commandKey, string setGap, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Int(setGap)) {
                    int gapValue = int.Parse(setGap);
                    if (gapValue >= 0 && gapValue <= 25) {
                        CrosshairOptionsObject.ReturnCrosshairGapSlider().value = gapValue;
                        CrosshairOptionsObject.SetGapSizeValue();
                        CrosshairOptionsObject.SaveCrosshairObject(true);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setGap); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setGap); }
            }
        }
        /// <summary>
        /// Crosshair red command: Sets crosshairs red color value (int 0-255)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setRed"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairRed(string commandKey, string setRed, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Int(setRed)) {
                    int redValue = int.Parse(setRed);
                    if (redValue >= 0 && redValue <= 255) {
                        CrosshairOptionsObject.ReturnCrosshairRedOutlineSlider().value = redValue;
                        CrosshairOptionsObject.SetRedValue();
                        CrosshairOptionsObject.SaveCrosshairObject(true);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setRed); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setRed); }
            }
        }
        /// <summary>
        /// Crosshair green command: Sets crosshairs green color value (int 0-255)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setGreen"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairGreen(string commandKey, string setGreen, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Int(setGreen)) {
                    int greenValue = int.Parse(setGreen);
                    if (greenValue >= 0 && greenValue <= 255) {
                        CrosshairOptionsObject.ReturnCrosshairGreenSlider().value = greenValue;
                        CrosshairOptionsObject.SetGreenValue();
                        CrosshairOptionsObject.SaveCrosshairObject(true);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setGreen); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setGreen); }
            }
        }
        /// <summary>
        /// Crosshair blue command: Sets crosshairs blue color value (int 0-255)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setBlue"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairBlue(string commandKey, string setBlue, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Int(setBlue)) {
                    int blueValue = int.Parse(setBlue);
                    if (blueValue >= 0 && blueValue <= 255) {
                        CrosshairOptionsObject.ReturnCrosshairBlueSlider().value = blueValue;
                        CrosshairOptionsObject.SetBlueValue();
                        CrosshairOptionsObject.SaveCrosshairObject(true);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setBlue); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setBlue); }
            }
        }
        /// <summary>
        /// Crosshair alpha command: Sets crosshairs alpha color value (int 0-255)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setAlpha"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairAlpha(string commandKey, string setAlpha, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Int(setAlpha)) {
                    int alphaValue = int.Parse(setAlpha);
                    if (alphaValue >= 0 && alphaValue <= 255) {
                        CrosshairOptionsObject.ReturnCrosshairAlphaSlider().value = alphaValue;
                        CrosshairOptionsObject.SetAlphaValue();
                        CrosshairOptionsObject.SaveCrosshairObject(true);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setAlpha); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setAlpha); }
            }
        }
        /// <summary>
        /// Crosshair outline command: Sets crosshairs outline on/off (0,1)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setOutline"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairOutline(string commandKey, string setOutline, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setOutline)) {
                    int outlineValue = int.Parse(setOutline);
                    ToggleHandler.SetCrosshairOutlineToggle(outlineValue != 0);
                    CrosshairSettings.SaveOutlineEnabled(outlineValue != 0);
                    CrosshairOptionsObject.SaveCrosshairObject(true);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setOutline); }
            }
        }
        /// <summary>
        /// Crosshair red outline command: Sets crosshairs red outline color value (int 0-255)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setRedOutline"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairRedOutline(string commandKey, string setRedOutline, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Int(setRedOutline)) {
                    int redOutlineValue = int.Parse(setRedOutline);
                    if (redOutlineValue >= 0 && redOutlineValue <= 255) {
                        CrosshairOptionsObject.ReturnCrosshairRedOutlineSlider().value = redOutlineValue;
                        CrosshairOptionsObject.SetRedOutlineValue();
                        CrosshairOptionsObject.SaveCrosshairObject(true);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setRedOutline); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setRedOutline); }
            }
        }
        /// <summary>
        /// Crosshair green outline command: Sets crosshairs green outline color value (int 0-255)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setGreenOutline"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairGreenOutline(string commandKey, string setGreenOutline, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Int(setGreenOutline)) {
                    int greenOutlineValue = int.Parse(setGreenOutline);
                    if (greenOutlineValue >= 0 && greenOutlineValue <= 255) {
                        CrosshairOptionsObject.ReturnCrosshairGreenOutlineSlider().value = greenOutlineValue;
                        CrosshairOptionsObject.SetGreenOutlineValue();
                        CrosshairOptionsObject.SaveCrosshairObject(true);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setGreenOutline); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setGreenOutline); }
            }
        }
        /// <summary>
        /// Crosshair blue outline command: Sets crosshairs blue outline color value (int 0-255)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setBlueOutline"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairBlueOutline(string commandKey, string setBlueOutline, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Int(setBlueOutline)) {
                    int blueOutlineValue = int.Parse(setBlueOutline);
                    if (blueOutlineValue >= 0 && blueOutlineValue <= 255) {
                        CrosshairOptionsObject.ReturnCrosshairBlueOutlineSlider().value = blueOutlineValue;
                        CrosshairOptionsObject.SetBlueOutlineValue();
                        CrosshairOptionsObject.SaveCrosshairObject(true);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setBlueOutline); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setBlueOutline); }
            }
        }
        /// <summary>
        /// Crosshair alpha outline command: Sets crosshairs alpha outline color value (int 0-255)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setAlphaOutline"></param>
        /// <param name="printNotset"></param>
        public static void SetCrosshairAlphaOutline(string commandKey, string setAlphaOutline, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Int(setAlphaOutline)) {
                    int alphaOutlineValue = int.Parse(setAlphaOutline);
                    if (alphaOutlineValue >= 0 && alphaOutlineValue <= 255) {
                        CrosshairOptionsObject.ReturnCrosshairAlphaOutlineSlider().value = alphaOutlineValue;
                        CrosshairOptionsObject.SetAlphaOutlineValue();
                        CrosshairOptionsObject.SaveCrosshairObject(true);
                    } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setAlphaOutline); }
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setAlphaOutline); }
            }
        }
        /// <summary>
        /// Print crosshair string command: Prints current crosshair string
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="commandValue"></param>
        /// <param name="printNotset"></param>
        public static void PrintCrosshairString(string commandKey, string commandValue, bool printNotset = false) {
            if (commandValue == "") {
                // print crosshair string
            } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, commandValue); }
        }
        /// <summary>
        /// Reset crosshair command: Resets crosshair to default values
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="commandValue"></param>
        /// <param name="printNotset"></param>
        public static void ResetCrosshair(string commandKey, string commandValue, bool printNotset = false) {
            if (commandValue == "") {
                CrosshairImportExport.SetResetConfirm();
                CrosshairSaveSystem.InitCrosshairSettingsDefaults();
                CrosshairImportExport.SetResetDefault();
            } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, commandValue); }
        }
        /// <summary>
        /// Import crosshair command: Imports new crosshair from sag string format (000601051255255255255255000000255)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setCrosshair"></param>
        /// <param name="printNotset"></param>
        public static void ImportCrossahir(string commandKey, string setCrosshair, bool printNotset = false) {
            if (ConsoleUtil.CheckValidCommandValue_Crosshair(setCrosshair)) {
                if (!CrosshairImportExport.ImportCrosshairFromString(setCrosshair)) { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setCrosshair); }
            } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setCrosshair); }
        }
        /// <summary>
        /// Import csgo crosshair command: Imports new crosshair from csgo string format (CSGO-c3YbL-vq5pC-oabtS-DJDTW-mRXXC)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="setCrosshairCSGO"></param>
        /// <param name="printNotset"></param>
        public static void ImportCrossahirCSGO(string commandKey, string setCrosshairCSGO, bool printNotset = false) {
            if (ConsoleUtil.CheckValidCommandValue_CrosshairCSGO(setCrosshairCSGO)) {
                if (!CrosshairImportExport.ImportCrosshairFromString(setCrosshairCSGO)) { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setCrosshairCSGO); }
            } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setCrosshairCSGO); }
        }
        /// <summary>
        /// Export crosshair command: Exports current crosshair string (000601051255255255255255000000255)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="commandValue"></param>
        /// <param name="printNotset"></param>
        public static void ExportCrossahir(string commandKey, string commandValue, bool printNotset = false) {
            if (commandValue == "") {
                // print crosshair string
            } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, commandValue); }
        }
        /// <summary>
        /// Export csgo crosshair command: Exports current csgo crosshair string (CSGO-c3YbL-vq5pC-oabtS-DJDTW-mRXXC)
        /// </summary>
        /// <param name="commandKey"></param>
        /// <param name="commandValue"></param>
        /// <param name="printNotset"></param>
        public static void ExportCrossahirCSGO(string commandKey, string commandValue, bool printNotset = false) {
            if (commandValue == "") {
                // print csgo crosshair string
            } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, commandValue); }
        }

        #endregion
    }
}
