using UnityEngine;

namespace SomeAimGame.Core {
    namespace Video {
        [System.Serializable]
        public class VideoSettingsDataSerial {
            public DisplayModes displayMode    = DisplayModes.FULLSCREEN;
            public int resolutionHeight        = 1920;
            public int resolutionWidth         = 1080;
            public int resolutionRefreshRate   = 60;
            public int monitorMain             = 0;
            public bool VSync                  = false;
            public int fpsLimit                = 0;
            public AntiAliasType antiAliasType = AntiAliasType.SMAA;
            public bool vignette               = false;
            public bool chromaticAberration    = false;

            public VideoSettingsDataSerial() {
                displayMode           = VideoSettings.displayMode;
                resolutionHeight      = VideoSettings.resolutionHeight;
                resolutionWidth       = VideoSettings.resolutionWidth;
                resolutionRefreshRate = VideoSettings.resolutionRefreshRate;
                monitorMain           = VideoSettings.monitorMain;
                VSync                 = VideoSettings.VSync;
                fpsLimit              = VideoSettings.fpsLimit;
                antiAliasType         = VideoSettings.antiAliasType;
                vignette              = VideoSettings.vignette;
                chromaticAberration   = VideoSettings.chromaticAberration;
            }
        }
    }
}