//using Steamworks;
using UnityEngine;
using UnityEngine.Video;
//using UnityEditor;

using SomeAimGame.Skybox;
using SomeAimGame.Gamemode;
using SomeAimGame.Targets;
using SomeAimGame.Utilities;
using SomeAimGame.Stats;
using SomeAimGame.SFX;
using SomeAimGame.Core.Video;

public class SettingsPanel : MonoBehaviour {
    public GameObject mainMenuCanvas, settingsPanel, afterPanel, extendedStatsPanel, steamDataContainer, devEventContainer, videoContainer;
    public GameObject[] targetThumbnailObjects, skyboxThumbnailObjects;
    public Sprite[] targetThumbnailSprites, skyboxThumbnailSprites;

    public static bool settingsOpen          = false;
    public static bool afterActionReportOpen = false;
    public static bool afterActionReportSet;

    public static float panelMoveSize = 75f;
    public static float panelWidth;

    RectTransform rt;

    private static VideoClip[] gamemodePreviewVideos;
    public VideoPlayer selectedVideoPlayer, scatterVideoPlayer, flickVideoPlayer, gridVideoPlayer, grid2VideoPlayer, pairsVideoPlayer, followVideoPlayer, globVideoPlayer;

    private static Vector3 openVector   = new Vector3(1f, 1f, 1f);
    private static Vector3 closedVector = new Vector3(0f, 0f, 1f);

    private static SettingsPanel settings;
    void Awake() { settings = this; }

    void Start() {
        rt                   = (RectTransform)settingsPanel.transform;
        panelWidth           = rt.rect.width;
        afterActionReportSet = false;

        // Load target color and skybox thumbnails.
        SettingsPanelUtil.LoadThumbnails(targetThumbnailObjects, targetThumbnailSprites);
        SettingsPanelUtil.LoadThumbnails(skyboxThumbnailObjects, skyboxThumbnailSprites);

        // Init saved settings for settings panel.
        ExtraSaveSystem    .InitSavedExtraSettings();
        CrosshairSaveSystem.InitSavedCrosshairSettings();
        WidgetSaveSystem   .InitSavedWidgetSettings();
        StatsManager       .HideExtraStatsPanel();
        CrosshairHide      .HideCrosshairs();

        // Close settings, 'AfterActionReport' and video containers at start.
        Util.GameObjectLoops.Util_SetObjectsLocalScale(closedVector, settings.settingsPanel, settings.afterPanel, settings.videoContainer);
        settings.steamDataContainer.SetActive(false);

        //MovePanelCount_Left(7);
        //Debug.Log($"Fullscreen: {Screen.fullScreen}");
        //Debug.Log($"Current resolution: {Screen.currentResolution}");

        //foreach (Display dis in Display.displays) { Debug.Log($"Display: {dis}"); }
        //foreach (Resolution res in Screen.resolutions) { Debug.Log($"Resolutions: {res}"); }
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
        CrosshairHide.HideCrosshairs();

        settings.settingsPanel.transform.localScale = openVector;
        settings.steamDataContainer.SetActive(true);
        settings.mainMenuCanvas.SetActive(true);

        if (LanguageSelect.languageSelectOpen) { LanguageSelect.CloseLanguageSelect_Static(); }

        OpenAction();
        settingsOpen = true;

        SubMenuHandler.ResetAllSubMenuScrollviewsTop();
        GameUI        .HideWidgetsUI();
        GameTime      .PauseGameTime();

        if (SubMenuHandler.activeSubMenuText == "CrosshairTitleText (TMP)") { CrosshairHide.ShowSettingsCrosshair(); }
    }

    /// <summary>
    /// Closes settings panel. [EVENT]
    /// </summary>
    public static void CloseSettingsPanel() {
        settings.settingsPanel.transform.localScale = closedVector;
        settings.steamDataContainer.SetActive(false);
        settings.mainMenuCanvas.SetActive(false);
        //Util.CanvasGroupState(settings.mainMenuCanvas.GetComponent<CanvasGroup>(), false);

        // If language select/notification object active, hide
        if (LanguageSelect.languageSelectOpen) { LanguageSelect.CloseLanguageSelect_Static(); }
        if (ExtraSettings.hideUI) { GameUI.ShowWidgetsUI(); }
        
        CheckSaveSettings();
        CrosshairHide.HideCrosshairs();
        CrosshairHide.ShowMainCrosshair();

        CloseAction();
        settingsOpen = false;

        GameTime.ContinueGameTime();
    }

    /// <summary>
    /// Opens 'AfterActionReport' panel. [EVENT]
    /// </summary>
    public static void OpenAfterActionReport() {
        CrosshairHide.HideCrosshairs();

        settings.mainMenuCanvas.SetActive(true);
        //Util.CanvasGroupState(settings.mainMenuCanvas.GetComponent<CanvasGroup>(), true);
        if (ExtraSettings.showExtraStats) { settings.extendedStatsPanel.SetActive(true); }
        OpenAction();

        settings.afterPanel.transform.localScale = openVector;
        afterActionReportOpen                    = true;

        StatsManager.ResetAARScrollView();
        GameUI.HideWidgetsUI();
    }

    /// <summary>
    /// Closes 'AfterActionReport' panel. [EVENT]
    /// </summary>
    public static void CloseAfterActionReport() {
        settings.mainMenuCanvas.SetActive(false);
        settings.extendedStatsPanel.SetActive(false);
        CloseAction();

        settings.afterPanel.transform.localScale = closedVector;
        afterActionReportOpen                    = false;

        if (ExtraSettings.hideUI) { GameUI.ShowWidgetsUI(); }

        CrosshairHide.HideCrosshairs();
        CrosshairHide.ShowMainCrosshair();
    }

    /// <summary>
    /// Enables post process effects and unlocks cursor from game.
    /// </summary>
    private static void OpenAction() {
        MouseLook.settingsOpen = true;
        //TempValues.SetSettingsOpenTemp(true);

        ManipulatePostProcess.SetPanelEffects(true);
        Util.UnlockCursor();
    }

    /// <summary>
    /// Disables post process effects and locks cursor to game.
    /// </summary>
    private static void CloseAction() {
        MouseLook.settingsOpen = false;
        //TempValues.SetSettingsOpenTemp(false);

        ManipulatePostProcess.SetPanelEffects(false);
        Util.LockCursor();
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
        gamemodePreviewVideos = PreviewManager.PopulateGamemodePreviews(GamemodeUtil.ReturnGamemodeType_StringFull(CosmeticsSettings.gamemode), TargetUtil.ReturnTargetColorType_StringFull(CosmeticsSettings.targetColor), SkyboxUtil.ReturnSkyboxType_StringFull(CosmeticsSettings.skybox));

        // Set clips for every gamemode preview button.
        Util.VideoLoops.Util_SetVideoPlayerClips(8, gamemodePreviewVideos, settings.scatterVideoPlayer, settings.flickVideoPlayer, settings.gridVideoPlayer, settings.grid2VideoPlayer, settings.pairsVideoPlayer, settings.followVideoPlayer, settings.globVideoPlayer, settings.selectedVideoPlayer);
        // Set gamemode select clips from loaded previews
        GamemodeSelect.gamemodeScatterClip_Loaded = gamemodePreviewVideos[0];
        GamemodeSelect.gamemodeFlickClip_Loaded   = gamemodePreviewVideos[1];
        GamemodeSelect.gamemodeGridClip_Loaded    = gamemodePreviewVideos[2];
        GamemodeSelect.gamemodeGrid2Clip_Loaded   = gamemodePreviewVideos[3];
        GamemodeSelect.gamemodePairsClip_Loaded   = gamemodePreviewVideos[4];
        GamemodeSelect.gamemodeFollowClip_Loaded  = gamemodePreviewVideos[5];
        GamemodeSelect.gamemodeGlobClip_Loaded    = gamemodePreviewVideos[6];
        // Set video player aspect ratios.
        Util.VideoLoops.Util_SetVideoPlayersAscpectRatio(VideoAspectRatio.FitHorizontally, settings.scatterVideoPlayer, settings.flickVideoPlayer, settings.gridVideoPlayer, settings.grid2VideoPlayer, settings.pairsVideoPlayer, settings.followVideoPlayer, settings.globVideoPlayer);
        // Play clips once set.
        Util.VideoLoops.Util_PlayVideoPlayers(settings.scatterVideoPlayer, settings.flickVideoPlayer, settings.gridVideoPlayer, settings.grid2VideoPlayer, settings.pairsVideoPlayer, settings.followVideoPlayer, settings.globVideoPlayer, settings.selectedVideoPlayer);
        settings.selectedVideoPlayer.Play();
    }

    /// <summary>
    /// Calls all 'CheckSave' methods to save ready settings.
    /// </summary>
    public static void CheckSaveSettings() {
        CrosshairOptionsObject.SaveCrosshairObject(false);
        CosmeticsSettings     .CheckSaveCosmeticsSettings();
        ExtraSettings         .CheckSaveExtraSettings();
        WidgetSettings        .CheckSaveWidgetSettings();
        SFXSettings           .CheckSaveSFXSettings();
        KeybindSettings       .CheckSaveKeybindSettings();
        TargetSoundSelect     .CheckSaveTargetSoundSelection();
        FPSLimitSlider        .CheckSaveFPSLimit();
        NotificationHandler   .CheckHideNotificationObject();
    }
}
