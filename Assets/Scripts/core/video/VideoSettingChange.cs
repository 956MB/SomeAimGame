using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSettingChange : MonoBehaviour, IPointerClickHandler {
            int dropdownInt, settingInt;

            public void OnPointerClick(PointerEventData pointerEventData) {
                VideoDropdowns dropdownOptionClick = (VideoDropdowns)dropdownInt;
                string clickedText                 = GetComponentsInChildren<TMP_Text>()[0].text;

                switch (dropdownOptionClick) {
                    case VideoDropdowns.DISPLAY_MODE:  ApplyVideoSettings.SetDisplayModePlaceholder(VideoSettingUtil.ReturnTypeString((FullScreenMode)settingInt), (FullScreenMode)settingInt); break;
                    case VideoDropdowns.RESOLUTION:    ApplyVideoSettings.SetResolutionPlaceholder(clickedText);                                                                                break;
                    case VideoDropdowns.MONITOR:       ApplyVideoSettings.SetMonitorPlaceholder(clickedText, settingInt);                                                                       break;
                    case VideoDropdowns.ANTI_ALIASING: ApplyVideoSettings.ApplyAntiAliasing(clickedText, (AntiAliasType)settingInt);                                                            break;
                }

                VideoSettingSelect.CheckCloseVideoSettingsDropdowns();
            }

            public void SetDropdownInt(int setDropdownInt, int setSettingInt) {
                dropdownInt = setDropdownInt;
                settingInt  = setSettingInt;
            }
        }
    }
}