using UnityEngine;
using UnityEngine.UI;

using SomeAimGame.Utilities;

namespace SomeAimGame.Skybox {
    public class SkyboxUtil : MonoBehaviour {
        private static string[] skyboxLongStrings  = { "Skybox-Pink", "Skybox-Golden", "Skybox-Night", "Skybox-Grey", "Skybox-Blue", "Skybox-Slate" };
        private static string[] skyboxShortStrings = { "Pink", "Golden", "Night", "Grey", "Blue", "Slate" };
        private static string[] skyboxi18nsStrings = { "skyboxpink", "skyboxgolden", "skyboxnight", "skyboxgrey", "skyboxblue", "skyboxslate" };

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
        /// Returns corresponding full skybox type string from supplied SkyboxType (typeSkybox).
        /// </summary>
        /// <param name="typeSkybox"></param>
        /// <returns></returns>
        public static string ReturnSkyboxType_StringFull(SkyboxType typeSkybox) {
            return skyboxLongStrings[(int)typeSkybox];
        }

        /// <summary>
        /// Returns corresponding short skybox type string from supplied SkyboxType (typeSkybox).
        /// </summary>
        /// <param name="typeSkybox"></param>
        /// <returns></returns>
        public static string ReturnSkyboxType_StringShort(SkyboxType typeSkybox) {
            return skyboxShortStrings[(int)typeSkybox];
        }

        /// <summary>
        /// Returns corresponding I18n translated skybox type string from supplied SkyboxType (typeSkybox).
        /// </summary>
        /// <param name="typeSkybox"></param>
        /// <returns></returns>
        public static string ReturnSkyboxType_StringTranslated(SkyboxType typeSkybox) {
            return I18nTextTranslator.SetTranslatedText(skyboxi18nsStrings[(int)typeSkybox]);
        }
    }
}