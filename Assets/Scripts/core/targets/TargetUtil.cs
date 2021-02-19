using UnityEngine;
using UnityEngine.UI;

using SomeAimGame.Gamemode;
using SomeAimGame.Utilities;

namespace SomeAimGame.Targets {
    public class TargetUtil : MonoBehaviour {
        private static string[] targetsLongStrings  = { "TargetColor-Red", "TargetColor-Orange", "TargetColor-Yellow", "TargetColor-Green", "TargetColor-Blue", "TargetColor-Purple", "TargetColor-Pink", "TargetColor-White", "TargetColor-Custom" };
        private static string[] targetsShortStrings = { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Pink", "White", "Custom" };
        private static string[] targetsi18nsStrings = { "colorred", "colororange", "coloryellow", "colorgreen", "colorblue", "colorpurple", "colorpink", "colorwhite" };

        #region Targets utils

        /// <summary>
        /// Returns whether or not correct active pair target hit.
        /// </summary>
        /// <param name="hitTarget"></param>
        /// <returns></returns>
        public static bool CheckPairHit(Vector3 hitTarget) {
            if (Vector3.Distance(hitTarget, SpawnTargets.activePairLocation) == 0) { return true; }

            return false;
        }

        /// <summary>
        /// Returns random spawn point (X/Y/Z) inside spawn area bounds based on current gamemode.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="gamemode"></param>
        /// <param name="targetSize"></param>
        /// <param name="stepCount"></param>
        /// <returns></returns>
        public static Vector3 RandomPointInBounds(Bounds bounds, GamemodeType gamemode, float targetSize, int stepCount) {
            float randomX = Random.Range(bounds.min.x + targetSize, bounds.max.x - targetSize);
            float randomY = Random.Range(bounds.min.y + targetSize, bounds.max.y - targetSize);
            float randomZ = Random.Range(bounds.min.z + targetSize, bounds.max.z - targetSize);

            if (gamemode == GamemodeType.GRID || gamemode == GamemodeType.GRID_2) {
                randomX = (float)(bounds.size.x * 1.75) - targetSize * 3;
                randomY = Mathf.Floor(randomY / stepCount);
                randomZ = Mathf.Floor(randomZ / stepCount);
                randomY = randomY * stepCount;
                randomZ = randomZ * stepCount;
            } else if (gamemode == GamemodeType.SCATTER) {
                randomX = Mathf.Floor(randomX / stepCount);
                randomY = Mathf.Floor(randomY / stepCount);
                randomZ = Mathf.Floor(randomZ / stepCount);
                randomX = randomX * stepCount;
                randomY = randomY * stepCount;
                randomZ = randomZ * stepCount;
            }

            return new Vector3(randomX, randomY, randomZ);
        }

        /// <summary>
        /// Returns randomly selected point in bounds Vectors3 from supplied Bounds (bounds).
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public static Vector3 RandomPointInBounds_Follow(Bounds bounds) {
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);

            return new Vector3(randomX / 2, randomY / 2, randomZ / 3 );
        }

        /// <summary>
        /// Returns randomly selected point in bounds Vector3 from supplied Bounds (bounds) on supplied side int (side).
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public static Vector3 RandomPointInBounds_Glob(Bounds bounds, int side) {
            float randomX, randomY, randomZ;

            if (side == 0) {
                randomX = bounds.min.x;
                randomY = Random.Range(bounds.min.y, bounds.max.y);
                randomZ = bounds.min.z;
                //randomZ = Random.Range(bounds.min.z, bounds.max.z);
            } else {
                randomX = bounds.min.x;
                randomY = Random.Range(bounds.min.y, bounds.max.y);
                randomZ = bounds.max.z;
                //randomZ = Random.Range(bounds.min.z, bounds.max.z);
            }

            //Debug.Log($"Side: {side}, X: {randomX}");

            return new Vector3(randomX / 2, randomY / 2, randomZ / 3 );
        }

        /// <summary>
        /// Picks random points (X/Y/Z) inside corresponding spawn area bounds for supplied side (left/right), returns spawn location Vector3.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="pairLeft"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
        public static Vector3 PickRandomPairs(Bounds bounds, bool pairLeft, float targetSize) {
            float randomX, randomY, randomZ;

            if (pairLeft) {
                randomX = Random.Range(bounds.min.x + targetSize, bounds.max.x - targetSize);
                randomY = Random.Range(bounds.min.y + targetSize, bounds.max.y - targetSize);
                randomZ = Random.Range(bounds.min.z, bounds.center.z);
            } else {
                randomX = Random.Range(bounds.min.x + targetSize, bounds.max.x - targetSize);
                randomY = Random.Range(bounds.min.y + targetSize, bounds.max.y - targetSize);
                randomZ = Random.Range(bounds.center.z, bounds.max.z);
            }

            return new Vector3( randomX, randomY, randomZ );
        }

        #endregion


        #region Switch case returns

        /// <summary>
        /// Returns corresponding target color type (TargetColor) from supplied string (targetColorTypeString).
        /// </summary>
        /// <param name="targetColorTypeString"></param>
        /// <returns></returns>
        public static TargetType ReturnTargetColorType_TargetColor(string targetColorTypeString) {
            //return targetsLong[]
            switch (targetColorTypeString) {
                case "TargetColor-Red":    return TargetType.RED;
                case "TargetColor-Orange": return TargetType.ORANGE;
                case "TargetColor-Yellow": return TargetType.YELLOW;
                case "TargetColor-Green":  return TargetType.GREEN;
                case "TargetColor-Blue":   return TargetType.BLUE;
                case "TargetColor-Purple": return TargetType.PURPLE;
                case "TargetColor-Pink":   return TargetType.PINK;
                case "TargetColor-White":  return TargetType.WHITE;
                default:                   return TargetType.CUSTOM;
            }
        }

        /// <summary>
        /// Returns corresponding full target color type string from supplied TargetColor (typeTargetColor).
        /// </summary>
        /// <param name="typeTargetColor"></param>
        /// <returns></returns>
        public static string ReturnTargetColorType_StringFull(TargetType typeTargetColor) {
            // TODO: set custom button names to saved names
            if (typeTargetColor == TargetType.CUSTOM && CustomTargetColorUtil.customColorIndex != -1) { return $"TargetColor-{CustomTargetColorUtil.GetCurrentCustomTargetName()}"; }
            return targetsLongStrings[(int)typeTargetColor];
        }

        /// <summary>
        /// Returns corresponding short target color type string from supplied TargetColor (typeTargetColor).
        /// </summary>
        /// <param name="typeTargetColor"></param>
        /// <returns></returns>
        public static string ReturnTargetColorType_StringShort(TargetType typeTargetColor) {
            return targetsShortStrings[(int)typeTargetColor];
        }

        /// <summary>
        /// Returns corresponding I18n translated target color type string from supplied TargetColor (typeTargetColor).
        /// </summary>
        /// <param name="typeTargetColor"></param>
        /// <returns></returns>
        public static string ReturnTargetColorType_StringTranslated(TargetType typeTargetColor) {
            if (typeTargetColor == TargetType.CUSTOM) { return CustomTargetColorUtil.GetCurrentCustomTargetName(); }
            return I18nTextTranslator.SetTranslatedText(targetsi18nsStrings[(int)typeTargetColor]);
        }

        #endregion

        /// <summary>
        /// Clears all target color button borders in settings panel (general sub-section).
        /// </summary>
        public static void ClearTargetColorButtonBorders() {
            foreach (GameObject buttonBorder in GameObject.FindGameObjectsWithTag("ButtonBorderTargetColor")) {
                if (buttonBorder != null) {
                    buttonBorder.GetComponent<Image>().color = InterfaceColors.unselectedColor;
                    buttonBorder.SetActive(false);
                }
            }
        }
    }
}