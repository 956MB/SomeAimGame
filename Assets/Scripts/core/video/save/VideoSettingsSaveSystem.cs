using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WindowsDisplayAPI;

using SomeAimGame.Utilities;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSettingsSaveSystem : MonoBehaviour {
            public Toggle vSyncToggle, vignetteToggle, chromaticAberrationToggle;

            private static VideoSettingsSaveSystem videoSave;
            void Awake() { videoSave = this; }

            public static void SaveVideoSettingsData(VideoSettings _) {
                BinaryFormatter formatter = new BinaryFormatter();
                string dirPath            = Application.persistentDataPath + "/prefs";
                string filePath           = dirPath + "/sag_video.prefs";

                DirectoryInfo dirInf = new DirectoryInfo(dirPath);
                if (!dirInf.Exists) { dirInf.Create(); }

                FileStream stream                 = new FileStream(filePath, FileMode.Create);
                VideoSettingsDataSerial videoData = new VideoSettingsDataSerial();
                formatter.Serialize(stream, videoData);
                stream.Close();
            }

            public static VideoSettingsDataSerial LoadVideoSettingsData() {
                string path = Application.persistentDataPath + "/prefs/sag_video.prefs";
                if (File.Exists(path)) {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream stream         = new FileStream(path, FileMode.Open);

                    VideoSettingsDataSerial videoData = formatter.Deserialize(stream) as VideoSettingsDataSerial;
                    stream.Close();

                    return videoData;
                } else {
                    return null;
                }
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

                    VideoSettings.LoadVideoSettings(loadedVideoSettingsData);
                } else {
                    //Debug.Log("failed to init extra in 'initSavedSettings', extra: " + loadedExtraData);
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
