using UnityEngine;

using SomeAimGame.Gamemode;
using SomeAimGame.Targets;

public class FollowRaycast : MonoBehaviour {
    public Camera playerCamera;
    private Ray ray;
    private RaycastHit raycastHit;
    Material currentTargetRendererMaterial;
    private bool targetPrimary    = true;
    private static int shotsHit   = 0;
    private static int shotsTaken = 0;

    private static FollowRaycast followRaycast;
    void Awake() { followRaycast = this; }
    private Color followTargetAlbedo, followTargetEmission, followTargetLight;

    void Update() {
        // Only if game timer is running and gamemode is "Gamemode-Follow".
        if (GunAction.timerRunning && SpawnTargets.gamemode == GamemodeType.FOLLOW) {
            ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

            if (Physics.Raycast(ray, out raycastHit)) {
                if (raycastHit.transform.CompareTag("Target")) {
                    if (!MouseLook.settingsOpen) {
                        shotsHit += 1;
                        SpawnTargets.shotsHit += 1;
                        GameUI.IncreaseScore_Follow();
                        GameUI.UpdateReactionTime();
                    }

                    if (!targetPrimary) {
                        try {
                            currentTargetRendererMaterial = TargetPathing.pathFollowerTarget.GetComponent<Renderer>().material;

                            currentTargetRendererMaterial.SetColor("_Color", followRaycast.followTargetAlbedo);
                            currentTargetRendererMaterial.SetColor("_EmissionColor", followRaycast.followTargetEmission);
                        } catch (MissingReferenceException mre) {
                            //Debug.Log("missing reference exception here: " + mre);
                        }
                        targetPrimary = true;
                    }
                } else {
                    if (!MouseLook.settingsOpen) {
                        SpawnTargets.shotMisses += 1;
                        GameUI.DecreaseScore_Follow();
                    }

                    if (targetPrimary) {
                        try {
                            currentTargetRendererMaterial = TargetPathing.pathFollowerTarget.GetComponent<Renderer>().material;

                            currentTargetRendererMaterial.SetColor("_Color", TargetColors.RedAlbedo);
                            currentTargetRendererMaterial.SetColor("_EmissionColor", TargetColors.RedEmission);
                        } catch (MissingReferenceException mre) {
                            //Debug.Log("missing reference exception here: " + mre);
                        }

                        targetPrimary = false;
                    }
                }
            }

            if (!MouseLook.settingsOpen) {
                shotsTaken += 1;
                SpawnTargets.totalCount += 1;
            }

            GameUI.UpdateAccuracy(shotsHit, shotsTaken);
        }
    }

    /// <summary>
    /// Changes target color in follow gamemode based on supplied albedo, emission and light.
    /// </summary>
    /// <param name="albedo"></param>
    /// <param name="emission"></param>
    /// <param name="light"></param>
    public static void ChangeFollowTargetColor(Color albedo, Color emission, Color light) {
        followRaycast.followTargetAlbedo   = albedo;
        followRaycast.followTargetEmission = emission;
        followRaycast.followTargetLight    = light;
    }
}
