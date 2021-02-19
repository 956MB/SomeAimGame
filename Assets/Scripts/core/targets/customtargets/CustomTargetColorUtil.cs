using UnityEngine;

using SomeAimGame.Utilities;
using UnityEngine.UI;
using System;

namespace SomeAimGame.Targets {
    public class CustomTargetColorUtil : MonoBehaviour {
        public static string[] customColorNameStrings;
        public static int      customColorIndex;
        public static string[] customTargetColors;
        // TODO: Texture2D[] for loaded target previews.

        public static Color customColorAlbedo, customColorEmission;

        /// <summary>
        /// Loads custom target color values from supplied button holding values.
        /// </summary>
        /// <param name="clickedTargetButton"></param>
        public static void LoadCustomColorFromButton(GameObject clickedTargetButton) {
            int colorIndex = clickedTargetButton.GetComponent<CustomTargetColor>().ColorIndex;
            //Debug.Log($"{clickedTargetButton.GetComponent<CustomTargetColor>().ColorIndex}");
            LoadCustomColorValues(colorIndex);

            ButtonClickHandler.ReplaceAndSaveTargetColor(TargetType.CUSTOM, customColorNameStrings[customColorIndex], customColorIndex);
        }
        /// <summary>
        /// Loads custom target color values from supplied values from save file. (string setCustomColorName, int setCustomColorIndex, string setCustomColorStrings).
        /// </summary>
        /// <param name="setCustomColorName"></param>
        /// <param name="setCustomColorIndex"></param>
        /// <param name="setCustomColorStrings"></param>
        public static void LoadCustomColorsFromSave(string setCustomColorNames, int setCustomColorIndex, string setCustomColorStrings) {
            customColorNameStrings = Util.SplitString(setCustomColorNames);
            customTargetColors     = Util.SplitString(setCustomColorStrings);
            // TODO: load target color button prefabs from split string vvv
            if (customTargetColors != null) {
                if (customTargetColors.Length >= 1) { CustomTargetPanel.LoadAllCustomTargets(customTargetColors.Length); }
            }   

            LoadCustomColorValues(setCustomColorIndex);

            //CustomTargetPanel.CreateAddNewCustomTargetButton();
        }
        /// <summary>
        /// Loads custom target color values from supplied name string (colorNameString) and index int (colorIndex).
        /// </summary>
        /// <param name="colorNameString"></param>
        /// <param name="colorIndex"></param>
        private static void LoadCustomColorValues(int colorIndex) {
            customColorIndex = colorIndex;

            if (customColorIndex != -1) {
                Color loadCustomColors = Util.HexToColor(customTargetColors[customColorIndex]);
                customColorAlbedo      = loadCustomColors;
                customColorEmission    = loadCustomColors;
            }
        }

        /// <summary>
        /// Changes current primary target color to set custom color.
        /// </summary>
        /// <param name="currentPrimaryTarget"></param>
        public static void ChangeCurrentTargetColorCustom(GameObject currentPrimaryTarget) {
            try {
                currentPrimaryTarget.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", customColorAlbedo);
                currentPrimaryTarget.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", customColorEmission);
            } catch (MissingReferenceException) {}
        }

        #region utils

        public static string GetCurrentCustomTargetName() {
            return customColorNameStrings[customColorIndex];
        }
        public static string GetCustomTargetName(int index) {
            return customColorNameStrings.Length >= index+1 ? customColorNameStrings[index] : EditCustomTarget.customTargetName;
        }
        public static int GetCustomTargetsCount() {
            return customTargetColors == null ? 1 : customTargetColors.Length + 1;
        }

        public static void CreateRuntimeThumbnail(Material thumbnailImageMat, GameObject applyGameObject) {
            Texture2D thumbnailTexture2D = RuntimePreviewGenerator.GenerateMaterialPreview(thumbnailImageMat, PrimitiveType.Sphere, 82, 82);
            Sprite thumbnailSprite       = Sprite.Create(thumbnailTexture2D, new Rect(0.0f, 0.0f, thumbnailTexture2D.width, thumbnailTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            
            applyGameObject.transform.GetComponent<Image>().sprite = thumbnailSprite;
        }

        #endregion
    }
}
