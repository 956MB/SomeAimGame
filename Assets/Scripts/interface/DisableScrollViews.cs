using UnityEngine;
using TMPro;

using SomeAimGame.Utilities;

public class DisableScrollViews : MonoBehaviour {
    public TMP_Text generalSubMenuText, controlsSubMenuText, crosshairSubMenuText, extraSubMenuText;
    public GameObject generalScrollView, controlsScrollView, crosshairScrollView, extraScrollView;

    void Start() {
        generalSubMenuText.color                 = InterfaceColors.unselectedColor;
        controlsSubMenuText.color                = InterfaceColors.unselectedColor;
        crosshairSubMenuText.color               = InterfaceColors.unselectedColor;
        extraSubMenuText.color                   = InterfaceColors.unselectedColor;
        generalScrollView.transform.localScale   = new Vector3(0, 0, 0);
        controlsScrollView.transform.localScale  = new Vector3(0, 0, 0);
        crosshairScrollView.transform.localScale = new Vector3(0, 0, 0);
        extraScrollView.transform.localScale     = new Vector3(0, 0, 0);
    }
}
