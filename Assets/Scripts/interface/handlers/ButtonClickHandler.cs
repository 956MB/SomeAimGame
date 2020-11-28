using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

using SomeAimGame.Skybox;
using SomeAimGame.Gamemode;
using SomeAimGame.Targets;
using SomeAimGame.Utilities;

public class ButtonClickHandler : MonoBehaviour, IPointerClickHandler {
    public TMP_Text targetColorSelected, skyboxSelected;
    GameObject buttonBorder;
    GamemodeType gamemodeClickedName;

    /// <summary>
    /// Closes settings panel.
    /// </summary>
    public void CloseSettingsPanel() { SettingsPanel.CloseSettingsPanel(); }
    /// <summary>
    /// Closes 'AfterActionReport' panel.
    /// </summary>
    public void CloseAfterActionReportPanel() { SettingsPanel.CloseAfterActionReport(); }
    /// <summary>
    /// Closes 'AfterActionReport' panel then restarts the game with current gamemode.
    /// </summary>
    public void RestartCurrentGame() {
        // EVENT:: for new gamemode start
        //DevEventHandler.CheckGamemodeEvent($"\"{gamemodeClickedName}\" {I18nTextTranslator.SetTranslatedText("eventgamemodestarted")}");

        GameUI.RestartGame(CosmeticsSettings.gamemode, true);
    }

    /// <summary>
    /// Sets hovered border to active and calls appropriate function for clicked button.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerClick(PointerEventData pointerEventData) {
        string clickedButtonName = pointerEventData.pointerCurrentRaycast.gameObject.name;

        try { buttonBorder = pointerEventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).transform.gameObject;
        } catch (UnityException) { }

        if (buttonBorder != null) {
            if (clickedButtonName.Contains("Gamemode")) {
                SelectNewGamemode(buttonBorder);
            } else if (clickedButtonName.Contains("TargetColor")) {
                SetNewTargetColor(buttonBorder);
            } else if (clickedButtonName.Contains("Skybox")) {
                SetNewSkyboxColor(buttonBorder);
            }
        }
    }

    /// <summary>
    /// Selects new gamemode with the supplied gamemode from the clicked button inside settings (gamemode sub-section).
    /// </summary>
    /// <param name="clickedButtonBorder"></param>
    public void SelectNewGamemode(GameObject clickedButtonBorder) {
        gamemodeClickedName = GamemodeUtil.ReturnGamemodeType_Gamemode(clickedButtonBorder.transform.parent.name);
        ClearGamemodeButtonBorders();
        clickedButtonBorder.GetComponent<Image>().color = InterfaceColors.selectedColor;
        clickedButtonBorder.SetActive(true);

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Click(); }

        // Populate selected gamemode based on button clicked in gamemode settings panel.
        GamemodeSelect.PopulateGamemodeSelect(gamemodeClickedName, CosmeticsSettings.quickStartGame);
        GamemodeSelect.ClearGamemodeButtonColors(GameObject.Find($"{GamemodeUtil.ReturnGamemodeType_StringFull(gamemodeClickedName)}-Text (TMP)").GetComponent<TMP_Text>(), true, true);

        // EVENT:: for new gamemode button clicked
        //DevEventHandler.CheckInterfaceEvent($"\"{gamemodeClickedName}\" {I18nTextTranslator.SetTranslatedText("eventinterfacegamemodebutton")}");
        // EVENT:: for new gamemode selected
        //DevEventHandler.CheckGamemodeEvent($"\"{gamemodeClickedName}\" {I18nTextTranslator.SetTranslatedText("eventinterfacegamemodebutton")}");
    }

    /// <summary>
    /// Sets new target color with supplied color from clicked button inside settings (general sub-section).
    /// </summary>
    /// <param name="clickedButtonBorder"></param>
    public void SetNewTargetColor(GameObject clickedButtonBorder) {
        TargetType targetColorClickedName = TargetUtil.ReturnTargetColorType_TargetColor(clickedButtonBorder.transform.parent.name);

        // Primary target color cannot be red if gamemode is "Gamemode-Follow"
        if (targetColorClickedName == TargetType.RED && SpawnTargets.gamemode == GamemodeType.FOLLOW) { return; }
        if (NotificationHandler.notificationOpen) { NotificationHandler.HideNotification(); }

        SpawnTargets.targetColorReset = true;
        ClearTargetColorButtonBorders();
        ButtonHoverHandler.selectedTargetColor = targetColorClickedName;
        clickedButtonBorder.GetComponent<Image>().color = InterfaceColors.selectedColor;
        clickedButtonBorder.SetActive(true);

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Click(); }

        // Replace all currently spawned targets colors.
        SpawnTargets.ReplaceCurrentTargetColors(targetColorClickedName);

        // Change selected target color text based on button clicked in general settings panel.
        switch (targetColorClickedName) {
            case TargetType.RED:
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colorred")}");
                break;
            case TargetType.ORANGE:
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colororange")}");
                break;
            case TargetType.YELLOW:
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("coloryellow")}");
                break;
            case TargetType.GREEN:
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colorgreen")}");
                break;
            case TargetType.BLUE:
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colorblue")}");
                break;
            case TargetType.PURPLE:
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colorpurple")}");
                break;
            case TargetType.PINK:
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colorpink")}");
                break;
            case TargetType.WHITE:
                targetColorSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("colorwhite")}");
                break;
        }

        // Saves new selected target color
        CosmeticsSettings.SaveTargetColorItem(targetColorClickedName);

        // Changes gamemode video previews when new target color selected, then saves
        SettingsPanel.LoadGamemodePreviews();

        // EVENT:: for new target color button clicked
        //DevEventHandler.CheckTargetsEvent($"\"{targetColorClickedName}\" {I18nTextTranslator.SetTranslatedText("eventinterfacetargetcolorbutton")}");
        // EVENT:: for new target color selected
        //DevEventHandler.CheckTargetsEvent($"{I18nTextTranslator.SetTranslatedText("eventtargetscolorchange")} \"{targetColorClickedName}\"");
    }

    /// <summary>
    /// Sets new skybox with supplied skybox from clicked button inside settings (general sub-section).
    /// </summary>
    /// <param name="clickedButtonBorder"></param>
    public void SetNewSkyboxColor(GameObject clickedButtonBorder) {
        SkyboxType skyboxClickedName = SkyboxUtil.ReturnSkyboxType_Skybox(clickedButtonBorder.transform.parent.name);

        ClearSkyboxButtonBorders();
        ButtonHoverHandler.selectedSkybox               = skyboxClickedName;
        clickedButtonBorder.GetComponent<Image>().color = InterfaceColors.selectedColor;
        clickedButtonBorder.SetActive(true);

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Click(); }

        // Set new skybox, then save currently selected skybox in cosmetics.
        SkyboxHandler.SetNewSkybox(skyboxClickedName);

        // Change selected skybox text based on button clicked in general settings panel.
        switch (skyboxClickedName) {
            case SkyboxType.PINK:
                skyboxSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("skyboxpink")}");
                break;
            case SkyboxType.GOLDEN:
                skyboxSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("skyboxgolden")}");
                break;
            case SkyboxType.NIGHT:
                skyboxSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("skyboxnight")}");
                break;
            case SkyboxType.GREY:
                skyboxSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("skyboxgrey")}");
                break;
            case SkyboxType.BLUE:
                skyboxSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("skyboxblue")}");
                break;
            case SkyboxType.SLATE:
                skyboxSelected.SetText($"//  {I18nTextTranslator.SetTranslatedText("skyboxslate")}");
                break;
        }

        // EVENT:: for new skybox button clicked
        //DevEventHandler.CheckInterfaceEvent($"\"{skyboxClickedName}\" {I18nTextTranslator.SetTranslatedText("eventinterfaceskyboxbutton")}");
        // EVENT:: for new skybox selected
        //DevEventHandler.CheckSkyboxEvent($"{I18nTextTranslator.SetTranslatedText("eventskyboxchange")} \"{skyboxClickedName}\"");
        
        // Saves new selected skybox
        CosmeticsSettings.SaveSkyboxItem(skyboxClickedName);

        SettingsPanel.LoadGamemodePreviews();
    }

    /// <summary>
    /// Clears all gamemode button borders in settings panel (gamemode sub-section).
    /// </summary>
    public static void ClearGamemodeButtonBorders() {
        foreach (GameObject buttonBorder in GameObject.FindGameObjectsWithTag("ButtonBorderGamemode")) {
            if (buttonBorder != null) {
                buttonBorder.GetComponent<Image>().color = InterfaceColors.unselectedColor;
                buttonBorder.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Clears all target color button borders in settings panel (general sub-section).
    /// </summary>
    public static void ClearTargetColorButtonBorders() {
        foreach (GameObject buttonBorder in GameObject.FindGameObjectsWithTag("ButtonBorderTargetColor")) {
            if (buttonBorder != null) {
                buttonBorder.GetComponent<Image>().color = InterfaceColors.unselectedColor;
                buttonBorder.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Clears all skybox button borders in settings panel (general sub-section).
    /// </summary>
    public static void ClearSkyboxButtonBorders() {
        foreach (GameObject buttonBorder in GameObject.FindGameObjectsWithTag("ButtonBorderSkybox")) {
            if (buttonBorder != null) {
                buttonBorder.GetComponent<Image>().color = InterfaceColors.unselectedColor;
                buttonBorder.SetActive(false);
            }
        }
    }
}
