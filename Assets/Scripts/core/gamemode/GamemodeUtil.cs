using UnityEngine;

namespace SomeAimGame.Gamemode {

    public class GamemodeUtil : MonoBehaviour {
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
                default:                 return GamemodeType.GRID;
            }
        }

        /// <summary>
        /// Returns corresponding full gamemode type string from supplied Gamemode (typeGamemode).
        /// </summary>
        /// <param name="typeGamemode"></param>
        /// <returns></returns>
        public static string ReturnGamemodeType_StringFull(GamemodeType typeGamemode) {
            switch (typeGamemode) {
                case GamemodeType.SCATTER: return "Gamemode-Scatter";
                case GamemodeType.FLICK:   return "Gamemode-Flick";
                case GamemodeType.GRID:    return "Gamemode-Grid";
                case GamemodeType.GRID_2:  return "Gamemode-Grid2";
                case GamemodeType.GRID_3:  return "Gamemode-Grid3";
                case GamemodeType.PAIRS:   return "Gamemode-Pairs";
                case GamemodeType.FOLLOW:  return "Gamemode-Follow";
                default:                   return "Gamemode-Scatter";
            }
        }

        /// <summary>
        /// Returns corresponding short gamemode type string from supplied Gamemode (typeGamemode).
        /// </summary>
        /// <param name="typeGamemode"></param>
        /// <returns></returns>
        public static string ReturnGamemodeType_StringShort(GamemodeType typeGamemode) {
            switch (typeGamemode) {
                case GamemodeType.SCATTER: return "Scatter";
                case GamemodeType.FLICK:   return "Flick";
                case GamemodeType.GRID:    return "Grid";
                case GamemodeType.GRID_2:  return "Grid2";
                case GamemodeType.GRID_3:  return "Grid3";
                case GamemodeType.PAIRS:   return "Pairs";
                case GamemodeType.FOLLOW:  return "Follow";
                default:                   return "Scatter";
            }
        }
    }
}