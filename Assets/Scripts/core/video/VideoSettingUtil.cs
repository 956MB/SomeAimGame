using UnityEngine;
using WindowsDisplayAPI;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSettingUtil : MonoBehaviour {
            public static bool CheckMatch(string currentSetting, string newSetting) {
                return currentSetting == newSetting;
            }

            /// <summary>
            /// Returns corresponding DisplayModes type string from supplied DisplayModes (displayModesType).
            /// </summary>
            /// <param name="typeSkybox"></param>
            /// <returns></returns>
            public static string ReturnTypeString(FullScreenMode displayModesType) {
                switch (displayModesType) {
                    case FullScreenMode.FullScreenWindow:    return "Fullscreen";
                    case FullScreenMode.ExclusiveFullScreen: return "Fullscreen Exclusive";
                    case FullScreenMode.MaximizedWindow:     return "Fullscreen Winodwed";
                    case FullScreenMode.Windowed:            return "Windowed";
                    default:                                 return "Fullscreen";
                }
            }

            /// <summary>
            /// Returns corresponding AntiAliasType string from supplied AntiAliasType (antiAliasType).
            /// </summary>
            /// <param name="typeSkybox"></param>
            /// <returns></returns>
            public static string ReturnTypeString(AntiAliasType antiAliasType) {
                switch (antiAliasType) {
                    case AntiAliasType.NONE: return "NONE";
                    case AntiAliasType.FXAA: return "FXAA";
                    case AntiAliasType.SMAA: return "SMAA";
                    case AntiAliasType.TAA:  return "TAA";
                    default:                 return "SMAA";
                }
            }

            public static void SettingChange(ref FullScreenMode refDiplayMode, ref string refPlaceholderString, ref bool refChangeReady, FullScreenMode setDiplayMode, string setPlaceholderString) {
                refDiplayMode        = setDiplayMode;
                refPlaceholderString = setPlaceholderString;
                refChangeReady       = true;
            }
             public static void SettingChange(ref int refInt, ref string refPlaceholderString, ref bool refChangeReady, int setInt, string setPlaceholderString) {
                refInt               = setInt;
                refPlaceholderString = setPlaceholderString;
                refChangeReady       = true;
            }
            public static void SettingChange(ref string refPlaceholderString, ref bool refChangeReady, string setPlaceholderString) {
                refPlaceholderString = setPlaceholderString;
                refChangeReady       = true;
            }

            #region returns

            public static Resolution[] ReturnAvailableResolutions() { return Screen.resolutions; }
            public static UnityEngine.Display[] ReturnConnectedMonitors_UnityEngine() { return UnityEngine.Display.displays; }
            public static WindowsDisplayAPI.DisplayConfig.PathDisplayTarget[] ReturnConnectedMonitors_WindowsDisplayAPI() { return WindowsDisplayAPI.DisplayConfig.PathDisplayTarget.GetDisplayTargets(); }

            public static Resolution ReturnCurrentScreenValues() { return Screen.currentResolution; }

            #endregion
        }
    }
}
