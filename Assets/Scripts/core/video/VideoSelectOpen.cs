using UnityEngine;

using SomeAimGame.SFX;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSelectOpen : MonoBehaviour {
            public void OpenVideoSettingDropdown(int dropdownType) {
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
