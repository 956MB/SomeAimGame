using UnityEngine;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Utilities;
using SomeAimGame.SFX;
using SomeAimGame.Core.Video;

public class SubMenuHandler : MonoBehaviour {
    public TMP_Text gamemodeSubMenuText, generalSubMenuText, controlsSubMenuText, crosshairSubMenuText, videoSubMenuText;
    public GameObject gamemodeContainer, generalContainer, controlsContainer, crosshairContainer, videoContainer, crosshairImageMain, crosshairImageSettings;
    public GameObject gamemodeScrollView, generalScrollView, controlsScrollView, crosshairScrollView, videoScrollView;
    public Image gamemodeBar, generalBar, controlsBar, crosshairBar, videoBar;
    //public ;

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
            case "GamemodeSubMenuButton":  SetSubMenu(gamemodeSubMenuText, gamemodeContainer, gamemodeBar);          break;
            case "GeneralSubMenuButton":   SetSubMenu(generalSubMenuText, generalContainer, generalBar);             break;
            case "ControlsSubMenuButton":  SetSubMenu(controlsSubMenuText, controlsContainer, controlsBar);          break;
            case "CrosshairSubMenuButton": SetSubMenu(crosshairSubMenuText, crosshairContainer, crosshairBar, true); break;
            case "VideoSubMenuButton":     SetSubMenu(videoSubMenuText, videoContainer, videoBar);                   break;
        }

        SettingsPanel.CheckSaveSettings();

        SFXManager.CheckPlayClick_Regular();
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
        //ScrollRectExtension.ScrollToTop(subMenuActive.GetComponent<ScrollRect>());
        subMenuBar.transform.gameObject.SetActive(true);

        if (showCrosshair) { CrosshairHide.ShowSettingsCrosshair(); }
    }

    public static void SetStartingSubMenu() {
        subMenu.SetSubMenu(subMenu.gamemodeSubMenuText, subMenu.gamemodeContainer, subMenu.gamemodeBar);
    }

    /// <summary>
    /// Sets sub menu text color to hovered color (hoveredSubMenuTextColor) when pointer enter.
    /// </summary>
    /// <param name="subMenuText"></param>
    public void SetHoveredSubMenuColor(TMP_Text subMenuText) {
        if (subMenuText.name != activeSubMenuText) {
            subMenuText.color = InterfaceColors.hoveredColor;
            SFXManager.CheckPlayHover_Title();
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
        Util.GameObjectLoops.Util_ClearTMPTextColor(InterfaceColors.unselectedColor, gamemodeSubMenuText, generalSubMenuText, controlsSubMenuText, crosshairSubMenuText, videoSubMenuText);
        Util.GameObjectLoops.Util_SetObjectsLocalScale(disabledSubMenuScrollView, gamemodeContainer, generalContainer, controlsContainer, crosshairContainer, videoContainer);
        Util.GameObjectLoops.Util_ImagesSetActive(false, gamemodeBar, generalBar, controlsBar, crosshairBar, videoBar);
        
        CrosshairHide.HideCrosshairs();
    }

    /// <summary>
    /// Resets all sub menu scrollviews to the top.
    /// </summary>
    public static void ResetAllSubMenuScrollviewsTop() {
        Util.GameObjectLoops.Util_ResetScrollViews_Top(subMenu.gamemodeScrollView, subMenu.generalScrollView, subMenu.controlsScrollView, subMenu.crosshairScrollView, subMenu.videoScrollView);
    }

    public static void ResetCrosshairScrollview() {
        ScrollRectExtension.ScrollToTop(subMenu.crosshairContainer.GetComponent<ScrollRect>());
    }
}
