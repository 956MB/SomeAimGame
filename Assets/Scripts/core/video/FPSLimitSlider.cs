using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SomeAimGame.Core {
    namespace Video {
        public class FPSLimitSlider : MonoBehaviour {
            public Slider limitFPSSlider;
            public TMP_Text limitFPSValueText;
            public TMP_Text limitFPSPlaceholderText;

            public static float limitFPSValue;
            private static bool FPSLimitChangeReady = false;

            private static FPSLimitSlider FPSLimit;
            void Awake() { FPSLimit = this; }
            
            // Init FPS Limit value slider with listener.
            void Start() { limitFPSSlider.onValueChanged.AddListener(delegate { SetFPSLimit(); }); }

            /// <summary>
            /// Sets FPS Limit with slider value. Also sets corresponding text, placeholder and slider with set value, then saves value with 'VideoSettings.SaveFPSLimitItem(limitFPSValue)'.
            /// </summary>
            public static void SetFPSLimit() {
                limitFPSValue = FPSLimit.limitFPSSlider.value;
                SetFPSLimitValueText(limitFPSValue);
                // set fps limit
                if (!FPSLimitChangeReady) { FPSLimitChangeReady = true; }
            }

            /// <summary>
            /// Sets FPS Limit text and placeholder to supplied value (limit).
            /// </summary>
            /// <param name="limit"></param>
            public static void SetFPSLimitValueText(float limit) {
                FPSLimit.limitFPSValueText.SetText($"{limit}");
                FPSLimit.limitFPSPlaceholderText.SetText($"{limit}");
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
                if (FPSLimitChangeReady) {
                    ApplyVideoSettings.ApplyFPSLimit((int)limitFPSValue);
                    VideoSettings.SaveFPSLimitItem((int)limitFPSValue);
                    VideoSettings.SaveVideoSettings_Static();
                    FPSLimitChangeReady = false;
                }
            }
        }
    }
}
