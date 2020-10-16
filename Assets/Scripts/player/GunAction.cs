﻿using UnityEngine;
using TMPro;

public class GunAction : MonoBehaviour {
    public Camera playerCamera;
    public TMP_Text reactionTimeText;
    public static bool timerRunning = true;
    public GameObject hitEffect;

    private static GunAction gunAction;
    void Awake() { gunAction = this; }

    void LateUpdate() {
        // Shoot (KeyCode.Mouse0) if game timer still running and settings panel not open (game paused).
        if (GameUI.timeCount > -1) {
            if (!MouseLook.settingsOpen && Input.GetKeyDown(KeyCode.Mouse0) && SpawnTargets.gamemode != "Gamemode-Follow") {
                Shoot();
            }
        } else {
            if (timerRunning) {
                //reactionTimeText.SetText($"{(int)GameUI.reactionTimeList.Average()}ms");
                //StatsManager.setAfterActionStats();

                //GameUI.stopEverything();
                timerRunning = false;

                // Show 'AfterActionReport' if setting enabled.
                if (ToggleHandler.ShowAAROn()) {
                    GameUI.HideUI();
                    SettingsPanel.OpenAfterActionReport();
                    //ConnectingLine2D.drawConnectingLine();
                }
            }
        }
    }

    /// <summary>
    /// Fires raycast from center screen to "shoot" and hit targets in game.
    /// </summary>
    private void Shoot() {
        RaycastHit gunHit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out gunHit)) {
            switch (gunHit.transform.gameObject.tag) {
                case "Target":
                    GameUI.UpdateReactionTime();
                    if (ToggleHandler.TargetSoundOn()) { HitSound.PlayHitSound(); }
                    //TriggerHitEffect(gunHit);

                    if (SpawnTargets.gamemode == "Gamemode-Pairs") {
                        if (!SpawnTargets.pairStarterActive) {
                            bool correctTargetHit = SpawnTargets.CheckPairHit(gunHit.transform.position);
                            if (correctTargetHit) {
                                GameUI.IncreaseScore();
                            } else {
                                GameUI.DecreaseScore();
                            }
                        }
                        SpawnTargets.CheckTargetCount(gunHit, true);
                    } else {
                        GameUI.IncreaseScore();
                        SpawnTargets.CheckTargetCount(gunHit, true);
                    }
                    break;
                default:
                    //TriggerHitEffect(gunHit);
                    if (SpawnTargets.gamemode != "Gamemode-Pairs") {
                        GameUI.DecreaseScore();
                        SpawnTargets.CheckTargetCount(gunHit, false);
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Triggers "impact" particle effect at raycast hit (hitPos) location.
    /// </summary>
    /// <param name="hitPos"></param>
    private static void TriggerHitEffect(RaycastHit hitPos) {
        GameObject hitEffectInstantiated = Instantiate(gunAction.hitEffect, hitPos.point, Quaternion.LookRotation(hitPos.normal));
        Destroy(hitEffectInstantiated, 2f);
    }
}
