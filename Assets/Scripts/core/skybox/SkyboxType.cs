using UnityEngine;

/// <summary>
/// Enum holding all skybox types.
/// </summary>
public enum Skybox {
    Pink   = 0,
    Golden = 1,
    Night  = 2,
    Grey   = 3,
    Blue   = 4,
    Slate  = 5,
}

public class SkyboxType : MonoBehaviour {
    /// <summary>
    /// Returns corresponding skybox type (Skybox) from supplied string (skyboxTypeString).
    /// </summary>
    /// <param name="targetColorTypeString"></param>
    /// <returns></returns>
    public static Skybox ReturnSkyboxType_Skybox(string skyboxTypeString) {
        switch (skyboxTypeString) {
            case "Skybox-Pink":   return Skybox.Pink;
            case "Skybox-Golden": return Skybox.Golden;
            case "Skybox-Night":  return Skybox.Night;
            case "Skybox-Grey":   return Skybox.Grey;
            case "Skybox-Blue":   return Skybox.Blue;
            case "Skybox-Slate":  return Skybox.Slate;
            default:              return Skybox.Slate;
        }
    }

    /// <summary>
    /// Returns corresponding full skybox type string from supplied Skybox (typeSkybox).
    /// </summary>
    /// <param name="typeTargetColor"></param>
    /// <returns></returns>
    public static string ReturnSkyboxType_StringFull(Skybox typeSkybox) {
        switch (typeSkybox) {
            case Skybox.Pink:   return "Skybox-Pink";
            case Skybox.Golden: return "Skybox-Golden";
            case Skybox.Night:  return "Skybox-Night";
            case Skybox.Grey:   return "Skybox-Grey";
            case Skybox.Blue:   return "Skybox-Blue";
            case Skybox.Slate:  return "Skybox-Slate";
            default:            return "Skybox-Slate";
        }
    }

    /// <summary>
    /// Returns corresponding short skybox type string from supplied Skybox (typeSkybox).
    /// </summary>
    /// <param name="typeTargetColor"></param>
    /// <returns></returns>
    public static string ReturnSkyboxType_StringShort(Skybox typeSkybox) {
        switch (typeSkybox) {
            case Skybox.Pink:   return "Pink";
            case Skybox.Golden: return "Golden";
            case Skybox.Night:  return "Night";
            case Skybox.Grey:   return "Grey";
            case Skybox.Blue:   return "Blue";
            case Skybox.Slate:  return "Slate";
            default:            return "Slate";
        }
    }
}