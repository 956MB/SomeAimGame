using UnityEngine;

using SomeAimGame.Utilities;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSettings : MonoBehaviour {
            public static FullScreenMode displayMode            = FullScreenMode.FullScreenWindow;
            public static int            resolutionWidth        = 1920;
            public static int            resolutionHeight       = 1080;
            public static int            resolutionRefreshRate  = 60;
            public static int            monitorMain            = 0;
            public static bool           VSync                  = false;
            public static int            fpsLimit               = 0;
            public static AntiAliasType  antiAliasType          = AntiAliasType.SMAA;
            public static bool           vignette               = false;
            public static bool           chromaticAberration    = false;

            private static VideoSettings videoSettings;
            void Awake() { videoSettings = this; }

            public static void SaveDisplayModeItem(FullScreenMode setDisplayMode) {         displayMode           = setDisplayMode; }
            public static void SaveResolutionHeightItem(int setResolutionHeight) {          resolutionHeight      = setResolutionHeight; }
            public static void SaveResoltionWidthItem(int setResolutionWidth) {             resolutionWidth       = setResolutionWidth; }
            public static void SaveResoltionRefreshRateItem(int setResolutionRefreshRate) { resolutionRefreshRate = setResolutionRefreshRate; }
            public static void SaveMonitorMainItem(int setMonitorMain) {                    monitorMain           = setMonitorMain; }
            public static void SaveVSyncItem(bool setVSync) {                               VSync                 = setVSync; }
            public static void SaveFPSLimitItem(int setFpsLimit) {                          fpsLimit              = setFpsLimit; }
            public static void SaveAntiAliasItem(AntiAliasType setAntiAliasType) {          antiAliasType         = setAntiAliasType; }
            public static void SaveVignetteItem(bool setVignette) {                         vignette              = setVignette; }
            public static void SaveChromaticAberrationItem(bool setCromaticAberration) {    chromaticAberration   = setCromaticAberration; }

            public void SaveVideoSettings() { VideoSettingsSaveSystem.SaveVideoSettingsData(); }
            public static void SaveVideoSettings_Static() { videoSettings.SaveVideoSettings(); }

            /// <summary>
            /// Saves default Video settings object (VideoSettings).
            /// </summary>
            /// <param name="setDisplayMode"></param>
            /// <param name="setResolutionWidth"></param>
            /// <param name="setResolutionHeight"></param>
            /// <param name="setResolutionRefreshRate"></param>
            /// <param name="setMonitorMain"></param>
            /// <param name="setVSync"></param>
            /// <param name="setFpsLimit"></param>
            /// <param name="setAntiAliasType"></param>
            /// <param name="setVignette"></param>
            /// <param name="setCromaticAberration"></param>
            public static void SaveAllExtraSettingsDefaults(FullScreenMode setDisplayMode, int setResolutionWidth, int setResolutionHeight, int setResolutionRefreshRate, int setMonitorMain, bool setVSync, int setFpsLimit, AntiAliasType setAntiAliasType, bool setVignette, bool setCromaticAberration) {
                displayMode           = setDisplayMode;
                resolutionWidth       = setResolutionWidth;
                resolutionHeight      = setResolutionHeight;
                resolutionRefreshRate = setResolutionRefreshRate;
                monitorMain           = setMonitorMain;
                VSync                 = setVSync;
                fpsLimit              = setFpsLimit;
                antiAliasType         = setAntiAliasType;
                vignette              = setVignette;
                chromaticAberration   = setCromaticAberration;

                videoSettings.SaveVideoSettings();
            }

            /// <summary>
            /// Loads Video settings data (VideoSettingsDataSerial) and sets values to this Video settings object (VideoSettings).
            /// </summary>
            /// <param name="videoData"></param>
            public static void LoadVideoSettings(VideoSettingsDataSerial videoData) {
                displayMode           = videoData.displayMode;
                resolutionWidth       = videoData.resolutionWidth;
                resolutionHeight      = videoData.resolutionHeight;
                resolutionRefreshRate = videoData.resolutionRefreshRate;
                monitorMain           = videoData.monitorMain;
                VSync                 = videoData.VSync;
                fpsLimit              = videoData.fpsLimit;
                antiAliasType         = videoData.antiAliasType;
                vignette              = videoData.vignette;
                chromaticAberration   = videoData.chromaticAberration;
            }
        }
    }
}
