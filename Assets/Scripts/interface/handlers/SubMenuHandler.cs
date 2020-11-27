using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubMenuHandler : MonoBehaviour {
    public TMP_Text gamemodeSubMenuText, generalSubMenuText, controlsSubMenuText, crosshairSubMenuText, extraSubMenuText;
    public GameObject gamemodeScrollView, generalScrollView, controlsScrollView, crosshairScrollView, extraScrollView, crosshairImageSettings;
    public Image gamemodeBar, generalBar, controlsBar, crosshairBar, extraBar;

    public static Vector3 disabledSubMenuScrollView = new Vector3(0, 0, 0);
    public static Vector3 enabledSubMenuScrollView  = new Vector3(1, 1, 1);

    public static string activeSubMenuText = "GamemodeTitleText (TMP)";

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
                SetSubMenu(gamemodeSubMenuText, gamemodeScrollView, gamemodeBar); break;
            case "GeneralSubMenuButton":
                SetSubMenu(generalSubMenuText, generalScrollView, generalBar); break;
            case "ControlsSubMenuButton":
                SetSubMenu(controlsSubMenuText, controlsScrollView, controlsBar); break;
            case "CrosshairSubMenuButton":
                SetSubMenu(crosshairSubMenuText, crosshairScrollView, crosshairBar, true); break;
            case "ExtraSubMenuButton":
                SetSubMenu(extraSubMenuText, extraScrollView, extraBar); break;
        }

        CrosshairOptionsObject.SaveCrosshairObject(false);

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Click(); }
    }

    /// <summary>
    /// Sets active sub menu view with supplied menu text (subMenuText), sub menu scrollview (subMenuActive), and sub menu bar (subMenuBar).
    /// </summary>
    /// <param name="subMenuText"></param>
    /// <param name="subMenuActive"></param>
    /// <param name="subMenuBar"></param>
    /// <param name="showCrosshair"></param>
    public void SetSubMenu(TMP_Text subMenuText, GameObject subMenuActive, Image subMenuBar, bool showCrosshair = false) {
        subMenuText.color                  = InterfaceColors.selectedColor;
        subMenuActive.transform.localScale = enabledSubMenuScrollView;
        ScrollRectExtension.ScrollToTop(subMenuActive.GetComponent<ScrollRect>());
        subMenuBar.transform.gameObject.SetActive(true);

        if (showCrosshair) { ShowSettingsCrosshair(); }
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
        
        HideSettingsCrosshair();
    }

    /// <summary>
    /// Hides crosshair image above crosshair settings panel.
    /// </summary>
    public static void HideSettingsCrosshair() {
        GameUI.HideGameObject_Layer(subMenu.crosshairImageSettings);
    }

    /// <summary>
    /// Shows crosshair image above crosshair settings panel.
    /// </summary>
    public static void ShowSettingsCrosshair() {
        GameObject crosshairCanvasPos = GameObject.Find($"SettingsCrosshairCanvas");
        GameUI.ShowGameObject_Layer(subMenu.crosshairImageSettings);
        //subMenu.crosshairImageSettings.transform.parent = crosshairCanvasPos.transform;
        subMenu.crosshairImageSettings.transform.position = crosshairCanvasPos.transform.position;
        //Debug.Log($"{subMenu.crosshairImageSettings.GetComponent<RectTransform>().sizeDelta.x}, {subMenu.crosshairImageSettings.GetComponent<RectTransform>().sizeDelta.y}");
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
