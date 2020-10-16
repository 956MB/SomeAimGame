using UnityEngine;
using TMPro;

public class DisableScrollViews : MonoBehaviour {
    public TMP_Text generalSubMenuText, controlsSubMenuText, crosshairSubMenuText, extraSubMenuText;
    public GameObject generalScrollView, controlsScrollView, crosshairScrollView, extraScrollView;

    void Start() {
        generalSubMenuText.color   = new Color32(255, 255, 255, 150); ;
        controlsSubMenuText.color  = new Color32(255, 255, 255, 150); ;
        crosshairSubMenuText.color = new Color32(255, 255, 255, 150); ;
        extraSubMenuText.color     = new Color32(255, 255, 255, 150); ;
        generalScrollView.transform.localScale   = new Vector3(0, 0, 0);
        controlsScrollView.transform.localScale  = new Vector3(0, 0, 0);
        crosshairScrollView.transform.localScale = new Vector3(0, 0, 0);
        extraScrollView.transform.localScale     = new Vector3(0, 0, 0);
    }
}
