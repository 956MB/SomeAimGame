using UnityEngine;
using TMPro;

using SomeAimGame.Gamemode;
using SomeAimGame.Targets;

public class GunAction : MonoBehaviour {
    public Camera playerCamera;
    public TMP_Text reactionTimeText;
    public static bool timerRunning = true;
    public GameObject hitEffect;

    private static GunAction gunAction;
    void Awake() { gunAction = this; }

    private void Start() {
        //TempValues.SetTimerRunningTemp(true);
    }

    void LateUpdate() {
        // Shoot (KeyCode.Mouse0) if game timer still running and settings panel not open (game paused).
        if (GameUI.timeCount > -1) {
            if (!MouseLook.settingsOpen && Keybinds.keybindsLoaded && SpawnTargets.gamemode != GamemodeType.FOLLOW) {
                if (Input.GetKeyDown(KeybindSettings.shoot)) { Shoot(); }
            }
        } else {
            if (timerRunning) {
                timerRunning = false;
                //TempValues.SetTimerRunningTemp(false);

                // Show 'AfterActionReport' if setting enabled.
                if (ToggleHandler.ShowAAROn()) {
                    GameUI.HideWidgetsUI();
                    SettingsPanel.OpenAfterActionReport();
                }
            }
        }
    }

    /// <summary>
    /// Fires raycast from center screen to "shoot" and hit targets in game. [EVENT]
    /// </summary>
    private void Shoot() {
        RaycastHit gunHit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out gunHit)) {
            switch (gunHit.transform.gameObject.tag) {
                case "Target":
                    GameUI.UpdateReactionTime();
                    if (ToggleHandler.TargetSoundOn()) { HitSound.PlayHitSound(); }

                    if (SpawnTargets.gamemode == GamemodeType.PAIRS) {
                        if (!SpawnTargets.pairStarterActive) {
                            bool correctTargetHit = SpawnTargets.CheckPairHit(gunHit.transform.position);
                            if (correctTargetHit) {
                                GameUI.IncreaseScore();

                                // EVENT:: for pair target hit, update score
                                //DevEventHandler.CheckTargetsEvent($"{I18nTextTranslator.SetTranslatedText("eventtargetshitpairs")} ({gunHit.transform.position})");
                            } else {
                                GameUI.DecreaseScore();
                                SpawnTargets.GamemodePairsMiss();

                                // EVENT:: for pair target miss, descrease score
                                //DevEventHandler.CheckTargetsEvent($"{I18nTextTranslator.SetTranslatedText("eventtargetsmisspairs")} ({gunHit.transform.position})");
                            }
                        }
                        SpawnTargets.CheckTargetCount(gunHit, true);
                    } else {
                        GameUI.IncreaseScore();
                        SpawnTargets.CheckTargetCount(gunHit, true);

                        // EVENT:: for target hit, update score
                        //DevEventHandler.CheckTargetsEvent($"{I18nTextTranslator.SetTranslatedText("eventtargetshit")} ({gunHit.transform.position})");
                    }
                    break;
                default:
                    if (SpawnTargets.gamemode != GamemodeType.PAIRS) {
                        GameUI.DecreaseScore();
                        SpawnTargets.CheckTargetCount(gunHit, false);

                        // EVENT:: for target miss, descrease score
                        //DevEventHandler.CheckTargetsEvent($"{I18nTextTranslator.SetTranslatedText("eventtargetsmiss")} ({gunHit.transform.position})");
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
