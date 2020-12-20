using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSettingChange : MonoBehaviour, IPointerClickHandler {
            int dropdownInt, settingInt;

            public void OnPointerClick(PointerEventData pointerEventData) {
                VideoDropdowns dropdownOptionClick = (VideoDropdowns)dropdownInt;
                string clickedText = GetComponentsInChildren<TMP_Text>()[0].text;

                if (dropdownOptionClick == VideoDropdowns.DISPLAY_MODE) {
                    ApplyVideoSettings.SetDisplayModePlaceholder(VideoSettingUtil.ReturnTypeString((DisplayModes)settingInt), (DisplayModes)settingInt);
                }
                if (dropdownOptionClick == VideoDropdowns.RESOLUTION) {
                    ApplyVideoSettings.SetResolutionPlaceholder(clickedText);
                }
                if (dropdownOptionClick == VideoDropdowns.MONITOR) {
                    ApplyVideoSettings.SetMonitorPlaceholder(clickedText, settingInt);
                }
                if (dropdownOptionClick == VideoDropdowns.ANTI_ALIASING) {
                    ApplyVideoSettings.ApplyAntiAliasing(clickedText, (AntiAliasType)settingInt);
                }

                VideoSettingSelect.CheckCloseVideoSettingsDropdowns();
                //VideoSettings.SaveVideoSettings_Static();
            }

            public void SetDropdownInt(int setDropdownInt, int setSettingInt) {
                this.dropdownInt = setDropdownInt;
                this.settingInt  = setSettingInt;
            }
        }
    }
}