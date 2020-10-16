using UnityEngine;
using UnityEngine.UI;

public class MinimapFollowPlayer : MonoBehaviour {
    public Transform FPP;
    public Camera minimapCamera;
    public Image minimapArrow;
    public bool rotateMinimap, followPlayer, projectionOrtho;
    Vector3 newMinimapCameraPosition;

    void FixedUpdate() {
        if (projectionOrtho) { minimapCamera.orthographic = true;
        } else { minimapCamera.orthographic = false; }

        if (followPlayer) {
            minimapCamera.orthographicSize = 7.5f;
            newMinimapCameraPosition = FPP.position;
            newMinimapCameraPosition.y = transform.position.y;
            transform.position = newMinimapCameraPosition;
        } else {
            minimapCamera.orthographicSize = 49f;
            transform.position = new Vector3(14f, 22f, -2f);
        }
        
        if (rotateMinimap) {
            transform.rotation = Quaternion.Euler(90f, FPP.eulerAngles.y, 0f);
        } else {
            //Debug.Log(FPP.transform.rotation.y);
            //Debug.Log(FPP.rotation.y);
            //Debug.Log(FPP.eulerAngles.y);
            minimapArrow.transform.rotation = Quaternion.Euler(0f, 0f, -FPP.eulerAngles.y);
        }
    }
}
