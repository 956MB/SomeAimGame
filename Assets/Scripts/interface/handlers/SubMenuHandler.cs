using UnityEngine;
using TMPro;

public class SubMenuHandler : MonoBehaviour {
    public TMP_Text gamemodeSubMenuText, generalSubMenuText, controlsSubMenuText, crosshairSubMenuText, extraSubMenuText;
    public GameObject gamemodeScrollView, generalScrollView, controlsScrollView, crosshairScrollView, extraScrollView;

    public static Color32 disabledSubMenuTextColor  = new Color32(255,255,255,150);
    public static Color32 enabledSubMenuTextColor   = new Color32(255,255,255,255);
    public static Vector3 disabledSubMenuScrollView = new Vector3(0, 0, 0);
    public static Vector3 enabledSubMenuScrollView  = new Vector3(1, 1, 1);

    private static SubMenuHandler subMenu;
    void Awake() { subMenu = this; }

    /// <summary>
    /// Sets active sub menu based on button clicked (buttonClicked).
    /// </summary>
    /// <param name="buttonClicked"></param>
    public void SetSubMenuText(GameObject buttonClicked) {
        string buttonClickedName = buttonClicked.name;
        ClearSubMenus();

        switch (buttonClickedName) {
            case "GamemodeSubMenuButton":
                gamemodeSubMenuText.color = enabledSubMenuTextColor;
                gamemodeScrollView.transform.localScale = enabledSubMenuScrollView;
                break;
            case "GeneralSubMenuButton":
                generalSubMenuText.color = enabledSubMenuTextColor;
                generalScrollView.transform.localScale = enabledSubMenuScrollView;
                break;
            case "ControlsSubMenuButton":
                controlsSubMenuText.color = enabledSubMenuTextColor;
                controlsScrollView.transform.localScale = enabledSubMenuScrollView;
                break;
            case "CrosshairSubMenuButton":
                crosshairSubMenuText.color = enabledSubMenuTextColor;
                crosshairScrollView.transform.localScale = enabledSubMenuScrollView;
                break;
            case "ExtraSubMenuButton":
                extraSubMenuText.color = enabledSubMenuTextColor;
                extraScrollView.transform.localScale = enabledSubMenuScrollView;
                break;
        }

        if (ToggleHandler.UISoundOn())
            UISound.PlayUISound02();
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
