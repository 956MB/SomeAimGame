using UnityEngine;

public class Keybinds : MonoBehaviour {

    void Update() {
        // Used game keybinds
        if (Input.GetKeyDown(KeyCode.H)) {
            ToggleWorldUI_Keybind();
        } else if (Input.GetKeyDown(KeyCode.R)) {
            TriggerGameRestart_Keybind();
        } else if (Input.GetKeyDown(KeyCode.Tab)) {
            ToggleAARPanel_Keybind();
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleSettingsPanel_Keybind();
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            MoveSettingsPanelRight_Keybind();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            MoveSettingsPanelLeft_Keybind();
        }

        // Dev events test keybinds
        if (DevEventHandler.cardsOn) {
            if (Input.GetKeyDown(KeyCode.Y)) {
                DevEventHandler.CreateGamemodeCard($"'Follow' {I18nTextTranslator.SetTranslatedText("eventgamemodeselected")}");
            } else if (Input.GetKeyDown(KeyCode.U)) {
                DevEventHandler.CreateTimeCard($"{I18nTextTranslator.SetTranslatedText("eventtimerchanged")} '00:60'");
            } else if (Input.GetKeyDown(KeyCode.I)) {
                DevEventHandler.CreateCrosshairCard($"{I18nTextTranslator.SetTranslatedText("eventcrosshairgap")} '6'");
            } else if (Input.GetKeyDown(KeyCode.O)) {
                DevEventHandler.CreateTargetsCard($"[Scatter] {I18nTextTranslator.SetTranslatedText("eventtargetsnewspawnprimary")} (13,42,90).");
            } else if (Input.GetKeyDown(KeyCode.P)) {
                DevEventHandler.CreateInterfaceCard($"FPS Counter {I18nTextTranslator.SetTranslatedText("eventinterfacewidgettoggle")}");
            } else if (Input.GetKeyDown(KeyCode.J)) {
                DevEventHandler.CreateSaveCard($"'Cosmetics' {I18nTextTranslator.SetTranslatedText("eventsettingsobjectsave")}");
            } else if (Input.GetKeyDown(KeyCode.K)) {
                DevEventHandler.CreateSkyboxCard($"{I18nTextTranslator.SetTranslatedText("eventskyboxchange")} 'Skybox_Pink'");
            } else if (Input.GetKeyDown(KeyCode.L)) {
                DevEventHandler.CreateLanguageCard($"{I18nTextTranslator.SetTranslatedText("eventlanguagegameset")} 'Korean' (KOR)");
            } else if (Input.GetKeyDown(KeyCode.Semicolon)) {
                DevEventHandler.CreateKeybindCard($"'HideUI' (H) {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}");
            } else if (Input.GetKeyDown(KeyCode.Quote)) {
                DevEventHandler.CreateSoundCard($"'HitTarget' {I18nTextTranslator.SetTranslatedText("eventsoundfired")}");
            } else if (Input.GetKeyDown(KeyCode.Quote)) {
                DevEventHandler.CreateNotificationCard($"'GamemodeError' {I18nTextTranslator.SetTranslatedText("eventnotificationcreated")}");
            } else if (Input.GetKeyDown(KeyCode.C)) {
                // print child count
                DevEventHandler.PrintEventGroupCount();
            } else if (Input.GetKeyDown(KeyCode.Backspace)) {
                // delete top element
                DevEventHandler.DestroyEventCard_Top();
            }
        }
    }

    /// <summary>
    /// Toggle WorldUI keybind function. [EVENT]
    /// </summary>
    private static void ToggleWorldUI_Keybind() {
        GameUI.ToggleWorldUI();
    }

    /// <summary>
    /// Trigger game restart keybind function.
    /// </summary>
    private static void TriggerGameRestart_Keybind() {
        GameUI.RestartGame(CosmeticsSettings.gamemode);

        // EVENT:: for game restart keybind pressed
        DevEventHandler.CheckKeybindEvent($"'RestartGame' [R] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}");
    }

    /// <summary>
    /// Toggle AAR keybind function. [EVENT]
    /// </summary>
    private static void ToggleAARPanel_Keybind() {
        if (SettingsPanel.afterActionReportSet) {
            if (!SettingsPanel.afterActionReportOpen) {
                SettingsPanel.CloseSettingsPanel();
                SettingsPanel.OpenAfterActionReport();
                GameUI.HideUI();
            } else {
                SettingsPanel.CloseAfterActionReport();
            }
        }
        
        // EVENT:: for toggle AAR keybind pressed
        DevEventHandler.CheckKeybindEvent($"'Toggle AAR' [TAB] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}");
    }

    /// <summary>
    /// Toggle settings panel keybind function. [EVENT]
    /// </summary>
    private static void ToggleSettingsPanel_Keybind() {
        if (SettingsPanel.settingsOpen) {
            SettingsPanel.CloseSettingsPanel();
        } else {
            if (!SettingsPanel.afterActionReportOpen) {
                SettingsPanel.OpenSettingsPanel();
            }
        }
        
        // EVENT:: for toggle settings panel keybind pressed
        DevEventHandler.CheckKeybindEvent($"'Toggle Settings Panel' [ESC] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}");
    }

    /// <summary>
    /// Move settings panel right keybind function.
    /// </summary>
    private static void MoveSettingsPanelRight_Keybind() {
        if (MouseLook.settingsOpen) {
            Vector3 settingsPanelPos_Right = SettingsPanel.MoveSettingsPanelRight();
            
            // EVENT:: for settings panel being moved right
            DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacepanelmoveright")} ({settingsPanelPos_Right})");
        }

        // EVENT:: for move settings panel right keybind pressed
        DevEventHandler.CheckKeybindEvent($"'Move Settings Panel Right' [RightArrow] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}");
    }

    /// <summary>
    /// Move settings panel left keybind function.
    /// </summary>
    private static void MoveSettingsPanelLeft_Keybind() {
        if (MouseLook.settingsOpen) {
            Vector3 settingsPanelPos_Left = SettingsPanel.MoveSettingsPanelLeft();

            // EVENT:: for settings panel being moved left
            DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacepanelmoveleft")} ({settingsPanelPos_Left})");
        }
        
        // EVENT:: for move settings panel left keybind pressed
        DevEventHandler.CheckKeybindEvent($"'Move Settings Panel Left' [LeftArrow] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}");
    }
}
