using UnityEngine;

public class CrosshairHide : MonoBehaviour {
    public GameObject crosshairMain, crosshairSettings;
    private Vector3 offScreen = new Vector3(-200, 0, 0);

    private static CrosshairHide crosshairHide;
    void Awake() { crosshairHide = this; }

    /// <summary>
    /// Hides crosshair image above crosshair settings panel.
    /// </summary>
    public static void HideCrosshairs() {
        crosshairHide.crosshairSettings.transform.position = crosshairHide.offScreen;
        GameUI.HideGameObject_Layer(crosshairHide.crosshairSettings);
        GameUI.HideGameObject_Layer(crosshairHide.crosshairMain);
    }

    public static void ShowMainCrosshair() {
        GameUI.ShowGameObject_Layer(crosshairHide.crosshairMain);
    }

    /// <summary>
    /// Shows crosshair image above crosshair settings panel.
    /// </summary>
    public static void ShowSettingsCrosshair() {
        GameObject crosshairCanvasPos = GameObject.Find($"CrosshairSettingsCanvas");
        GameUI.ShowGameObject_Layer(crosshairHide.crosshairSettings);
        //subMenu.crosshairImageSettings.transform.parent = crosshairCanvasPos.transform;
        crosshairHide.crosshairSettings.transform.position = crosshairCanvasPos.transform.position;
        //Debug.Log($"{subMenu.crosshairImageSettings.GetComponent<RectTransform>().sizeDelta.x}, {subMenu.crosshairImageSettings.GetComponent<RectTransform>().sizeDelta.y}");
    }
}
