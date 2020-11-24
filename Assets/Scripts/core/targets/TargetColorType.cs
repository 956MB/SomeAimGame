using UnityEngine;

/// <summary>
/// Enum holding all target color types.
/// </summary>
public enum TargetColor {
    RED,
    ORANGE,
    YELLOW,
    GREEN,
    BLUE,
    PURPLE,
    PINK,
    WHITE
}

public class TargetColorType : MonoBehaviour {
    /// <summary>
    /// Returns corresponding target color type (TargetColor) from supplied string (targetColorTypeString).
    /// </summary>
    /// <param name="targetColorTypeString"></param>
    /// <returns></returns>
    public static TargetColor ReturnTargetColorType_TargetColor(string targetColorTypeString) {
        switch (targetColorTypeString) {
            case "TargetColor-Red":    return TargetColor.RED;
            case "TargetColor-Orange": return TargetColor.ORANGE;
            case "TargetColor-Yellow": return TargetColor.YELLOW;
            case "TargetColor-Green":  return TargetColor.GREEN;
            case "TargetColor-Blue":   return TargetColor.BLUE;
            case "TargetColor-Purple": return TargetColor.PURPLE;
            case "TargetColor-Pink":   return TargetColor.PINK;
            case "TargetColor-White":  return TargetColor.WHITE;
            default:                   return TargetColor.YELLOW;
        }
    }

    /// <summary>
    /// Returns corresponding full target color type string from supplied TargetColor (typeTargetColor).
    /// </summary>
    /// <param name="typeTargetColor"></param>
    /// <returns></returns>
    public static string ReturnTargetColorType_StringFull(TargetColor typeTargetColor) {
        switch (typeTargetColor) {
            case TargetColor.RED:    return "TargetColor-Red";
            case TargetColor.ORANGE: return "TargetColor-Orange";
            case TargetColor.YELLOW: return "TargetColor-Yellow";
            case TargetColor.GREEN:  return "TargetColor-Green";
            case TargetColor.BLUE:   return "TargetColor-Blue";
            case TargetColor.PURPLE: return "TargetColor-Purple";
            case TargetColor.PINK:   return "TargetColor-Pink";
            case TargetColor.WHITE:  return "TargetColor-White";
            default:                 return "TargetColor-Yellow";
        }
    }

    /// <summary>
    /// Returns corresponding short target color type string from supplied TargetColor (typeTargetColor).
    /// </summary>
    /// <param name="typeTargetColor"></param>
    /// <returns></returns>
    public static string ReturnTargetColorType_StringShort(TargetColor typeTargetColor) {
        switch (typeTargetColor) {
            case TargetColor.RED:    return "Red";
            case TargetColor.ORANGE: return "Orange";
            case TargetColor.YELLOW: return "Yellow";
            case TargetColor.GREEN:  return "Green";
            case TargetColor.BLUE:   return "Blue";
            case TargetColor.PURPLE: return "Purple";
            case TargetColor.PINK:   return "Pink";
            case TargetColor.WHITE:  return "White";
            default:                 return "Yellow";
        }
    }
}