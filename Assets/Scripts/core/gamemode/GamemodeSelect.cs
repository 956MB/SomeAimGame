using UnityEngine;
using UnityEngine.Video;
using TMPro;

using SomeAimGame.Targets;
using SomeAimGame.Utilities;
using UnityEngine.UI;
using SomeAimGame.SFX;

namespace SomeAimGame.Gamemode {
    public class GamemodeSelect : MonoBehaviour {
        public GameObject gamemodeSelectObject, testContainerObject;
        public TMP_Text gamemodeNameText, gamemodeTypeText, gamemodeDescriptionText_TMP_Text, gamemodeStartButtonText;
        public Text gamemodeDescriptionText_Text;
        public TMP_Text gamemodeScatterText, gamemodeFlickText, gamemodeGridText, gamemodeGrid2Text, gamemodePairsText, gamemodeFollowText, gamemodeGlobText;
        private string scatterDescription, flickDescription, gridDescription, grid2Description, pairsDescription, followDescription, globDescription;
        public static TMP_Text selectedGamemodeText;
        public VideoPlayer gamemodePreviewVideoRT;

        private static GamemodeType currentOpenGamemode;

        public VideoClip gamemodeScatterClip, gamemodeFlickClip, gamemodeGridClip, gamemodeGrid2Clip, gamemodePairsClip, gamemodeFollowClip, gamemodeGlobClip;
        public static VideoClip gamemodeScatterClip_Loaded, gamemodeFlickClip_Loaded, gamemodeGridClip_Loaded, gamemodeGrid2Clip_Loaded, gamemodePairsClip_Loaded, gamemodeFollowClip_Loaded, gamemodeGlobClip_Loaded;

        public static GamemodeSelect gamemodeSelect;
        private void Awake() { gamemodeSelect = this; }

        private void Start() {
            LoadTranslatedDescriptions(I18nTextTranslator.SetTranslatedText("gamemodescatterdescription"), I18nTextTranslator.SetTranslatedText("gamemodeflickdescription"), I18nTextTranslator.SetTranslatedText("gamemodegriddescription"), I18nTextTranslator.SetTranslatedText("gamemodegrid2description"), I18nTextTranslator.SetTranslatedText("gamemodepairsdescription"), I18nTextTranslator.SetTranslatedText("gamemodefollowdescription"), I18nTextTranslator.SetTranslatedText("gamemodeglobdescription"));
        }

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
        public static void PopulateGamemodeSelectText(GamemodeType gamemodeName, bool quickStart) {
            // Set current selected gamemode and its buttons hover border.
            currentOpenGamemode                       = gamemodeName;
            ButtonHoverHandler.selectedGamemode       = gamemodeName;
            ButtonHoverHandler.selectedGamemodeString = GamemodeUtil.ReturnGamemodeType_StringFull(gamemodeName);
            NotificationHandler.CheckHideNotificationObject();

            if (quickStart) {
                gamemodeSelect.GamemodeSelectStart();
            } else {
                switch (gamemodeName) {
                    case GamemodeType.SCATTER: PopulateAllGamemodeText("gamemodestartscatter", "gamemodecapsscatter", "gamemodetypespeed", InterfaceColors.gamemodeEasyColor, gamemodeSelect.scatterDescription);   break;
                    case GamemodeType.FLICK:   PopulateAllGamemodeText("gamemodestartflick", "gamemodecapsflick", "gamemodetypecontrol", InterfaceColors.gamemodeEasyColor, gamemodeSelect.flickDescription);       break;
                    case GamemodeType.GRID:    PopulateAllGamemodeText("gamemodestartgrid", "gamemodecapsgrid", "gamemodetypespeed", InterfaceColors.gamemodeEasyColor, gamemodeSelect.gridDescription);            break;
                    case GamemodeType.GRID_2:  PopulateAllGamemodeText("gamemodestartgrid2", "gamemodecapsgrid2", "gamemodetypecontrol", InterfaceColors.gamemodeHardColor, gamemodeSelect.grid2Description);       break;
                    case GamemodeType.PAIRS:   PopulateAllGamemodeText("gamemodestartpairs", "gamemodecapspairs", "gamemodetypecontrol", InterfaceColors.gamemodeMediumColor, gamemodeSelect.pairsDescription);     break;
                    case GamemodeType.FOLLOW:  PopulateAllGamemodeText("gamemodestartfollow", "gamemodecapsfollow", "gamemodetypetracking", InterfaceColors.gamemodeMediumColor, gamemodeSelect.followDescription); break;
                    case GamemodeType.GLOB:    PopulateAllGamemodeText("gamemodestartglob", "gamemodecapsglob", "gamemodetypetracking", InterfaceColors.gamemodeMediumColor, gamemodeSelect.globDescription);       break;
                }
            }
        }

        public static void PopulateGamemodeSelectVideoClip(GamemodeType gamemodeName) {
            switch (gamemodeName) {
                case GamemodeType.SCATTER: PopulateGamemodeVideoClip(gamemodeScatterClip_Loaded); break;
                case GamemodeType.FLICK:   PopulateGamemodeVideoClip(gamemodeFlickClip_Loaded);   break;
                case GamemodeType.GRID:    PopulateGamemodeVideoClip(gamemodeGridClip_Loaded);    break;
                case GamemodeType.GRID_2:  PopulateGamemodeVideoClip(gamemodeGrid2Clip_Loaded);   break;
                case GamemodeType.PAIRS:   PopulateGamemodeVideoClip(gamemodePairsClip_Loaded);   break;
                case GamemodeType.FOLLOW:  PopulateGamemodeVideoClip(gamemodeFollowClip_Loaded);  break;
                case GamemodeType.GLOB:    PopulateGamemodeVideoClip(gamemodeGlobClip_Loaded);    break;
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
        private static void PopulateGamemodeName_Text(string gamemodeNameContent, bool translate = false) {
            string setText = translate ? I18nTextTranslator.SetTranslatedText(gamemodeNameContent) : gamemodeNameContent;

            gamemodeSelect.gamemodeNameText.SetText($"{setText}");
        }
        /// <summary>
        /// Populates gamemode type text inside 'GamemodeSelect' in settings. (gamemode sub-section).
        /// </summary>
        /// <param name="gamemodeTypeContent"></param>
        /// <param name="typeColor"></param>
        private static void PopulateGamemodeType_Text(string gamemodeTypeContent, Color32 typeColor, bool translate = false) {
            string setText = translate ? I18nTextTranslator.SetTranslatedText(gamemodeTypeContent) : gamemodeTypeContent;

            gamemodeSelect.gamemodeTypeText.SetText($"{setText}");
            gamemodeSelect.gamemodeTypeText.color = typeColor;
        }
        /// <summary>
        /// Populates gamemode name description inside 'GamemodeSelect' in settings. (gamemode sub-section).
        /// </summary>
        /// <param name="gamemodeDescriptionContent"></param>
        private static void PopulateGamemodeDescription_Text(string gamemodeDescriptionContent, bool translate = false) {
            string setText = translate ? I18nTextTranslator.SetTranslatedText(gamemodeDescriptionContent) : gamemodeDescriptionContent;
            //gamemodeSelect.gamemodeDescriptionText.SetText($"{gamemodeDescriptionContent}");
            gamemodeSelect.gamemodeDescriptionText_TMP_Text.SetText($"{setText}");
            //gamemodeSelect.gamemodeDescriptionText_Text.text = $"{setText}";
        }
        /// <summary>
        /// Populates gamemode startbutton text inside 'GamemodeSelect' in settings. (gamemode sub-section).
        /// </summary>
        /// <param name="gamemodeNameContent"></param>
        private static void PopulateGamemodeStartButton_Text(string gamemodeNameStartContent, bool translate = false) {
            string setText = translate ? I18nTextTranslator.SetTranslatedText(gamemodeNameStartContent) : gamemodeNameStartContent;

            gamemodeSelect.gamemodeStartButtonText.SetText($"{setText}");
        }
        /// <summary>
        /// Populates all gamemode info inside 'GamemodeSelect' in settings. (gamemode sub-section).
        /// </summary>
        /// <param name="gamemodeName"></param>
        /// <param name="gamemodeType"></param>
        /// <param name="gamemodeTypeColor"></param>
        /// <param name="gamemodeDescription"></param>
        private static void PopulateAllGamemodeText(string gamemodeNameStart, string gamemodeName, string gamemodeType, Color32 gamemodeTypeColor, string 
    gamemodeDescription) {
            PopulateGamemodeName_Text(gamemodeName, true);
            PopulateGamemodeType_Text(gamemodeType, gamemodeTypeColor, true);
            PopulateGamemodeDescription_Text(gamemodeDescription);
            PopulateGamemodeStartButton_Text(gamemodeNameStart, true);
        }

        public static void LoadTranslatedDescriptions(string setScatterDescription, string setFlickDescription, string setGridDescription, string setGrid2Description, string setPairsDescription, string setFollowDescription, string setGlobDescription) {
            gamemodeSelect.scatterDescription = setScatterDescription;
            gamemodeSelect.flickDescription   = setFlickDescription;
            gamemodeSelect.gridDescription    = setGridDescription;
            gamemodeSelect.grid2Description   = setGrid2Description;
            gamemodeSelect.pairsDescription   = setPairsDescription;
            gamemodeSelect.followDescription  = setFollowDescription;
            gamemodeSelect.globDescription    = setGlobDescription;
        }

        /// <summary>
        /// Starts new gamemode when gamemode start button in 'GamemodeSelect' panel clicked. Does nothing if current gamemode start clicked.
        /// </summary>
        public void GamemodeSelectStart() {
            //Debug.Log($"current gamemode: {SpawnTargets.gamemode}, new: {currentOpenGamemode}");
            if (SpawnTargets.gamemode != currentOpenGamemode) {
                if (!CheckFollowGamemode()) { return; }

                //SettingsPanel.ToggleSettingsPanel();
                CosmeticsSettings.SaveGamemodeItem(currentOpenGamemode);
                SpawnTargets.StartNewGamemode(currentOpenGamemode);
            } else {
                NotificationHandler.ShowTimedNotification_Translated($"gamemodecaps{GamemodeUtil.ReturnGamemodeType_StringShort(currentOpenGamemode).ToLower()}", $": {I18nTextTranslator.SetTranslatedText("selectedgamemodewarning")}", InterfaceColors.notificationColorYellow);

                SFXManager.CheckPlayError();
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
                    SFXManager.CheckPlayError();
                    return false;
                } else if (ExtraSettings.gameTimer == 0) {
                    NotificationHandler.ShowTimedNotification_Translated("followtimerwarning", "", InterfaceColors.notificationColorRed);
                    SFXManager.CheckPlayError();
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
                Util.GameObjectLoops.ClearTMPTextColor(InterfaceColors.hoveredColor, gamemodeSelect.gamemodeScatterText, gamemodeSelect.gamemodeFlickText, gamemodeSelect.gamemodeGridText, gamemodeSelect.gamemodeGrid2Text, gamemodeSelect.gamemodePairsText, gamemodeSelect.gamemodeFollowText, gamemodeSelect.gamemodeGlobText);
            }

            if (selected) { buttonText.color = InterfaceColors.selectedColor; } else { buttonText.color = InterfaceColors.hoveredColor; }
        }
    }
}
