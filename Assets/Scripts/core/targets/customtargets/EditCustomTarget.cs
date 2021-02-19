using UnityEngine;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Utilities;

namespace SomeAimGame.Targets {
    public class EditCustomTarget : MonoBehaviour {
        public Slider targetRedSlider, targetGreenSlider, targetBlueSlider;
        public static string customTargetName;
        public TMP_Text targetRedPlaceholderText, targetGreenPlaceholderText, targetBluePlaceholderText;
        public TMP_Text targetRedValueText, targetGreenValueText, targetBlueValueText;
        public TMP_InputField customTargetNameInput;
        public GameObject saveButton;
        //public Image newTargetButtonBorder;

        public static float customRedValue, customGreenValue, customBlueValue;

        public static bool newTargetChangeMade;

        private static EditCustomTarget editCustomTarget;
        void Awake() { editCustomTarget = this; }

        private void Start() {
            targetRedSlider.onValueChanged.AddListener(delegate { SetTargetRedValue(); });
            targetGreenSlider.onValueChanged.AddListener(delegate { SetTargetGreenValue(); });
            targetBlueSlider.onValueChanged.AddListener(delegate { SetTargetBlueValue(); });
           
            newTargetChangeMade = false;
            SetSaveButtonState(false);
            ResetCustomTargetValues();
        }

        /// <summary>
        /// Changes value of custom target color red.
        /// </summary>
        private void SetTargetRedValue() {   ColorValueChange(ref customRedValue, targetRedSlider.value, targetRedPlaceholderText, targetRedValueText); }
        /// <summary>
        /// Changes value of custom target color green.
        /// </summary>
        private void SetTargetGreenValue() { ColorValueChange(ref customGreenValue, targetGreenSlider.value, targetGreenPlaceholderText, targetGreenValueText); }
        /// <summary>
        /// Changes value of custom target color blue.
        /// </summary>
        private void SetTargetBlueValue() {  ColorValueChange(ref customBlueValue, targetBlueSlider.value, targetBluePlaceholderText, targetBlueValueText); }

        /// <summary>
        /// Handles color change for custom target and sets slider, text and save button state.
        /// </summary>
        /// <param name="changedValue"></param>
        /// <param name="setNewValue"></param>
        /// <param name="placeholderText"></param>
        /// <param name="valueText"></param>
        private void ColorValueChange(ref float changedValue, float setNewValue, TMP_Text placeholderText, TMP_Text valueText) {
            if (!newTargetChangeMade) {
                newTargetChangeMade = true;
                SetSaveButtonState(true);
            }
            
            changedValue = setNewValue;
            Util.SetSliderOptionText(placeholderText, valueText, changedValue);

            SetNewTargetBorderColor();
            CheckStartingValues();
        }

        /// <summary>
        /// Saves color values of rgba to new custom target color.
        /// </summary>
        public static void SaveNewCustomTarget() {
            string newColorString = ColorUtility.ToHtmlStringRGBA(ReturnCustomColor());
            CosmeticsSettings.SaveCustomTarget(customTargetName, newColorString);
            // TODO: keep custom target button in group after save
            CancelNewCustomTarget();
            CustomTargetPanel.CloseCustomTargetPanel();
        }
        /// <summary>
        /// Cancels new custom target color and resets everything to default state.
        /// </summary>
        public static void CancelNewCustomTarget() {
            newTargetChangeMade = false;
            SetSaveButtonState(false);
            editCustomTarget.ResetCustomTargetValues();
        }

        /// <summary>
        /// Resets state of all color value sliders and save button if all color values return to default 255f.
        /// </summary>
        private void CheckStartingValues() {
            if (customRedValue == 255f && customGreenValue == 255f && customBlueValue == 255f) {
                newTargetChangeMade = false;
                SetSaveButtonState(false);
            }
        }

        #region setters/getters
        
        public static void SetCustomTargetName(string nameString, bool setText = false) {
            customTargetName = nameString;
            if (setText) { editCustomTarget.customTargetNameInput.text = $"{nameString}"; }
        }

        /// <summary>
        /// Sets state of save button in custom target panel to supplied bool (state).
        /// </summary>
        /// <param name="state"></param>
        public static void SetSaveButtonState(bool state) { Util.SetCanvasGroupState_DisableHover(editCustomTarget.saveButton.GetComponent<CanvasGroup>(), state); }

        /// <summary>
        /// Sets color of placeholder custom target button border to color values red/green/blue/alpha.
        /// </summary>
        private void SetNewTargetBorderColor() {
            CustomTargetPanel.newCreatedCustomTargetButton.transform.GetChild(0).GetComponent<Image>().color = ReturnCustomColor();
        }

        public static Color ReturnCustomColor() {
            //Color32 newSetColor32 = new Color32((byte)customRedValue, (byte)customGreenValue, (byte)customBlueValue, (byte)customAlphaValue);
            return new Color(customRedValue/255f, customGreenValue/255f, customBlueValue/255f, 255f/255f);
        }

        /// <summary>
        /// Resets all color values of custom target to default 255f.
        /// </summary>
        private void ResetCustomTargetValues() {
            customRedValue = customGreenValue = customBlueValue = 255f;
            targetRedSlider.value   = 255f;
            targetGreenSlider.value = 255f;
            targetBlueSlider.value  = 255f;
        }

        #endregion
    }
}
