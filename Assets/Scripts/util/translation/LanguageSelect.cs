using UnityEngine;
using TMPro;

using SomeAimGame.Utilities;
using SomeAimGame.SFX;

namespace SomeAimGame.Core {
    public class LanguageSelect : MonoBehaviour {
        public TMP_Text langText, arrowText;
        public GameObject languageSelectDropdownObject, languageSelectContainerObject, languageSelectRestartButton;
        public static bool languageSelectOpen     = false;
        public static bool languageSelectDisabled = false;
        public static bool languageSelectRestartButtonEnabled = false;

        public static LanguageSelect langSelect;
        private void Awake() { langSelect = this; }

        void Start() {
            SetLanguageCodeText(LanguageSetting.activeLanguageCode);
            CloseLanguageSelect_Static();
            DisableLanguageRestartButton();
        }

        /// <summary>
        /// Sets language code text in settings UI.
        /// </summary>
        /// <param name="langCode"></param>
        public static void SetLanguageCodeText(string langCode) {
            langSelect.langText.SetText($"{ReturnLanguageTextFull(langCode)}");
        }

        /// <summary>
        /// Non statiic method that toggles language select scrollview on button press.
        /// </summary>
        public void ToggleLanguageSelect() { ToggleLanguageSelect_Static(); }

        /// <summary>
        /// Toggles language select scrollview.
        /// </summary>
        public static void ToggleLanguageSelect_Static() {
            TimerSelect.CheckCloseTimerSelect();
            TargetSoundSelect.CheckCloseTargetSoundDropdowns();

            if (!languageSelectOpen) {
                OpenLanguageSelect_Static();
                SFXManager.CheckPlayClick_Button();
            } else {
                CloseLanguageSelect_Static();
            }
        }

        /// <summary>
        /// Opens language select scrollview.
        /// </summary>
        public static void OpenLanguageSelect_Static() {
            langSelect.languageSelectDropdownObject.transform.localScale = DropdownUtils.dropdownBodyOpenScale;
            Util.RefreshRootLayoutGroup(langSelect.languageSelectDropdownObject);
            langSelect.arrowText.transform.localScale = DropdownUtils.arrowupScale;
            languageSelectOpen = true;
        }

        /// <summary>
        /// Closes language select scrollview.
        /// </summary>
        public static void CloseLanguageSelect_Static() {
            langSelect.languageSelectDropdownObject.transform.localScale = DropdownUtils.dropdownBodyClosedScale;
            langSelect.arrowText.transform.localScale = DropdownUtils.arrowDownScale;
            languageSelectOpen = false;
        }

        public static void EnableLanguageRestartButton() {
            langSelect.languageSelectRestartButton.SetActive(true);
            Util.RefreshRootLayoutGroup(langSelect.languageSelectContainerObject);
            languageSelectRestartButtonEnabled = true;
        }

        public static void DisableLanguageRestartButton() {
            langSelect.languageSelectRestartButton.SetActive(false);
            Util.RefreshRootLayoutGroup(langSelect.languageSelectContainerObject);
            languageSelectRestartButtonEnabled = false;
        }

        /// <summary>
        /// Closes language select if selection panel open.
        /// </summary>
        public static void CheckCloseLanguageSelect() {
            if (languageSelectOpen) { CloseLanguageSelect_Static(); }
        }

        public static string ReturnLanguageTextFull(string langCode) {
            switch (langCode) {
                case "EN": return "English (United States)";
                case "RU": return "Pусский (Россия)";
                case "FI": return "Suomalainen (Suomi)";
                case "KO": return "한국어 (대한민국)";
                case "JA": return "日本人 (日本)";
                default:   return "English (United States)";
            }
        }
    }
}
