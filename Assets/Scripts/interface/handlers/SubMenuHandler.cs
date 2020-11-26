using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubMenuHandler : MonoBehaviour {
    public TMP_Text gamemodeSubMenuText, generalSubMenuText, controlsSubMenuText, crosshairSubMenuText, extraSubMenuText;
    public GameObject gamemodeScrollView, generalScrollView, controlsScrollView, crosshairScrollView, extraScrollView;
    public Image gamemodeBar, generalBar, controlsBar, crosshairBar, extraBar;

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
                gamemodeSubMenuText.color               = InterfaceColors.selectedColor;
                gamemodeScrollView.transform.localScale = enabledSubMenuScrollView;
                ScrollRectExtension.ScrollToTop(gamemodeScrollView.GetComponent<ScrollRect>());
                gamemodeBar.transform.gameObject.SetActive(true);
                break;
            case "GeneralSubMenuButton":
                generalSubMenuText.color               = InterfaceColors.selectedColor;
                generalScrollView.transform.localScale = enabledSubMenuScrollView;
                ScrollRectExtension.ScrollToTop(generalScrollView.GetComponent<ScrollRect>());
                generalBar.transform.gameObject.SetActive(true);
                break;
            case "ControlsSubMenuButton":
                controlsSubMenuText.color               = InterfaceColors.selectedColor;
                controlsScrollView.transform.localScale = enabledSubMenuScrollView;
                ScrollRectExtension.ScrollToTop(controlsScrollView.GetComponent<ScrollRect>());
                controlsBar.transform.gameObject.SetActive(true);
                break;
            case "CrosshairSubMenuButton":
                crosshairSubMenuText.color               = InterfaceColors.selectedColor;
                crosshairScrollView.transform.localScale = enabledSubMenuScrollView;
                ScrollRectExtension.ScrollToTop(crosshairScrollView.GetComponent<ScrollRect>());
                crosshairBar.transform.gameObject.SetActive(true);
                break;
            case "ExtraSubMenuButton":
                extraSubMenuText.color               = InterfaceColors.selectedColor;
                extraScrollView.transform.localScale = enabledSubMenuScrollView;
                ScrollRectExtension.ScrollToTop(extraScrollView.GetComponent<ScrollRect>());
                extraBar.transform.gameObject.SetActive(true);
                break;
        }

        CrosshairOptionsObject.SaveCrosshairObject(false);

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Click(); }
    }

    /// <summary>
    /// Sets sub menu text color to hovered color (hoveredSubMenuTextColor) when pointer enter.
    /// </summary>
    /// <param name="subMenuText"></param>
    public void SetHoveredSubMenuColor(TMP_Text subMenuText) {
        if (subMenuText.name != activeSubMenuText) {
            subMenuText.color = InterfaceColors.hoveredColor;
            if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_HoverOuter(); }
        }
    }

    /// <summary>
    /// Sets sub menu text color to disabled color (disabledSubMenuTextColor) when pointer exit.
    /// </summary>
    /// <param name="subMenuText"></param>
    public void ClearHoveredSubMenuColor(TMP_Text subMenuText) {
        if (subMenuText.name != activeSubMenuText) { subMenuText.color = InterfaceColors.unselectedColor; }
    }

    /// <summary>
    /// Clears all sub menu buttons to unselected color and resets all scroll view localscales.
    /// </summary>
    public void ClearSubMenus() {
        gamemodeSubMenuText.color                = InterfaceColors.unselectedColor;
        generalSubMenuText.color                 = InterfaceColors.unselectedColor;
        controlsSubMenuText.color                = InterfaceColors.unselectedColor;
        crosshairSubMenuText.color               = InterfaceColors.unselectedColor;
        extraSubMenuText.color                   = InterfaceColors.unselectedColor;
        gamemodeScrollView.transform.localScale  = disabledSubMenuScrollView;
        generalScrollView.transform.localScale   = disabledSubMenuScrollView;
        controlsScrollView.transform.localScale  = disabledSubMenuScrollView;
        crosshairScrollView.transform.localScale = disabledSubMenuScrollView;
        extraScrollView.transform.localScale     = disabledSubMenuScrollView;
        gamemodeBar.transform.gameObject.SetActive(false);
        generalBar.transform.gameObject.SetActive(false);
        controlsBar.transform.gameObject.SetActive(false);
        crosshairBar.transform.gameObject.SetActive(false);
        extraBar.transform.gameObject.SetActive(false);
    }

    /// <summary>
    /// Resets all sub menu scrollviews to the top.
    /// </summary>
    public static void ResetAllScrollviewsTop() {
        ScrollRectExtension.ScrollToTop(subMenu.gamemodeScrollView.GetComponent<ScrollRect>());
        ScrollRectExtension.ScrollToTop(subMenu.generalScrollView.GetComponent<ScrollRect>());
        ScrollRectExtension.ScrollToTop(subMenu.controlsScrollView.GetComponent<ScrollRect>());
        ScrollRectExtension.ScrollToTop(subMenu.crosshairScrollView.GetComponent<ScrollRect>());
        ScrollRectExtension.ScrollToTop(subMenu.extraScrollView.GetComponent<ScrollRect>());
    }
}
