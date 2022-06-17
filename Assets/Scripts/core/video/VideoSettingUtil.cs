using UnityEngine;
using WindowsDisplayAPI;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSettingUtil : MonoBehaviour {
            private static string[] displayModesStrings = { "Fullscreen Exclusive", "Fullscreen", "Fullscreen Winodwed", "Windowed" };
            private static string[] antiAliasStrings    = { "NONE", "FXAA", "SMAA", "TAA" };

            /// <summary>
            /// Returns whether or not supplied current setting string (currentSetting) and new setting string (newSetting) match.
            /// </summary>
            /// <param name="currentSetting"></param>
            /// <param name="newSetting"></param>
            /// <returns></returns>
            public static bool CheckMatch(string currentSetting, string newSetting) {
                return currentSetting == newSetting;
            }

            /// <summary>
            /// Returns corresponding DisplayModes type string from supplied DisplayModes (displayModesType).
            /// </summary>
            /// <param name="typeSkybox"></param>
            /// <returns></returns>
            public static string ReturnTypeString(FullScreenMode displayModesType) {
                return displayModesStrings[(int)displayModesType];
            }

            /// <summary>
            /// Returns corresponding AntiAliasType string from supplied AntiAliasType (antiAliasType).
            /// </summary>
            /// <param name="typeSkybox"></param>
            /// <returns></returns>
            public static string ReturnTypeString(AntiAliasType antiAliasType) {
                return antiAliasStrings[(int)antiAliasType];
            }

            /// <summary>
            /// Triggers current setting change for supplied display mode (refDiplayMode).
            /// </summary>
            /// <param name="refDisplayMode"></param>
            /// <param name="refPlaceholderString"></param>
            /// <param name="refChangeReady"></param>
            /// <param name="setDisplayMode"></param>
            /// <param name="setPlaceholderString"></param>
            public static void SettingChange(ref FullScreenMode refDisplayMode, ref string refPlaceholderString, ref bool refChangeReady, FullScreenMode setDisplayMode, string setPlaceholderString) {
                refDisplayMode       = setDisplayMode;
                refPlaceholderString = setPlaceholderString;
                refChangeReady       = true;
            }
            /// <summary>
            /// Triggers current setting change for supplied int (refInt).
            /// </summary>
            /// <param name="refInt"></param>
            /// <param name="refPlaceholderString"></param>
            /// <param name="refChangeReady"></param>
            /// <param name="setInt"></param>
            /// <param name="setPlaceholderString"></param>
            public static void SettingChange(ref int refInt, ref string refPlaceholderString, ref bool refChangeReady, int setInt, string setPlaceholderString) {
                refInt               = setInt;
                refPlaceholderString = setPlaceholderString;
                refChangeReady       = true;
            }
            /// <summary>
            /// Triggers current setting change for supplied refPlaceholderString (refPlaceholderString).
            /// </summary>
            /// <param name="refPlaceholderString"></param>
            /// <param name="refChangeReady"></param>
            /// <param name="setPlaceholderString"></param>
            public static void SettingChange(ref string refPlaceholderString, ref bool refChangeReady, string setPlaceholderString) {
                refPlaceholderString = setPlaceholderString;
                refChangeReady       = true;
            }
            public static void MatchPlaceholderCurrentSetting(ref string placeholder, ref string current, string setSetting) {
                placeholder = current = setSetting;
            }

            #region returns

            /// <summary>
            /// Returns array of available resolutions for current main display.
            /// </summary>
            /// <returns></returns>
            public static Resolution[] ReturnAvailableResolutions() { return Screen.resolutions; }
            /// <summary>
            /// Returns connected displays.
            /// </summary>
            /// <returns></returns>
            public static UnityEngine.Display[] ReturnConnectedMonitors_UnityEngine() { return UnityEngine.Display.displays; }
            /// <summary>
            /// Returns connected displays further info.
            /// </summary>
            /// <returns></returns>
            public static WindowsDisplayAPI.DisplayConfig.PathDisplayTarget[] ReturnConnectedMonitors_WindowsDisplayAPI() { return WindowsDisplayAPI.DisplayConfig.PathDisplayTarget.GetDisplayTargets(); }
            /// <summary>
            /// Returns current display resolution values.
            /// </summary>
            /// <returns></returns>
            public static Resolution ReturnCurrentScreenValues() { return Screen.currentResolution; }

            public static int ReturnFPSLimitFromValue(int value) {
                switch (value) {
                    case 0:  return 0;
                    case 1:  return 30;
                    case 2:  return 60;
                    case 3:  return 144;
                    case 4:  return 240;
                    default: return 0;
                }
            }

            #endregion
        }
    }
}
