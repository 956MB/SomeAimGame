using Steamworks;
using UnityEngine;

public class Keybinds : MonoBehaviour {

    void Update() {
        //if (Input.GetKeyDown(KeyCode.M)) {
        //    ToggleMinimap.toggleMinimap();
        if (Input.GetKeyDown(KeyCode.H)) {
            GameUI.ToggleWorldUI();
        } else if (Input.GetKeyDown(KeyCode.R)) {
            //Debug.Log("restart keybind hit");
            GameUI.RestartGame(CosmeticsSettings.gamemode);
            //GameUI.triggerRestart = true;
            //if (GameUI.timeCount < 0) {
            //    Debug.Log("new timer coroutine started");
            //    GameUI.restartTimerRoutine();
            //}
        } else if (Input.GetKeyDown(KeyCode.Tab)) {
            if (SettingsPanel.afterActionReportSet) {
                if (!SettingsPanel.afterActionReportOpen) {
                    SettingsPanel.OpenAfterActionReport();
                    SettingsPanel.CloseSettingsPanel();
                    GameUI.HideUI();
                } else {
                    SettingsPanel.CloseAfterActionReport();
                    //GameUI.showUI();
                }
            }
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            if (SettingsPanel.settingsOpen) {
                SettingsPanel.CloseSettingsPanel();
            } else {
                if (!SettingsPanel.afterActionReportOpen) {
                    SettingsPanel.OpenSettingsPanel();
                }
            }

            //if (SettingsPanel.afterActionReportOpen) {
            //    SettingsPanel.closeAfterActionReport();
            //}
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (MouseLook.settingsOpen) {
                SettingsPanel.MoveSettingsPanelRight();
            }
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (MouseLook.settingsOpen) {
                SettingsPanel.MoveSettingsPanelLeft();
            }
        }

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
