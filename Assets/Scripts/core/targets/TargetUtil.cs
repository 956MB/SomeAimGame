using UnityEngine;

namespace SomeAimGame.Targets {

    public class TargetUtil : MonoBehaviour {
        /// <summary>
        /// Returns corresponding target color type (TargetColor) from supplied string (targetColorTypeString).
        /// </summary>
        /// <param name="targetColorTypeString"></param>
        /// <returns></returns>
        public static TargetType ReturnTargetColorType_TargetColor(string targetColorTypeString) {
            switch (targetColorTypeString) {
                case "TargetColor-Red":    return TargetType.RED;
                case "TargetColor-Orange": return TargetType.ORANGE;
                case "TargetColor-Yellow": return TargetType.YELLOW;
                case "TargetColor-Green":  return TargetType.GREEN;
                case "TargetColor-Blue":   return TargetType.BLUE;
                case "TargetColor-Purple": return TargetType.PURPLE;
                case "TargetColor-Pink":   return TargetType.PINK;
                case "TargetColor-White":  return TargetType.WHITE;
                default:                   return TargetType.YELLOW;
            }
        }

        /// <summary>
        /// Returns corresponding full target color type string from supplied TargetColor (typeTargetColor).
        /// </summary>
        /// <param name="typeTargetColor"></param>
        /// <returns></returns>
        public static string ReturnTargetColorType_StringFull(TargetType typeTargetColor) {
            switch (typeTargetColor) {
                case TargetType.RED:    return "TargetColor-Red";
                case TargetType.ORANGE: return "TargetColor-Orange";
                case TargetType.YELLOW: return "TargetColor-Yellow";
                case TargetType.GREEN:  return "TargetColor-Green";
                case TargetType.BLUE:   return "TargetColor-Blue";
                case TargetType.PURPLE: return "TargetColor-Purple";
                case TargetType.PINK:   return "TargetColor-Pink";
                case TargetType.WHITE:  return "TargetColor-White";
                default:                return "TargetColor-Yellow";
            }
        }

        /// <summary>
        /// Returns corresponding short target color type string from supplied TargetColor (typeTargetColor).
        /// </summary>
        /// <param name="typeTargetColor"></param>
        /// <returns></returns>
        public static string ReturnTargetColorType_StringShort(TargetType typeTargetColor) {
            switch (typeTargetColor) {
                case TargetType.RED:    return "Red";
                case TargetType.ORANGE: return "Orange";
                case TargetType.YELLOW: return "Yellow";
                case TargetType.GREEN:  return "Green";
                case TargetType.BLUE:   return "Blue";
                case TargetType.PURPLE: return "Purple";
                case TargetType.PINK:   return "Pink";
                case TargetType.WHITE:  return "White";
                default:                return "Yellow";
            }
        }
    }
}