using UnityEngine;

namespace SomeAimGame.Skybox {

    public class SkyboxUtil : MonoBehaviour {
        /// <summary>
        /// Returns corresponding skybox type (Skybox) from supplied string (skyboxTypeString).
        /// </summary>
        /// <param name="skyboxTypeString"></param>
        /// <returns></returns>
        public static SkyboxType ReturnSkyboxType_Skybox(string skyboxTypeString) {
            switch (skyboxTypeString) {
                case "Skybox-Pink":   return SkyboxType.PINK;
                case "Skybox-Golden": return SkyboxType.GOLDEN;
                case "Skybox-Night":  return SkyboxType.NIGHT;
                case "Skybox-Grey":   return SkyboxType.GREY;
                case "Skybox-Blue":   return SkyboxType.BLUE;
                case "Skybox-Slate":  return SkyboxType.SLATE;
                default:              return SkyboxType.SLATE;
            }
        }

        /// <summary>
        /// Returns corresponding full skybox type string from supplied Skybox (typeSkybox).
        /// </summary>
        /// <param name="typeSkybox"></param>
        /// <returns></returns>
        public static string ReturnSkyboxType_StringFull(SkyboxType typeSkybox) {
            switch (typeSkybox) {
                case SkyboxType.PINK:   return "Skybox-Pink";
                case SkyboxType.GOLDEN: return "Skybox-Golden";
                case SkyboxType.NIGHT:  return "Skybox-Night";
                case SkyboxType.GREY:   return "Skybox-Grey";
                case SkyboxType.BLUE:   return "Skybox-Blue";
                case SkyboxType.SLATE:  return "Skybox-Slate";
                default:                return "Skybox-Slate";
            }
        }

        /// <summary>
        /// Returns corresponding short skybox type string from supplied Skybox (typeSkybox).
        /// </summary>
        /// <param name="typeSkybox"></param>
        /// <returns></returns>
        public static string ReturnSkyboxType_StringShort(SkyboxType typeSkybox) {
            switch (typeSkybox) {
                case SkyboxType.PINK:   return "Pink";
                case SkyboxType.GOLDEN: return "Golden";
                case SkyboxType.NIGHT:  return "Night";
                case SkyboxType.GREY:   return "Grey";
                case SkyboxType.BLUE:   return "Blue";
                case SkyboxType.SLATE:  return "Slate";
                default:                return "Slate";
            }
        }
    }
}