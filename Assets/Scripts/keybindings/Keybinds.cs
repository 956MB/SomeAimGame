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
    }
}
