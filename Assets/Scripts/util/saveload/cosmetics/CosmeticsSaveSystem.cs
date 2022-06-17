using UnityEngine;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Skybox;
using SomeAimGame.Gamemode;
using SomeAimGame.Targets;
using SomeAimGame.Utilities;

public class CosmeticsSaveSystem : MonoBehaviour {
    //public GameObject targetColorRedBorder, targetColorOrangeBorder, targetColorYellowBorder, targetColorGreenBorder, targetColorBlueBorder, targetColorPurpleBorder, targetColorPinkBorder, targetColorWhiteBorder;
    //public GameObject skyboxPinkBorder, skyboxGoldenBorder, skyboxNightBorder, skyboxGreyBorder, skyboxBlueBorder, skyboxSlateBorder;
    public GameObject[] gamemodeBorders, targetBorders, skyboxBorders;
    //public GameObject gamemodeScatterBorder, gamemodeFlickBorder, gamemodeGridBorder, gamemodeGrid2Border, gamemodePairsBorder, gamemodeFollowBorder, gamemodeGlobBorder, gamemodePatrolBorder;
    public GameObject settingsPanel, afterActionReportPanel, extraStatsPanel, consolePanel;
    public TMP_Text targetColorSelected, skyboxSelected, showModeText;
    public static string activeTargetColorText, activeSkyboxText;
    public Toggle quickStartToggle;

    private static CosmeticsSaveSystem cosmeticsSaveLoad;
    void Awake() { cosmeticsSaveLoad = this; }

    /// <summary>
    /// Saves supplied cosmetics object (CosmeticsSettings) to file.
    /// </summary>
    /// <param name="cosmeticsSettings"></param>
    public static void SaveCosmeticsSettingsData() {
        CosmeticsDataSerial cosmeticsData = new CosmeticsDataSerial();
        SaveLoadUtil.SaveDataSerial("/prefs", "/sag_cosmetics.prefs", cosmeticsData);
    }

    /// <summary>
    /// Loads cosmetics data (CosmeticsDataSerial) from file.
    /// </summary>
    /// <returns></returns>
    public static CosmeticsDataSerial LoadCosmeticsSettingsData() {
        CosmeticsDataSerial cosmeticsData = (CosmeticsDataSerial)SaveLoadUtil.LoadDataSerial("/prefs/sag_cosmetics.prefs", SaveType.COSMETICS);
        return cosmeticsData;
    }

    /// <summary>
    /// Inits saved cosmetics object and sets all cosmetics values.
    /// </summary>
    public static void InitSavedCosmeticsSettings() {
        CosmeticsDataSerial loadedCosmeticsData = LoadCosmeticsSettingsData();

        if (loadedCosmeticsData != null) {
            SetCosmeticsValues(loadedCosmeticsData.gamemode, loadedCosmeticsData.targetColor, loadedCosmeticsData.customColorNameStrings, loadedCosmeticsData.customColorIndex, loadedCosmeticsData.customColorStrings, loadedCosmeticsData.skybox, 960f, 540f, 1455.711f, 638.3904f, loadedCosmeticsData.consolePanelX, loadedCosmeticsData.consolePanelY, loadedCosmeticsData.quickStartGame);
            
            CosmeticsSettings.LoadCosmeticsSettings(loadedCosmeticsData);
        } else {
            InitCosmeticsSettingsDefaults();
        }

        SettingsPanel.LoadGamemodePreviews();
    }

    /// <summary>
    /// Inits default cosmetics values and saves to file on first launch.
    /// </summary>
    public static void InitCosmeticsSettingsDefaults() {
        SetCosmeticsValues(GamemodeType.GRID, TargetType.YELLOW, "", -1, "", SkyboxType.SLATE, 960f, 540f, 1455.711f, 638.3904f, 0f, 885.6f, false);

        // Saves defaults to new 'cometics.settings' file.
        CosmeticsSettings.SaveAllCosmeticsToggleDefaults(GamemodeType.GRID, TargetType.YELLOW, "", -1, "", SkyboxType.SLATE, 960f, 540f, 1455.711f, 638.3904f, 0f, 885.6f, false);
    }

    /// <summary>
    /// Sets current gamemode with supplied gamemode string (gamemode), and corresponding button/translated text in settings panel.
    /// </summary>
    /// <param name="gamemode"></param>
    private static void SetGamemode(GamemodeType gamemode, bool quickStart) {
        //ButtonHoverHandler.selectedGamemode = gamemode;
        SpawnTargets.gamemode = gamemode;
        ClearGamemodeButtonBorders();

        // Set showMode text to gamemode.
        cosmeticsSaveLoad.showModeText.SetText($"{GamemodeUtil.ReturnGamemodeType_StringShort(gamemode)}");

        // Populates gamemode select panel with saved gamemode.
        GamemodeSelect.PopulateGamemodeSelectText(gamemode, quickStart);
        GamemodeSelect.PopulateGamemodeSelectVideoClip(gamemode);
        GamemodeSelect.ClearGamemodeButtonColors(GameObject.Find($"{GamemodeUtil.ReturnGamemodeType_StringFull(gamemode)}-Text (TMP)").GetComponent<TMP_Text>(), true, true);

        SetGamemodeBorder(gamemode);
    }

    /// <summary>
    /// Sets current target color with supplied color string (targetColor) based on current gamemode (gamemode), and corresponding button/translated text in settings panel.
    /// </summary>
    /// <param name="targetColor"></param>
    /// <param name="gamemode"></param>
    private static void SetTargetColor(TargetType targetColor, GamemodeType gamemode, string setCustomColorNames, int setCustomColorIndex, string setCustomColorStrings) {
        //Debug.Log($"names: [{setCustomColorNames}] index: [{setCustomColorIndex}] colors: [{setCustomColorStrings}]");
        CustomTargetColorUtil.LoadCustomColorsFromSave(setCustomColorNames, setCustomColorIndex, setCustomColorStrings);
        SpawnTargets.SetTargetColor(targetColor, gamemode == GamemodeType.FOLLOW);

        ButtonHoverHandler.selectedTargetColor = targetColor;
        ClearTargetButtonBorders();

        SetTargetColorBorder(targetColor);

        activeTargetColorText = $"//  {TargetUtil.ReturnTargetColorType_StringTranslated(targetColor)}";
    }

    /// <summary>
    /// Sets current skybox with supplied skybox string (skybox), and corresponding button/translated text in settings panel.
    /// </summary>
    /// <param name="skybox"></param>
    private static void SetSkybox(SkyboxType skybox) {
        ClearSkyboxButtonBorders();
        ButtonHoverHandler.selectedSkybox = skybox;
        SkyboxHandler.SetNewSkybox(skybox);

        switch (skybox) {
            case SkyboxType.PINK:   SetCosmeticsItems(cosmeticsSaveLoad.skyboxBorders[0], cosmeticsSaveLoad.skyboxSelected, "skyboxpink");   break;
            case SkyboxType.GOLDEN: SetCosmeticsItems(cosmeticsSaveLoad.skyboxBorders[1], cosmeticsSaveLoad.skyboxSelected, "skyboxgolden"); break;
            case SkyboxType.NIGHT:  SetCosmeticsItems(cosmeticsSaveLoad.skyboxBorders[2], cosmeticsSaveLoad.skyboxSelected, "skyboxnight");  break;
            case SkyboxType.GREY:   SetCosmeticsItems(cosmeticsSaveLoad.skyboxBorders[3], cosmeticsSaveLoad.skyboxSelected, "skyboxgrey");   break;
            case SkyboxType.BLUE:   SetCosmeticsItems(cosmeticsSaveLoad.skyboxBorders[4], cosmeticsSaveLoad.skyboxSelected, "skyboxblue");   break;
            case SkyboxType.SLATE:  SetCosmeticsItems(cosmeticsSaveLoad.skyboxBorders[5], cosmeticsSaveLoad.skyboxSelected, "skyboxslate");  break;
        }

        activeSkyboxText = $"//  {SkyboxUtil.ReturnSkyboxType_StringTranslated(skybox)}";
    }

    #region public utils

    public static void SetGamemodeBorder(GamemodeType setType) {
        switch (setType) {
            case GamemodeType.SCATTER: SetGamemodeBorder(cosmeticsSaveLoad.gamemodeBorders[0]);                                                      break;
            case GamemodeType.FLICK:   SetGamemodeBorder(cosmeticsSaveLoad.gamemodeBorders[1]);                                                      break;
            case GamemodeType.GRID:    SetGamemodeBorder(cosmeticsSaveLoad.gamemodeBorders[2]);                                                      break;
            case GamemodeType.GRID_2:  SetGamemodeBorder(cosmeticsSaveLoad.gamemodeBorders[3]); cosmeticsSaveLoad.showModeText.SetText($"GRID II");  break;
            //case GamemodeType.GRID_3:  SetGamemodeBorder(cosmeticsSaveLoad.gamemodeBorders[4]); cosmeticsSaveLoad.showModeText.SetText($"GRID III"); break;
            case GamemodeType.PAIRS:   SetGamemodeBorder(cosmeticsSaveLoad.gamemodeBorders[4]);                                                      break;
            case GamemodeType.FOLLOW:  SetGamemodeBorder(cosmeticsSaveLoad.gamemodeBorders[5]);                                                      break;
            case GamemodeType.GLOB:    SetGamemodeBorder(cosmeticsSaveLoad.gamemodeBorders[6]);                                                      break;
        }
    }

    public static void SetTargetColorBorder(TargetType setType) {
        switch (setType) {
            case TargetType.RED:    SetCosmeticsItems(cosmeticsSaveLoad.targetBorders[0], cosmeticsSaveLoad.targetColorSelected, "colorred");    break;
            case TargetType.ORANGE: SetCosmeticsItems(cosmeticsSaveLoad.targetBorders[1], cosmeticsSaveLoad.targetColorSelected, "colororange"); break;
            case TargetType.YELLOW: SetCosmeticsItems(cosmeticsSaveLoad.targetBorders[2], cosmeticsSaveLoad.targetColorSelected, "coloryellow"); break;
            case TargetType.GREEN:  SetCosmeticsItems(cosmeticsSaveLoad.targetBorders[3], cosmeticsSaveLoad.targetColorSelected, "colorgreen");  break;
            case TargetType.BLUE:   SetCosmeticsItems(cosmeticsSaveLoad.targetBorders[4], cosmeticsSaveLoad.targetColorSelected, "colorblue");   break;
            case TargetType.PURPLE: SetCosmeticsItems(cosmeticsSaveLoad.targetBorders[5], cosmeticsSaveLoad.targetColorSelected, "colorpurple"); break;
            case TargetType.PINK:   SetCosmeticsItems(cosmeticsSaveLoad.targetBorders[6], cosmeticsSaveLoad.targetColorSelected, "colorpink");   break;
            case TargetType.WHITE:  SetCosmeticsItems(cosmeticsSaveLoad.targetBorders[7], cosmeticsSaveLoad.targetColorSelected, "colorwhite");  break;
            case TargetType.CUSTOM: break; // TODO: set custom color button
        }
    }

    public static void ClearGamemodeButtonBorders() {
        foreach (GameObject gamemodeBorder in cosmeticsSaveLoad.gamemodeBorders) {
            if (gamemodeBorder != null) { gamemodeBorder.GetComponent<Image>().color = InterfaceColors.inactiveColor; }
        }
    }
    public static void ClearTargetButtonBorders() {
        foreach (GameObject targetBorder in cosmeticsSaveLoad.targetBorders) {
            if (targetBorder != null) { targetBorder.GetComponent<Image>().color = InterfaceColors.inactiveColor; }
        }
    }
    public static void ClearSkyboxButtonBorders() {
        foreach (GameObject skyboxBorder in cosmeticsSaveLoad.skyboxBorders) {
            if (skyboxBorder != null) { skyboxBorder.GetComponent<Image>().color = InterfaceColors.inactiveColor; }
        }
    }

    #endregion

    /// <summary>
    /// Sets active gamemode border in settings (gamemode sub-section).
    /// </summary>
    /// <param name="gamemodeBorder"></param>
    private static void SetGamemodeBorder(GameObject gamemodeBorder) {
        gamemodeBorder.GetComponent<Image>().color = InterfaceColors.selectedColor;
        //gamemodeBorder.SetActive(true);
    }

    /// <summary>
    /// Sets active borders (target color / skybox) in settings (general sub-section).
    /// </summary>
    /// <param name="border"></param>
    /// <param name="selectedText"></param>
    /// <param name="activeText"></param>
    /// <param name="translatedTextID"></param>
    private static void SetCosmeticsItems(GameObject border, TMP_Text selectedText, string translatedTextID) {
        border.GetComponent<Image>().color = InterfaceColors.selectedColor;
        //border.SetActive(true);
        //selectedText.SetText($"//  {I18nTextTranslator.SetTranslatedText(translatedTextID)}");
    }

    //private static void SetCustomTargetButton() {

    //}

    private static void SetCosmeticsValues(GamemodeType setGamemode, TargetType setTargetColor, string setCustomNameStrings, int setCustomColorIndex, string setCustomColorStrings, SkyboxType setSkybox, float setPanelSettingsX, float setPanelSettingsY, float setPanelExtraStatsX, float setPanelExtraStatsY, float setPanelConsoleX, float setPanelConsoleY, bool setQuickStart) {
        SetQuickStartGame(setQuickStart);
        SetGamemode(setGamemode, setQuickStart);
        SetTargetColor(setTargetColor, setGamemode, setCustomNameStrings, setCustomColorIndex, setCustomColorStrings);
        //SetSkybox(setSkybox);
        SetPanelPos(cosmeticsSaveLoad.consolePanel, setPanelConsoleX, setPanelConsoleY);
    }

    /// <summary>
    /// Sets supplied panel X and Y position from supplied cordinates (panelX) (panelY).
    /// </summary>
    /// <param name="setPanel"></param>
    /// <param name="panelX"></param>
    /// <param name="panelY"></param>
    private static void SetPanelPos(GameObject setPanel, float panelX, float panelY) { setPanel.transform.position = new Vector3(panelX, panelY, 0f); }
    
    public static void SetQuickStartGame(bool setQuickStart) {
        cosmeticsSaveLoad.quickStartToggle.isOn = setQuickStart;

        if (setQuickStart) {
            GamemodeSelect.CloseGamemodeSelect();
        } else {
            GamemodeSelect.OpenGamemodeSelect();
        }
    }
}
