//using Steamworks;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Video;
//using UnityEditor;

public class SettingsPanel : MonoBehaviour {
    public GameObject mainMenuCanvas, settingsPanel, afterPanel, extendedStatsPanel;
    public static bool settingsOpen = false;
    public static bool afterActionReportOpen = false;
    public static bool afterActionReportSet;
    public GameObject targetColorOrange, targetColorPurple;
    public Sprite targetColorRedThumbnail, targetColorOrangeThumbnail, targetColorYellowThumbnail, targetColorGreenThumbnail, targetColorBlueThumbnail, targetColorPurpleThumbnail, targetColorPinkThumbnail, targetColorWhiteThumbnail;
    public GameObject canvasRed, canvasOrange, canvasYellow, canvasGreen, canvasBlue, canvasPurple, canvasPink, canvasWhite;
    public Sprite skyboxPinkThumbnail, skyboxGoldenThumbnail, skyboxNightThumbnail, skyboxGreyThumbnail, skyboxBlueThumbnail, skyboxSlateThumbnail;
    public GameObject skyboxCanvasPink, skyboxCanvasGolden, skyboxCanvasNight, skyboxCanvasGrey, skyboxCanvasStars, skyboxCanvasSlate;
    public static float panelMoveSize = 88.6f;
    public static float panelWidth;
    RectTransform rt;

    private static GamemodePreviews gamemodePreviewVideos;
    public VideoPlayer selectedVideoPlayer, scatterVideoPlayer, flickVideoPlayer, gridVideoPlayer, grid2VideoPlayer, pairsVideoPlayer, followVideoPlayer;
    //private static Texture2D thumbnailTexture2D;
    //private static Sprite thumbnailSprite;

    private static SettingsPanel settings;
    void Awake() { settings = this; }

    void Start() {
        rt = (RectTransform)settingsPanel.transform;
        panelWidth = rt.rect.width;
        afterActionReportSet = false;
        //LeanTween.moveX(settings.settingsPanel, -280, 0f);

        // Load target color, skybox and gamemode previews.
        LoadTargetColorThumbnails();
        LoadSkyboxThumbnails();
        //LoadGamemodePreviews();

        // Init saved settings for settings panel.
        ExtraSaveSystem.InitSavedExtraSettings();
        StatsManager.HideExtraStatsPanel();
        CrosshairSaveSystem.InitSavedCrosshairSettings();

        // Close settings and 'AfterActionReport' panels at start.
        settings.settingsPanel.transform.localScale = new Vector3(0f, 0f, 0f);
        settings.afterPanel.transform.localScale = new Vector3(0f, 0f, 0f);
        
        //CosmeticsSaveSystem.initSettingsDefaults();
        //settingsPanel.SetActive(false);
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
            GameUI.ShowUI();
        } else {
            OpenAfterActionReport();
            GameUI.HideUI();
        }

        afterActionReportOpen = !afterActionReportOpen;
    }

    /// <summary>
    /// Opens settings panel.
    /// </summary>
    public static void OpenSettingsPanel() {
        //settings.darkenBackground.SetActive(true);
        settings.mainMenuCanvas.SetActive(true);
        OpenAction();
        settings.settingsPanel.transform.localScale = new Vector3(1.15f, 1.1f, 1f);
        settingsOpen = true;
        GameUI.HideUI();
    }

    /// <summary>
    /// Closes settings panel.
    /// </summary>
    public static void CloseSettingsPanel() {
        //settings.darkenBackground.SetActive(false);
        settings.mainMenuCanvas.SetActive(false);
        CloseAction();
        settings.settingsPanel.transform.localScale = new Vector3(0f, 0f, 0f);
        settingsOpen = false;

        if (ExtraSettings.hideUI) { GameUI.ShowUI(); }
    }

    /// <summary>
    /// Opens 'AfterActionReport' panel.
    /// </summary>
    public static void OpenAfterActionReport() {
        //settings.darkenBackground.SetActive(true);
        settings.mainMenuCanvas.SetActive(true);
        if (ExtraSettings.showExtraStats) {
            settings.extendedStatsPanel.SetActive(true);
        }
        OpenAction();
        settings.afterPanel.transform.localScale = new Vector3(1.15f, 1.1f, 1f);
        afterActionReportOpen = true;
        GameUI.HideUI();
    }

    /// <summary>
    /// Closes 'AfterActionReport' panel.
    /// </summary>
    public static void CloseAfterActionReport() {
        //settings.darkenBackground.SetActive(false);
        settings.mainMenuCanvas.SetActive(false);
        settings.extendedStatsPanel.SetActive(false);
        CloseAction();
        settings.afterPanel.transform.localScale = new Vector3(0f, 0f, 0f);
        afterActionReportOpen = false;

        if (ExtraSettings.hideUI) { GameUI.ShowUI(); }
    }

    /// <summary>
    /// Enables post process effects and unlocks cursor from game.
    /// </summary>
    private static void OpenAction() {
        MouseLook.settingsOpen = true;
        ManipulatePostProcess.EnableEffects();
        UnlockCursor();
    }

    /// <summary>
    /// Disables post process effects and locks cursor to game.
    /// </summary>
    private static void CloseAction() {
        MouseLook.settingsOpen = false;
        ManipulatePostProcess.DisableEffects();
        LockCursor();
    }

    /// <summary>
    /// Locks cursor if settings panel closed and game active.
    /// </summary>
    public static void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    /// <summary>
    /// Unlocks cursor if settings panel open and game inactive.
    /// </summary>
    public static void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Places settings panel to supplied location X.
    /// </summary>
    /// <param name="panelX"></param>
    public static void PlaceSettingsPanel(float panelX) {
        Vector3 panelPos = settings.settingsPanel.transform.position;
        panelPos.x = panelX;
        settings.settingsPanel.transform.position = panelPos;
        //Debug.Log("current panel x : " + settings.settingsPanel.transform.position.x);
    }

    /// <summary>
    /// Returns center of screen X.
    /// </summary>
    /// <returns></returns>
    public static float ReturnScreenCenter() { return Screen.width / 2; }

    /// <summary>
    /// Moves settings panel location right along X (not currently used, i think).
    /// </summary>
    public static void MoveSettingsPanelRight() {
        float currentPanelX = settings.settingsPanel.transform.position.x;
        float newPanelX = (currentPanelX + panelMoveSize) + (panelWidth / 2);

        if (newPanelX < Screen.width) {
            Vector3 panelPos = settings.settingsPanel.transform.position;
            panelPos.x += panelMoveSize;
            settings.settingsPanel.transform.position = panelPos;

            //CosmeticsSettings.saveSettingsPanelItem(settings.settingsPanel.transform.position.x);
        }
    }
    /// <summary>
    /// Moves settings panel location left along X (not currently used, i think).
    /// </summary>
    public static void MoveSettingsPanelLeft() {
        float currentPanelX = settings.settingsPanel.transform.position.x;
        float newPanelX = (currentPanelX - panelMoveSize) - (panelWidth / 2);

        if (newPanelX > 0) {
            Vector3 panelPos = settings.settingsPanel.transform.position;
            panelPos.x -= panelMoveSize;
            settings.settingsPanel.transform.position = panelPos;

            //CosmeticsSettings.saveSettingsPanelItem(settings.settingsPanel.transform.position.x);
        }
    }

    /// <summary>
    /// Loads then plays all gamemode preview videos in their repsective buttons from the current gamemode, target color and skybox.
    /// </summary>
    public static void LoadGamemodePreviews() {
        gamemodePreviewVideos = VideoManager.PopulateGamemodePreviews(CosmeticsSettings.gamemode, CosmeticsSettings.targetColor, CosmeticsSettings.skybox);

        // Set clips for every gamemode preview button.
        settings.scatterVideoPlayer.clip  = gamemodePreviewVideos.gamemodeScatterPreview;
        settings.flickVideoPlayer.clip    = gamemodePreviewVideos.gamemodeFlickPreview;
        settings.gridVideoPlayer.clip     = gamemodePreviewVideos.gamemodeGridPreview;
        settings.grid2VideoPlayer.clip    = gamemodePreviewVideos.gamemodeGrid2Preview;
        settings.pairsVideoPlayer.clip    = gamemodePreviewVideos.gamemodePairsPreview;
        settings.followVideoPlayer.clip   = gamemodePreviewVideos.gamemodeFollowPreview;
        settings.selectedVideoPlayer.clip = gamemodePreviewVideos.gamemodeSelectedPreview;
        // Set video player aspect ratios.
        settings.scatterVideoPlayer.aspectRatio  = VideoAspectRatio.FitVertically;
        settings.flickVideoPlayer.aspectRatio    = VideoAspectRatio.FitVertically;
        settings.gridVideoPlayer.aspectRatio     = VideoAspectRatio.FitVertically;
        settings.grid2VideoPlayer.aspectRatio    = VideoAspectRatio.FitVertically;
        settings.pairsVideoPlayer.aspectRatio    = VideoAspectRatio.FitVertically;
        settings.followVideoPlayer.aspectRatio   = VideoAspectRatio.FitVertically;
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
        // Load from asset preview (backup).
        //thumbnailTexture2D = AssetPreview.GetAssetPreview(settings.targetColorRed);
        //thumbnailSprite = Sprite.Create(thumbnailTexture2D, new Rect(0.0f, 0.0f, thumbnailTexture2D.width, thumbnailTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
        //settings.canvasOrange.transform.GetComponent<UnityEngine.UI.Image>().sprite = thumbnailSprite;

        // Red target
        settings.canvasRed.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.targetColorRedThumbnail;
        // Green target
        settings.canvasGreen.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.targetColorGreenThumbnail;
        // Blue target
        settings.canvasBlue.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.targetColorBlueThumbnail;
        // Yellow target
        settings.canvasYellow.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.targetColorYellowThumbnail;
        // Pink target
        settings.canvasPink.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.targetColorPinkThumbnail;
        // White target
        settings.canvasWhite.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.targetColorWhiteThumbnail;
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
        settings.skyboxCanvasPink.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.skyboxPinkThumbnail;
        // Golden skybox
        settings.skyboxCanvasGolden.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.skyboxGoldenThumbnail;
        // Night skybox
        settings.skyboxCanvasNight.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.skyboxNightThumbnail;
        // Grey skybox
        settings.skyboxCanvasGrey.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.skyboxGreyThumbnail;
        // Blue skybox
        settings.skyboxCanvasStars.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.skyboxBlueThumbnail;
        // Slate skybox
        settings.skyboxCanvasSlate.transform.GetComponent<UnityEngine.UI.Image>().sprite = settings.skyboxSlateThumbnail;
    }
}
