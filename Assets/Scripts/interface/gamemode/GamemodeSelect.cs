using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class GamemodeSelect : MonoBehaviour {
    public GameObject gamemodeSelectObject;
    public TMP_Text gamemodeNameText, gamemodeTypeText, gamemodeDescriptionText;
    public VideoPlayer gamemodePreviewVideoRT;
    public TMP_Text gamemodeStartButtonText;

    private static string currentOpenGamemode = "";

    private static Color32 easyColor   = new Color32(0, 255, 0, 255);
    private static Color32 mediumColor = new Color32(255, 209, 0, 255);
    private static Color32 hardColor   = new Color32(255, 0, 0, 255);

    public VideoClip gamemodeScatterClip, gamemodeFlickClip, gamemodeGridClip, gamemodeGrid2Clip, gamemodePairsClip, gamemodeFollowClip;
    public static VideoClip gamemodeScatterClip_Loaded, gamemodeFlickClip_Loaded, gamemodeGridClip_Loaded, gamemodeGrid2Clip_Loaded, gamemodePairsClip_Loaded, gamemodeFollowClip_Loaded;

    public static GamemodeSelect gamemodeSelect;
    private void Awake() { gamemodeSelect = this; }

    /// <summary>
    /// Closes 'GamemodeSelect' panel.
    /// </summary>
    public static void OpenGamemodeSelect() { gamemodeSelect.gamemodeSelectObject.SetActive(true); }
    /// <summary>
    /// Opens 'GamemodeSelect' panel.
    /// </summary>
    public static void CloseGamemodeSelect() { gamemodeSelect.gamemodeSelectObject.SetActive(false); }

    /// <summary>
    /// Sets current selected gamemode, and calls 'PopulateAllGamemodeInfo()' to populate all 'GamemodeSelect' panel info.
    /// </summary>
    /// <param name="gamemodeName"></param>
    public static void PopulateGamemodeSelect(string gamemodeName, bool quickStart) {
        // Set current selected gamemode and its buttons hover border.
        currentOpenGamemode                 = gamemodeName;
        ButtonHoverHandler.selectedGamemode = gamemodeName;
        if (NotificationHandler.notificationOpen) { NotificationHandler.HideNotification(); }

        if (quickStart) {
            gamemodeSelect.GamemodeSelectStart();
        } else {
            //VideoClip selectedVideoClip = VideoManager.PopulateIndividualClip(gamemodeName, CosmeticsSettings.targetColor, CosmeticsSettings.skybox);

            switch (gamemodeName) {
                case "Gamemode-Scatter":
                    PopulateAllGamemodeInfo(gamemodeScatterClip_Loaded, "gamemodestartscatter", "gamemodecapsscatter", "gamemodetypespeed", easyColor, "gamemodescatterdescription");
                    break;
                case "Gamemode-Flick":
                    PopulateAllGamemodeInfo(gamemodeFlickClip_Loaded, "gamemodestartflick", "gamemodecapsflick", "gamemodetypecontrol", easyColor, "gamemodeflickdescription");
                    break;
                case "Gamemode-Grid":
                    PopulateAllGamemodeInfo(gamemodeGridClip_Loaded, "gamemodestartgrid", "gamemodecapsgrid", "gamemodetypespeed", easyColor, "gamemodegriddescription");
                    break;
                case "Gamemode-Grid2":
                    PopulateAllGamemodeInfo(gamemodeGrid2Clip_Loaded, "gamemodestartgrid2", "gamemodecapsgrid2", "gamemodetypecontrol", hardColor, "gamemodegrid2description");
                    break;
                case "Gamemode-Pairs":
                    PopulateAllGamemodeInfo(gamemodePairsClip_Loaded, "gamemodestartpairs", "gamemodecapspairs", "gamemodetypecontrol", mediumColor, "gamemodepairsdescription");
                    break;
                case "Gamemode-Follow":
                    PopulateAllGamemodeInfo(gamemodeFollowClip_Loaded, "gamemodestartfollow", "gamemodecapsfollow", "gamemodetypetracking", mediumColor, "gamemodefollowdescription");
                    break;
            }
        }
    }

    /// <summary>
    /// Populates gamemode video clip inside 'GamemodeSelect' in settings. (gamemode sub-section).
    /// </summary>
    /// <param name="gamemodeVideoClip"></param>
    private static void PopulateGamemodeVideoClip(VideoClip gamemodeVideoClip) {
        gamemodeSelect.gamemodePreviewVideoRT.clip = gamemodeVideoClip;
        if (!gamemodeSelect.gamemodePreviewVideoRT.isPlaying) { gamemodeSelect.gamemodePreviewVideoRT.Play(); }
    }
    /// <summary>
    /// Populates gamemode name text inside 'GamemodeSelect' in settings. (gamemode sub-section).
    /// </summary>
    /// <param name="gamemodeNameContent"></param>
    private static void PopulateGamemodeName_TranslatedText(string gamemodeNameContent) {
        gamemodeSelect.gamemodeNameText.SetText($"{I18nTextTranslator.SetTranslatedText(gamemodeNameContent)}");
    }
    /// <summary>
    /// Populates gamemode type text inside 'GamemodeSelect' in settings. (gamemode sub-section).
    /// </summary>
    /// <param name="gamemodeTypeContent"></param>
    /// <param name="typeColor"></param>
    private static void PopulateGamemodeType_TranslatedText(string gamemodeTypeContent, Color32 typeColor) {
        gamemodeSelect.gamemodeTypeText.SetText($"{I18nTextTranslator.SetTranslatedText(gamemodeTypeContent)}");
        gamemodeSelect.gamemodeTypeText.color = typeColor;
    }
    /// <summary>
    /// Populates gamemode name description inside 'GamemodeSelect' in settings. (gamemode sub-section).
    /// </summary>
    /// <param name="gamemodeDescriptionContent"></param>
    private static void PopulateGamemodeDescription_TranslatedText(string gamemodeDescriptionContent) {
        //gamemodeSelect.gamemodeDescriptionText.SetText($"{gamemodeDescriptionContent}");
        gamemodeSelect.gamemodeDescriptionText.SetText($"{I18nTextTranslator.SetTranslatedText(gamemodeDescriptionContent)}");
    }
    /// <summary>
    /// Populates gamemode startbutton text inside 'GamemodeSelect' in settings. (gamemode sub-section).
    /// </summary>
    /// <param name="gamemodeNameContent"></param>
    private static void PopulateGamemodeStartButton_TranslatedText(string gamemodeNameStartContent) {
        gamemodeSelect.gamemodeStartButtonText.SetText($"{I18nTextTranslator.SetTranslatedText(gamemodeNameStartContent)}");
    }
    /// <summary>
    /// Populates all gamemode info inside 'GamemodeSelect' in settings. (gamemode sub-section).
    /// </summary>
    /// <param name="gamemodeName"></param>
    /// <param name="gamemodeType"></param>
    /// <param name="gamemodeTypeColor"></param>
    /// <param name="gamemodeDescription"></param>
    private static void PopulateAllGamemodeInfo(VideoClip gamemodeVideoClip, string gamemodeNameStart, string gamemodeName, string gamemodeType, Color32 gamemodeTypeColor, string 
gamemodeDescription) {
        PopulateGamemodeName_TranslatedText(gamemodeName);
        PopulateGamemodeType_TranslatedText(gamemodeType, gamemodeTypeColor);
        PopulateGamemodeDescription_TranslatedText(gamemodeDescription);
        PopulateGamemodeStartButton_TranslatedText(gamemodeNameStart);
        PopulateGamemodeVideoClip(gamemodeVideoClip);
    }

    /// <summary>
    /// Starts new gamemode when gamemode start button in 'GamemodeSelect' panel clicked. Does nothing if current gamemode start clicked.
    /// </summary>
    public void GamemodeSelectStart() {
        if (SpawnTargets.gamemode != currentOpenGamemode) {
            if (currentOpenGamemode == "Gamemode-Follow" && CosmeticsSettings.targetColor == "TargetColor-Red") {
                NotificationHandler.ShowTimedNotification_Translated("followwarning", "", NotificationHandler.notificationColorRed);
                return;
            }
            //Debug.Log(currentOpenGamemode);
            SettingsPanel.ToggleSettingsPanel();
            //Debug.Log("before save");
            CosmeticsSettings.SaveGamemodeItem(currentOpenGamemode);
            //Debug.Log("after save, before start new gamemode");
            SpawnTargets.StartNewGamemode(currentOpenGamemode);
            //Debug.Log("after start new gamemode");
        } else {
            NotificationHandler.ShowTimedNotification_Translated($"gamemodecaps{currentOpenGamemode.Split('-')[1].ToLower()}", $": {I18nTextTranslator.SetTranslatedText("selectedgamemodewarning")}", NotificationHandler.notificationColorYellow);
        }
    }
}
