using UnityEngine;
using UnityEngine.UI;

public class ToggleMinimap : MonoBehaviour {
    public Image minimapContainer;
    public Image minimapArrow;
    public RawImage minimapRawImage;
    public static bool minimapEnabled;

    private static ToggleMinimap Minimap;
    void Awake() { Minimap = this; }

    void Start() { enableDisableMinimap(); }

    public static void toggleMinimap() {
        minimapEnabled = minimapEnabled ? false : true;
        enableDisableMinimap();
    }

    public static void enableDisableMinimap() {
        if (minimapEnabled) {
            Minimap.minimapContainer.enabled = true;
            Minimap.minimapRawImage.enabled = true;
            Minimap.minimapArrow.enabled = true;
        } else {
            Minimap.minimapContainer.enabled = false;
            Minimap.minimapRawImage.enabled = false;
            Minimap.minimapArrow.enabled = false;
        }
    }
}
