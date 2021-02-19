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
            SetCosmeticsValues(loadedCosmeticsData.gamemode, loadedCosmeticsData.targetColor, loadedCosmeticsData.customColorNameStrings, loadedCosmeticsData.customColorIndex, loadedCosmeticsData.customColorStrings, loadedCosmeticsData.skybox, 960f, 540f, 1455.711f, 638.3904f, loadedCosmeticsData.quickStartGame);
            
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
        SetCosmeticsValues(GamemodeType.GRID, TargetType.YELLOW, "", -1, "", SkyboxType.SLATE, 960f, 540f, 1455.711f, 638.3904f, false);

        // Saves defaults to new 'cometics.settings' file.
        CosmeticsSettings.SaveAllCosmeticsToggleDefaults(GamemodeType.GRID, TargetType.YELLOW, "", -1, "", SkyboxType.SLATE, 960f, 540f, 1455.711f, 638.3904f, false);
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
        cosmeticsSaveLoad.showModeText.SetText($"{GamemodeUtil.ReturnGamemodeType_StringShort(gamemode)}");

        // Populates gamemode select panel with saved gamemode.
        GamemodeSelect.PopulateGamemodeSelect(gamemode, quickStart);
        GamemodeSelect.ClearGamemodeButtonColors(GameObject.Find($"{GamemodeUtil.ReturnGamemodeType_StringFull(gamemode)}-Text (TMP)").GetComponent<TMP_Text>(), true, true);

        switch (gamemode) {
            case GamemodeType.SCATTER: SetGamemodeBorder(cosmeticsSaveLoad.gamemodeScatterBorder); break;
            case GamemodeType.FLICK:   SetGamemodeBorder(cosmeticsSaveLoad.gamemodeFlickBorder);   break;
            case GamemodeType.GRID:    SetGamemodeBorder(cosmeticsSaveLoad.gamemodeGridBorder);    break;
            case GamemodeType.GRID_2:  SetGamemodeBorder(cosmeticsSaveLoad.gamemodeGrid2Border); cosmeticsSaveLoad.showModeText.SetText($"GRID II");  break;
            case GamemodeType.GRID_3:  SetGamemodeBorder(cosmeticsSaveLoad.gamemodeGrid2Border); cosmeticsSaveLoad.showModeText.SetText($"GRID III"); break;
            case GamemodeType.PAIRS:   SetGamemodeBorder(cosmeticsSaveLoad.gamemodePairsBorder);   break;
            case GamemodeType.FOLLOW:  SetGamemodeBorder(cosmeticsSaveLoad.gamemodeFollowBorder);  break;
            case GamemodeType.GLOB:    SetGamemodeBorder(cosmeticsSaveLoad.gamemodeGlobBorder);    break;
        }
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
        TargetUtil.ClearTargetColorButtonBorders();

        SetTargetColorBorder(targetColor);

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
            case SkyboxType.PINK:   SetCosmeticsItems(cosmeticsSaveLoad.skyboxPinkBorder, cosmeticsSaveLoad.skyboxSelected, "skyboxpink");     break;
            case SkyboxType.GOLDEN: SetCosmeticsItems(cosmeticsSaveLoad.skyboxGoldenBorder, cosmeticsSaveLoad.skyboxSelected, "skyboxgolden"); break;
            case SkyboxType.NIGHT:  SetCosmeticsItems(cosmeticsSaveLoad.skyboxNightBorder, cosmeticsSaveLoad.skyboxSelected, "skyboxnight");   break;
            case SkyboxType.GREY:   SetCosmeticsItems(cosmeticsSaveLoad.skyboxGreyBorder, cosmeticsSaveLoad.skyboxSelected, "skyboxgrey");     break;
            case SkyboxType.BLUE:   SetCosmeticsItems(cosmeticsSaveLoad.skyboxBlueBorder, cosmeticsSaveLoad.skyboxSelected, "skyboxblue");     break;
            case SkyboxType.SLATE:  SetCosmeticsItems(cosmeticsSaveLoad.skyboxSlateBorder, cosmeticsSaveLoad.skyboxSelected, "skyboxslate");   break;
        }

        activeSkyboxText = $"//  {SkyboxUtil.ReturnSkyboxType_StringTranslated(skybox)}";
    }

    #region public utils

    public static void SetTargetColorBorder(TargetType setType) {
        switch (setType) {
            case TargetType.RED:    SetCosmeticsItems(cosmeticsSaveLoad.targetColorRedBorder, cosmeticsSaveLoad.targetColorSelected, "colorred");       break;
            case TargetType.ORANGE: SetCosmeticsItems(cosmeticsSaveLoad.targetColorOrangeBorder, cosmeticsSaveLoad.targetColorSelected, "colororange"); break;
            case TargetType.YELLOW: SetCosmeticsItems(cosmeticsSaveLoad.targetColorYellowBorder, cosmeticsSaveLoad.targetColorSelected, "coloryellow"); break;
            case TargetType.GREEN:  SetCosmeticsItems(cosmeticsSaveLoad.targetColorGreenBorder, cosmeticsSaveLoad.targetColorSelected, "colorgreen");   break;
            case TargetType.BLUE:   SetCosmeticsItems(cosmeticsSaveLoad.targetColorBlueBorder, cosmeticsSaveLoad.targetColorSelected, "colorblue");     break;
            case TargetType.PURPLE: SetCosmeticsItems(cosmeticsSaveLoad.targetColorPurpleBorder, cosmeticsSaveLoad.targetColorSelected, "colorpurple"); break;
            case TargetType.PINK:   SetCosmeticsItems(cosmeticsSaveLoad.targetColorPinkBorder, cosmeticsSaveLoad.targetColorSelected, "colorpink");     break;
            case TargetType.WHITE:  SetCosmeticsItems(cosmeticsSaveLoad.targetColorWhiteBorder, cosmeticsSaveLoad.targetColorSelected, "colorwhite");   break;
            case TargetType.CUSTOM: break; // TODO: set custom color button
        }
    }

    #endregion

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
        //selectedText.SetText($"//  {I18nTextTranslator.SetTranslatedText(translatedTextID)}");
    }

    //private static void SetCustomTargetButton() {

    //}

    private static void SetCosmeticsValues(GamemodeType setGamemode, TargetType setTargetColor, string setCustomNameStrings, int setCustomColorIndex, string setCustomColorStrings, SkyboxType setSkybox, float setPanelSettingsX, float setPanelSettingsY, float setPanelExtraStatsX, float setPanelExtraStatsY, bool setQuickStart) {
        SetQuickStartGame(setQuickStart);
        SetGamemode(setGamemode, setQuickStart);
        SetTargetColor(setTargetColor, setGamemode, setCustomNameStrings, setCustomColorIndex, setCustomColorStrings);
        SetSkybox(setSkybox);
    }

    /// <summary>
    /// Sets settings panel X and Y position from supplied cordinates (panelX) (panelY).
    /// </summary>
    /// <param name="panelX"></param>
    /// <param name="panelY"></param>
    private static void SetSettingsPanel(float panelX, float panelY) {          cosmeticsSaveLoad.settingsPanel.transform.position = new Vector3(panelX, panelY, 0f); }
    /// <summary>
    /// Sets 'AfterActionReport' panel X and Y position from supplied cordinates (panelX) (panelY).
    /// </summary>
    /// <param name="panelX"></param>
    /// <param name="panelY"></param>
    private static void SetAfterActionReportPanel(float panelX, float panelY) { cosmeticsSaveLoad.afterActionReportPanel.transform.position = new Vector3(panelX, panelY, 0f); }
    /// <summary>
    /// Sets extra stats panel X and Y position from supplied cordinates (panelX) (panelY).
    /// </summary>
    /// <param name="panelX"></param>
    /// <param name="panelY"></param>
    private static void SetExtraStatsPanel(float panelX, float panelY) {        cosmeticsSaveLoad.extraStatsPanel.transform.position = new Vector3(panelX, panelY, 0f); }

    public static void SetQuickStartGame(bool setQuickStart) {
        cosmeticsSaveLoad.quickStartToggle.isOn = setQuickStart;

        if (setQuickStart) {
            GamemodeSelect.CloseGamemodeSelect();
        } else {
            GamemodeSelect.OpenGamemodeSelect();
        }
    }
}
