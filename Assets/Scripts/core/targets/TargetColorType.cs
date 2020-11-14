using UnityEngine;

/// <summary>
/// Enum holding all target color types.
/// </summary>
public enum TargetColor {
    Red    = 0,
    Orange = 1,
    Yellow = 2,
    Green  = 3,
    Blue   = 4,
    Purple = 5,
    Pink   = 6,
    White  = 7
}

public class TargetColorType : MonoBehaviour {
    /// <summary>
    /// Returns corresponding target color type (TargetColor) from supplied string (targetColorTypeString).
    /// </summary>
    /// <param name="targetColorTypeString"></param>
    /// <returns></returns>
    public static TargetColor ReturnTargetColorType_TargetColor(string targetColorTypeString) {
        switch (targetColorTypeString) {
            case "TargetColor-Red":    return TargetColor.Red;
            case "TargetColor-Orange": return TargetColor.Orange;
            case "TargetColor-Yellow": return TargetColor.Yellow;
            case "TargetColor-Green":  return TargetColor.Green;
            case "TargetColor-Blue":   return TargetColor.Blue;
            case "TargetColor-Purple": return TargetColor.Purple;
            case "TargetColor-Pink":   return TargetColor.Pink;
            case "TargetColor-White":  return TargetColor.White;
            default:                   return TargetColor.Yellow;
        }
    }

    /// <summary>
    /// Returns corresponding full target color type string from supplied TargetColor (typeTargetColor).
    /// </summary>
    /// <param name="typeTargetColor"></param>
    /// <returns></returns>
    public static string ReturnTargetColorType_StringFull(TargetColor typeTargetColor) {
        switch (typeTargetColor) {
            case TargetColor.Red:    return "TargetColor-Red";
            case TargetColor.Orange: return "TargetColor-Orange";
            case TargetColor.Yellow: return "TargetColor-Yellow";
            case TargetColor.Green:  return "TargetColor-Green";
            case TargetColor.Blue:   return "TargetColor-Blue";
            case TargetColor.Purple: return "TargetColor-Purple";
            case TargetColor.Pink:   return "TargetColor-Pink";
            case TargetColor.White:  return "TargetColor-White";
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
            case TargetColor.Red:    return "Red";
            case TargetColor.Orange: return "Orange";
            case TargetColor.Yellow: return "Yellow";
            case TargetColor.Green:  return "Green";
            case TargetColor.Blue:   return "Blue";
            case TargetColor.Purple: return "Purple";
            case TargetColor.Pink:   return "Pink";
            case TargetColor.White:  return "White";
            default:                 return "Yellow";
        }
    }
}