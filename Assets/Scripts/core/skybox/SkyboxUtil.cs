using UnityEngine;
using UnityEngine.UI;

using SomeAimGame.Utilities;

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
        /// Returns corresponding full skybox type string from supplied SkyboxType (typeSkybox).
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
        /// Returns corresponding short skybox type string from supplied SkyboxType (typeSkybox).
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

        /// <summary>
        /// Returns corresponding I18n translated skybox type string from supplied SkyboxType (typeSkybox).
        /// </summary>
        /// <param name="typeSkybox"></param>
        /// <returns></returns>
        public static string ReturnSkyboxType_StringTranslated(SkyboxType typeSkybox) {
            switch (typeSkybox) {
                case SkyboxType.PINK:   return I18nTextTranslator.SetTranslatedText("skyboxpink");
                case SkyboxType.GOLDEN: return I18nTextTranslator.SetTranslatedText("skyboxgolden");
                case SkyboxType.NIGHT:  return I18nTextTranslator.SetTranslatedText("skyboxnight");
                case SkyboxType.GREY:   return I18nTextTranslator.SetTranslatedText("skyboxgrey");
                case SkyboxType.BLUE:   return I18nTextTranslator.SetTranslatedText("skyboxblue");
                case SkyboxType.SLATE:  return I18nTextTranslator.SetTranslatedText("skyboxslate");
                default:                return I18nTextTranslator.SetTranslatedText("skyboxslate");
            }
        }

        /// <summary>
        /// Clears all skybox button borders in settings panel (general sub-section).
        /// </summary>
        public static void ClearSkyboxButtonBorders() {
            foreach (GameObject buttonBorder in GameObject.FindGameObjectsWithTag("ButtonBorderSkybox")) {
                if (buttonBorder != null) {
                    buttonBorder.GetComponent<Image>().color = InterfaceColors.unselectedColor;
                    buttonBorder.SetActive(false);
                }
            }
        }
    }
}