using UnityEngine;

/// <summary>
/// Enum holding all skybox types.
/// </summary>
public enum Skybox {
    PINK,
    GOLDEN,
    NIGHT,
    GREY,
    BLUE,
    SLATE,
}

public class SkyboxType : MonoBehaviour {
    /// <summary>
    /// Returns corresponding skybox type (Skybox) from supplied string (skyboxTypeString).
    /// </summary>
    /// <param name="skyboxTypeString"></param>
    /// <returns></returns>
    public static Skybox ReturnSkyboxType_Skybox(string skyboxTypeString) {
        switch (skyboxTypeString) {
            case "Skybox-Pink":   return Skybox.PINK;
            case "Skybox-Golden": return Skybox.GOLDEN;
            case "Skybox-Night":  return Skybox.NIGHT;
            case "Skybox-Grey":   return Skybox.GREY;
            case "Skybox-Blue":   return Skybox.BLUE;
            case "Skybox-Slate":  return Skybox.SLATE;
            default:              return Skybox.SLATE;
        }
    }

    /// <summary>
    /// Returns corresponding full skybox type string from supplied Skybox (typeSkybox).
    /// </summary>
    /// <param name="typeSkybox"></param>
    /// <returns></returns>
    public static string ReturnSkyboxType_StringFull(Skybox typeSkybox) {
        switch (typeSkybox) {
            case Skybox.PINK:   return "Skybox-Pink";
            case Skybox.GOLDEN: return "Skybox-Golden";
            case Skybox.NIGHT:  return "Skybox-Night";
            case Skybox.GREY:   return "Skybox-Grey";
            case Skybox.BLUE:   return "Skybox-Blue";
            case Skybox.SLATE:  return "Skybox-Slate";
            default:            return "Skybox-Slate";
        }
    }

    /// <summary>
    /// Returns corresponding short skybox type string from supplied Skybox (typeSkybox).
    /// </summary>
    /// <param name="typeSkybox"></param>
    /// <returns></returns>
    public static string ReturnSkyboxType_StringShort(Skybox typeSkybox) {
        switch (typeSkybox) {
            case Skybox.PINK:   return "Pink";
            case Skybox.GOLDEN: return "Golden";
            case Skybox.NIGHT:  return "Night";
            case Skybox.GREY:   return "Grey";
            case Skybox.BLUE:   return "Blue";
            case Skybox.SLATE:  return "Slate";
            default:            return "Slate";
        }
    }
}