using UnityEngine;
using TMPro;

using SomeAimGame.Utilities;
using SomeAimGame.SFX;

namespace SomeAimGame.Core {
    public class TimerSelect : MonoBehaviour {
        public TMP_Text timerText, arrowText;
        public GameObject timerSelectDropdownObject, timerSelectContainerObject, timerRestartButton;
        public static bool timerSelectOpen                 = false;
        public static bool timerSelectDisabled             = false;
        public static bool timerSelectRestartButtonEnabled = false;
        
        public static TimerSelect timerSelect;
        private void Awake() { timerSelect = this; }

        void Start() {
            SetTimerDropdownText(ExtraSettings.gameTimer);
            CloseTimerSelect_Static();
            DisableTimerRestartButton();
        }

        /// <summary>
        /// Sets timer text in settings UI.
        /// </summary>
        /// <param name="langCode"></param>
        public static void SetTimerDropdownText(int timerNum) {
            timerSelect.timerText.SetText($"{ReturnTimerTextFull(timerNum)}");
        }

        /// <summary>
        /// Non statiic method that toggles timer select scrollview on button press.
        /// </summary>
        public void ToggleTimerSelect() { ToggleTimerSelect_Static(); }

        /// <summary>
        /// Toggles timer select scrollview.
        /// </summary>
        public static void ToggleTimerSelect_Static() {
            LanguageSelect.CheckCloseLanguageSelect();
            TargetSoundSelect.CheckCloseTargetSoundDropdowns();

            if (!timerSelectOpen) {
                OpenTimerSelect_Static();
                SFXManager.CheckPlayClick_Button();
            } else {
                CloseTimerSelect_Static();
            }
        }

        /// <summary>
        /// Opens timer select scrollview.
        /// </summary>
        public static void OpenTimerSelect_Static() {
            timerSelect.timerSelectDropdownObject.transform.localScale = DropdownUtils.dropdownBodyOpenScale;
            Util.RefreshRootLayoutGroup(timerSelect.timerSelectDropdownObject);
            timerSelect.arrowText.transform.localScale = DropdownUtils.arrowupScale;
            timerSelectOpen = true;
        }

        /// <summary>
        /// Closes timer select scrollview.
        /// </summary>
        public static void CloseTimerSelect_Static() {
            timerSelect.timerSelectDropdownObject.transform.localScale = DropdownUtils.dropdownBodyClosedScale;
            timerSelect.arrowText.transform.localScale = DropdownUtils.arrowDownScale;
            timerSelectOpen = false;
        }

        public static void EnableTimerRestartButton() {
            timerSelect.timerRestartButton.SetActive(true);
            Util.RefreshRootLayoutGroup(timerSelect.timerSelectContainerObject);
            timerSelectRestartButtonEnabled = true;
        }

        public static void DisableTimerRestartButton() {
            timerSelect.timerRestartButton.SetActive(false);
            Util.RefreshRootLayoutGroup(timerSelect.timerSelectContainerObject);
            timerSelectRestartButtonEnabled = false;
        }

        /// <summary>
        /// Closes timer select if selection panel open.
        /// </summary>
        public static void CheckCloseTimerSelect() {
            if (timerSelectOpen) { CloseTimerSelect_Static(); }
        }

        public static string ReturnTimerTextFull(int timerNum) {
            switch (timerNum) {
                case 30:  return I18nTextTranslator.SetTranslatedText("30timertext");
                case 60:  return I18nTextTranslator.SetTranslatedText("60timertext");
                case 90:  return I18nTextTranslator.SetTranslatedText("90timertext");
                case 120: return I18nTextTranslator.SetTranslatedText("120timertext");
                default:  return I18nTextTranslator.SetTranslatedText("30timertext");
            }
        }
    }
}