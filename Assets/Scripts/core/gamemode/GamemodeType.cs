using UnityEngine;

/// <summary>
/// Enum holding all gamemode types.
/// </summary>
public enum Gamemode {
    Scatter = 0,
    Flick   = 1,
    Grid    = 2,
    Grid2   = 3,
    Grid3   = 4,
    Pairs   = 5,
    Follow  = 6
}

public class GamemodeType : MonoBehaviour {
    /// <summary>
    /// Returns corresponding gamemode type (Gamemode) from supplied string (gamemodeTypeString).
    /// </summary>
    /// <param name="gamemodeTypeString"></param>
    /// <returns></returns>
    public static Gamemode ReturnGamemodeType_Gamemode(string gamemodeTypeString) {
        switch (gamemodeTypeString) {
            case "Gamemode-Scatter": return Gamemode.Scatter;
            case "Gamemode-Flick":   return Gamemode.Flick;
            case "Gamemode-Grid":    return Gamemode.Grid;
            case "Gamemode-Grid2":   return Gamemode.Grid2;
            case "Gamemode-Grid3":   return Gamemode.Grid3;
            case "Gamemode-Pairs":   return Gamemode.Pairs;
            case "Gamemode-Follow":  return Gamemode.Follow;
            default:                 return Gamemode.Grid;
        }
    }

    /// <summary>
    /// Returns corresponding full gamemode type string from supplied Gamemode (typeGamemode).
    /// </summary>
    /// <param name="typeGamemode"></param>
    /// <returns></returns>
    public static string ReturnGamemodeType_StringFull(Gamemode typeGamemode) {
        switch (typeGamemode) {
            case Gamemode.Scatter: return "Gamemode-Scatter";
            case Gamemode.Flick:   return "Gamemode-Flick";
            case Gamemode.Grid:    return "Gamemode-Grid";
            case Gamemode.Grid2:   return "Gamemode-Grid2";
            case Gamemode.Grid3:   return "Gamemode-Grid3";
            case Gamemode.Pairs:   return "Gamemode-Pairs";
            case Gamemode.Follow:  return "Gamemode-Follow";
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
            case Gamemode.Scatter: return "Scatter";
            case Gamemode.Flick:   return "Flick";
            case Gamemode.Grid:    return "Grid";
            case Gamemode.Grid2:   return "Grid2";
            case Gamemode.Grid3:   return "Grid3";
            case Gamemode.Pairs:   return "Pairs";
            case Gamemode.Follow:  return "Follow";
            default:               return "Scatter";
        }
    }
}