using UnityEngine;
using TMPro;

public class ButtonHoverHandler : MonoBehaviour {
    //public Texture2D hoverBorder;
    public GameObject childBorder;
    public static string selectedGamemode;
    public static string selectedTargetColor;
    public static string selectedSkybox;

    public TMP_Text targetColorSelected, skyboxSelected;

    //void Start() {
    //    childBorder = this.transform.Find("Border").gameObject;
    //    //childBorder.SetActive(false);
    //}

    /// <summary>
    /// Enables hovered button border and sets 'selected/hovered' text on 'pointerEnter'.
    /// </summary>
    /// <param name="hoveredButton"></param>
    public void ButtonHoverEnter(GameObject hoveredButton) {
        if (hoveredButton.name != selectedTargetColor && hoveredButton.name != selectedSkybox && hoveredButton.name != selectedGamemode) {
            childBorder.SetActive(true);
            SetHoverButtonText(hoveredButton.name);
            //CursorHandler.setHoverCursorStatic();
            if (ToggleHandler.UISoundOn()) { UISound.PlayUISound(); }

        }
    }

    /// <summary>
    /// Disables hovered button border and sets 'selected/hovered' text back to selected on 'pointerExit'.
    /// </summary>
    /// <param name="hoveredButton"></param>
    public void ButtonHoverExit(GameObject hoveredButton) {
        //CursorHandler.setDefaultCursorStatic();
        if (hoveredButton.name != selectedTargetColor && hoveredButton.name != selectedSkybox && hoveredButton.name != selectedGamemode) {
            childBorder.SetActive(false);
            targetColorSelected.SetText(CosmeticsSaveSystem.activeTargetColorText);
            skyboxSelected.SetText(CosmeticsSaveSystem.activeSkyboxText);
        }
    }

    public void GamemodeStartHover() {
        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound(); }
    }

    /// <summary>
    /// Sets selected target color or skybox text with translated text.
    /// </summary>
    /// <param name="buttonHoverName"></param>
    private void SetHoverButtonText(string buttonHoverName) {
        switch (buttonHoverName) {
            case "TargetColor-Red":
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colorred")}");
                break;
            case "TargetColor-Orange":
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colororange")}");
                break;
            case "TargetColor-Yellow":
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("coloryellow")}");
                break;
            case "TargetColor-Green":
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colorgreen")}");
                break;
            case "TargetColor-Blue":
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colorblue")}");
                break;
            case "TargetColor-Purple":
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colorpurple")}");
                break;
            case "TargetColor-Pink":
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colorpink")}");
                break;
            case "TargetColor-White":
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colorwhite")}");
                break;
            case "Skybox-Pink":
                skyboxSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("skyboxpink")}");
                break;
            case "Skybox-Golden":
                skyboxSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("skyboxgolden")}");
                break;
            case "Skybox-Night":
                skyboxSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("skyboxnight")}");
                break;
            case "Skybox-Grey":
                skyboxSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("skyboxgrey")}");
                break;
            case "Skybox-Blue":
                skyboxSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("skyboxblue")}");
                break;
            case "Skybox-Slate":
                skyboxSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("skyboxslate")}");
                break;
        }
    }
}
