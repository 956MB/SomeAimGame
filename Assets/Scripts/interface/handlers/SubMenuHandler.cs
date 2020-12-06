﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Utilities;
using SomeAimGame.SFX;

public class SubMenuHandler : MonoBehaviour {
    public TMP_Text gamemodeSubMenuText, generalSubMenuText, controlsSubMenuText, crosshairSubMenuText, extraSubMenuText;
    public GameObject gamemodeContainer, generalContainer, controlsContainer, crosshairContainer, extraContainer, crosshairImageSettings;
    public GameObject gamemodeScrollView, generalScrollView, controlsScrollView, crosshairScrollView, extraScrollView;
    public Image gamemodeBar, generalBar, controlsBar, crosshairBar, extraBar;
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
            case "GamemodeSubMenuButton":
                SetSubMenu(gamemodeSubMenuText, gamemodeContainer, gamemodeBar); break;
            case "GeneralSubMenuButton":
                SetSubMenu(generalSubMenuText, generalContainer, generalBar); break;
            case "ControlsSubMenuButton":
                SetSubMenu(controlsSubMenuText, controlsContainer, controlsBar); break;
            case "CrosshairSubMenuButton":
                SetSubMenu(crosshairSubMenuText, crosshairContainer, crosshairBar, true); break;
            case "ExtraSubMenuButton":
                SetSubMenu(extraSubMenuText, extraContainer, extraBar); break;
        }

        CrosshairOptionsObject.SaveCrosshairObject(false);
        TargetSoundSelect.CheckSaveTargetSoundSelection();
        NotificationHandler.CheckHideNotificationObject();

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

        if (showCrosshair) { ShowSettingsCrosshair(); }
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
        Util.GameObjectLoops.Util_ClearTMPTextColor(InterfaceColors.unselectedColor, gamemodeSubMenuText, generalSubMenuText, controlsSubMenuText, crosshairSubMenuText);

        Util.GameObjectLoops.Util_SetObjectsLocalScale(disabledSubMenuScrollView, gamemodeContainer, generalContainer, controlsContainer, crosshairContainer);

        Util.GameObjectLoops.Util_ImagesSetActive(false, gamemodeBar, generalBar, controlsBar, crosshairBar);
        
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
        GameObject crosshairCanvasPos = GameObject.Find($"CrosshairSettingsCanvas");
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
        //ScrollRectExtension.ScrollToTop(subMenu.extraScrollView.GetComponent<ScrollRect>());
    }

    public static void ResetCrosshairScrollview() {
        ScrollRectExtension.ScrollToTop(subMenu.crosshairContainer.GetComponent<ScrollRect>());
    }
}
