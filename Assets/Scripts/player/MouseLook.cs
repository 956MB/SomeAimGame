using UnityEngine;

public class MouseLook : MonoBehaviour {
    [Range(0, 10f)]
    public static float mouseSensitivity;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -90F;
    public float maximumY = 90F;
    float rotationY       = 0F;

    public static bool settingsOpen, afterActionReportOpen;

    void Awake() {
        // Lock/hide cursor to game on awake.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    void Start() {
        mouseSensitivity = 4f;

        // Init saved mouse sesitivity setting.
        ExtraSaveSystem.InitSavedExtraSettings();
        StatsManager.HideExtraStatsPanel();
    }

    void Update() {
        // Only detect mouse movement in game if settings panel/'AfterActionReport' are not open.
        if (!settingsOpen && !afterActionReportOpen) {
            // new mouseLook:
            if (axes == RotationAxes.MouseXAndY) {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

                rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            } else if (axes == RotationAxes.MouseX) {
                transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0);
            } else {
                rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
}
