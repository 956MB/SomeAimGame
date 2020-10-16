using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DisableMinimapShadows : MonoBehaviour {
    public Camera mainCamera;

    private ShadowQuality _originalShadowSettings;

    private void Awake() {
        mainCamera = GetComponent<Camera>();
        _originalShadowSettings = QualitySettings.shadows;
    }

    private void OnEnable() {
        Camera.onPreRender += MyPreRender;
        Camera.onPostRender += MyPostRender;
    }

    private void OnDisable() {
        Camera.onPreRender -= MyPreRender;
        Camera.onPostRender -= MyPostRender;
    }

    private void MyPreRender(Camera cam) {
        QualitySettings.shadows = cam == mainCamera ? _originalShadowSettings : ShadowQuality.Disable;
        //QualitySettings.shadows = ShadowQuality.Disable;
    }

    private void MyPostRender(Camera cam) {
        QualitySettings.shadows = _originalShadowSettings;
    }
}
