using UnityEngine;
using UnityEngine.UI;
using WindowsDisplayAPI;

using SomeAimGame.Utilities;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSettingsSaveSystem : MonoBehaviour {
            public Toggle vSyncToggle, vignetteToggle, chromaticAberrationToggle;

            private static VideoSettingsSaveSystem videoSave;
            void Awake() { videoSave = this; }

            /// <summary>
            /// Saves supplied videos settings object (VideoSettings) to file.
            /// </summary>
            public static void SaveVideoSettingsData() {
                VideoSettingsDataSerial videoData = new VideoSettingsDataSerial();
                SaveLoadUtil.SaveDataSerial("/prefs", "/sag_video.prefs", videoData);
            }

            /// <summary>
            /// Loads video settings data (VideoSettingsDataSerial) from file.
            /// </summary>
            /// <returns></returns>
            public static VideoSettingsDataSerial LoadVideoSettingsData() {
                VideoSettingsDataSerial videoData = (VideoSettingsDataSerial)SaveLoadUtil.LoadDataSerial("/prefs/sag_video.prefs", SaveType.VIDEO);
                return videoData;
            }

            /// <summary>
            /// Inits saved video settings object and sets all video settings values.
            /// </summary>
            public static void InitSavedVideoSettings() {
                VideoSettingsDataSerial loadedVideoSettingsData = LoadVideoSettingsData();
                if (loadedVideoSettingsData != null) {
                    SetVideoSettingsValues(loadedVideoSettingsData.displayMode, loadedVideoSettingsData.resolutionWidth, loadedVideoSettingsData.resolutionHeight, loadedVideoSettingsData.resolutionRefreshRate, loadedVideoSettingsData.monitorMain, loadedVideoSettingsData.VSync, loadedVideoSettingsData.fpsLimit, loadedVideoSettingsData.antiAliasType, loadedVideoSettingsData.vignette, loadedVideoSettingsData.chromaticAberration);

                    VideoSettings.LoadVideoSettings(loadedVideoSettingsData);
                } else {
                    //Debug.Log("failed to init extra in 'initSavedSettings', extra: " + loadedVideoSettingsData);
                    InitVideoSettingsDefaults();
                }
            }

            /// <summary>
            /// Inits default video settings values and saves to file on first launch.
            /// </summary>
            public static void InitVideoSettingsDefaults() {
                // Get users default monitor res/refresh on init
                Resolution currentRes = VideoSettingUtil.ReturnCurrentScreenValues();
                //Debug.Log($"current res: {currentRes.refreshRate}");

                SetVideoSettingsValues(FullScreenMode.FullScreenWindow, currentRes.width, currentRes.height, currentRes.refreshRate, 0, false, 0, AntiAliasType.SMAA, false, false);

                VideoSettings.SaveAllExtraSettingsDefaults(FullScreenMode.FullScreenWindow, currentRes.width, currentRes.height, currentRes.refreshRate, 0, false, 0, AntiAliasType.SMAA, false, false);
            }


            #region sets

            private static void SetDisplayMode(FullScreenMode displayMode) {
                string displayModeString = VideoSettingUtil.ReturnTypeString(displayMode);
                VideoSettingSelect.SetDisplayModeText(displayModeString);
                ApplyVideoSettings.SetDisplayModeCurrent(displayModeString);
            }
            private static void SetResolution(int w, int h, FullScreenMode displayMode, int refresh) {
                string resolutionFormatted = $"{w} x {h} {Util.ReturnAspectRatio_string(w, h)} ({refresh}Hz)";
                ApplyVideoSettings.ApplyResolution(w, h, displayMode, refresh);
                VideoSettingSelect.SetResolutionText(resolutionFormatted);
                ApplyVideoSettings.SetResolutionCurrent(resolutionFormatted);
            }
            private static void SetMonitor(int setMonitor) {
                WindowsDisplayAPI.DisplayConfig.PathDisplayTarget[] connectedDisplays = VideoSettingUtil.ReturnConnectedMonitors_WindowsDisplayAPI();
                string monitorString = $"Display {setMonitor+1}: {connectedDisplays[setMonitor].FriendlyName}";
                ApplyVideoSettings.ApplyMonitor(setMonitor);
                VideoSettingSelect.SetMonitorText(monitorString);
                ApplyVideoSettings.SetMonitorCurrent(monitorString);
            }
            private static void SetFPSLimit(int setFPSLimit) {
                FPSLimitSlider.limitFPSValue = setFPSLimit;
                ApplyVideoSettings.ApplyFPSLimit(setFPSLimit);
                FPSLimitSlider.SetFPSLimitValueText(setFPSLimit);
                FPSLimitSlider.SetFPSLimitSlider(setFPSLimit);
            }
            private static void SetAntiAlias(AntiAliasType setAntiAlias) {
                string antiAliasString = VideoSettingUtil.ReturnTypeString(setAntiAlias);
                // set AA
                VideoSettingSelect.SetAntiAliasingText(antiAliasString);
            }
            private static void SetVSyncToggle(bool vSyncToggle) {
                ApplyVideoSettings.ApplyVSync(vSyncToggle);
                videoSave.vSyncToggle.isOn = vSyncToggle;
            }
            private static void SetVignetteToggle(bool vignetteToggle) {
                ApplyVideoSettings.ApplyVignette(vignetteToggle);
                videoSave.vignetteToggle.isOn = vignetteToggle;
            }
            private static void SetChromaticAberrationToggle(bool chromaticAberrationToggle) {
                // set CA
                videoSave.chromaticAberrationToggle.isOn = chromaticAberrationToggle;
            }

            public static void SetVideoSettingsValues(FullScreenMode setDisplayMode, int setResolutionWidth, int setResolutionHeight, int setResolutionRefreshRate, int setMonitorMain, bool setVSync, int setFpsLimit, AntiAliasType setAntiAliasType, bool setVignette, bool setCromaticAberration) {
                SetDisplayMode(setDisplayMode);
                SetResolution(setResolutionWidth, setResolutionHeight, setDisplayMode, setResolutionRefreshRate);
                SetMonitor(setMonitorMain);
                SetVSyncToggle(setVSync);
                SetFPSLimit(setFpsLimit);
                SetAntiAlias(setAntiAliasType);
                SetVignetteToggle(setVignette);
                SetChromaticAberrationToggle(setCromaticAberration);
            }

            #endregion

            // TODO: SET VIDEO SETTINGS
            /*
             * Display mode:            DONE
             * Resolution:              DONE
             * Monitor:                 DONE
             * FPS Limit:               DONE
             * Anti-alias:
             * VSync:                   DONE?
             * Vignette:                DONE
             * Chromatic Aberration:
             */
        }
    }
}
