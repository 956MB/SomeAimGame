using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class CosmeticsSaveSystem : MonoBehaviour {
    public GameObject targetColorRedBorder, targetColorOrangeBorder, targetColorYellowBorder, targetColorGreenBorder, targetColorBlueBorder, targetColorPurpleBorder, targetColorPinkBorder, targetColorWhiteBorder;
    public GameObject skyboxPinkBorder, skyboxGoldenBorder, skyboxNightBorder, skyboxGreyBorder, skyboxBlueBorder, skyboxSlateBorder;
    public GameObject gamemodeScatterBorder, gamemodeFlickBorder, gamemodeGridBorder, gamemodeGrid2Border, gamemodePairsBorder, gamemodeFollowBorder, gamemodePatrolBorder;
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
    public static void SaveCosmeticsSettingsData(CosmeticsSettings cosmeticsSettings) {
        BinaryFormatter formatter = new BinaryFormatter();
        string dirPath            = Application.persistentDataPath + "/settings";
        string filePath           = dirPath + "/cosmetics.settings";

        DirectoryInfo dirInf = new DirectoryInfo(dirPath);
        if (!dirInf.Exists) { dirInf.Create(); }

        FileStream stream                 = new FileStream(filePath, FileMode.Create);
        CosmeticsDataSerial cosmeticsData = new CosmeticsDataSerial();
        formatter.Serialize(stream, cosmeticsData);
        stream.Close();
    }

    /// <summary>
    /// Loads cosmetics data (CosmeticsDataSerial) from file.
    /// </summary>
    /// <returns></returns>
    public static CosmeticsDataSerial LoadCosmeticsSettingsData() {
        string path = Application.persistentDataPath + "/settings/cosmetics.settings";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream         = new FileStream(path, FileMode.Open);

            CosmeticsDataSerial cosmeticsData = formatter.Deserialize(stream) as CosmeticsDataSerial;
            stream.Close();

            return cosmeticsData;
        } else {
            return null;
        }
    }

    /// <summary>
    /// Inits saved cosmetics object and sets all cosmetics values.
    /// </summary>
    public static void InitSavedCosmeticsSettings() {
        CosmeticsDataSerial loadedCosmeticsData = LoadCosmeticsSettingsData();

        if (loadedCosmeticsData != null) {
            CosmeticsSettings.LoadCosmeticsSettings(loadedCosmeticsData);

            SetQuickStartGame(loadedCosmeticsData.quickStartGame);
            SetGamemode(loadedCosmeticsData.gamemode, loadedCosmeticsData.quickStartGame);
            SetTargetColor(loadedCosmeticsData.targetColor, loadedCosmeticsData.gamemode);
            SetSkybox(loadedCosmeticsData.skybox);
            
            CosmeticsSettings.SaveAllCosmeticsToggleDefaults(loadedCosmeticsData.gamemode, loadedCosmeticsData.targetColor, loadedCosmeticsData.skybox, loadedCosmeticsData.afterActionReportPanelX, loadedCosmeticsData.afterActionReportPanelY, loadedCosmeticsData.extraStatsPanelX, loadedCosmeticsData.extraStatsPanelY, loadedCosmeticsData.quickStartGame);
        } else {
            InitCosmeticsSettingsDefaults();
        }
    }

    /// <summary>
    /// Inits default cosmetics values and saves to file on first launch.
    /// </summary>
    public static void InitCosmeticsSettingsDefaults() {
        // Gamemode value.
        SetGamemode(Gamemode.Grid, false);

        // Target color value.
        SetTargetColor(TargetColor.Yellow, Gamemode.Grid);
        saveLoad.targetColorSelected.SetText($"//    {I18nTextTranslator.SetTranslatedText("coloryellow")}");
        
        // Skybox value.
        SetSkybox(Skybox.Slate);
        saveLoad.skyboxSelected.SetText($"//    {I18nTextTranslator.SetTranslatedText("skyboxslate")}");
        
        // Settings and extra stats panel values.
        //SetAfterActionReportPanel(960f, 540f);
        //SetExtraStatsPanel(1455.711f, 638.3904f);

        saveLoad.quickStartToggle.isOn = false;

        // Saves defaults to new 'cometics.settings' file.
        CosmeticsSettings.SaveAllCosmeticsToggleDefaults(Gamemode.Grid, TargetColor.Yellow, Skybox.Slate, 960f, 540f, 1455.711f, 638.3904f, false);
    }

    /// <summary>
    /// Sets current gamemode with supplied gamemode string (gamemode), and corresponding button/translated text in settings panel.
    /// </summary>
    /// <param name="gamemode"></param>
    private static void SetGamemode(Gamemode gamemode, bool quickStart) {
        ButtonHoverHandler.selectedGamemode = gamemode;
        ButtonClickHandler.ClearGamemodeButtonBorders();
        SpawnTargets.gamemode = gamemode;

        // Set showMode text to gamemode.
        saveLoad.showModeText.SetText($"{GamemodeType.ReturnGamemodeType_StringShort(gamemode)}");

        // Populates gamemode select panel with saved gamemode.
        GamemodeSelect.PopulateGamemodeSelect(gamemode, quickStart);
        GamemodeSelect.ClearGamemodeButtonColors(GameObject.Find($"{GamemodeType.ReturnGamemodeType_StringFull(gamemode)}-Text (TMP)").GetComponent<TMP_Text>(), true, true);

        switch (gamemode) {
            case Gamemode.Scatter:
                SetGamemodeBorder(saveLoad.gamemodeScatterBorder);
                break;
            case Gamemode.Flick:
                SetGamemodeBorder(saveLoad.gamemodeFlickBorder);
                break;
            case Gamemode.Grid:
                SetGamemodeBorder(saveLoad.gamemodeGridBorder);
                break;
            case Gamemode.Grid2:
                SetGamemodeBorder(saveLoad.gamemodeGrid2Border);
                saveLoad.showModeText.SetText($"GRID II");
                break;
            case Gamemode.Grid3:
                SetGamemodeBorder(saveLoad.gamemodeGrid2Border);
                saveLoad.showModeText.SetText($"GRID III");
                break;
            case Gamemode.Pairs:
                SetGamemodeBorder(saveLoad.gamemodePairsBorder);
                break;
            case Gamemode.Follow:
                SetGamemodeBorder(saveLoad.gamemodeFollowBorder);
                break;
        }
    }

    /// <summary>
    /// Sets current target color with supplied color string (targetColor) based on current gamemode (gamemode), and corresponding button/translated text in settings panel.
    /// </summary>
    /// <param name="targetColor"></param>
    /// <param name="gamemode"></param>
    private static void SetTargetColor(TargetColor targetColor, Gamemode gamemode) {
        if (gamemode == Gamemode.Follow) {
            SpawnTargets.SetTargetColor(targetColor, true);
        } else {
            SpawnTargets.SetTargetColor(targetColor, false);
        }

        ButtonHoverHandler.selectedTargetColor = targetColor;
        ButtonClickHandler.ClearTargetColorButtonBorders();

        switch (targetColor) {
            case TargetColor.Red:
                SetCosmeticsItems(saveLoad.targetColorRedBorder, saveLoad.targetColorSelected, "colorred");
                activeTargetColorText = $"//  {I18nTextTranslator.SetTranslatedText("colorred")}";
                break;
            case TargetColor.Orange:
                SetCosmeticsItems(saveLoad.targetColorOrangeBorder, saveLoad.targetColorSelected, "colororange");
                activeTargetColorText = $"//  {I18nTextTranslator.SetTranslatedText("colororange")}";
                break;
            case TargetColor.Yellow:
                SetCosmeticsItems(saveLoad.targetColorYellowBorder, saveLoad.targetColorSelected, "coloryellow");
                activeTargetColorText = $"//  {I18nTextTranslator.SetTranslatedText("coloryellow")}";
                break;
            case TargetColor.Green:
                SetCosmeticsItems(saveLoad.targetColorGreenBorder, saveLoad.targetColorSelected, "colorgreen");
                activeTargetColorText = $"//  {I18nTextTranslator.SetTranslatedText("colorgreen")}";
                break;
            case TargetColor.Blue:
                SetCosmeticsItems(saveLoad.targetColorBlueBorder, saveLoad.targetColorSelected, "colorblue");
                activeTargetColorText = $"//  {I18nTextTranslator.SetTranslatedText("colorblue")}";
                break;
            case TargetColor.Purple:
                SetCosmeticsItems(saveLoad.targetColorPurpleBorder, saveLoad.targetColorSelected, "colorpurple");
                activeTargetColorText = $"//  {I18nTextTranslator.SetTranslatedText("colorpurple")}";
                break;
            case TargetColor.Pink:
                SetCosmeticsItems(saveLoad.targetColorPinkBorder, saveLoad.targetColorSelected, "colorpink");
                activeTargetColorText = $"//  {I18nTextTranslator.SetTranslatedText("colorpink")}";
                break;
            case TargetColor.White:
                SetCosmeticsItems(saveLoad.targetColorWhiteBorder, saveLoad.targetColorSelected, "colorwhite");
                activeTargetColorText = $"//  {I18nTextTranslator.SetTranslatedText("colorwhite")}";
                break;
        }
    }

    /// <summary>
    /// Sets current skybox with supplied skybox string (skybox), and corresponding button/translated text in settings panel.
    /// </summary>
    /// <param name="skybox"></param>
    private static void SetSkybox(Skybox skybox) {
        ButtonClickHandler.ClearSkyboxButtonBorders();
        ButtonHoverHandler.selectedSkybox = skybox;
        SkyboxHandler.SetNewSkybox(skybox);

        switch (skybox) {
            case Skybox.Pink:
                SetCosmeticsItems(saveLoad.skyboxPinkBorder, saveLoad.skyboxSelected, "skyboxpink");
                activeSkyboxText = $"//  {I18nTextTranslator.SetTranslatedText("skyboxpink")}";
                break;
            case Skybox.Golden:
                SetCosmeticsItems(saveLoad.skyboxGoldenBorder, saveLoad.skyboxSelected, "skyboxgolden");
                activeSkyboxText = $"//  {I18nTextTranslator.SetTranslatedText("skyboxgolden")}";
                break;
            case Skybox.Night:
                SetCosmeticsItems(saveLoad.skyboxNightBorder, saveLoad.skyboxSelected, "skyboxnight");
                activeSkyboxText = $"//  {I18nTextTranslator.SetTranslatedText("skyboxnight")}";
                break;
            case Skybox.Grey:
                SetCosmeticsItems(saveLoad.skyboxGreyBorder, saveLoad.skyboxSelected, "skyboxgrey");
                activeSkyboxText = $"//  {I18nTextTranslator.SetTranslatedText("skyboxgrey")}";
                break;
            case Skybox.Blue:
                SetCosmeticsItems(saveLoad.skyboxBlueBorder, saveLoad.skyboxSelected, "skyboxblue");
                activeSkyboxText = $"//  {I18nTextTranslator.SetTranslatedText("skyboxblue")}";
                break;
            case Skybox.Slate:
                SetCosmeticsItems(saveLoad.skyboxSlateBorder, saveLoad.skyboxSelected, "skyboxslate");
                activeSkyboxText = $"//  {I18nTextTranslator.SetTranslatedText("skyboxslate")}";
                break;
        }
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
