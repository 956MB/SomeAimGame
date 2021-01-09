using UnityEngine;
using TMPro;

using SomeAimGame.Utilities;

public class DisableScrollViews : MonoBehaviour {
    public TMP_Text generalSubMenuText, controlsSubMenuText, crosshairSubMenuText, extraSubMenuText;
    public GameObject generalScrollView, controlsScrollView, crosshairScrollView, extraScrollView;
    Vector3 zero = new Vector3(0, 0, 0);

    void Start() {
        generalSubMenuText.color                 = InterfaceColors.unselectedColor;
        controlsSubMenuText.color                = InterfaceColors.unselectedColor;
        crosshairSubMenuText.color               = InterfaceColors.unselectedColor;
        extraSubMenuText.color                   = InterfaceColors.unselectedColor;
        generalScrollView.transform.localScale   = zero;
        controlsScrollView.transform.localScale  = zero;
        crosshairScrollView.transform.localScale = zero;
        extraScrollView.transform.localScale     = zero;
    }
}
