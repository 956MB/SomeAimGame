using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonClickHandler : MonoBehaviour, IPointerClickHandler {
    public TMP_Text targetColorSelected, skyboxSelected;
    GameObject buttonBorder;
    string gamemodeClickedName;

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
        //SettingsPanel.CloseAfterActionReport();
        // EVENT:: for new gamemode start
        DevEventHandler.CheckGamemodeEvent($"\"{gamemodeClickedName}\" {I18nTextTranslator.SetTranslatedText("eventgamemodestarted")}");

        GameUI.RestartGame(CosmeticsSettings.gamemode);
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
        gamemodeClickedName = clickedButtonBorder.transform.parent.name;
        ClearGamemodeButtonBorders();
        clickedButtonBorder.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        clickedButtonBorder.SetActive(true);

        // Populate selected gamemode based on button clicked in gamemode settings panel.
        GamemodeSelect.PopulateGamemodeSelect(gamemodeClickedName, CosmeticsSettings.quickStartGame);

        // EVENT:: for new gamemode button clicked
        DevEventHandler.CheckInterfaceEvent($"\"{gamemodeClickedName}\" {I18nTextTranslator.SetTranslatedText("eventinterfacegamemodebutton")}");
        // EVENT:: for new gamemode selected
        DevEventHandler.CheckGamemodeEvent($"\"{gamemodeClickedName}\" {I18nTextTranslator.SetTranslatedText("eventinterfacegamemodebutton")}");
    }

    /// <summary>
    /// Sets new target color with supplied color from clicked button inside settings (general sub-section).
    /// </summary>
    /// <param name="clickedButtonBorder"></param>
    public void SetNewTargetColor(GameObject clickedButtonBorder) {
        string targetColorClickedName = clickedButtonBorder.transform.parent.name;

        // Primary target color cannot be red if gamemode is "Gamemode-Follow"
        if (targetColorClickedName == "TargetColor-Red" && SpawnTargets.gamemode == "Gamemode-Follow") { return; }
        if (NotificationHandler.notificationOpen) { NotificationHandler.HideNotification(); }

        SpawnTargets.targetColorReset = true;
        ClearTargetColorButtonBorders();
        ButtonHoverHandler.selectedTargetColor = targetColorClickedName;
        clickedButtonBorder.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        clickedButtonBorder.SetActive(true);

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound02(); }

        // Replace all currently spawned targets colors.
        SpawnTargets.ReplaceCurrentTargetColors(targetColorClickedName);

        // Change selected target color text based on button clicked in general settings panel.
        switch (targetColorClickedName) {
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
        }

        // Saves new selected target color
        CosmeticsSettings.SaveTargetColorItem(targetColorClickedName);

        // Changes gamemode video previews when new target color selected, then saves
        SettingsPanel.LoadGamemodePreviews();

        // EVENT:: for new target color button clicked
        DevEventHandler.CheckTargetsEvent($"\"{targetColorClickedName}\" {I18nTextTranslator.SetTranslatedText("eventinterfacetargetcolorbutton")}");
        // EVENT:: for new target color selected
        DevEventHandler.CheckTargetsEvent($"{I18nTextTranslator.SetTranslatedText("eventtargetscolorchange")} \"{targetColorClickedName}\"");
    }

    /// <summary>
    /// Sets new skybox with supplied skybox from clicked button inside settings (general sub-section).
    /// </summary>
    /// <param name="clickedButtonBorder"></param>
    public void SetNewSkyboxColor(GameObject clickedButtonBorder) {
        string skyboxClickedName = clickedButtonBorder.transform.parent.name;

        ClearSkyboxButtonBorders();
        //Debug.Log("GHJGJHGHGGHJGHGHJG:: " + skyboxClickedName);
        ButtonHoverHandler.selectedSkybox               = skyboxClickedName;
        clickedButtonBorder.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        clickedButtonBorder.SetActive(true);

        // Set new skybox, then save currently selected skybox in cosmetics.
        Skybox.SetNewSkybox(skyboxClickedName);

        // Change selected skybox text based on button clicked in general settings panel.
        switch (skyboxClickedName) {
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

        // EVENT:: for new skybox button clicked
        DevEventHandler.CheckInterfaceEvent($"\"{skyboxClickedName}\" {I18nTextTranslator.SetTranslatedText("eventinterfaceskyboxbutton")}");
        // EVENT:: for new skybox selected
        DevEventHandler.CheckSkyboxEvent($"{I18nTextTranslator.SetTranslatedText("eventskyboxchange")} \"{skyboxClickedName}\"");
        
        // Saves new selected skybox
        CosmeticsSettings.SaveSkyboxItem(skyboxClickedName);
    }

    /// <summary>
    /// Clears all gamemode button borders in settings panel (gamemode sub-section).
    /// </summary>
    public static void ClearGamemodeButtonBorders() {
        foreach (GameObject buttonBorder in GameObject.FindGameObjectsWithTag("ButtonBorderGamemode")) {
            if (buttonBorder != null) {
                buttonBorder.GetComponent<Image>().color = new Color32(255, 255, 255, 120);
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
                buttonBorder.GetComponent<Image>().color = new Color32(255, 255, 255, 120);
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
                buttonBorder.GetComponent<Image>().color = new Color32(255, 255, 255, 120);
                buttonBorder.SetActive(false);
            }
        }
    }
}
