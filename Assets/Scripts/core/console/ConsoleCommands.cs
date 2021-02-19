using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SomeAimGame.Targets;
using SomeAimGame.Gamemode;
using SomeAimGame.SFX;

namespace SomeAimGame.Console {
    public class ConsoleCommands : MonoBehaviour {
        public static Dictionary<string, Type> commandsDict = new Dictionary<string, Type>();

        private void Start() {
            InitCommandsDict();
        }

        private void InitCommandsDict() {
            commandsDict.Add("ga_gamemode", typeof(int));

            commandsDict.Add("ta_target_color", typeof(int));

            commandsDict.Add("so_target_hitsound", typeof(int));
            commandsDict.Add("so_target_misssound", typeof(int));
            commandsDict.Add("so_ui_sound", typeof(int));

            commandsDict.Add("wi_widget_mode", typeof(int));
            commandsDict.Add("wi_widget_fps", typeof(int));
            commandsDict.Add("wi_widget_time", typeof(int));
            commandsDict.Add("wi_widget_score", typeof(int));
            commandsDict.Add("wi_widget_accuracy", typeof(int));
            commandsDict.Add("wi_widget_streak", typeof(int));
            commandsDict.Add("wi_widget_ttk", typeof(int));
            commandsDict.Add("wi_widget_kps", typeof(int));

            commandsDict.Add("co_mouse_sensitivity", typeof(float));

            //commandsDict.Add("co_keybind_shoot", typeof(float));
            //commandsDict.Add("co_keybind_togglewidets", typeof(float));
            //commandsDict.Add("co_keybind_restart", typeof(float));
            //commandsDict.Add("co_keybind_toggleaar", typeof(float));
            //commandsDict.Add("co_keybind_togglesettings", typeof(float));

            commandsDict.Add("wi_widget_mode", typeof(int));
            commandsDict.Add("wi_widget_fps", typeof(int));
            commandsDict.Add("wi_widget_time", typeof(int));
            commandsDict.Add("wi_widget_score", typeof(int));
            commandsDict.Add("wi_widget_accuracy", typeof(int));
            commandsDict.Add("wi_widget_streak", typeof(int));
            commandsDict.Add("wi_widget_ttk", typeof(int));
            commandsDict.Add("wi_widget_kps", typeof(int));
            commandsDict.Add("wi_widget_mode", typeof(int));
            commandsDict.Add("wi_widget_fps", typeof(int));
            commandsDict.Add("wi_widget_time", typeof(int));
            commandsDict.Add("wi_widget_score", typeof(int));
            commandsDict.Add("wi_widget_accuracy", typeof(int));
            commandsDict.Add("wi_widget_streak", typeof(int));
            commandsDict.Add("wi_widget_ttk", typeof(int));
            commandsDict.Add("wi_widget_kps", typeof(int));
            commandsDict.Add("wi_widget_kps", typeof(int));
            commandsDict.Add("wi_widget_kps", typeof(int));
        }

        #region commands

        public static void PrintCommandList() {

        }

        public static void PrintGameVersion() {

        }
        
        public static void SetGamemode(string commandKey, string setGamemode, bool printNotset = false) {

        }
        
        public static void SetTargetColor(string commandKey, string setTargetColor, bool printNotset = false) {
            if (printNotset) {
                // TODO: print method for console
                Debug.Log($"command: {commandKey}");
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
        
        public static void SetUISound(string commandKey, string setUISound, bool printNotset = false) {
            if (printNotset) {
            
            } else {
                if (ConsoleUtil.CheckValidCommandValue_Bool(setUISound)) {
                    int uiSoundValue = int.Parse(setUISound);
                    SFXSettings.SaveUISoundOn(uiSoundValue != 0);
                } else { ConsoleUtil.ThrowInvalidCommandValueError(commandKey, setUISound); }
            }
        }

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
    }
}
