using UnityEngine;
using TMPro;

using SomeAimGame.Utilities;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSettingSelect : MonoBehaviour {
            public TMP_Text displayModeText, resolutionText, monitorText, antiAliasingText;
            public TMP_Text displayModeArrowText, resolutionArrowText, monitorArrowText, antiAliasingArrowText;
            public GameObject displayModeDropdownBody, resolutionDropdownBody, monitorDropdownBody, antiAliasingDropdownBody;
            public CanvasGroup settingsSaveContainer;

            public static bool displayModeSelectOpen  = false;
            public static bool resolutionSelectOpen   = false;
            public static bool monitorSelectOpen      = false;
            public static bool antiAliasingSelectOpen = false;
            public static bool videoSettingsSaveReady = false;

            public static VideoSettingSelect videoSettingsSelect;
            private void Awake() { videoSettingsSelect = this; }

            private void Start() {
                VideoSettingsSaveSystem.InitSavedVideoSettings();

                VideoDropdownManager.InitAllVideoSettingsDropdowns();
                CheckCloseVideoSettingsDropdowns(true);
                SetVideoSettingsSaveContainerState(false);
                
                // set video settings
            }

            /// <summary>
            /// Closes any/all open video setting dropdowns.
            /// </summary>
            public static void CheckCloseVideoSettingsDropdowns(bool overrideClose = false) {
                if (displayModeSelectOpen || overrideClose) {  SetDisplayModeSelect(false); }
                if (resolutionSelectOpen || overrideClose) {   SetResolutionSelect(false); }
                if (monitorSelectOpen || overrideClose) {      SetMonitorSelect(false); }
                if (antiAliasingSelectOpen || overrideClose) { SetAntiAliasingSelect(false); }
            }

            // Sets individual video setting dropdown text with supplied string.
            public static void SetDisplayModeText(string setDisplayMode) {   videoSettingsSelect.displayModeText.SetText($"{setDisplayMode}"); }
            public static void SetResolutionText(string setResoluton) {      videoSettingsSelect.resolutionText.SetText($"{setResoluton}"); }
            public static void SetMonitorText(string setMonitor) {           videoSettingsSelect.monitorText.SetText($"{setMonitor}"); }
            public static void SetAntiAliasingText(string setAntiAliasing) { videoSettingsSelect.antiAliasingText.SetText($"{setAntiAliasing}"); }

            public static void ToggleDisplayModeSelect_Static() {  SetDisplayModeSelect(!displayModeSelectOpen); }
            public static void ToggleResolutionSelect_Static() {   SetResolutionSelect(!resolutionSelectOpen); }
            public static void ToggleMonitorSelect_Static() {      SetMonitorSelect(!monitorSelectOpen); }
            public static void ToggleAntiAliasingSelect_Static() { SetAntiAliasingSelect(!antiAliasingSelectOpen); }

            public static void SetVideoSettingsSaveContainerState(bool videoSettingsSaveState) {
                Util.SetCanvasGroupState(videoSettingsSelect.settingsSaveContainer, videoSettingsSaveState);
            }

            /// <summary>
            /// Sets state (opensed/closed) of Display Mode dropdown.
            /// </summary>
            /// <param name="state"></param>
            public static void SetDisplayModeSelect(bool state) {
                DropdownUtils.SetDropdownState(state, videoSettingsSelect.displayModeDropdownBody, videoSettingsSelect.displayModeArrowText, ref displayModeSelectOpen);
            }
            /// <summary>
            /// Sets state (opensed/closed) of Resolution dropdown.
            /// </summary>
            /// <param name="state"></param>
            public static void SetResolutionSelect(bool state) {
                DropdownUtils.SetDropdownState(state, videoSettingsSelect.resolutionDropdownBody, videoSettingsSelect.resolutionArrowText, ref resolutionSelectOpen);
            }
            /// <summary>
            /// Sets state (opensed/closed) of Monitor dropdown.
            /// </summary>
            /// <param name="state"></param>
            public static void SetMonitorSelect(bool state) {
                DropdownUtils.SetDropdownState(state, videoSettingsSelect.monitorDropdownBody, videoSettingsSelect.monitorArrowText, ref monitorSelectOpen);
            }
            /// <summary>
            /// Sets state (opensed/closed) of Anti-Aliasing dropdown.
            /// </summary>
            /// <param name="state"></param>
            public static void SetAntiAliasingSelect(bool state) {
                DropdownUtils.SetDropdownState(state, videoSettingsSelect.antiAliasingDropdownBody, videoSettingsSelect.antiAliasingArrowText, ref antiAliasingSelectOpen);
            }
        }
    }
}
