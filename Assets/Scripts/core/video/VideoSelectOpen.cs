using UnityEngine;

using SomeAimGame.SFX;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSelectOpen : MonoBehaviour {
            /// <summary>
            /// Open corresponding video setting dropdown from supplied (VideoDropdowns)int.
            /// </summary>
            /// <param name="dropdownType"></param>
            public void OpenVideoSettingDropdown(int dropdownType) {
                VideoSettingSelect.CheckCloseVideoSettingsDropdowns();

                switch ((VideoDropdowns)dropdownType) {
                    case VideoDropdowns.DISPLAY_MODE:  VideoSettingSelect.ToggleDisplayModeSelect_Static();  break;
                    case VideoDropdowns.RESOLUTION:    VideoSettingSelect.ToggleResolutionSelect_Static();   break;
                    case VideoDropdowns.MONITOR:       VideoSettingSelect.ToggleMonitorSelect_Static();      break;
                    case VideoDropdowns.ANTI_ALIASING: VideoSettingSelect.ToggleAntiAliasingSelect_Static(); break;
                }

                SFXManager.CheckPlayClick_Button();
            }
        }
    }
}
