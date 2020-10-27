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
            DevEventHandler.CreateGamemodeEvent("'Follow' GAMEMODE SELECTED.");
        } else if (Input.GetKeyDown(KeyCode.U)) {
            DevEventHandler.CreateTimeEvent("GAME TIMER CHANGED TO '00:60'.");
        } else if (Input.GetKeyDown(KeyCode.I)) {
            DevEventHandler.CreateCrosshairEvent("CROSSHAIR GAP CHANGED TO '6'.");
        } else if (Input.GetKeyDown(KeyCode.O)) {
            DevEventHandler.CreateTargetsEvent("[Scatter] NEW TARGET SPAWN: (13,42,90).");
        } else if (Input.GetKeyDown(KeyCode.P)) {
            DevEventHandler.CreateInterfaceEvent("FPS Counter TOGGLE CLICKED.");
        } else if (Input.GetKeyDown(KeyCode.J)) {
            DevEventHandler.CreateSaveEvent("MOUSE SENSETIVITY SETTING SAVED.");
        } else if (Input.GetKeyDown(KeyCode.K)) {
            DevEventHandler.CreateSkyboxEvent("SKYBOX CHANGED TO 'Skybox_Pink'.");
        } else if (Input.GetKeyDown(KeyCode.L)) {
            DevEventHandler.CreateLanguageEvent("GAME LANGUAGE SET TO 'Korean' (KOR).");
        } else if (Input.GetKeyDown(KeyCode.Semicolon)) {
            DevEventHandler.CreateKeybindEvent("'HideUI' (H) KEYBIND PRESSED.");
        } else if (Input.GetKeyDown(KeyCode.Quote)) {
            DevEventHandler.CreateSoundEvent("'HitTarget' SOUND EVENT FIRED.");
        } else if (Input.GetKeyDown(KeyCode.C)) {
            // print child count
            DevEventHandler.PrintEventGroupCount();
        } else if (Input.GetKeyDown(KeyCode.Backspace)) {
            // delete top element
            DevEventHandler.DestroyEventCard_Top();
        }
    }
}
