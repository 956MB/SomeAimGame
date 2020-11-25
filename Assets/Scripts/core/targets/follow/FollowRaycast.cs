using UnityEngine;

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
        if (GunAction.timerRunning && SpawnTargets.gamemode == Gamemode.FOLLOW) {
            ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

            if (Physics.Raycast(ray, out raycastHit)) {
                if (raycastHit.transform.CompareTag("Target")) {
                    if (!MouseLook.settingsOpen) { GameUI.IncreaseScore_Follow(); }
                    shotsHit += 1;

                    if (!targetPrimary) {
                        try {
                            currentTargetRendererMaterial = GenerateFollowPath.pathFollowerTarget.GetComponent<Renderer>().material;

                            currentTargetRendererMaterial.SetColor("_Color", followRaycast.followTargetAlbedo);
                            currentTargetRendererMaterial.SetColor("_EmissionColor", followRaycast.followTargetEmission);
                        } catch (MissingReferenceException mre) {
                            //Debug.Log("missing reference exception here: " + mre);
                        }
                        targetPrimary = true;
                    }
                } else {
                    if (!MouseLook.settingsOpen) { GameUI.DecreaseScore_Follow(); }

                    if (targetPrimary) {
                        try {
                            currentTargetRendererMaterial = GenerateFollowPath.pathFollowerTarget.GetComponent<Renderer>().material;

                            currentTargetRendererMaterial.SetColor("_Color", TargetColors.RedAlbedo);
                            currentTargetRendererMaterial.SetColor("_EmissionColor", TargetColors.RedEmission);
                        } catch (MissingReferenceException mre) {
                            //Debug.Log("missing reference exception here: " + mre);
                        }

                        targetPrimary = false;
                    }
                }
            }

            shotsTaken += 1;
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
