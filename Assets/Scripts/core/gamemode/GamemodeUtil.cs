using UnityEngine;
using UnityEngine.UI;

using SomeAimGame.Utilities;

namespace SomeAimGame.Gamemode {
    public class GamemodeUtil : MonoBehaviour {
        private static string[] gamemodeLongStrings  = { "Gamemode-Scatter", "Gamemode-Flick", "Gamemode-Grid", "Gamemode-Grid2", "Gamemode-Grid3", "Gamemode-Pairs", "Gamemode-Follow", "Gamemode-Glob" };
        private static string[] gamemodeShortStrings = { "Scatter", "Flick", "Grid", "Grid2", "Grid3", "Pairs", "Follow", "Glob" };

        /// <summary>
        /// Returns corresponding gamemode type (Gamemode) from supplied string (gamemodeTypeString).
        /// </summary>
        /// <param name="gamemodeTypeString"></param>
        /// <returns></returns>
        public static GamemodeType ReturnGamemodeType_Gamemode(string gamemodeTypeString) {
            switch (gamemodeTypeString) {
                case "Gamemode-Scatter": return GamemodeType.SCATTER;
                case "Gamemode-Flick":   return GamemodeType.FLICK;
                case "Gamemode-Grid":    return GamemodeType.GRID;
                case "Gamemode-Grid2":   return GamemodeType.GRID_2;
                case "Gamemode-Grid3":   return GamemodeType.GRID_3;
                case "Gamemode-Pairs":   return GamemodeType.PAIRS;
                case "Gamemode-Follow":  return GamemodeType.FOLLOW;
                case "Gamemode-Glob":    return GamemodeType.GLOB;
                default:                 return GamemodeType.GRID;
            }
        }

        /// <summary>
        /// Returns corresponding full gamemode type string from supplied Gamemode (typeGamemode).
        /// </summary>
        /// <param name="typeGamemode"></param>
        /// <returns></returns>
        public static string ReturnGamemodeType_StringFull(GamemodeType typeGamemode) {
            return gamemodeLongStrings[(int)typeGamemode];
        }

        /// <summary>
        /// Returns corresponding short gamemode type string from supplied Gamemode (typeGamemode).
        /// </summary>
        /// <param name="typeGamemode"></param>
        /// <returns></returns>
        public static string ReturnGamemodeType_StringShort(GamemodeType typeGamemode) {
            return gamemodeShortStrings[(int)typeGamemode];
        }

        /// <summary>
        /// Clears all gamemode button borders in settings panel (gamemode sub-section).
        /// </summary>
        public static void ClearGamemodeButtonBorders() {
            foreach (GameObject buttonBorder in GameObject.FindGameObjectsWithTag("ButtonBorderGamemode")) {
                if (buttonBorder != null) {
                    buttonBorder.GetComponent<Image>().color = InterfaceColors.unselectedColor;
                    buttonBorder.SetActive(false);
                }
            }
        }
    }
}