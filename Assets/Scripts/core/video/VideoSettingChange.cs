using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSettingChange : MonoBehaviour, IPointerClickHandler {
            int dropdownInt, settingInt;
            string settingText;

            public void OnPointerClick(PointerEventData pointerEventData) {
                switch ((VideoDropdowns)dropdownInt) {
                    case VideoDropdowns.DISPLAY_MODE:  ApplyVideoSettings.SetDisplayModePlaceholder(VideoSettingUtil.ReturnTypeString((FullScreenMode)settingInt), (FullScreenMode)settingInt); break;
                    case VideoDropdowns.RESOLUTION:    ApplyVideoSettings.SetResolutionPlaceholder(settingText);                                                                                break;
                    case VideoDropdowns.MONITOR:       ApplyVideoSettings.SetMonitorPlaceholder(settingText, settingInt);                                                                       break;
                    case VideoDropdowns.ANTI_ALIASING: ApplyVideoSettings.ApplyAntiAliasing(settingText, (AntiAliasType)settingInt);                                                            break;
                }

                VideoSettingSelect.CheckCloseVideoSettingsDropdowns();
            }

            public void SetSettingValues(int setDropdownInt, int setSettingInt, string setSettingText) {
                dropdownInt = setDropdownInt;
                settingInt  = setSettingInt;
                settingText = setSettingText;
            }
        }
    }
}