using UnityEngine;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Skybox;
using SomeAimGame.Gamemode;
using SomeAimGame.Targets;
using SomeAimGame.Utilities;

public class CosmeticsSaveSystem : MonoBehaviour {
    public GameObject targetColorRedBorder, targetColorOrangeBorder, targetColorYellowBorder, targetColorGreenBorder, targetColorBlueBorder, targetColorPurpleBorder, targetColorPinkBorder, targetColorWhiteBorder;
    public GameObject skyboxPinkBorder, skyboxGoldenBorder, skyboxNightBorder, skyboxGreyBorder, skyboxBlueBorder, skyboxSlateBorder;
    public GameObject gamemodeScatterBorder, gamemodeFlickBorder, gamemodeGridBorder, gamemodeGrid2Border, gamemodePairsBorder, gamemodeFollowBorder, gamemodeGlobBorder, gamemodePatrolBorder;
    public GameObject settingsPanel, afterActionReportPanel, extraStatsPanel;
    public TMP_Text targetColorSelected, skyboxSelected, showModeText;
    public static string activeTargetColorText, activeSkyboxText;
    public Toggle quickStartToggle;

    private static CosmeticsSaveSystem saveLoad;
    void Awake() { saveLoad = this; }

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
            SetQuickStartGame(loadedCosmeticsData.quickStartGame);
            SetGamemode(loadedCosmeticsData.gamemode, loadedCosmeticsData.quickStartGame);
            SetTargetColor(loadedCosmeticsData.targetColor, loadedCosmeticsData.gamemode);
            SetSkybox(loadedCosmeticsData.skybox);
            
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
        // Gamemode value.
        SetGamemode(GamemodeType.GRID, false);

        // Target color value.
        SetTargetColor(TargetType.YELLOW, GamemodeType.GRID);
        saveLoad.targetColorSelected.SetText($"//    {I18nTextTranslator.SetTranslatedText("coloryellow")}");
        
        // Skybox value.
        SetSkybox(SkyboxType.SLATE);
        saveLoad.skyboxSelected.SetText($"//    {I18nTextTranslator.SetTranslatedText("skyboxslate")}");
        
        // Settings and extra stats panel values.
        //SetAfterActionReportPanel(960f, 540f);
        //SetExtraStatsPanel(1455.711f, 638.3904f);

        saveLoad.quickStartToggle.isOn = false;

        // Saves defaults to new 'cometics.settings' file.
        CosmeticsSettings.SaveAllCosmeticsToggleDefaults(GamemodeType.GRID, TargetType.YELLOW, SkyboxType.SLATE, 960f, 540f, 1455.711f, 638.3904f, false);
    }

    /// <summary>
    /// Sets current gamemode with supplied gamemode string (gamemode), and corresponding button/translated text in settings panel.
    /// </summary>
    /// <param name="gamemode"></param>
    private static void SetGamemode(GamemodeType gamemode, bool quickStart) {
        ButtonHoverHandler.selectedGamemode = gamemode;
        GamemodeUtil.ClearGamemodeButtonBorders();
        SpawnTargets.gamemode = gamemode;

        // Set showMode text to gamemode.
        saveLoad.showModeText.SetText($"{GamemodeUtil.ReturnGamemodeType_StringShort(gamemode)}");

        // Populates gamemode select panel with saved gamemode.
        GamemodeSelect.PopulateGamemodeSelect(gamemode, quickStart);
        GamemodeSelect.ClearGamemodeButtonColors(GameObject.Find($"{GamemodeUtil.ReturnGamemodeType_StringFull(gamemode)}-Text (TMP)").GetComponent<TMP_Text>(), true, true);

        switch (gamemode) {
            case GamemodeType.SCATTER: SetGamemodeBorder(saveLoad.gamemodeScatterBorder); break;
            case GamemodeType.FLICK:   SetGamemodeBorder(saveLoad.gamemodeFlickBorder);   break;
            case GamemodeType.GRID:    SetGamemodeBorder(saveLoad.gamemodeGridBorder);    break;
            case GamemodeType.GRID_2:
                SetGamemodeBorder(saveLoad.gamemodeGrid2Border);
                saveLoad.showModeText.SetText($"GRID II");
                break;
            case GamemodeType.GRID_3:
                SetGamemodeBorder(saveLoad.gamemodeGrid2Border);
                saveLoad.showModeText.SetText($"GRID III");
                break;
            case GamemodeType.PAIRS:  SetGamemodeBorder(saveLoad.gamemodePairsBorder);  break;
            case GamemodeType.FOLLOW: SetGamemodeBorder(saveLoad.gamemodeFollowBorder); break;
            case GamemodeType.GLOB:   SetGamemodeBorder(saveLoad.gamemodeGlobBorder);   break;
        }
    }

    /// <summary>
    /// Sets current target color with supplied color string (targetColor) based on current gamemode (gamemode), and corresponding button/translated text in settings panel.
    /// </summary>
    /// <param name="targetColor"></param>
    /// <param name="gamemode"></param>
    private static void SetTargetColor(TargetType targetColor, GamemodeType gamemode) {
        SpawnTargets.SetTargetColor(targetColor, gamemode == GamemodeType.FOLLOW);

        ButtonHoverHandler.selectedTargetColor = targetColor;
        TargetUtil.ClearTargetColorButtonBorders();

        switch (targetColor) {
            case TargetType.RED:    SetCosmeticsItems(saveLoad.targetColorRedBorder, saveLoad.targetColorSelected, "colorred");       break;
            case TargetType.ORANGE: SetCosmeticsItems(saveLoad.targetColorOrangeBorder, saveLoad.targetColorSelected, "colororange"); break;
            case TargetType.YELLOW: SetCosmeticsItems(saveLoad.targetColorYellowBorder, saveLoad.targetColorSelected, "coloryellow"); break;
            case TargetType.GREEN:  SetCosmeticsItems(saveLoad.targetColorGreenBorder, saveLoad.targetColorSelected, "colorgreen");   break;
            case TargetType.BLUE:   SetCosmeticsItems(saveLoad.targetColorBlueBorder, saveLoad.targetColorSelected, "colorblue");     break;
            case TargetType.PURPLE: SetCosmeticsItems(saveLoad.targetColorPurpleBorder, saveLoad.targetColorSelected, "colorpurple"); break;
            case TargetType.PINK:   SetCosmeticsItems(saveLoad.targetColorPinkBorder, saveLoad.targetColorSelected, "colorpink");     break;
            case TargetType.WHITE:  SetCosmeticsItems(saveLoad.targetColorWhiteBorder, saveLoad.targetColorSelected, "colorwhite");   break;
        }

        activeTargetColorText = $"//  {TargetUtil.ReturnTargetColorType_StringTranslated(targetColor)}";
    }

    /// <summary>
    /// Sets current skybox with supplied skybox string (skybox), and corresponding button/translated text in settings panel.
    /// </summary>
    /// <param name="skybox"></param>
    private static void SetSkybox(SkyboxType skybox) {
        SkyboxUtil.ClearSkyboxButtonBorders();
        ButtonHoverHandler.selectedSkybox = skybox;
        SkyboxHandler.SetNewSkybox(skybox);

        switch (skybox) {
            case SkyboxType.PINK:   SetCosmeticsItems(saveLoad.skyboxPinkBorder, saveLoad.skyboxSelected, "skyboxpink");     break;
            case SkyboxType.GOLDEN: SetCosmeticsItems(saveLoad.skyboxGoldenBorder, saveLoad.skyboxSelected, "skyboxgolden"); break;
            case SkyboxType.NIGHT:  SetCosmeticsItems(saveLoad.skyboxNightBorder, saveLoad.skyboxSelected, "skyboxnight");   break;
            case SkyboxType.GREY:   SetCosmeticsItems(saveLoad.skyboxGreyBorder, saveLoad.skyboxSelected, "skyboxgrey");     break;
            case SkyboxType.BLUE:   SetCosmeticsItems(saveLoad.skyboxBlueBorder, saveLoad.skyboxSelected, "skyboxblue");     break;
            case SkyboxType.SLATE:  SetCosmeticsItems(saveLoad.skyboxSlateBorder, saveLoad.skyboxSelected, "skyboxslate");   break;
        }

        activeSkyboxText = $"//  {SkyboxUtil.ReturnSkyboxType_StringTranslated(skybox)}";
    }

    /// <summary>
    /// Sets active gamemode border in settings (gamemode sub-section).
    /// </summary>
    /// <param name="gamemodeBorder"></param>
    private static void SetGamemodeBorder(GameObject gamemodeBorder) {
        gamemodeBorder.GetComponent<Image>().color = InterfaceColors.selectedColor;
        gamemodeBorder.SetActive(true);
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
        border.SetActive(true);
        selectedText.SetText($"//  {I18nTextTranslator.SetTranslatedText(translatedTextID)}");
    }

    /// <summary>
    /// Sets settings panel X and Y position from supplied cordinates (panelX) (panelY).
    /// </summary>
    /// <param name="panelX"></param>
    /// <param name="panelY"></param>
    private static void SetSettingsPanel(float panelX, float panelY) { saveLoad.settingsPanel.transform.position = new Vector3(panelX, panelY, 0f); }

    /// <summary>
    /// Sets 'AfterActionReport' panel X and Y position from supplied cordinates (panelX) (panelY).
    /// </summary>
    /// <param name="panelX"></param>
    /// <param name="panelY"></param>
    private static void SetAfterActionReportPanel(float panelX, float panelY) { saveLoad.afterActionReportPanel.transform.position = new Vector3(panelX, panelY, 0f); }

    /// <summary>
    /// Sets extra stats panel X and Y position from supplied cordinates (panelX) (panelY).
    /// </summary>
    /// <param name="panelX"></param>
    /// <param name="panelY"></param>
    private static void SetExtraStatsPanel(float panelX, float panelY) { saveLoad.extraStatsPanel.transform.position = new Vector3(panelX, panelY, 0f); }

    public static void SetQuickStartGame(bool setQuickStart) {
        saveLoad.quickStartToggle.isOn = setQuickStart;

        if (setQuickStart) {
            GamemodeSelect.CloseGamemodeSelect();
        } else {
            GamemodeSelect.OpenGamemodeSelect();
        }
    }
}
