using UnityEngine;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSettingsButtonActions : MonoBehaviour {
            /// <summary>
            /// Button click action to apply video settings.
            /// </summary>
            public void ApplyVideoSettings_Action() {
                ApplyVideoSettings.ApplyAllVideoSettings();
            }
            /// <summary>
            /// Button click action to revert video settings.
            /// </summary>
            public void RevertVideoSettings_Action() {
                ApplyVideoSettings.RevertVideoSettings();
            }
        }
    }
}
