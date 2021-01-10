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

            public static void SaveVideoSettingsData() {
                VideoSettingsDataSerial videoData = new VideoSettingsDataSerial();
                SaveLoadUtil.SaveDataSerial("/prefs", "/sag_video.prefs", videoData);
            }

            public static VideoSettingsDataSerial LoadVideoSettingsData() {
                VideoSettingsDataSerial videoData = (VideoSettingsDataSerial)SaveLoadUtil.LoadDataSerial("/prefs/sag_video.prefs", SaveType.VIDEO);
                return videoData;
            }

            public static void InitSavedVideoSettings() {
                VideoSettingsDataSerial loadedVideoSettingsData = LoadVideoSettingsData();
                if (loadedVideoSettingsData != null) {
                    SetDisplayMode(loadedVideoSettingsData.displayMode);
                    SetResolution(loadedVideoSettingsData.resolutionWidth, loadedVideoSettingsData.resolutionHeight, loadedVideoSettingsData.displayMode, loadedVideoSettingsData.resolutionRefreshRate);
                    SetMonitor(loadedVideoSettingsData.monitorMain);
                    SetVSyncToggle(loadedVideoSettingsData.VSync);
                    SetFPSLimit(loadedVideoSettingsData.fpsLimit);
                    SetAntiAlias(loadedVideoSettingsData.antiAliasType);
                    SetVignetteToggle(loadedVideoSettingsData.vignette);
                    SetChromaticAberrationToggle(loadedVideoSettingsData.chromaticAberration);
                    //Debug.Log("SAVED FPS LIMIT: " + loadedVideoSettingsData.fpsLimit);

                    VideoSettings.LoadVideoSettings(loadedVideoSettingsData);
                } else {
                    //Debug.Log("failed to init extra in 'initSavedSettings', extra: " + loadedVideoSettingsData);
                    InitVideoSettingsDefaults();
                }
            }

            public static void InitVideoSettingsDefaults() {
                // Get users default monitor res/refresh on init
                Resolution currentRes = VideoSettingUtil.ReturnCurrentScreenValues();
                //Debug.Log($"current res: {currentRes.refreshRate}");

                SetDisplayMode(FullScreenMode.FullScreenWindow);
                SetResolution(currentRes.width, currentRes.height, FullScreenMode.FullScreenWindow, currentRes.refreshRate);
                SetMonitor(0);
                SetVSyncToggle(false);
                SetFPSLimit(0);
                SetAntiAlias(AntiAliasType.SMAA);
                SetVignetteToggle(false);
                SetChromaticAberrationToggle(false);

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
