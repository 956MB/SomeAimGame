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
        if (DevEventHandler.eventsOn) {
            if (Input.GetKeyDown(KeyCode.Y)) {
                DevEventHandler.CreateGamemodeEvent($"'Follow' {I18nTextTranslator.SetTranslatedText("eventgamemodeselected")}");
            } else if (Input.GetKeyDown(KeyCode.U)) {
                DevEventHandler.CreateTimeEvent($"{I18nTextTranslator.SetTranslatedText("eventtimerchanged")} '00:60'");
            } else if (Input.GetKeyDown(KeyCode.I)) {
                DevEventHandler.CreateCrosshairEvent($"{I18nTextTranslator.SetTranslatedText("eventcrosshairgap")} '6'");
            } else if (Input.GetKeyDown(KeyCode.O)) {
                DevEventHandler.CreateTargetsEvent($"[Scatter] {I18nTextTranslator.SetTranslatedText("eventtargetsnewspawnprimary")} (13,42,90).");
            } else if (Input.GetKeyDown(KeyCode.P)) {
                DevEventHandler.CreateInterfaceEvent($"FPS Counter {I18nTextTranslator.SetTranslatedText("eventinterfacewidgettoggle")}");
            } else if (Input.GetKeyDown(KeyCode.J)) {
                DevEventHandler.CreateSaveEvent($"'Cosmetics' {I18nTextTranslator.SetTranslatedText("eventsettingsobjectsave")}");
            } else if (Input.GetKeyDown(KeyCode.K)) {
                DevEventHandler.CreateSkyboxEvent($"{I18nTextTranslator.SetTranslatedText("eventskyboxchange")} 'Skybox_Pink'");
            } else if (Input.GetKeyDown(KeyCode.L)) {
                DevEventHandler.CreateLanguageEvent($"{I18nTextTranslator.SetTranslatedText("eventlanguagegameset")} 'Korean' (KOR)");
            } else if (Input.GetKeyDown(KeyCode.Semicolon)) {
                DevEventHandler.CreateKeybindEvent($"'HideUI' (H) {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}");
            } else if (Input.GetKeyDown(KeyCode.Quote)) {
                DevEventHandler.CreateSoundEvent($"'HitTarget' {I18nTextTranslator.SetTranslatedText("eventsoundfired")}");
            } else if (Input.GetKeyDown(KeyCode.Quote)) {
                DevEventHandler.CreateNotificationEvent($"'GamemodeError' {I18nTextTranslator.SetTranslatedText("eventnotificationcreated")}");
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
    /// Toggle WorldUI keybind function.
    /// </summary>
    private static void ToggleWorldUI_Keybind() {
        GameUI.ToggleWorldUI();

        // EVENT:: for toggle world UI keybind pressed
        if (DevEventHandler.eventsOn) { DevEventHandler.CreateKeybindEvent($"'Toggle WorldUI' [H] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}"); }
    }

    /// <summary>
    /// Trigger game restart keybind function.
    /// </summary>
    private static void TriggerGameRestart_Keybind() {
        GameUI.RestartGame(CosmeticsSettings.gamemode);

        // EVENT:: for game restart keybind pressed
        if (DevEventHandler.eventsOn) { DevEventHandler.CreateKeybindEvent($"'RestartGame' [R] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}"); }
    }

    /// <summary>
    /// Toggle AAR keybind function.
    /// </summary>
    private static void ToggleAARPanel_Keybind() {
        if (SettingsPanel.afterActionReportSet) {
            if (!SettingsPanel.afterActionReportOpen) {
                SettingsPanel.OpenAfterActionReport();
                SettingsPanel.CloseSettingsPanel();
                GameUI.HideUI();

                // EVENT:: for AAR panel being opened
                if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfaceaaropened")}"); }
            } else {
                SettingsPanel.CloseAfterActionReport();

                // EVENT:: for AAR panel being opened
                if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfaceaarclosed")}"); }
            }
        }
        
        // EVENT:: for toggle AAR keybind pressed
        if (DevEventHandler.eventsOn) { DevEventHandler.CreateKeybindEvent($"'Toggle AAR' [TAB] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}"); }
    }

    /// <summary>
    /// Toggle settings panel keybind function.
    /// </summary>
    private static void ToggleSettingsPanel_Keybind() {
        if (SettingsPanel.settingsOpen) {
            SettingsPanel.CloseSettingsPanel();

            // EVENT:: for settings panel being closed
            if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacesettingsclosed")}"); }
        } else {
            if (!SettingsPanel.afterActionReportOpen) {
                SettingsPanel.OpenSettingsPanel();

                // EVENT:: for settings panel being opened
                if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacesettingsopened")}"); }
            }
        }
        
        // EVENT:: for toggle settings panel keybind pressed
        if (DevEventHandler.eventsOn) { DevEventHandler.CreateKeybindEvent($"'Toggle Settings Panel' [ESC] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}"); }
    }

    /// <summary>
    /// Move settings panel right keybind function.
    /// </summary>
    private static void MoveSettingsPanelRight_Keybind() {
        if (MouseLook.settingsOpen) {
            Vector3 settingsPanelPos_Right = SettingsPanel.MoveSettingsPanelRight();
            
            // EVENT:: for settings panel being moved right
            if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacepanelmoveright")} ({settingsPanelPos_Right})"); }
        }

        // EVENT:: for move settings panel right keybind pressed
        if (DevEventHandler.eventsOn) { DevEventHandler.CreateKeybindEvent($"'Move Settings Panel Right' [RightArrow] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}"); }
    }

    /// <summary>
    /// Move settings panel left keybind function.
    /// </summary>
    private static void MoveSettingsPanelLeft_Keybind() {
        if (MouseLook.settingsOpen) {
            Vector3 settingsPanelPos_Left = SettingsPanel.MoveSettingsPanelLeft();

            // EVENT:: for settings panel being moved left
            if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacepanelmoveleft")} ({settingsPanelPos_Left})"); }
        }
        
        // EVENT:: for move settings panel left keybind pressed
        if (DevEventHandler.eventsOn) { DevEventHandler.CreateKeybindEvent($"'Move Settings Panel Left' [LeftArrow] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}"); }
    }
}
