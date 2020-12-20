using UnityEngine;

using SomeAimGame.Utilities;

namespace SomeAimGame.Core {
    namespace Video {
        public class ApplyVideoSettings : MonoBehaviour {
            private static string displayModeStringCurrent, resolutionStringCurrent, monitorStringCurrent, displayModeStringPlaceholder, resolutionStringPlaceholder, monitorStringPlaceholder;

            private static int widthPlaceholder, heightPlaceholder, refreshPlaceholder;
            private static DisplayModes displayModePlaceholder;
            private static int monitorPlaceholder;

            private static bool videoSettingsSaveReady = false;
            private static bool displayModeChangeReady = false;
            private static bool resolutionChangeReady  = false;
            private static bool monitorChangeReady     = false;

            #region placeholders

            /// <summary>
            /// Sets new display mode setting placeholder and sets videoSettingsSaveReady true.
            /// </summary>
            /// <param name="setText"></param>
            /// <param name="newDisplayMode"></param>
            public static void SetDisplayModePlaceholder(string setText, DisplayModes newDisplayMode) {
                VideoSettingSelect.SetDisplayModeText(setText);
                VideoSettingUtil.SettingChange(ref displayModePlaceholder, ref displayModeStringPlaceholder, ref displayModeChangeReady, newDisplayMode, setText);
                ToggleSettingsSaveContainer(displayModeStringPlaceholder, displayModeStringCurrent, ref displayModeChangeReady);
            }
            /// <summary>
            /// Sets new resolution setting placeholder and sets videoSettingsSaveReady true.
            /// </summary>
            /// <param name="resolutionString"></param>
            public static void SetResolutionPlaceholder(string resolutionString) {
                VideoSettingSelect.SetResolutionText(resolutionString);
                VideoSettingUtil.SettingChange(ref resolutionStringPlaceholder, ref resolutionChangeReady, resolutionString);

                string stripped = resolutionString.Replace("Hz", "").Replace("(", "").Replace(")", "");
                string [] resolutionSplit = stripped.Split(' ');

                if (!int.TryParse(resolutionSplit[0], out widthPlaceholder)) {   widthPlaceholder   = 1920; }
                if (!int.TryParse(resolutionSplit[2], out heightPlaceholder)) {  heightPlaceholder  = 1080; }
                if (!int.TryParse(resolutionSplit[4], out refreshPlaceholder)) { refreshPlaceholder = 60; }
                
                ToggleSettingsSaveContainer(resolutionStringPlaceholder, resolutionStringCurrent, ref resolutionChangeReady);
            }
            /// <summary>
            /// Sets new monitor setting placeholder and sets videoSettingsSaveReady true.
            /// </summary>
            /// <param name="setText"></param>
            /// <param name="monitorSet"></param>
            public static void SetMonitorPlaceholder(string setText, int monitorSet) {
                VideoSettingSelect.SetMonitorText(setText);
                VideoSettingUtil.SettingChange(ref monitorPlaceholder, ref monitorStringPlaceholder, ref monitorChangeReady, monitorSet, setText);

                ToggleSettingsSaveContainer(monitorStringPlaceholder, monitorStringCurrent, ref monitorChangeReady);
            }

            #endregion

            #region apply

            /// <summary>
            /// Applies new Display Mode setting.
            /// </summary>
            public static void ApplyDisplayMode() {
                if (!VideoSettingUtil.CheckMatch(displayModeStringPlaceholder, displayModeStringCurrent)) {
                    // set display mode
                    VideoSettings.SaveDisplayModeItem(displayModePlaceholder);
                }
            }
            /// <summary>
            /// Applies new Resolution setting.
            /// </summary>
            public static void ApplyResolution() {
                if (!VideoSettingUtil.CheckMatch(resolutionStringPlaceholder, resolutionStringCurrent)) {
                    // set resolution
                    VideoSettings.SaveResoltionWidthItem(widthPlaceholder);
                    VideoSettings.SaveResolutionHeightItem(heightPlaceholder);
                    VideoSettings.SaveResoltionRefreshRateItem(refreshPlaceholder);
                }
            }
            /// <summary>
            /// Applies new Monitor setting.
            /// </summary>
            public static void ApplyMonitor() {
                if (!VideoSettingUtil.CheckMatch(monitorStringPlaceholder, monitorStringCurrent)) {
                    // set monitor
                    VideoSettings.SaveMonitorMainItem(monitorPlaceholder);
                }
            }
            /// <summary>
            /// Applies new Anti Aliasing setting.
            /// </summary>
            /// <param name="setText"></param>
            /// <param name="antiAliasType"></param>
            public static void ApplyAntiAliasing(string setText, AntiAliasType antiAliasType) {
                // set anti aliasing
                VideoSettingSelect.SetAntiAliasingText(setText);
                VideoSettings.SaveAntiAliasItem(antiAliasType);
                VideoSettings.SaveVideoSettings_Static();
            }
            /// <summary>
            /// Applies new VSync setting.
            /// </summary>
            /// <param name="enabled"></param>
            public static void ApplyVSync(bool enabled) {
                // set vsync
                VideoSettings.SaveVSyncItem(enabled);
                VideoSettings.SaveVideoSettings_Static();
            }
            /// <summary>
            /// Applies new Vignette setting.
            /// </summary>
            /// <param name="enabled"></param>
            public static void ApplyVignette(bool enabled) {
                //ManipulatePostProcess.SetVIG(enabled);
                VideoSettings.SaveVignetteItem(enabled);
                VideoSettings.SaveVideoSettings_Static();
            }
            /// <summary>
            /// Applies new Chromatic Aberration setting.
            /// </summary>
            /// <param name="enabled"></param>
            public static void ApplyChromaticAberration(bool enabled) {
                //ManipulatePostProcess.SetCA(enabled);
                VideoSettings.SaveChromaticAberrationItem(enabled);
                VideoSettings.SaveVideoSettings_Static();
            }

            #endregion

            /// <summary>
            /// Applies all new video settings, sets save container state, then saves VideoSettings object.
            /// </summary>
            public static void ApplyAllVideoSettings() {
                if (videoSettingsSaveReady) {
                    ApplyDisplayMode();
                    ApplyResolution();
                    ApplyMonitor();

                    videoSettingsSaveReady = displayModeChangeReady = resolutionChangeReady = monitorChangeReady = false;
                    VideoSettingSelect.SetVideoSettingsSaveContainerState(false);

                    VideoSettings.SaveVideoSettings_Static();
                }
            }
            /// <summary>
            /// Reverts all video settings to previously saved values, then sets save container state.
            /// </summary>
            public static void RevertVideoSettings() {
                if (videoSettingsSaveReady) {
                    VideoSettingSelect.SetDisplayModeText(displayModeStringCurrent);
                    VideoSettingSelect.SetResolutionText(resolutionStringCurrent);
                    VideoSettingSelect.SetMonitorText(monitorStringCurrent);

                    SetDisplayModeCurrent(displayModeStringCurrent);
                    SetResolutionCurrent(resolutionStringCurrent);
                    SetMonitorCurrent(monitorStringCurrent);

                    videoSettingsSaveReady = displayModeChangeReady = resolutionChangeReady = monitorChangeReady = false;
                    VideoSettingSelect.SetVideoSettingsSaveContainerState(false);
                }
            }

            public static void ToggleSettingsSaveContainer(string placeholder, string current, ref bool changeReady) {
                // If new setting selected and setting save container not ready.
                if (!VideoSettingUtil.CheckMatch(placeholder, current) && !videoSettingsSaveReady) {
                    videoSettingsSaveReady = true;
                    VideoSettingSelect.SetVideoSettingsSaveContainerState(true);
                }

                // If matching setting selected
                if (VideoSettingUtil.CheckMatch(placeholder, current) && videoSettingsSaveReady) {
                    // If new matching setting is only setting currently ready for save, settings are reverted.
                    if (displayModeChangeReady && !resolutionChangeReady && !monitorChangeReady) {
                        RevertVideoSettings();
                    } else if (!displayModeChangeReady && resolutionChangeReady && !monitorChangeReady) {
                        RevertVideoSettings();
                    } else if (!displayModeChangeReady && !resolutionChangeReady && monitorChangeReady) {
                        RevertVideoSettings();
                    } else {
                        changeReady = false;
                    }
                }
            }

            public static void SetDisplayModeCurrent(string current) { displayModeStringCurrent = displayModeStringPlaceholder = current; }
            public static void SetResolutionCurrent(string current) {  resolutionStringCurrent  = resolutionStringPlaceholder  = current; }
            public static void SetMonitorCurrent(string current) {     monitorStringCurrent     = monitorStringPlaceholder     = current; }
        }
    }
}