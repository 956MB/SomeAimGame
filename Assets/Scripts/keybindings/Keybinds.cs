﻿using UnityEngine;

using SomeAimGame.Console;

public class Keybinds : MonoBehaviour {
    public static bool keybindsLoaded = false;

    private void Start() {
        KeybindSaveSystem.InitSavedKeybindSettings();
    }

    void Update() {
        if (keybindsLoaded) {
            if (Input.GetKeyUp(KeybindSettings.toggleWidgets)) {
                ToggleWidgetsUI_Keybind();
            } else if (Input.GetKeyUp(KeybindSettings.gameRestart)) {
                TriggerGameRestart_Keybind();
            } else if (Input.GetKeyUp(KeybindSettings.toggleAAR)) {
                ToggleAARPanel_Keybind();
            } else if (Input.GetKeyUp(KeybindSettings.toggleSettings)) {
                ToggleSettingsPanel_Keybind();
            } else if (Input.GetKeyUp(KeybindSettings.toggleConsole)) {
                ToggleConsole_Keybind();
            }

            if (SAGConsole.consoleActive && Input.GetKeyUp(KeyCode.Return)) {
                SAGConsole.SubmitConsoleInput();
            }

            //else if (Input.GetKeyUp(KeyCode.RightArrow)) {
            //    MoveSettingsPanelRight_Keybind();
            //} else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            //    MoveSettingsPanelLeft_Keybind();
            //}
        }
    }

    /// <summary>
    /// Toggle WorldUI keybind function. [EVENT]
    /// </summary>
    private static void ToggleWidgetsUI_Keybind() {
        if (!SettingsPanel.settingsOpen && !SAGConsole.consoleActive) { GameUI.ToggleWidgetsUI(); }
    }

    /// <summary>
    /// Trigger game restart keybind function.
    /// </summary>
    private static void TriggerGameRestart_Keybind() {
        if (!SettingsPanel.settingsOpen && !SAGConsole.consoleActive) { GameUI.RestartGame(CosmeticsSettings.gamemode, true); }
    }

    /// <summary>
    /// Toggle AAR keybind function. [EVENT]
    /// </summary>
    private static void ToggleAARPanel_Keybind() {
        if (!SAGConsole.consoleActive) {
            if (SettingsPanel.afterActionReportSet) {
                if (!SettingsPanel.afterActionReportOpen && !SettingsPanel.settingsOpen) {
                    SettingsPanel.CloseSettingsPanel();
                    SettingsPanel.OpenAfterActionReport();
                    GameUI.HideWidgetsUI();
                } else {
                    SettingsPanel.CloseAfterActionReport();
                }
            }
        }
    }

    /// <summary>
    /// Toggle settings panel keybind function. [EVENT]
    /// </summary>
    private static void ToggleSettingsPanel_Keybind() {
        if (KeybindsHandler.currentKey == null) {
            if (!SAGConsole.consoleActive) {
                if (SettingsPanel.settingsOpen) {
                    SettingsPanel.CloseSettingsPanel();
                } else {
                    if (!SettingsPanel.afterActionReportOpen) {
                        SettingsPanel.OpenSettingsPanel();
                    } else {
                        SettingsPanel.CloseAfterActionReport();
                    }
                }
            } else {
                SAGConsole.CloseConsole();
            }
        }
    }

    private static void ToggleConsole_Keybind() {
        // TODO: toggle conmsole
        SAGConsole.ToggleConsoleActive();
    }

    /// <summary>
    /// Move settings panel right keybind function.
    /// </summary>
    private static void MoveSettingsPanelRight_Keybind() {
        if (MouseLook.settingsOpen && !CrosshairImportExport.importExportPanelOpen) {
            Vector3 settingsPanelPos_Right = SettingsPanel.MoveSettingsPanelRight();
        }
    }

    /// <summary>
    /// Move settings panel left keybind function.
    /// </summary>
    private static void MoveSettingsPanelLeft_Keybind() {
        if (MouseLook.settingsOpen && !CrosshairImportExport.importExportPanelOpen) {
            Vector3 settingsPanelPos_Left = SettingsPanel.MoveSettingsPanelLeft();
        }
    }
}
