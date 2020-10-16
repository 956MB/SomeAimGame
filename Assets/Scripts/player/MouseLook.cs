using UnityEngine;

public class MouseLook : MonoBehaviour {
    [Range(0, 10f)]
    public static float mouseSensitivity;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    //public float sensitivityX = 15F;
    //public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -90F;
    public float maximumY = 90F;
    float rotationY = 0F;


    //// private static float mouseStep = 5.0f;
    //private float xRotation = 0f;
    //private float mouseX, mouseY;
    public static bool settingsOpen, afterActionReportOpen;
    //Vector3 rotation;
    ////public Transform playerBody;
    //public Camera vcam;


    void Awake() {
        // Lock/hide cursor to game on awake.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start() {
        //Debug.Log("mouseLook start called");
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        mouseSensitivity = 4f;
        //mouseSensitivity *= 100f;

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


            // old mouseLook:
            //mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            //mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //xRotation -= mouseY;
            //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotation: Quaternion.Euler(x, y, z)
            //playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
/* float rotLeftRight = Input.GetAxis("Mouse X") * f_mouseSensitivity;
   transform.Rotate(0, rotLeftRight, 0);

   verRotate -= Input.GetAxis("Mouse Y") * f_mouseSensitivity;
   verRotate = Mathf.Clamp(verRotate, -upDownRange, upDownRange);
   Camera.main.transform.localRotation = Quaternion.Euler(verRotate, 0, 0);*/
