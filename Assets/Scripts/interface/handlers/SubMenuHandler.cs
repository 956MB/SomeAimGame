using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubMenuHandler : MonoBehaviour {
    public TMP_Text gamemodeSubMenuText, generalSubMenuText, controlsSubMenuText, crosshairSubMenuText, extraSubMenuText;
    public GameObject gamemodeScrollView, generalScrollView, controlsScrollView, crosshairScrollView, extraScrollView;

    public static Color32 disabledSubMenuTextColor  = new Color32(255,255,255,150);
    public static Color32 enabledSubMenuTextColor   = new Color32(255,255,255,255);
    public static Color32 hoveredSubMenuTextColor   = new Color32(255,255,255,190);
    public static Vector3 disabledSubMenuScrollView = new Vector3(0, 0, 0);
    public static Vector3 enabledSubMenuScrollView  = new Vector3(1, 1, 1);

    private static string activeSubMenuText = "GamemodeTitleText (TMP)";

    private static SubMenuHandler subMenu;
    void Awake() { subMenu = this; }

    /// <summary>
    /// Sets active sub menu based on button clicked (buttonClicked).
    /// </summary>
    /// <param name="buttonClicked"></param>
    public void SetSubMenuText(GameObject buttonClicked) {
        string buttonClickedName = buttonClicked.name;
        activeSubMenuText = buttonClicked.GetComponentInChildren<TMP_Text>().name;
        ClearSubMenus();

        switch (buttonClickedName) {
            case "GamemodeSubMenuButton":
                gamemodeSubMenuText.color               = enabledSubMenuTextColor;
                gamemodeScrollView.transform.localScale = enabledSubMenuScrollView;
                ScrollRectExtension.ScrollToTop(gamemodeScrollView.GetComponent<ScrollRect>());
                break;
            case "GeneralSubMenuButton":
                generalSubMenuText.color               = enabledSubMenuTextColor;
                generalScrollView.transform.localScale = enabledSubMenuScrollView;
                ScrollRectExtension.ScrollToTop(generalScrollView.GetComponent<ScrollRect>());
                break;
            case "ControlsSubMenuButton":
                controlsSubMenuText.color               = enabledSubMenuTextColor;
                controlsScrollView.transform.localScale = enabledSubMenuScrollView;
                ScrollRectExtension.ScrollToTop(controlsScrollView.GetComponent<ScrollRect>());
                break;
            case "CrosshairSubMenuButton":
                crosshairSubMenuText.color               = enabledSubMenuTextColor;
                crosshairScrollView.transform.localScale = enabledSubMenuScrollView;
                ScrollRectExtension.ScrollToTop(crosshairScrollView.GetComponent<ScrollRect>());
                break;
            case "ExtraSubMenuButton":
                extraSubMenuText.color               = enabledSubMenuTextColor;
                extraScrollView.transform.localScale = enabledSubMenuScrollView;
                ScrollRectExtension.ScrollToTop(extraScrollView.GetComponent<ScrollRect>());
                break;
        }

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound02(); }
    }

    /// <summary>
    /// Sets sub menu text color to hovered color (hoveredSubMenuTextColor) when pointer enter.
    /// </summary>
    /// <param name="subMenuText"></param>
    public void SetHoveredSubMenuColor(TMP_Text subMenuText) {
        if (subMenuText.name != activeSubMenuText) { subMenuText.color = hoveredSubMenuTextColor; }
    }

    /// <summary>
    /// Sets sub menu text color to disabled color (disabledSubMenuTextColor) when pointer exit.
    /// </summary>
    /// <param name="subMenuText"></param>
    public void ClearHoveredSubMenuColor(TMP_Text subMenuText) {
        if (subMenuText.name != activeSubMenuText) { subMenuText.color = disabledSubMenuTextColor; }
    }

    /// <summary>
    /// Clears all sub menu buttons to unselected color and resets all scroll view localscales.
    /// </summary>
    public void ClearSubMenus() {
        gamemodeSubMenuText.color  = disabledSubMenuTextColor;
        generalSubMenuText.color   = disabledSubMenuTextColor;
        controlsSubMenuText.color  = disabledSubMenuTextColor;
        crosshairSubMenuText.color = disabledSubMenuTextColor;
        extraSubMenuText.color     = disabledSubMenuTextColor;
        gamemodeScrollView.transform.localScale  = disabledSubMenuScrollView;
        generalScrollView.transform.localScale   = disabledSubMenuScrollView;
        controlsScrollView.transform.localScale  = disabledSubMenuScrollView;
        crosshairScrollView.transform.localScale = disabledSubMenuScrollView;
        extraScrollView.transform.localScale     = disabledSubMenuScrollView;
    }
}
