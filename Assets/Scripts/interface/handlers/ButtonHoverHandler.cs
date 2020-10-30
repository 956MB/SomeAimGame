using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    //public Texture2D hoverBorder;
    public GameObject childBorder;
    public static string selectedGamemode;
    public static string selectedTargetColor;
    public static string selectedSkybox;

    private static string currentHoveredButton;

    public TMP_Text targetColorSelected, skyboxSelected;

    /// <summary>
    /// Enables hovered button border and sets 'selected/hovered' text on 'pointerEnter'.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerEnter(PointerEventData pointerEventData) {
        string buttonName = pointerEventData.pointerCurrentRaycast.gameObject.name;
        currentHoveredButton = buttonName;
        if (buttonName != selectedTargetColor && buttonName != selectedSkybox && buttonName != selectedGamemode) {
            childBorder.SetActive(true);
            SetHoverButtonText(buttonName);
            //CursorHandler.setHoverCursorStatic();
            if (ToggleHandler.UISoundOn()) { UISound.PlayUISound(); }

        }
    }

    /// <summary>
    /// Disables hovered button border and sets 'selected/hovered' text back to selected on 'pointerExit'.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerExit(PointerEventData pointerEventData) {
        if (currentHoveredButton != selectedTargetColor && currentHoveredButton != selectedSkybox && currentHoveredButton != selectedGamemode) {
            childBorder.SetActive(false);
            targetColorSelected.SetText(CosmeticsSaveSystem.activeTargetColorText);
            skyboxSelected.SetText(CosmeticsSaveSystem.activeSkyboxText);
        }
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
            default:
                break;
        }

        if (buttonHoverName.Contains("TargetColor")) {
            DevEventHandler.CheckInterfaceEvent($"\"{buttonHoverName}\" {I18nTextTranslator.SetTranslatedText("eventinterfacetargetcolorbuttonhover")}");
        } else if (buttonHoverName.Contains("Skybox")) {
            DevEventHandler.CheckInterfaceEvent($"\"{buttonHoverName}\" {I18nTextTranslator.SetTranslatedText("eventinterfaceskyboxbuttonhover")}");
        }
    }
}
