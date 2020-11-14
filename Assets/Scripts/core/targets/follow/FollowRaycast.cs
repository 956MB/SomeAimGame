using UnityEngine;

public class FollowRaycast : MonoBehaviour {
    public Camera playerCamera;
    private Ray ray;
    private RaycastHit raycastHit;
    Material currentTargetRendererMaterial;
    //Light targetLight;
    private bool targetPrimary    = true;
    private static int shotsHit   = 0;
    private static int shotsTaken = 0;

    private static FollowRaycast followRaycast;
    void Awake() { followRaycast = this; }
    private Color followTargetAlbedo, followTargetEmission, followTargetLight;

    //void Start() {
    //    currentTargetRendererMaterial = GenerateFollowPath.pathFollowerTarget.GetComponent<Renderer>().material;
    //    targetLight = GenerateFollowPath.pathFollowerTarget.GetComponent<Light>();
    //}

    void Update() {
        // Only if game timer is running and gamemode is "Gamemode-Follow".
        if (GunAction.timerRunning && SpawnTargets.gamemode == Gamemode.Follow) {
            ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

            if (Physics.Raycast(ray, out raycastHit)) {
                if (raycastHit.transform.CompareTag("Target")) {
                    if (!MouseLook.settingsOpen) { GameUI.IncreaseScore_Follow(); }
                    shotsHit += 1;
                    //Debug.Log($"Hit follow pos: {raycastHit.transform.position}");

                    if (!targetPrimary) {
                        try {
                            currentTargetRendererMaterial = GenerateFollowPath.pathFollowerTarget.GetComponent<Renderer>().material;
                            //targetLight = GenerateFollowPath.pathFollowerTarget.GetComponent<Light>();

                            currentTargetRendererMaterial.SetColor("_Color", followRaycast.followTargetAlbedo);
                            currentTargetRendererMaterial.SetColor("_EmissionColor", followRaycast.followTargetEmission);
                        } catch (MissingReferenceException mre) {
                            Debug.Log("missing reference exception here: " + mre);
                        }
                        //targetLight.color = followRaycast.followTargetLight;
                        targetPrimary = true;
                    }
                }
                else {
                    if (!MouseLook.settingsOpen) { GameUI.DecreaseScore_Follow(); }

                    if (targetPrimary) {
                        try {
                            currentTargetRendererMaterial = GenerateFollowPath.pathFollowerTarget.GetComponent<Renderer>().material;
                            //targetLight = GenerateFollowPath.pathFollowerTarget.GetComponent<Light>();

                            currentTargetRendererMaterial.SetColor("_Color", TargetColors.RedAlbedo());
                            currentTargetRendererMaterial.SetColor("_EmissionColor", TargetColors.RedEmission());
                        } catch (MissingReferenceException mre) {
                            Debug.Log("missing reference exception here: " + mre);
                        }

                        //targetLight.color = TargetColors.RedLight();
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
        //followRaycast.currentTargetRendererMaterial = GenerateFollowPath.pathFollowerTarget.GetComponent<Renderer>().material;
        //followRaycast.targetLight = GenerateFollowPath.pathFollowerTarget.GetComponent<Light>();

        //followRaycast.currentTargetRendererMaterial.SetColor("_Color", albedo);
        //followRaycast.currentTargetRendererMaterial.SetColor("_EmissionColor", emission);
        //followRaycast.targetLight.color = light;
    }
}
