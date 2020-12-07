using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

using SomeAimGame.Skybox;
using SomeAimGame.Gamemode;
using SomeAimGame.Targets;
using SomeAimGame.Utilities;
using SomeAimGame.SFX;

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

        try {
            buttonBorder = pointerEventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).transform.gameObject;
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
        GamemodeUtil.ClearGamemodeButtonBorders();
        clickedButtonBorder.GetComponent<Image>().color = InterfaceColors.selectedColor;
        clickedButtonBorder.SetActive(true);

        // Populate selected gamemode based on button clicked in gamemode settings panel.
        GamemodeSelect.PopulateGamemodeSelect(gamemodeClickedName, CosmeticsSettings.quickStartGame);
        GamemodeSelect.ClearGamemodeButtonColors(GameObject.Find($"{GamemodeUtil.ReturnGamemodeType_StringFull(gamemodeClickedName)}-Text (TMP)").GetComponent<TMP_Text>(), true, true);

        SFXManager.CheckPlayClick_Regular();

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
        NotificationHandler.CheckHideNotificationObject();

        SpawnTargets.targetColorReset = true;
        TargetUtil.ClearTargetColorButtonBorders();
        ButtonHoverHandler.selectedTargetColor = targetColorClickedName;
        clickedButtonBorder.GetComponent<Image>().color = InterfaceColors.selectedColor;
        clickedButtonBorder.SetActive(true);

        SFXManager.CheckPlayClick_Regular();

        // Replace all currently spawned targets colors.
        SpawnTargets.ReplaceCurrentTargetColors(targetColorClickedName);

        // Change selected target color text based on button clicked in general settings panel.
        targetColorSelected.SetText($"//  {TargetUtil.ReturnTargetColorType_StringTranslated(targetColorClickedName)}");

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

        SkyboxUtil.ClearSkyboxButtonBorders();
        ButtonHoverHandler.selectedSkybox               = skyboxClickedName;
        clickedButtonBorder.GetComponent<Image>().color = InterfaceColors.selectedColor;
        clickedButtonBorder.SetActive(true);

        SFXManager.CheckPlayClick_Regular();

        // Set new skybox, then save currently selected skybox in cosmetics.
        SkyboxHandler.SetNewSkybox(skyboxClickedName);

        // Change selected skybox text based on button clicked in general settings panel.
        skyboxSelected.SetText($"//  {SkyboxUtil.ReturnSkyboxType_StringTranslated(skyboxClickedName)}");
        
        // Saves new selected skybox
        CosmeticsSettings.SaveSkyboxItem(skyboxClickedName);

        SettingsPanel.LoadGamemodePreviews();
        
        // EVENT:: for new skybox button clicked
        //DevEventHandler.CheckInterfaceEvent($"\"{skyboxClickedName}\" {I18nTextTranslator.SetTranslatedText("eventinterfaceskyboxbutton")}");
        // EVENT:: for new skybox selected
        //DevEventHandler.CheckSkyboxEvent($"{I18nTextTranslator.SetTranslatedText("eventskyboxchange")} \"{skyboxClickedName}\"");
    }
}
