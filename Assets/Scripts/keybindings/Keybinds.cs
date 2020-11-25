using UnityEngine;

public class Keybinds : MonoBehaviour {
    public static bool keybindsLoaded = false;

    private void Start() {
        KeybindSaveSystem.InitSavedKeybindSettings();
    }

    void Update() {
        if (keybindsLoaded) {
            if (Input.GetKeyDown(KeybindSettings.toggleWidgets)) {
                ToggleWidgetsUI_Keybind();
            } else if (Input.GetKeyDown(KeybindSettings.gameRestart)) {
                TriggerGameRestart_Keybind();
            } else if (Input.GetKeyDown(KeybindSettings.toggleAAR)) {
                ToggleAARPanel_Keybind();
            } else if (Input.GetKeyDown(KeybindSettings.toggleSettings)) {
                ToggleSettingsPanel_Keybind();
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                MoveSettingsPanelRight_Keybind();
            } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                MoveSettingsPanelLeft_Keybind();
            }
        }
    }

    /// <summary>
    /// Toggle WorldUI keybind function. [EVENT]
    /// </summary>
    private static void ToggleWidgetsUI_Keybind() {
        if (!SettingsPanel.settingsOpen) { GameUI.ToggleWidgetsUI(); }
    }

    /// <summary>
    /// Trigger game restart keybind function.
    /// </summary>
    private static void TriggerGameRestart_Keybind() {
        if (!SettingsPanel.settingsOpen) { GameUI.RestartGame(CosmeticsSettings.gamemode, true); }

        // EVENT:: for game restart keybind pressed
        //DevEventHandler.CheckKeybindEvent($"'RestartGame' [R] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}");
    }

    /// <summary>
    /// Toggle AAR keybind function. [EVENT]
    /// </summary>
    private static void ToggleAARPanel_Keybind() {
        if (SettingsPanel.afterActionReportSet) {
            if (!SettingsPanel.afterActionReportOpen && !SettingsPanel.settingsOpen) {
                SettingsPanel.CloseSettingsPanel();
                SettingsPanel.OpenAfterActionReport();
                GameUI.HideWidgetsUI();
            } else {
                SettingsPanel.CloseAfterActionReport();
            }
        }
        
        // EVENT:: for toggle AAR keybind pressed
        //DevEventHandler.CheckKeybindEvent($"'Toggle AAR' [TAB] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}");
    }

    /// <summary>
    /// Toggle settings panel keybind function. [EVENT]
    /// </summary>
    private static void ToggleSettingsPanel_Keybind() {
        if (KeybindsHandler.currentKey == null) {
            if (SettingsPanel.settingsOpen) {
                SettingsPanel.CloseSettingsPanel();
            } else {
                if (!SettingsPanel.afterActionReportOpen) {
                    SettingsPanel.OpenSettingsPanel();
                }
            }
        }
        
        // EVENT:: for toggle settings panel keybind pressed
        //DevEventHandler.CheckKeybindEvent($"'Toggle Settings Panel' [ESC] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}");
    }

    /// <summary>
    /// Move settings panel right keybind function.
    /// </summary>
    private static void MoveSettingsPanelRight_Keybind() {
        if (MouseLook.settingsOpen && !CrosshairImportExport.importExportPanelOpen) {
            Vector3 settingsPanelPos_Right = SettingsPanel.MoveSettingsPanelRight();
            
            // EVENT:: for settings panel being moved right
            //DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacepanelmoveright")} ({settingsPanelPos_Right})");
        }

        // EVENT:: for move settings panel right keybind pressed
        //DevEventHandler.CheckKeybindEvent($"'Move Settings Panel Right' [RightArrow] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}");
    }

    /// <summary>
    /// Move settings panel left keybind function.
    /// </summary>
    private static void MoveSettingsPanelLeft_Keybind() {
        if (MouseLook.settingsOpen && !CrosshairImportExport.importExportPanelOpen) {
            Vector3 settingsPanelPos_Left = SettingsPanel.MoveSettingsPanelLeft();

            // EVENT:: for settings panel being moved left
            //DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacepanelmoveleft")} ({settingsPanelPos_Left})");
        }
        
        // EVENT:: for move settings panel left keybind pressed
        //DevEventHandler.CheckKeybindEvent($"'Move Settings Panel Left' [LeftArrow] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}");
    }
}
