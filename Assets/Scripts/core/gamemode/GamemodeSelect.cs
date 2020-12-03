using UnityEngine;
using UnityEngine.Video;
using TMPro;

using SomeAimGame.Targets;
using SomeAimGame.Utilities;

namespace SomeAimGame.Gamemode {

    public class GamemodeSelect : MonoBehaviour {
        public GameObject gamemodeSelectObject;
        public TMP_Text gamemodeNameText, gamemodeTypeText, gamemodeDescriptionText, gamemodeStartButtonText;
        public TMP_Text gamemodeScatterText, gamemodeFlickText, gamemodeGridText, gamemodeGrid2Text, gamemodePairsText, gamemodeFollowText;
        public static TMP_Text selectedGamemodeText;
        public VideoPlayer gamemodePreviewVideoRT;

        private static GamemodeType currentOpenGamemode;

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
        public static void PopulateGamemodeSelect(GamemodeType gamemodeName, bool quickStart) {
            // Set current selected gamemode and its buttons hover border.
            currentOpenGamemode                       = gamemodeName;
            ButtonHoverHandler.selectedGamemode       = gamemodeName;
            ButtonHoverHandler.selectedGamemodeString = GamemodeUtil.ReturnGamemodeType_StringFull(gamemodeName);
            if (NotificationHandler.notificationOpen) { NotificationHandler.HideNotification(); }

            if (quickStart) {
                gamemodeSelect.GamemodeSelectStart();
            } else {
                switch (gamemodeName) {
                    case GamemodeType.SCATTER:
                        PopulateAllGamemodeInfo(gamemodeScatterClip_Loaded, "gamemodestartscatter", "gamemodecapsscatter", "gamemodetypespeed", InterfaceColors.gamemodeEasyColor, "gamemodescatterdescription");
                        break;
                    case GamemodeType.FLICK:
                        PopulateAllGamemodeInfo(gamemodeFlickClip_Loaded, "gamemodestartflick", "gamemodecapsflick", "gamemodetypecontrol", InterfaceColors.gamemodeEasyColor, "gamemodeflickdescription");
                        break;
                    case GamemodeType.GRID:
                        PopulateAllGamemodeInfo(gamemodeGridClip_Loaded, "gamemodestartgrid", "gamemodecapsgrid", "gamemodetypespeed", InterfaceColors.gamemodeEasyColor, "gamemodegriddescription");
                        break;
                    case GamemodeType.GRID_2:
                        PopulateAllGamemodeInfo(gamemodeGrid2Clip_Loaded, "gamemodestartgrid2", "gamemodecapsgrid2", "gamemodetypecontrol", InterfaceColors.gamemodeHardColor, "gamemodegrid2description");
                        break;
                    case GamemodeType.PAIRS:
                        PopulateAllGamemodeInfo(gamemodePairsClip_Loaded, "gamemodestartpairs", "gamemodecapspairs", "gamemodetypecontrol", InterfaceColors.gamemodeMediumColor, "gamemodepairsdescription");
                        break;
                    case GamemodeType.FOLLOW:
                        PopulateAllGamemodeInfo(gamemodeFollowClip_Loaded, "gamemodestartfollow", "gamemodecapsfollow", "gamemodetypetracking", InterfaceColors.gamemodeMediumColor, "gamemodefollowdescription");
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
                if (!CheckFollowGamemode()) { return; }

                //SettingsPanel.ToggleSettingsPanel();
                CosmeticsSettings.SaveGamemodeItem(currentOpenGamemode);
                SpawnTargets.StartNewGamemode(currentOpenGamemode);
            } else {
                NotificationHandler.ShowTimedNotification_Translated($"gamemodecaps{GamemodeUtil.ReturnGamemodeType_StringShort(currentOpenGamemode).ToLower()}", $": {I18nTextTranslator.SetTranslatedText("selectedgamemodewarning")}", InterfaceColors.notificationColorYellow);

                if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Error(); }
            }
        }

        /// <summary>
        /// If new gamemode being started is 'Follow', target color must not be red, and game timer must not be 0 (infinity).
        /// </summary>
        /// <returns></returns>
        private static bool CheckFollowGamemode() {
            if (currentOpenGamemode == GamemodeType.FOLLOW) {
                if (CosmeticsSettings.targetColor == TargetType.RED) {
                    NotificationHandler.ShowTimedNotification_Translated("followcolorwarning", "", InterfaceColors.notificationColorRed);
                    if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Error(); }
                    return false;
                } else if (ExtraSettings.gameTimer == 0) {
                    NotificationHandler.ShowTimedNotification_Translated("followtimerwarning", "", InterfaceColors.notificationColorRed);
                    if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Error(); }
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Clears all gamemode button text colors, then sets selected text color.
        /// </summary>
        /// <param name="buttonText"></param>
        /// <param name="selected"></param>
        /// <param name="clearColors"></param>
        public static void ClearGamemodeButtonColors(TMP_Text buttonText, bool selected, bool clearColors) {
            if (clearColors) {
                Util.GameObjectLoops.Util_ClearTMPTextColor(InterfaceColors.hoveredColor, gamemodeSelect.gamemodeScatterText, gamemodeSelect.gamemodeFlickText, gamemodeSelect.gamemodeGridText, gamemodeSelect.gamemodeGrid2Text, gamemodeSelect.gamemodePairsText, gamemodeSelect.gamemodeFollowText);
            }

            if (selected) { buttonText.color = InterfaceColors.selectedColor; } else { buttonText.color = InterfaceColors.hoveredColor; }
        }
    }
}
