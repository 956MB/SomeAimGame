using UnityEngine;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Utilities;

public class CrosshairOptionsObject : MonoBehaviour {
    public Slider crosshairSizeSlider, crosshairThicknessSlider, crosshairGapSlider, crosshairOutlineSlider, crosshairRedSlider, crosshairGreenSlider, crosshairBlueSlider, crosshairAlphaSlider, crosshairRedOutlineSlider, crosshairGreenOutlineSlider, crosshairBlueOutlineSlider, crosshairAlphaOutlineSlider;

    public TMP_Text crosshairSizeValueText, crosshairThicknessValueText, crosshairGapValueText, crosshairOutlineValueText, crosshairRedValueText, crosshairGreenValueText, crosshairBlueValueText, crosshairAlphaValueText, crosshairRedOutlineValueText, crosshairGreenOutlineValueText, crosshairBlueOutlineValueText, crosshairAlphaOutlineValueText;
    public TMP_Text crosshairSizeValueTextPlaceholder, crosshairThicknessValueTextPlaceholder, crosshairGapValueTextPlaceholder, crosshairOutlineValueTextPlaceholder, crosshairRedValueTextPlaceholder, crosshairGreenValueTextPlaceholder, crosshairBlueValueTextPlaceholder, crosshairAlphaValueTextPlaceholder, crosshairRedOutlineValueTextPlaceholder, crosshairGreenOutlineValueTextPlaceholder, crosshairBlueOutlineValueTextPlaceholder, crosshairAlphaOutlineValueTextPlaceholder;

    private static float crosshairOutlineThicknessValue, crosshairSizeValue, crosshairThicknessValue, crosshairGapValue, crosshairOutlineValue, crosshairRedValue, crosshairGreenValue, crosshairBlueValue, crosshairAlphaValue, crosshairRedOutlineValue, crosshairGreenOutlineValue, crosshairBlueOutlineValue, crosshairAlphaOutlineValue;

    public CanvasGroup outlineContainerCanvasGroup;

    public static bool crossahairSaveReady = false;

    public SimpleCrosshair simpleCrosshair;

    private static CrosshairOptionsObject crosshairOptions;
    void Awake() { crosshairOptions = this; }

    void Start() {
        if (simpleCrosshair == null) {
            Debug.LogError("You have not set the target SimpleCrosshair. Disabling.");
            enabled = false;
        }

        // Init all crosshair value sliders with listeners.
        crosshairSizeSlider.onValueChanged.AddListener(delegate { SetCrosshairSizeValue(); });
        crosshairThicknessSlider.onValueChanged.AddListener(delegate { SetThicknessSizeValue(); });
        crosshairGapSlider.onValueChanged.AddListener(delegate { SetGapSizeValue(); });
        crosshairRedSlider.onValueChanged.AddListener(delegate { SetRedValue(); });
        crosshairGreenSlider.onValueChanged.AddListener(delegate { SetGreenValue(); });
        crosshairBlueSlider.onValueChanged.AddListener(delegate { SetBlueValue(); });
        crosshairAlphaSlider.onValueChanged.AddListener(delegate { SetAlphaValue(); });
        crosshairRedOutlineSlider.onValueChanged.AddListener(delegate { SetRedOutlineValue(); });
        crosshairGreenOutlineSlider.onValueChanged.AddListener(delegate { SetGreenOutlineValue(); });
        crosshairBlueOutlineSlider.onValueChanged.AddListener(delegate { SetBlueOutlineValue(); });
        crosshairAlphaOutlineSlider.onValueChanged.AddListener(delegate { SetAlphaOutlineValue(); });
    }

    //    CrosshairSettings.SaveOutline(crosshairOutlineThicknessValue);
    //}
    /// <summary>
    /// Sets crosshair size from slider, then saves value.
    /// </summary>
    public static void SetCrosshairSizeValue() {
        crosshairSizeValue = crosshairOptions.crosshairSizeSlider.value;
        crosshairOptions.simpleCrosshair.SetSize((int)crosshairSizeValue, true);
        SetCrosshairOptionText(crosshairOptions.crosshairSizeValueText, crosshairOptions.crosshairSizeValueTextPlaceholder, crosshairSizeValue);

        CrosshairSettings.SaveSize(crosshairSizeValue);
        if (!crossahairSaveReady) { crossahairSaveReady = true; }
    }
    /// <summary>
    /// Sets crosshair thickness from slider, then saves value.
    /// </summary>
    public static void SetThicknessSizeValue() {
        crosshairThicknessValue = crosshairOptions.crosshairThicknessSlider.value;
        crosshairOptions.simpleCrosshair.SetThickness((int)crosshairThicknessValue, true);
        SetCrosshairOptionText(crosshairOptions.crosshairThicknessValueText, crosshairOptions.crosshairThicknessValueTextPlaceholder, crosshairThicknessValue);

        CrosshairSettings.SaveThickness(crosshairThicknessValue);
        if (!crossahairSaveReady) { crossahairSaveReady = true; }
    }
    /// <summary>
    /// Sets crosshair gap size from slider, then saves value.
    /// </summary>
    public static void SetGapSizeValue() {
        crosshairGapValue = crosshairOptions.crosshairGapSlider.value;
        crosshairOptions.simpleCrosshair.SetGap((int)crosshairGapValue, true);
        SetCrosshairOptionText(crosshairOptions.crosshairGapValueText, crosshairOptions.crosshairGapValueTextPlaceholder, crosshairGapValue);

        CrosshairSettings.SaveGap(crosshairGapValue);
        if (!crossahairSaveReady) { crossahairSaveReady = true; }
    }

    /// <summary>
    /// Sets crosshair red color from slider, then saves value.
    /// </summary>
    public static void SetRedValue() {
        crosshairRedValue = crosshairOptions.crosshairRedSlider.value;
        crosshairOptions.simpleCrosshair.SetColor(CrosshairColorChannel.RED, (int)crosshairRedValue, true);
        SetCrosshairOptionText(crosshairOptions.crosshairRedValueText, crosshairOptions.crosshairRedValueTextPlaceholder, crosshairRedValue);

        CrosshairSettings.SaveRed(crosshairRedValue);
        if (!crossahairSaveReady) { crossahairSaveReady = true; }
    }
    /// <summary>
    /// Sets crosshair green color from slider, then saves value.
    /// </summary>
    public static void SetGreenValue() {
        crosshairGreenValue = crosshairOptions.crosshairGreenSlider.value;
        crosshairOptions.simpleCrosshair.SetColor(CrosshairColorChannel.GREEN, (int)crosshairGreenValue, true);
        SetCrosshairOptionText(crosshairOptions.crosshairGreenValueText, crosshairOptions.crosshairGreenValueTextPlaceholder, crosshairGreenValue);

        CrosshairSettings.SaveGreen(crosshairGreenValue);
        if (!crossahairSaveReady) { crossahairSaveReady = true; }
    }
    /// <summary>
    /// Sets crosshair blue color from slider, then saves value.
    /// </summary>
    public static void SetBlueValue() {
        crosshairBlueValue = crosshairOptions.crosshairBlueSlider.value;
        crosshairOptions.simpleCrosshair.SetColor(CrosshairColorChannel.BLUE, (int)crosshairBlueValue, true);
        SetCrosshairOptionText(crosshairOptions.crosshairBlueValueText, crosshairOptions.crosshairBlueValueTextPlaceholder, crosshairBlueValue);

        CrosshairSettings.SaveBlue(crosshairBlueValue);
        if (!crossahairSaveReady) { crossahairSaveReady = true; }
    }
    /// <summary>
    /// Sets crosshair alpha from slider, then saves value.
    /// </summary>
    public static void SetAlphaValue() {
        crosshairAlphaValue = crosshairOptions.crosshairAlphaSlider.value;
        crosshairOptions.simpleCrosshair.SetColor(CrosshairColorChannel.ALPHA, (int)crosshairAlphaValue, true);
        SetCrosshairOptionText(crosshairOptions.crosshairAlphaValueText, crosshairOptions.crosshairAlphaValueTextPlaceholder, crosshairAlphaValue);

        CrosshairSettings.SaveAlpha(crosshairAlphaValue);
        if (!crossahairSaveReady) { crossahairSaveReady = true; }
    }

    /// <summary>
    /// Sets crosshair red outline color from slider, then saves value.
    /// </summary>
    public static void SetRedOutlineValue() {
        crosshairRedOutlineValue = crosshairOptions.crosshairRedOutlineSlider.value;
        crosshairOptions.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.RED, (int)crosshairRedOutlineValue, true);
        SetCrosshairOptionText(crosshairOptions.crosshairRedOutlineValueText, crosshairOptions.crosshairRedOutlineValueTextPlaceholder, crosshairRedOutlineValue);

        CrosshairSettings.SaveRedOutline(crosshairRedOutlineValue);
        if (!crossahairSaveReady) { crossahairSaveReady = true; }
    }
    /// <summary>
    /// Sets crosshair green outline color from slider, then saves value.
    /// </summary>
    public static void SetGreenOutlineValue() {
        crosshairGreenOutlineValue = crosshairOptions.crosshairGreenOutlineSlider.value;
        crosshairOptions.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.GREEN, (int)crosshairGreenOutlineValue, true);
        SetCrosshairOptionText(crosshairOptions.crosshairGreenOutlineValueText, crosshairOptions.crosshairGreenOutlineValueTextPlaceholder, crosshairGreenOutlineValue);

        CrosshairSettings.SaveGreenOutline(crosshairGreenOutlineValue);
        if (!crossahairSaveReady) { crossahairSaveReady = true; }
    }
    /// <summary>
    /// Sets crosshair blue outline color from slider, then saves value.
    /// </summary>
    public static void SetBlueOutlineValue() {
        crosshairBlueOutlineValue = crosshairOptions.crosshairBlueOutlineSlider.value;
        crosshairOptions.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.BLUE, (int)crosshairBlueOutlineValue, true);
        SetCrosshairOptionText(crosshairOptions.crosshairBlueOutlineValueText, crosshairOptions.crosshairBlueOutlineValueTextPlaceholder, crosshairBlueOutlineValue);

        CrosshairSettings.SaveBlueOutline(crosshairBlueOutlineValue);
        if (!crossahairSaveReady) { crossahairSaveReady = true; }
    }
    /// <summary>
    /// Sets crosshair alpha outline from slider, then saves value.
    /// </summary>
    public static void SetAlphaOutlineValue() {
        crosshairAlphaOutlineValue = crosshairOptions.crosshairAlphaOutlineSlider.value;
        crosshairOptions.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.ALPHA, (int)crosshairAlphaOutlineValue, true);
        SetCrosshairOptionText(crosshairOptions.crosshairAlphaOutlineValueText, crosshairOptions.crosshairAlphaOutlineValueTextPlaceholder, crosshairAlphaOutlineValue);

        CrosshairSettings.SaveAlphaOutline(crosshairAlphaOutlineValue);
        if (!crossahairSaveReady) { crossahairSaveReady = true; }
    }

    /// <summary>
    /// Sets crosshair outline options container state to 'disabled' or 'enabled'.
    /// </summary>
    /// <param name="outlineContainerEnabled"></param>
    public static void SetOutlineContainerState(bool outlineContainerEnabled) {
        Util.CanvasGroupState(crosshairOptions.outlineContainerCanvasGroup, outlineContainerEnabled);
    }

    /// <summary>
    /// Saves entire crosshair object to crosshair.settings if 'crossahairSaveReady' set true.
    /// </summary>
    public static void SaveCrosshairObject(bool overrideSave = false) {
        if (crossahairSaveReady || overrideSave) {
            //Debug.Log($"New string:               {crosshairOptions.simpleCrosshair.ExportCrosshairString()}");
            //SimpleCrosshair.SetCrosshairString_Static();
            CrosshairSettings.SaveCrosshairSettings_Static();
            crossahairSaveReady = false;
        }
    }

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
}
