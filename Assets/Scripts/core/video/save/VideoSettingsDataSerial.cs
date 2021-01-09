using UnityEngine;

namespace SomeAimGame.Core {
    namespace Video {
        [System.Serializable]
        public class VideoSettingsDataSerial {
            public FullScreenMode displayMode;
            public int resolutionHeight;
            public int resolutionWidth;
            public int resolutionRefreshRate;
            public int monitorMain;
            public bool VSync;
            public int fpsLimit;
            public AntiAliasType antiAliasType;
            public bool vignette;
            public bool chromaticAberration;

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