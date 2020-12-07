using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

using SomeAimGame.Skybox;
using SomeAimGame.Gamemode;
using SomeAimGame.Targets;
using SomeAimGame.Utilities;
using SomeAimGame.SFX;

public class ButtonHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    //public Texture2D hoverBorder;
    public GameObject childBorder;
    public static TargetType selectedTargetColor;
    public static SkyboxType selectedSkybox;
    public static GamemodeType selectedGamemode;
    public static string selectedGamemodeString;

    private static string currentHoveredButton;

    public TMP_Text targetColorSelected, skyboxSelected;

    /// <summary>
    /// Enables hovered button border and sets 'selected/hovered' text on 'pointerEnter'.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerEnter(PointerEventData pointerEventData) {
        string buttonName = pointerEventData.pointerCurrentRaycast.gameObject.name;
        currentHoveredButton = buttonName;

        if (buttonName != TargetUtil.ReturnTargetColorType_StringFull(selectedTargetColor) && buttonName != SkyboxUtil.ReturnSkyboxType_StringFull(selectedSkybox) && buttonName != GamemodeUtil.ReturnGamemodeType_StringFull(selectedGamemode)) {
            childBorder.SetActive(true);
            SetHoverButtonText(buttonName);
            if (currentHoveredButton.Contains("Gamemode")) { SetHoverButtonColor(buttonName, GameObject.Find($"{buttonName}-Text (TMP)").GetComponent<TMP_Text>()); }
            //CursorHandler.setHoverCursorStatic();
            SFXManager.CheckPlayHover_Regular();
        }
    }

    /// <summary>
    /// Disables hovered button border and sets 'selected/hovered' text back to selected on 'pointerExit'.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerExit(PointerEventData pointerEventData) {
        if (currentHoveredButton != TargetUtil.ReturnTargetColorType_StringFull(selectedTargetColor) && currentHoveredButton != SkyboxUtil.ReturnSkyboxType_StringFull(selectedSkybox) && currentHoveredButton != selectedGamemodeString) {
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
        if (buttonHoverName.Contains("TargetColor-")) { targetColorSelected.SetText($"//  {TargetUtil.ReturnTargetColorType_StringTranslated(TargetUtil.ReturnTargetColorType_TargetColor(buttonHoverName))}"); }

        if (buttonHoverName.Contains("Skybox-")) { skyboxSelected.SetText($"//  {SkyboxUtil.ReturnSkyboxType_StringTranslated(SkyboxUtil.ReturnSkyboxType_Skybox(buttonHoverName))}"); }

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
