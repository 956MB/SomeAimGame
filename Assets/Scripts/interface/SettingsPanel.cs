//using Steamworks;
using UnityEngine;
using UnityEngine.Video;
//using UnityEditor;

using SomeAimGame.Skybox;
using SomeAimGame.Gamemode;
using SomeAimGame.Targets;
using SomeAimGame.Utilities;

public class SettingsPanel : MonoBehaviour {
    public GameObject mainMenuCanvas, settingsPanel, afterPanel, extendedStatsPanel, steamDataContainer, devEventContainer, crosshairImage;
    public static bool settingsOpen          = false;
    public static bool afterActionReportOpen = false;
    public static bool afterActionReportSet;
    public GameObject targetColorOrange, targetColorPurple;
    public Sprite targetColorRedThumbnail, targetColorOrangeThumbnail, targetColorYellowThumbnail, targetColorGreenThumbnail, targetColorBlueThumbnail, targetColorPurpleThumbnail, targetColorPinkThumbnail, targetColorWhiteThumbnail;
    public GameObject canvasRed, canvasOrange, canvasYellow, canvasGreen, canvasBlue, canvasPurple, canvasPink, canvasWhite;
    public Sprite skyboxPinkThumbnail, skyboxGoldenThumbnail, skyboxNightThumbnail, skyboxGreyThumbnail, skyboxBlueThumbnail, skyboxSlateThumbnail;
    public GameObject skyboxCanvasPink, skyboxCanvasGolden, skyboxCanvasNight, skyboxCanvasGrey, skyboxCanvasStars, skyboxCanvasSlate;
    public static float panelMoveSize = 75f;
    public static float panelWidth;
    RectTransform rt;

    private static VideoClip[] gamemodePreviewVideos;
    public VideoPlayer selectedVideoPlayer, scatterVideoPlayer, flickVideoPlayer, gridVideoPlayer, grid2VideoPlayer, pairsVideoPlayer, followVideoPlayer;

    private static SettingsPanel settings;
    void Awake() { settings = this; }

    void Start() {
        rt                   = (RectTransform)settingsPanel.transform;
        panelWidth           = rt.rect.width;
        afterActionReportSet = false;

        // Load target color, skybox and gamemode previews.
        LoadTargetColorThumbnails();
        LoadSkyboxThumbnails();

        // Init saved settings for settings panel.
        ExtraSaveSystem.InitSavedExtraSettings();
        CrosshairSaveSystem.InitSavedCrosshairSettings();
        WidgetSaveSystem.InitSavedWidgetSettings();
        StatsManager.HideExtraStatsPanel();
        SubMenuHandler.HideSettingsCrosshair();

        // Close settings and 'AfterActionReport' panels at start.
        settings.settingsPanel.transform.localScale = new Vector3(0f, 0f, 1f);
        settings.afterPanel.transform.localScale    = new Vector3(0f, 0f, 1f);
        settings.steamDataContainer.SetActive(false);

        //MovePanelCount_Left(7);
    }

    /// <summary>
    /// Shows settings panel if hidden, and hides settings panel if shown (toggle).
    /// </summary>
    public static void ToggleSettingsPanel() {
        if (settingsOpen) {
            CloseSettingsPanel();
        } else {
            OpenSettingsPanel();
        }

        settingsOpen = !settingsOpen;
    }

    /// <summary>
    /// Shows 'AfterActionReport' panel if hidden, and hides 'AfterActionReport' panel if shown (toggle).
    /// </summary>
    public static void ToggleAfterActionReportPanel() {
        if (afterActionReportOpen) {
            CloseAfterActionReport();
            GameUI.ShowWidgetsUI();
        } else {
            OpenAfterActionReport();
            GameUI.HideWidgetsUI();
        }

        afterActionReportOpen = !afterActionReportOpen;
    }

    /// <summary>
    /// Opens settings panel. [EVENT]
    /// </summary>
    public static void OpenSettingsPanel() {
        settings.settingsPanel.transform.localScale = new Vector3(1f, 1f, 1f);
        settings.steamDataContainer.SetActive(true);
        settings.mainMenuCanvas.SetActive(true);

        if (LanguageSelect.languageSelectOpen) { LanguageSelect.CloseLanguageSelect_Static(); }

        OpenAction();
        settingsOpen = true;
        SubMenuHandler.ResetAllScrollviewsTop();
        GameUI.HideWidgetsUI();
        GameUI.HideGameObject_Layer(settings.crosshairImage);

        if (SubMenuHandler.activeSubMenuText == "CrosshairTitleText (TMP)") { SubMenuHandler.ShowSettingsCrosshair(); }
        // EVENT:: for settings panel being opened
        //DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacesettingsopened")}");
    }

    /// <summary>
    /// Closes settings panel. [EVENT]
    /// </summary>
    public static void CloseSettingsPanel() {
        settings.settingsPanel.transform.localScale = new Vector3(0f, 0f, 1f);
        settings.steamDataContainer.SetActive(false);
        settings.mainMenuCanvas.SetActive(false);

        // If language select/notification object active, hide
        if (LanguageSelect.languageSelectOpen) { LanguageSelect.CloseLanguageSelect_Static(); }
        if (NotificationHandler.notificationOpen) { NotificationHandler.HideNotification(); }
        if (ExtraSettings.hideUI) { GameUI.ShowWidgetsUI(); }
        
        CrosshairOptionsObject.SaveCrosshairObject(false);
        GameUI.ShowGameObject_Layer(settings.crosshairImage);
        SubMenuHandler.HideSettingsCrosshair();

        CloseAction();
        settingsOpen = false;


        // EVENT:: for settings panel being closed
        //DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacesettingsclosed")}");
    }

    /// <summary>
    /// Opens 'AfterActionReport' panel. [EVENT]
    /// </summary>
    public static void OpenAfterActionReport() {
        settings.mainMenuCanvas.SetActive(true);
        if (ExtraSettings.showExtraStats) { settings.extendedStatsPanel.SetActive(true); }
        OpenAction();

        settings.afterPanel.transform.localScale = new Vector3(1f, 1f, 1f);
        afterActionReportOpen                    = true;

        StatsManager.ResetAARScrollView();
        GameUI.HideWidgetsUI();
        GameUI.HideGameObject_Layer(settings.crosshairImage);

        // EVENT:: for AAR panel being opened
        //DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfaceaaropened")}");
    }

    /// <summary>
    /// Closes 'AfterActionReport' panel. [EVENT]
    /// </summary>
    public static void CloseAfterActionReport() {
        //settings.darkenBackground.SetActive(false);
        settings.mainMenuCanvas.SetActive(false);
        settings.extendedStatsPanel.SetActive(false);
        CloseAction();

        settings.afterPanel.transform.localScale = new Vector3(0f, 0f, 1f);
        afterActionReportOpen                    = false;

        if (ExtraSettings.hideUI) { GameUI.ShowWidgetsUI(); }
        GameUI.ShowGameObject_Layer(settings.crosshairImage);

        // EVENT:: for AAR panel being opened
        //DevEventHandler.CheckInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfaceaarclosed")}");
    }

    /// <summary>
    /// Enables post process effects and unlocks cursor from game.
    /// </summary>
    private static void OpenAction() {
        MouseLook.settingsOpen = true;
        //TempValues.SetSettingsOpenTemp(true);

        ManipulatePostProcess.EnableEffects();
        UnlockCursor();
    }

    /// <summary>
    /// Disables post process effects and locks cursor to game.
    /// </summary>
    private static void CloseAction() {
        MouseLook.settingsOpen = false;
        //TempValues.SetSettingsOpenTemp(false);

        ManipulatePostProcess.DisableEffects();
        LockCursor();
    }

    /// <summary>
    /// Locks cursor if settings panel closed and game active.
    /// </summary>
    public static void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }
    /// <summary>
    /// Unlocks cursor if settings panel open and game inactive.
    /// </summary>
    public static void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;
    }

    /// <summary>
    /// Places settings panel to supplied location X.
    /// </summary>
    /// <param name="panelX"></param>
    public static void PlaceSettingsPanel(float panelX) {
        Vector3 panelPos                          = settings.settingsPanel.transform.position;
        panelPos.x                                = panelX;
        settings.settingsPanel.transform.position = panelPos;
    }

    /// <summary>
    /// Returns center of screen X.
    /// </summary>
    /// <returns></returns>
    public static float ReturnScreenCenter() { return Screen.width / 2; }

    /// <summary>
    /// Moves settings panel location right along X (not currently used, i think).
    /// </summary>
    public static Vector3 MoveSettingsPanelRight() {
        float currentPanelX = settings.settingsPanel.transform.position.x;
        float newPanelX     = (currentPanelX + panelMoveSize) + (panelWidth / 2);

        if (newPanelX < Screen.width) {
            Vector3 panelPos                          = settings.settingsPanel.transform.position;
            panelPos.x                                += panelMoveSize;
            settings.settingsPanel.transform.position = panelPos;

            return panelPos;
        } else {
            return settings.settingsPanel.transform.position;
        }
    }
    /// <summary>
    /// Moves settings panel location left along X (not currently used, i think).
    /// </summary>
    public static Vector3 MoveSettingsPanelLeft() {
        float currentPanelX = settings.settingsPanel.transform.position.x;
        float newPanelX     = (currentPanelX - panelMoveSize) - (panelWidth / 2);

        if (newPanelX > 0) {
            Vector3 panelPos                          = settings.settingsPanel.transform.position;
            panelPos.x                                -= panelMoveSize;
            settings.settingsPanel.transform.position = panelPos;

            return panelPos;
        } else {
            return settings.settingsPanel.transform.position;
        }
    }

    /// <summary>
    /// Moves settings panel left supplied number of times (count).
    /// </summary>
    /// <param name="count"></param>
    public static void MovePanelCount_Left(int count) { for (int i = 0; i < count; i++) { MoveSettingsPanelLeft(); } }
    /// <summary>
    /// Moves settings panel right supplied number of times (count).
    /// </summary>
    /// <param name="count"></param>
    public static void MovePanelCount_Right(int count) { for (int i = 0; i < count; i++) { MoveSettingsPanelRight(); } }

    /// <summary>
    /// Loads then plays all gamemode preview videos in their repsective buttons from the current gamemode, target color and skybox.
    /// </summary>
    public static void LoadGamemodePreviews() {
        gamemodePreviewVideos = VideoManager.PopulateGamemodePreviews(GamemodeUtil.ReturnGamemodeType_StringFull(CosmeticsSettings.gamemode), TargetUtil.ReturnTargetColorType_StringFull(CosmeticsSettings.targetColor), SkyboxUtil.ReturnSkyboxType_StringFull(CosmeticsSettings.skybox));

        // Set clips for every gamemode preview button.
        settings.scatterVideoPlayer.clip  = gamemodePreviewVideos[0];
        settings.flickVideoPlayer.clip    = gamemodePreviewVideos[1];
        settings.gridVideoPlayer.clip     = gamemodePreviewVideos[2];
        settings.grid2VideoPlayer.clip    = gamemodePreviewVideos[3];
        settings.pairsVideoPlayer.clip    = gamemodePreviewVideos[4];
        settings.followVideoPlayer.clip   = gamemodePreviewVideos[5];
        settings.selectedVideoPlayer.clip = gamemodePreviewVideos[6];
        // Set gamemode select clips from loaded previews
        GamemodeSelect.gamemodeScatterClip_Loaded = gamemodePreviewVideos[0];
        GamemodeSelect.gamemodeFlickClip_Loaded   = gamemodePreviewVideos[1];
        GamemodeSelect.gamemodeGridClip_Loaded    = gamemodePreviewVideos[2];
        GamemodeSelect.gamemodeGrid2Clip_Loaded   = gamemodePreviewVideos[3];
        GamemodeSelect.gamemodePairsClip_Loaded   = gamemodePreviewVideos[4];
        GamemodeSelect.gamemodeFollowClip_Loaded  = gamemodePreviewVideos[5];
        // Set video player aspect ratios.
        settings.scatterVideoPlayer.aspectRatio  = VideoAspectRatio.NoScaling;
        settings.flickVideoPlayer.aspectRatio    = VideoAspectRatio.NoScaling;
        settings.gridVideoPlayer.aspectRatio     = VideoAspectRatio.NoScaling;
        settings.grid2VideoPlayer.aspectRatio    = VideoAspectRatio.NoScaling;
        settings.pairsVideoPlayer.aspectRatio    = VideoAspectRatio.NoScaling;
        settings.followVideoPlayer.aspectRatio   = VideoAspectRatio.NoScaling;
        settings.selectedVideoPlayer.aspectRatio = VideoAspectRatio.FitVertically;
        // Play clips once set.
        settings.scatterVideoPlayer.Play();
        settings.flickVideoPlayer.Play();
        settings.gridVideoPlayer.Play();
        settings.grid2VideoPlayer.Play();
        settings.pairsVideoPlayer.Play();
        settings.followVideoPlayer.Play();
        settings.selectedVideoPlayer.Play();
    }

    /// <summary>
    /// Loads target color thumbnail sprites and sets them to corresponding buttons in settings panel (general sub-section).
    /// </summary>
    private static void LoadTargetColorThumbnails() {
        // Red target
        settings.canvasRed.transform.GetComponent<UnityEngine.UI.Image>().sprite    = settings.targetColorRedThumbnail;
        // Green target
        settings.canvasGreen.transform.GetComponent<UnityEngine.UI.Image>().sprite  = settings.targetColorGreenThumbnail;
        // Blue target
        settings.canvasBlue.transform.GetComponent<UnityEngine.UI.Image>().sprite   = settings.targetColorBlueThumbnail;
        // Yellow target
        settings.canvasYellow.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.targetColorYellowThumbnail;
        // Pink target
        settings.canvasPink.transform.GetComponent<UnityEngine.UI.Image>().sprite   = settings.targetColorPinkThumbnail;
        // White target
        settings.canvasWhite.transform.GetComponent<UnityEngine.UI.Image>().sprite  = settings.targetColorWhiteThumbnail;
        // Orange target
        settings.canvasOrange.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.targetColorOrangeThumbnail;
        // Purple target
        settings.canvasPurple.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.targetColorPurpleThumbnail;
    }

    /// <summary>
    /// Loads skybox thumbnail sprites and sets them to corresponding buttons in settings panel (general sub-section).
    /// </summary>
    private static void LoadSkyboxThumbnails() {
        // Pink skybox
        settings.skyboxCanvasPink.transform.GetComponent<UnityEngine.UI.Image>().sprite   = settings.skyboxPinkThumbnail;
        // Golden skybox
        settings.skyboxCanvasGolden.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.skyboxGoldenThumbnail;
        // Night skybox
        settings.skyboxCanvasNight.transform.GetComponent<UnityEngine.UI.Image>().sprite  = settings.skyboxNightThumbnail;
        // Grey skybox
        settings.skyboxCanvasGrey.transform.GetComponent<UnityEngine.UI.Image>().sprite   = settings.skyboxGreyThumbnail;
        // Blue skybox
        settings.skyboxCanvasStars.transform.GetComponent<UnityEngine.UI.Image>().sprite  = settings.skyboxBlueThumbnail;
        // Slate skybox
        settings.skyboxCanvasSlate.transform.GetComponent<UnityEngine.UI.Image>().sprite  = settings.skyboxSlateThumbnail;
    }
}
