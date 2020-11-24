using UnityEngine;

/// <summary>
/// Enum holding all gamemode types.
/// </summary>
public enum Gamemode {
    SCATTER,
    FLICK,
    GRID,
    GRID_2,
    GRID_3,
    PAIRS,
    FOLLOW
}

public class GamemodeType : MonoBehaviour {
    /// <summary>
    /// Returns corresponding gamemode type (Gamemode) from supplied string (gamemodeTypeString).
    /// </summary>
    /// <param name="gamemodeTypeString"></param>
    /// <returns></returns>
    public static Gamemode ReturnGamemodeType_Gamemode(string gamemodeTypeString) {
        switch (gamemodeTypeString) {
            case "Gamemode-Scatter": return Gamemode.SCATTER;
            case "Gamemode-Flick":   return Gamemode.FLICK;
            case "Gamemode-Grid":    return Gamemode.GRID;
            case "Gamemode-Grid2":   return Gamemode.GRID_2;
            case "Gamemode-Grid3":   return Gamemode.GRID_3;
            case "Gamemode-Pairs":   return Gamemode.PAIRS;
            case "Gamemode-Follow":  return Gamemode.FOLLOW;
            default:                 return Gamemode.GRID;
        }
    }

    /// <summary>
    /// Returns corresponding full gamemode type string from supplied Gamemode (typeGamemode).
    /// </summary>
    /// <param name="typeGamemode"></param>
    /// <returns></returns>
    public static string ReturnGamemodeType_StringFull(Gamemode typeGamemode) {
        switch (typeGamemode) {
            case Gamemode.SCATTER: return "Gamemode-Scatter";
            case Gamemode.FLICK:   return "Gamemode-Flick";
            case Gamemode.GRID:    return "Gamemode-Grid";
            case Gamemode.GRID_2:  return "Gamemode-Grid2";
            case Gamemode.GRID_3:  return "Gamemode-Grid3";
            case Gamemode.PAIRS:   return "Gamemode-Pairs";
            case Gamemode.FOLLOW:  return "Gamemode-Follow";
            default:               return "Gamemode-Scatter";
        }
    }

    /// <summary>
    /// Returns corresponding short gamemode type string from supplied Gamemode (typeGamemode).
    /// </summary>
    /// <param name="typeGamemode"></param>
    /// <returns></returns>
    public static string ReturnGamemodeType_StringShort(Gamemode typeGamemode) {
        switch (typeGamemode) {
            case Gamemode.SCATTER: return "Scatter";
            case Gamemode.FLICK:   return "Flick";
            case Gamemode.GRID:    return "Grid";
            case Gamemode.GRID_2:  return "Grid2";
            case Gamemode.GRID_3:  return "Grid3";
            case Gamemode.PAIRS:   return "Pairs";
            case Gamemode.FOLLOW:  return "Follow";
            default:               return "Scatter";
        }
    }
}