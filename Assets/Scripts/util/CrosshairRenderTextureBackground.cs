using UnityEngine;

public class CrosshairRenderTextureBackground : MonoBehaviour {
    public Camera mainCamera;
    public RenderTexture renderTexture;

    private static CrosshairRenderTextureBackground CRTB;
    void Awake() { CRTB = this; }

    //private void Start() {
    //    initRenderTexture();
    //}

    public static void initRenderTexture() {
        int startingMask            = CRTB.mainCamera.cullingMask;
        var newMask                 = startingMask & ~(1 << 10);
        CRTB.mainCamera.cullingMask = newMask;

        GameUI.HideUI();

        CRTB.mainCamera.targetTexture = CRTB.renderTexture;
        RenderTexture.active          = CRTB.renderTexture;
        CRTB.mainCamera.Render();
        RenderTexture.active          = null;
        CRTB.mainCamera.targetTexture = null;
        
        GameUI.ShowUI();
        CRTB.mainCamera.cullingMask = startingMask;
    }
}
