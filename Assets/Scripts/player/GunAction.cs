using UnityEngine;
using TMPro;

using SomeAimGame.Gamemode;
using SomeAimGame.Targets;
using SomeAimGame.SFX;
using SomeAimGame.Console;

public class GunAction : MonoBehaviour {
    public Camera playerCamera;
    public TMP_Text reactionTimeText;
    public static bool timerRunning = true;
    public GameObject hitEffect;

    private RaycastHit gunHit;

    private static GunAction gunAction;
    void Awake() { gunAction = this; }

    void LateUpdate() {
        // Shoot (KeyCode.Mouse0) if game timer still running and settings panel not open (game paused).
        if (GameUI.timeCount > -1) {
            if (!MouseLook.settingsOpen && Keybinds.keybindsLoaded && SpawnTargets.gamemode != GamemodeType.FOLLOW && !GameUI.countdownShown && !SAGConsole.consoleActive) {
                if (Input.GetKeyDown(KeybindSettings.shoot)) { Shoot(); }
            }
        } else {
            if (timerRunning) {
                timerRunning = false;

                // Show 'AfterActionReport' if setting enabled.
                //GameUI.HideWidgetsUI();
                //SettingsPanel.OpenAfterActionReport();
            }
        }
    }

    /// <summary>
    /// Fires raycast from center screen to "shoot" and hit targets in game. [EVENT]
    /// </summary>
    private void Shoot() {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out gunHit)) {
            switch (gunHit.transform.gameObject.tag) {
                case "Target":
                    GameUI.UpdateReactionTime();
                    SFXManager.CheckPlayTargetHit();

                    if (SpawnTargets.gamemode == GamemodeType.PAIRS) {
                        if (!SpawnTargets.pairStarterActive) {
                            bool correctTargetHit = TargetUtil.CheckPairHit(gunHit.transform.position);
                            if (correctTargetHit) {
                                GameUI.IncreaseScore();
                            } else {
                                GameUI.DecreaseScore();
                                SpawnTargets.GamemodePairsMiss();
                            }
                        }
                        SpawnTargets.CheckTargetHit(gunHit, true);
                    } else {
                        GameUI.IncreaseScore();
                        SpawnTargets.CheckTargetHit(gunHit, true);
                    }
                    break;

                default:
                    if (SpawnTargets.gamemode != GamemodeType.PAIRS) {
                        GameUI.DecreaseScore();
                        SpawnTargets.CheckTargetHit(gunHit, false);
                        SFXManager.CheckPlayTargetMiss();
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
