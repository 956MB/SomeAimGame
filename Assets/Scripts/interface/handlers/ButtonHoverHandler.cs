using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    //public Texture2D hoverBorder;
    public GameObject childBorder;
    public static Gamemode selectedGamemode;
    public static string selectedGamemodeString;
    public static TargetColor selectedTargetColor;
    public static Skybox selectedSkybox;

    private static string currentHoveredButton;

    public TMP_Text targetColorSelected, skyboxSelected;

    /// <summary>
    /// Enables hovered button border and sets 'selected/hovered' text on 'pointerEnter'.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerEnter(PointerEventData pointerEventData) {
        string buttonName = pointerEventData.pointerCurrentRaycast.gameObject.name;
        //string buttonNameGamemode = GamemodeType.ReturnGamemodeType_StringFull(buttonName);
        currentHoveredButton = buttonName;
        if (buttonName != TargetColorType.ReturnTargetColorType_StringFull(selectedTargetColor) && buttonName != SkyboxType.ReturnSkyboxType_StringFull(selectedSkybox) && buttonName != GamemodeType.ReturnGamemodeType_StringFull(selectedGamemode)) {
            childBorder.SetActive(true);
            SetHoverButtonText(buttonName);
            if (currentHoveredButton.Contains("Gamemode")) { SetHoverButtonColor(buttonName, GameObject.Find($"{buttonName}-Text (TMP)").GetComponent<TMP_Text>()); }
            //CursorHandler.setHoverCursorStatic();
            if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_HoverInner(); }
        }
    }

    /// <summary>
    /// Disables hovered button border and sets 'selected/hovered' text back to selected on 'pointerExit'.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerExit(PointerEventData pointerEventData) {
        if (currentHoveredButton != TargetColorType.ReturnTargetColorType_StringFull(selectedTargetColor) && currentHoveredButton != SkyboxType.ReturnSkyboxType_StringFull(selectedSkybox) && currentHoveredButton != selectedGamemodeString) {
            childBorder.SetActive(false);
            targetColorSelected.SetText(CosmeticsSaveSystem.activeTargetColorText);
            skyboxSelected.SetText(CosmeticsSaveSystem.activeSkyboxText);

            if (currentHoveredButton.Contains("Gamemode")) { GamemodeSelect.ClearGamemodeButtonColors(GameObject.Find($"{currentHoveredButton}-Text (TMP)").GetComponent<TMP_Text>(), false, true); }
            if (currentHoveredButton.Contains("Gamemode")) { GamemodeSelect.ClearGamemodeButtonColors(GameObject.Find($"{selectedGamemodeString}-Text (TMP)").GetComponent<TMP_Text>(), true, false); }
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

        //if (buttonHoverName.Contains("TargetColor")) {
        //    DevEventHandler.CheckInterfaceEvent($"\"{buttonHoverName}\" {I18nTextTranslator.SetTranslatedText("eventinterfacetargetcolorbuttonhover")}");
        //} else if (buttonHoverName.Contains("Skybox")) {
        //    DevEventHandler.CheckInterfaceEvent($"\"{buttonHoverName}\" {I18nTextTranslator.SetTranslatedText("eventinterfaceskyboxbuttonhover")}");
        //}
    }

    /// <summary>
    /// Sets hovered gamemode button text color to selected. (Usually hovered, but selected looks a bit better.)
    /// </summary>
    /// <param name="buttonName"></param>
    /// <param name="childButtonText"></param>
    private void SetHoverButtonColor(string buttonName, TMP_Text childButtonText) {
        childButtonText.color = InterfaceColors.selectedColor;
    }
}
