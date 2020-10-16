using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrosshairOptionsObjectInput : MonoBehaviour {
    public Slider crosshairSizeSlider, crosshairThicknessSlider, crosshairGapSlider, crosshairOutlineSlider, crosshairRedSlider, crosshairGreenSlider, crosshairBlueSlider, crosshairAlphaSlider;
    public Toggle centerDotToggle, OutlineEnabledToggle;

    public TMP_Text crosshairSizeValueText, crosshairThicknessValueText, crosshairGapValueText, crosshairOutlineValueText, crosshairRedValueText, crosshairGreenValueText, crosshairBlueValueText, crosshairAlphaValueText;
    public TMP_Text crosshairSizeValueTextPlaceholder, crosshairThicknessValueTextPlaceholder, crosshairGapValueTextPlaceholder, crosshairOutlineValueTextPlaceholder, crosshairRedValueTextPlaceholder, crosshairGreenValueTextPlaceholder, crosshairBlueValueTextPlaceholder, crosshairAlphaValueTextPlaceholder;

    public SimpleCrosshair simpleCrosshair;
    private void Start() {
        if (simpleCrosshair == null) {
            Debug.LogError("You have not set the target SimpleCrosshair. Disabling.");
            enabled = false;
        }
    }

    private static CrosshairOptionsObjectInput crosshairOptionsInput;
    void Awake() { crosshairOptionsInput = this; }

    /// <summary>
    /// Sets supplied option toggle (optionToggle) on/off from supplied (value).
    /// </summary>
    /// <param name="optionToggle"></param>
    /// <param name="value"></param>
    public static void SetCrosshairOptionToggle(Toggle optionToggle, bool value) { optionToggle.isOn = value; }
    /// <summary>
    /// Sets supplied option slider (optionSlider) value from supplied (value).
    /// </summary>
    /// <param name="optionSlider"></param>
    /// <param name="value"></param>
    public static void SetCrosshairOptionSlider(Slider optionSlider, float value) { optionSlider.value = value; }
    /// <summary>
    /// Sets supplied crosshair element text (valueText) and placeholder text (valueTextPlaceholder) to (value).
    /// </summary>
    /// <param name="valueText"></param>
    /// <param name="valueTextPlaceholder"></param>
    /// <param name="value"></param>
    public static void SetCrosshairOptionText(TMP_Text valueText, TMP_Text valueTextPlaceholder, float value) {
        valueText.SetText($"{value}");
        valueTextPlaceholder.SetText($"{value}");
    }

    /// <summary>
    /// Sets crosshair size from string value, then saves value.
    /// </summary>
    /// <param name="optionValue"></param>
    public void SetSaveCrosshairOptionSize_Input(string optionValue) {
        if (optionValue != "" && optionValue != " " && optionValue != "  ") {
            float newOptionValue = float.Parse(optionValue);

            if (newOptionValue >= 1f && newOptionValue <= 45f) {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairSizeValueText, crosshairOptionsInput.crosshairSizeValueTextPlaceholder, newOptionValue);
                crosshairSizeSlider.value = newOptionValue;

                simpleCrosshair.SetSize((int)newOptionValue, true);
                CrosshairSettings.SaveSize(newOptionValue);
            } else {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairSizeValueText, crosshairOptionsInput.crosshairSizeValueTextPlaceholder, CrosshairSettings.size);
                crosshairSizeSlider.value = CrosshairSettings.size;
            }
        }
    }
    /// <summary>
    /// Sets crosshair thickness from string value, then saves value.
    /// </summary>
    /// <param name="optionValue"></param>
    public void SetSaveCrosshairOptionThickness_Input(string optionValue) {
        if (optionValue != "" && optionValue != " " && optionValue != "  ") {
            float newOptionValue = float.Parse(optionValue);

            if (newOptionValue >= 1f && newOptionValue <= 15f) {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairThicknessValueText, crosshairOptionsInput.crosshairThicknessValueTextPlaceholder, newOptionValue);
                crosshairThicknessSlider.value = newOptionValue;

                simpleCrosshair.SetThickness((int)newOptionValue, true);
                CrosshairSettings.SaveThickness(newOptionValue);
            } else {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairThicknessValueText, crosshairOptionsInput.crosshairThicknessValueTextPlaceholder, CrosshairSettings.thickness);
                crosshairThicknessSlider.value = CrosshairSettings.thickness;
            }
        }
    }
    /// <summary>
    /// Sets crosshair gap from string value, then saves value.
    /// </summary>
    /// <param name="optionValue"></param>
    public void SetSaveCrosshairOptionGap_Input(string optionValue) {
        if (optionValue != "" && optionValue != " " && optionValue != "  ") {
            float newOptionValue = float.Parse(optionValue);

            if (newOptionValue >= 0f && newOptionValue <= 25f) {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairGapValueText, crosshairOptionsInput.crosshairGapValueTextPlaceholder, newOptionValue);
                crosshairGapSlider.value = newOptionValue;

                simpleCrosshair.SetGap((int)newOptionValue, true);
                CrosshairSettings.SaveGap(newOptionValue);
            } else {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairGapValueText, crosshairOptionsInput.crosshairGapValueTextPlaceholder, CrosshairSettings.gap);
                crosshairGapSlider.value = CrosshairSettings.gap;
            }
        }
    }
    /// <summary>
    /// Sets crosshair outline from string value, then saves value.
    /// </summary>
    /// <param name="optionValue"></param>
    public void SetSaveCrosshairOptionOutline_Input(string optionValue) {
        if (optionValue != "" && optionValue != " " && optionValue != "  ") {
            float newOptionValue = float.Parse(optionValue);
            SetCrosshairOptionText(crosshairOptionsInput.crosshairOutlineValueText, crosshairOptionsInput.crosshairOutlineValueTextPlaceholder, newOptionValue);
            crosshairOutlineSlider.value = newOptionValue;

            CrosshairSettings.SaveOutline(newOptionValue);
        }
    }
    /// <summary>
    /// Sets crosshair red color from string value, then saves value.
    /// </summary>
    /// <param name="optionValue"></param>
    public void SetSaveCrosshairOptionRed_Input(string optionValue) {
        if (optionValue != "" && optionValue != " " && optionValue != "  " && optionValue != "   ") {
            float newOptionValue = float.Parse(optionValue);

            if (newOptionValue >= 0f && newOptionValue <= 255f) {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairRedValueText, crosshairOptionsInput.crosshairRedValueTextPlaceholder, newOptionValue);
                crosshairRedSlider.value = newOptionValue;

                simpleCrosshair.SetColor(CrosshairColorChannel.RED, (int)newOptionValue, true);
                CrosshairSettings.SaveRed(newOptionValue);
            } else {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairRedValueText, crosshairOptionsInput.crosshairRedValueTextPlaceholder, CrosshairSettings.red);
                crosshairRedSlider.value = CrosshairSettings.red;
            }
        }
    }
    /// <summary>
    /// Sets crosshair green color from string value, then saves value.
    /// </summary>
    /// <param name="optionValue"></param>
    public void SetSaveCrosshairOptionGreen_Input(string optionValue) {
        if (optionValue != "" && optionValue != " " && optionValue != "  " && optionValue != "   ") {
            float newOptionValue = float.Parse(optionValue);

            if (newOptionValue >= 0f && newOptionValue <= 255f) {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairGreenValueText, crosshairOptionsInput.crosshairGreenValueTextPlaceholder, newOptionValue);
                crosshairGreenSlider.value = newOptionValue;

                simpleCrosshair.SetColor(CrosshairColorChannel.GREEN, (int)newOptionValue, true);
                CrosshairSettings.SaveGreen(newOptionValue);
            } else {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairGreenValueText, crosshairOptionsInput.crosshairGreenValueTextPlaceholder, CrosshairSettings.green);
                crosshairGreenSlider.value = CrosshairSettings.green;
            }
        }
    }
    /// <summary>
    /// Sets crosshair blue color from string value, then saves value.
    /// </summary>
    /// <param name="optionValue"></param>
    public void SetSaveCrosshairOptionBlue_Input(string optionValue) {
        if (optionValue != "" && optionValue != " " && optionValue != "  " && optionValue != "   ") {
            float newOptionValue = float.Parse(optionValue);
            
            if (newOptionValue >= 0f && newOptionValue <= 255f) {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairBlueValueText, crosshairOptionsInput.crosshairBlueValueTextPlaceholder, newOptionValue);
                crosshairBlueSlider.value = newOptionValue;

                simpleCrosshair.SetColor(CrosshairColorChannel.BLUE, (int)newOptionValue, true);
                CrosshairSettings.SaveBlue(newOptionValue);
            } else {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairBlueValueText, crosshairOptionsInput.crosshairBlueValueTextPlaceholder, CrosshairSettings.blue);
                crosshairBlueSlider.value = CrosshairSettings.blue;
            }
        }
    }
    /// <summary>
    /// Sets crosshair alpha from string value, then saves value.
    /// </summary>
    /// <param name="optionValue"></param>
    public void SetSaveCrosshairOptionAlpha_Input(string optionValue) {
        if (optionValue != "" && optionValue != " " && optionValue != "  " && optionValue != "   ") {
            float newOptionValue = float.Parse(optionValue);
            
            if (newOptionValue >= 0f && newOptionValue <= 255f) {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairAlphaValueText, crosshairOptionsInput.crosshairAlphaValueTextPlaceholder, newOptionValue);
                crosshairAlphaSlider.value = newOptionValue;

                simpleCrosshair.SetColor(CrosshairColorChannel.ALPHA, (int)newOptionValue, true);
                CrosshairSettings.SaveAlpha(newOptionValue);
            } else {
                SetCrosshairOptionText(crosshairOptionsInput.crosshairAlphaValueText, crosshairOptionsInput.crosshairAlphaValueTextPlaceholder, CrosshairSettings.alpha);
                crosshairAlphaSlider.value = CrosshairSettings.alpha;
            }
        }
    }
}
