using SomeAimGame.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairSaveSystem : MonoBehaviour {
    public Slider crosshairSizeSlider, crosshairThicknessSlider, crosshairGapSlider, crosshairOutlineSlider, crosshairRedSlider, crosshairGreenSlider, crosshairBlueSlider, crosshairAlphaSlider, crosshairRedOutlineSlider, crosshairGreenOutlineSlider, crosshairBlueOutlineSlider, crosshairAlphaOutlineSlider;
    public TMP_Text crosshairSizeValueText, crosshairThicknessValueText, crosshairGapValueText, crosshairOutlineValueText, crosshairRedValueText, crosshairGreenValueText, crosshairBlueValueText, crosshairAlphaValueText, crosshairRedOutlineValueText, crosshairGreenOutlineValueText, crosshairBlueOutlineValueText, crosshairAlphaOutlineValueText;
    public TMP_Text crosshairSizeValueTextPlaceholder, crosshairThicknessValueTextPlaceholder, crosshairGapValueTextPlaceholder, crosshairOutlineValueTextPlaceholder, crosshairRedValueTextPlaceholder, crosshairGreenValueTextPlaceholder, crosshairBlueValueTextPlaceholder, crosshairAlphaValueTextPlaceholder, crosshairRedOutlineValueTextPlaceholder, crosshairGreenOutlineValueTextPlaceholder, crosshairBlueOutlineValueTextPlaceholder, crosshairAlphaOutlineValueTextPlaceholder;
    public Toggle TStyleToggle, centerDotToggle, OutlineEnabledToggle;
    public SimpleCrosshair simpleCrosshair;

    private static CrosshairSaveSystem crosshairSave;
    void Awake() { crosshairSave = this; }

    private void Start() {
        if (simpleCrosshair == null) {
            Debug.LogError("You have not set the target SimpleCrosshair. Disabling!");
            enabled = false;
        }
    }

    /// <summary>
    /// Saves supplied crosshair object (CrosshairSettings) to file.
    /// </summary>
    /// <param name="crosshairSettings"></param>
    public static void SaveCrosshairSettingsData() {
        CrosshairDataSerial crosshairData = new CrosshairDataSerial();
        SaveLoadUtil.SaveDataSerial("/prefs", "/sag_crosshair.prefs", crosshairData);
    }

    /// <summary>
    /// Loads crosshair data (CrosshairDataSerial) from file.
    /// </summary>
    /// <returns></returns>
    public static CrosshairDataSerial LoadCrosshairSettingsData() {
        CrosshairDataSerial crosshairData = (CrosshairDataSerial)SaveLoadUtil.LoadDataSerial("/prefs/sag_crosshair.prefs", SaveType.CROSSHAIR);
        return crosshairData;
    }

    /// <summary>
    /// Inits saved crosshair object and sets all crosshair values.
    /// </summary>
    public static void InitSavedCrosshairSettings() {
        CrosshairDataSerial loadedCrosshairData = LoadCrosshairSettingsData();
        if (loadedCrosshairData != null) {
            //Debug.Log($"Crosshair string ON LOAD: {loadedCrosshairData.crosshairString}");

            if (!crosshairSave.simpleCrosshair.ParseCrosshairString(loadedCrosshairData.crosshairString, true, false)) {
                Debug.Log($"INVALID CROSSHAIR STRING: {loadedCrosshairData.crosshairString}");

                InitCrosshairSettingsDefaults();
            } else {
                SetAllCrosshairValues(loadedCrosshairData.TStyle, loadedCrosshairData.centerDot, loadedCrosshairData.size, loadedCrosshairData.thickness, loadedCrosshairData.gap, loadedCrosshairData.outlineEnabled, loadedCrosshairData.red, loadedCrosshairData.green, loadedCrosshairData.blue, loadedCrosshairData.alpha, loadedCrosshairData.outlineRed, loadedCrosshairData.outlineGreen, loadedCrosshairData.outlineBlue, loadedCrosshairData.outlineAlpha, true);
                SetAllCrosshairControls(loadedCrosshairData.TStyle, loadedCrosshairData.centerDot, loadedCrosshairData.size, loadedCrosshairData.thickness, loadedCrosshairData.gap, loadedCrosshairData.outlineEnabled, loadedCrosshairData.red, loadedCrosshairData.green, loadedCrosshairData.blue, loadedCrosshairData.alpha, loadedCrosshairData.outlineRed, loadedCrosshairData.outlineGreen, loadedCrosshairData.outlineBlue, loadedCrosshairData.outlineAlpha);

                CrosshairSettings.LoadCrosshairSettings(loadedCrosshairData);
            }
        } else {
            //Debug.Log("failed to init cosmetics in 'initSettingsDefaults', cosmetics: " + loadedCosmeticsData);
            InitCrosshairSettingsDefaults();
        }

        // Close crosshair import/export and presets panels on start.
        CrosshairImportExport.CloseImportExportPanel_Static();
        CrosshairPresets.ClosePresetsPanel_Static();
        CrosshairPresets.HidePresetsButton();
    }

    /// <summary>
    /// Inits default crosshair values and saves to file on first launch.
    /// </summary>
    public static void InitCrosshairSettingsDefaults() {
        //
        //     │
        //  ──   ──  Default Crosshair: 000601050255255255255000000000255
        //     │
        // 
        // 0        0          06    01         05   1        255  255    255   255    255         000           000          255
        // T-Style  CenterDot  Size  Thickness  Gap  Outline  Red  Green  Blue  Alpha  RedOutline  GreenOutline  BlueOutline  AlphaOutline

        SetAllCrosshairValues(false, false, 6f, 1f, 5f, true, 255f, 255f, 255f, 255f, 0f, 0f, 0f, 255f, false);
        SetAllCrosshairControls(false, false, 6f, 1f, 5f, true, 255f, 255f, 255f, 255f, 0f, 0f, 0f, 255f);

        crosshairSave.simpleCrosshair.GenerateCrosshair();

        // Saves defaults to new 'sag_crosshair.settings' file.
        CrosshairSettings.SaveAllCrosshairDefaults(false, false, 6f, 1f, 5f, true, 255f, 255f, 255f, 255f, 0f, 0f, 0f, 255f, "000601050255255255255000000000255");
    }

    /// <summary>
    /// Sets crosshair tstyle value and toggle to supplied bool (setTStyle), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setTStyle"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairTStyle(bool setTStyle, bool redraw) {              crosshairSave.simpleCrosshair.SetTStyle(setTStyle, redraw); }
    /// <summary>
    /// Sets crosshair center dot value and toggle to supplied bool (setCenterDot), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setCenterDot"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairCenterDot(bool setCenterDot, bool redraw) {        crosshairSave.simpleCrosshair.SetCenterDot(setCenterDot, redraw); }
    /// <summary>
    /// Sets crosshair outline enable value and toggle to supplied bool (setOutlineEnable), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setOutlineEnable"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairOutline(bool setOutlineEnable, bool redraw) {      crosshairSave.simpleCrosshair.SetOutlineEnabled(setOutlineEnable, redraw); }
    /// <summary>
    /// Sets crosshair size value, slider and text to supplied float (setSize), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setSize"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairSize(float setSize, bool redraw) {                 crosshairSave.simpleCrosshair.SetSize((int)setSize, redraw); }
    /// <summary>
    /// Sets crosshair thickness value, slider and text to supplied float (setThickness), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setThickness"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairThickness(float setThickness, bool redraw) {       crosshairSave.simpleCrosshair.SetThickness((int)setThickness, redraw); }
    /// <summary>
    /// Sets crosshair gap value, slider and text to supplied float (setGap), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setGap"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairGap(float setGap, bool redraw) {                   crosshairSave.simpleCrosshair.SetGap((int)setGap, redraw); }
    /// <summary>
    /// Sets crosshair red color value, slider and text to supplied float (setRed), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setRed"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairRed(float setRed, bool redraw) {                   crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.RED, (int)setRed, redraw); }
    /// <summary>
    /// Sets crosshair green color value, slider and text to supplied float (setGreen), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setGreen"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairGreen(float setGreen, bool redraw) {               crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.GREEN, (int)setGreen, redraw); }
    /// <summary>
    /// Sets crosshair blue color value, slider and text to supplied float (setBlue), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setBlue"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairBlue(float setBlue, bool redraw) {                 crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.BLUE, (int)setBlue, redraw); }
    /// <summary>
    /// Sets crosshair alpha value, slider and text to supplied float (setAlpha), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setAlpha"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairAlpha(float setAlpha, bool redraw) {               crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.ALPHA, (int)setAlpha, redraw); }
    /// <summary>
    /// Sets crosshair red outline color value, slider and text to supplied float (setRed), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setRedOutline"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairRedOutline(float setRedOutline, bool redraw) {     crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.RED, (int)setRedOutline, redraw); }
    /// <summary>
    /// Sets crosshair green outline color value, slider and text to supplied float (setGreen), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setGreenOutline"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairGreenOutline(float setGreenOutline, bool redraw) { crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.GREEN, (int)setGreenOutline, redraw); }
    /// <summary>
    /// Sets crosshair blue outline color value, slider and text to supplied float (setBlue), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setBlueOutline"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairBlueOutline(float setBlueOutline, bool redraw) {   crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.BLUE, (int)setBlueOutline, redraw); }
    /// <summary>
    /// Sets crosshair alpha outline value, slider and text to supplied float (setAlpha), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setAlphaOutline"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairAlphaOutline(float setAlphaOutline, bool redraw) { crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.ALPHA, (int)setAlphaOutline, redraw); }
    
    private static void SetAllCrosshairValues(bool setTStyle, bool setCenterDot, float setSize, float setThickness, float setGap, bool setOutline, float setRed, float setGreen, float setBlue, float setAlpha, float setRedOutline, float setGreenOutline, float setBlueOutline, float setAlphaOutline, bool redrawEnd) {
        SetCrosshairTStyle(setTStyle, false);
        SetCrosshairCenterDot(setCenterDot, false);
        SetCrosshairSize(setSize, false);
        SetCrosshairThickness(setThickness, false);
        SetCrosshairGap(setGap, false);
        SetCrosshairOutline(setOutline, false);
        SetCrosshairRed(setRed, false);
        SetCrosshairGreen(setGreen, false);
        SetCrosshairBlue(setBlue, false);
        SetCrosshairAlpha(setAlpha, false);
        SetCrosshairRedOutline(setRedOutline, false);
        SetCrosshairGreenOutline(setGreenOutline, false);
        SetCrosshairBlueOutline(setBlueOutline, false);
        SetCrosshairAlphaOutline(setAlphaOutline, redrawEnd);
    }

    /// <summary>
    /// Sets all crosshair options toggles and sliders to supplied values.
    /// </summary>
    /// <param name="setTStyle"></param>
    /// <param name="setCenterDot"></param>
    /// <param name="setSize"></param>
    /// <param name="setThickness"></param>
    /// <param name="setGap"></param>
    /// <param name="setOutline"></param>
    /// <param name="setRed"></param>
    /// <param name="setGreen"></param>
    /// <param name="setBlue"></param>
    /// <param name="setAlpha"></param>
    /// <param name="setRedOutline"></param>
    /// <param name="setGreenOutline"></param>
    /// <param name="setBlueOutline"></param>
    /// <param name="setAlphaOutline"></param>
    public static void SetAllCrosshairControls(bool setTStyle, bool setCenterDot, float setSize, float setThickness, float setGap, bool setOutline, float setRed, float setGreen, float setBlue, float setAlpha, float setRedOutline, float setGreenOutline, float setBlueOutline, float setAlphaOutline) {
        crosshairSave.TStyleToggle.isOn         = setTStyle;
        crosshairSave.centerDotToggle.isOn      = setCenterDot;
        crosshairSave.OutlineEnabledToggle.isOn = setOutline;
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairSizeSlider, crosshairSave.crosshairSizeValueText, crosshairSave.crosshairSizeValueTextPlaceholder, setSize);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairThicknessSlider, crosshairSave.crosshairThicknessValueText, crosshairSave.crosshairThicknessValueTextPlaceholder, setThickness);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairGapSlider, crosshairSave.crosshairGapValueText, crosshairSave.crosshairGapValueTextPlaceholder, setGap);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairRedSlider, crosshairSave.crosshairRedValueText, crosshairSave.crosshairRedValueTextPlaceholder, setRed);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairGreenSlider, crosshairSave.crosshairGreenValueText, crosshairSave.crosshairGreenValueTextPlaceholder, setGreen);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairBlueSlider, crosshairSave.crosshairBlueValueText, crosshairSave.crosshairBlueValueTextPlaceholder, setBlue);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairAlphaSlider, crosshairSave.crosshairAlphaValueText, crosshairSave.crosshairAlphaValueTextPlaceholder, setAlpha);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairRedOutlineSlider, crosshairSave.crosshairRedOutlineValueText, crosshairSave.crosshairRedOutlineValueTextPlaceholder, setRedOutline);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairGreenOutlineSlider, crosshairSave.crosshairGreenOutlineValueText, crosshairSave.crosshairGreenOutlineValueTextPlaceholder, setGreenOutline);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairBlueOutlineSlider, crosshairSave.crosshairBlueOutlineValueText, crosshairSave.crosshairBlueOutlineValueTextPlaceholder, setBlueOutline);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairAlphaOutlineSlider, crosshairSave.crosshairAlphaOutlineValueText, crosshairSave.crosshairAlphaOutlineValueTextPlaceholder, setAlphaOutline);
        CrosshairOptionsObject.SetOutlineContainerState(setOutline);
    }
}
