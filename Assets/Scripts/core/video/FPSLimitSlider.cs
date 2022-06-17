using UnityEngine;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Utilities;

namespace SomeAimGame.Core {
    namespace Video {
        public class FPSLimitSlider : MonoBehaviour {
            public Slider limitFPSSlider;
            public TMP_Text limitFPSValueText;
            public TMP_Text limitFPSPlaceholderText;

            public static float limitFPSValue;
            //private static bool FPSLimitChangeReady = false;

            private static FPSLimitSlider FPSLimit;
            void Awake() { FPSLimit = this; }
            
            // Init FPS Limit value slider with listener.
            void Start() { limitFPSSlider.onValueChanged.AddListener(delegate { SetFPSLimit(); }); }

            /// <summary>
            /// Sets FPS Limit with slider value. Also sets corresponding text, placeholder and slider with set value, then saves value with 'VideoSettings.SaveFPSLimitItem(limitFPSValue)'.
            /// </summary>
            public static void SetFPSLimit() {
                limitFPSValue = FPSLimit.limitFPSSlider.value;
                SetFPSLimitValueText(VideoSettingUtil.ReturnFPSLimitFromValue((int)limitFPSValue));
                //if (!FPSLimitChangeReady) { FPSLimitChangeReady = true; }
            }

            /// <summary>
            /// Sets FPS Limit text and placeholder to supplied value (limit).
            /// </summary>
            /// <param name="limit"></param>
            public static void SetFPSLimitValueText(int limit) {
                FPSLimit.limitFPSValueText.SetText($"{limit}");
                FPSLimit.limitFPSPlaceholderText.SetText($"{limit}");

                if (limit <= 0) {
                    Util.SetTextPlaceholderColors(FPSLimit.limitFPSValueText, FPSLimit.limitFPSPlaceholderText, InterfaceColors.unselectedColor);
                } else {
                    Util.SetTextPlaceholderColors(FPSLimit.limitFPSValueText, FPSLimit.limitFPSPlaceholderText, InterfaceColors.selectedColor);
                }
            }

            /// <summary>
            /// Sets FPS Limit slider to supplied value (limit).
            /// </summary>
            /// <param name="limit"></param>
            public static void SetFPSLimitSlider(float limit) { FPSLimit.limitFPSSlider.value = limit; }

            /// <summary>
            /// Checks if FPS Limit value changed, then saves value to VideoSettings.
            /// </summary>
            public static void CheckSaveFPSLimit() {
                // BUG: When checking if save is ready, sometimes value of fps is not saved? it saves instead null value of 0 every other time? IDK. Forcing save everytime keeps fps limit set.
                ApplyVideoSettings.ApplyFPSLimit(VideoSettingUtil.ReturnFPSLimitFromValue((int)limitFPSValue));
                VideoSettings.SaveFPSLimitItem((int)limitFPSValue);
                VideoSettings.SaveVideoSettings_Static();
                //FPSLimitChangeReady = false;
            }
        }
    }
}
