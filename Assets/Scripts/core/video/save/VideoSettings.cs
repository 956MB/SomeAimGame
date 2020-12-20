﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SomeAimGame.Utilities;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSettings : MonoBehaviour {
            public static DisplayModes displayMode    = DisplayModes.FULLSCREEN;
            public static int resolutionHeight        = 1920;
            public static int resolutionWidth         = 1080;
            public static int resolutionRefreshRate   = 60;
            public static int monitorMain             = 0;
            public static bool VSync                  = false;
            public static int fpsLimit                = 0;
            public static AntiAliasType antiAliasType = AntiAliasType.SMAA;
            public static bool vignette               = false;
            public static bool chromaticAberration    = false;

            private static VideoSettings videoSettings;
            void Awake() { videoSettings = this; }

            public static void SaveDisplayModeItem(DisplayModes setDisplayMode) {           displayMode           = setDisplayMode; }
            public static void SaveResolutionHeightItem(int setResolutionHeight) {          resolutionHeight      = setResolutionHeight; }
            public static void SaveResoltionWidthItem(int setResolutionWidth) {             resolutionWidth       = setResolutionWidth; }
            public static void SaveResoltionRefreshRateItem(int setResolutionRefreshRate) { resolutionRefreshRate = setResolutionRefreshRate; }
            public static void SaveMonitorMainItem(int setMonitorMain) {                    monitorMain           = setMonitorMain; }
            public static void SaveVSyncItem(bool setVSync) {                               VSync                 = setVSync; }
            public static void SaveFPSLimitItem(int setFpsLimit) {                          fpsLimit              = setFpsLimit; }
            public static void SaveAntiAliasItem(AntiAliasType setAntiAliasType) {          antiAliasType         = setAntiAliasType; }
            public static void SaveVignetteItem(bool setVignette) {                         vignette              = setVignette; }
            public static void SaveChromaticAberrationItem(bool setCromaticAberration) {    chromaticAberration   = setCromaticAberration; }

            public void SaveVideoSettings() { VideoSettingsSaveSystem.SaveVideoSettingsData(this); }
            public static void SaveVideoSettings_Static() { videoSettings.SaveVideoSettings(); }

            public static void SaveAllExtraSettingsDefaults(DisplayModes setDisplayMode, int setResolutionWidth, int setResolutionHeight, int setResolutionRefreshRate, int setMonitorMain, bool setVSync, int setFpsLimit, AntiAliasType setAntiAliasType, bool setVignette, bool setCromaticAberration) {
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

            public static void LoadVideoSettings(VideoSettingsDataSerial videoData) {
                displayMode           = videoData.displayMode;
                resolutionHeight      = videoData.resolutionHeight;
                resolutionWidth       = videoData.resolutionWidth;
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
