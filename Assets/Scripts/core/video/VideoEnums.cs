
namespace SomeAimGame.Core {
    namespace Video {
        /// <summary>
        /// Enum holding all video setting dropdowns
        /// </summary>
        public enum VideoDropdowns {
            DISPLAY_MODE,
            RESOLUTION,
            MONITOR,
            ANTI_ALIASING
        }

        /// <summary>
        /// Enum holding all video display modes.
        /// </summary>
        public enum DisplayModes {
            FULLSCREEN,
            FULLSCREEN_EXCLUSIVE,
            FULLSCREEN_WINDOWED,
            WINDOWED
        }

        /// <summary>
        /// Enum holding all video anti aliasing types.
        /// </summary>
        public enum AntiAliasType {
            NONE,
            FXAA,
            SMAA,
            TAA
        }
    }
}
