using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

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
    public static void SaveCrosshairSettingsData(CrosshairSettings crosshairSettings) {
        BinaryFormatter formatter = new BinaryFormatter();
        string dirPath            = Application.persistentDataPath + "/prefs";
        string filePath           = dirPath + "/sag_crosshair.prefs";

        DirectoryInfo dirInf = new DirectoryInfo(dirPath);
        if (!dirInf.Exists) { dirInf.Create(); }

        FileStream stream                 = new FileStream(filePath, FileMode.Create);
        CrosshairDataSerial crosshairData = new CrosshairDataSerial();
        formatter.Serialize(stream, crosshairData);
        stream.Close();
    }

    /// <summary>
    /// Loads crosshair data (CrosshairDataSerial) from file.
    /// </summary>
    /// <returns></returns>
    public static CrosshairDataSerial LoadCrosshairSettingsData() {
        string path = Application.persistentDataPath + "/prefs/sag_crosshair.prefs";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream         = new FileStream(path, FileMode.Open);

            CrosshairDataSerial crosshairData = formatter.Deserialize(stream) as CrosshairDataSerial;
            stream.Close();

            return crosshairData;
        } else {
            return null;
        }
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
                SetCrosshairTStyleToggle(loadedCrosshairData.TStyle, true);
                SetCrosshairOutlineToggle(loadedCrosshairData.outlineEnabled, true);
                SetCrosshairCenterDotToggle(loadedCrosshairData.centerDot, true);
                SetCrosshairSizeSlider(loadedCrosshairData.size, true);
                SetCrosshairThicknessSlider(loadedCrosshairData.thickness, true);
                SetCrosshairGapSlider(loadedCrosshairData.gap, true);
                // Crosshair color
                SetCrosshairRedSlider(loadedCrosshairData.red, true);
                SetCrosshairGreenSlider(loadedCrosshairData.green, true);
                SetCrosshairBlueSlider(loadedCrosshairData.blue, true);
                SetCrosshairAlphaSlider(loadedCrosshairData.alpha, true);
                // Crosshair outline color
                SetCrosshairRedOutlineSlider(loadedCrosshairData.outlineRed, true);
                SetCrosshairGreenOutlineSlider(loadedCrosshairData.outlineGreen, true);
                SetCrosshairBlueOutlineSlider(loadedCrosshairData.outlineBlue, true);
                SetCrosshairAlphaOutlineSlider(loadedCrosshairData.outlineAlpha, true);

                CrosshairOptionsObject.SetOutlineContainerState(loadedCrosshairData.outlineEnabled);
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
        // Default Crosshair:
        //
        // 0        0          06    01         05   1        255  255    255   255    255         000           000          255
        // T-Style  CenterDot  Size  Thickness  Gap  Outline  Red  Green  Blue  Alpha  RedOutline  GreenOutline  BlueOutline  AlphaOutline

        SetCrosshairTStyleToggle(false, false);
        SetCrosshairCenterDotToggle(false, false);
        SetCrosshairSizeSlider(6f, false);
        SetCrosshairThicknessSlider(1f, false);
        SetCrosshairGapSlider(5, false);
        SetCrosshairOutlineToggle(true, false);
        // Crosshair color
        SetCrosshairRedSlider(255f, false);
        SetCrosshairGreenSlider(255f, false);
        SetCrosshairBlueSlider(255f, false);
        SetCrosshairAlphaSlider(255f, false);
        // Crosshair outline color
        SetCrosshairRedOutlineSlider(0f, false);
        SetCrosshairGreenOutlineSlider(0f, false);
        SetCrosshairBlueOutlineSlider(0f, false);
        SetCrosshairAlphaOutlineSlider(255f, false);

        crosshairSave.simpleCrosshair.GenerateCrosshair();

        CrosshairOptionsObject.SetOutlineContainerState(true);

        // Saves defaults to new 'sag_crosshair.settings' file.
        CrosshairSettings.SaveAllCrosshairDefaults(false, false, 6f, 1f, 5f, true, 255f, 255f, 255f, 255f, 0f, 0f, 0f, 255f, "000601050255255255255000000000255");
    }

    /// <summary>
    /// Sets crosshair tstyle value and toggle to supplied bool (setTStyle), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setTStyle"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairTStyleToggle(bool setTStyle, bool redraw) {
        crosshairSave.simpleCrosshair.SetTStyle(setTStyle, redraw);
        crosshairSave.TStyleToggle.isOn = setTStyle;
        CrosshairSettings.SaveTStyle(setTStyle);
    }
    /// <summary>
    /// Sets crosshair center dot value and toggle to supplied bool (setCenterDot), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setCenterDot"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairCenterDotToggle(bool setCenterDot, bool redraw) {
        crosshairSave.simpleCrosshair.SetCenterDot(setCenterDot, redraw);
        crosshairSave.centerDotToggle.isOn = setCenterDot;
        CrosshairSettings.SaveCenterDot(setCenterDot);
    }
    /// <summary>
    /// Sets crosshair outline enable value and toggle to supplied bool (setOutlineEnable), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setOutlineEnable"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairOutlineToggle(bool setOutlineEnable, bool redraw) {
        crosshairSave.simpleCrosshair.SetOutlineEnabled(setOutlineEnable, redraw);
        crosshairSave.OutlineEnabledToggle.isOn = setOutlineEnable;
        CrosshairSettings.SaveOutlineEnabled(setOutlineEnable);
    }
    /// <summary>
    /// Sets crosshair size value, slider and text to supplied float (setSize), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setSize"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairSizeSlider(float setSize, bool redraw) {
        crosshairSave.simpleCrosshair.SetSize((int)setSize, redraw);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairSizeSlider, crosshairSave.crosshairSizeValueText, crosshairSave.crosshairSizeValueTextPlaceholder, setSize);
        CrosshairSettings.SaveSize(setSize);
    }
    /// <summary>
    /// Sets crosshair thickness value, slider and text to supplied float (setThickness), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setThickness"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairThicknessSlider(float setThickness, bool redraw) {
        crosshairSave.simpleCrosshair.SetThickness((int)setThickness, redraw);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairThicknessSlider, crosshairSave.crosshairThicknessValueText, crosshairSave.crosshairThicknessValueTextPlaceholder, setThickness);
        CrosshairSettings.SaveThickness(setThickness);
    }
    /// <summary>
    /// Sets crosshair gap value, slider and text to supplied float (setGap), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setGap"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairGapSlider(float setGap, bool redraw) {
        crosshairSave.simpleCrosshair.SetGap((int)setGap, redraw);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairGapSlider, crosshairSave.crosshairGapValueText, crosshairSave.crosshairGapValueTextPlaceholder, setGap);
        CrosshairSettings.SaveGap(setGap);
    }
    /// <summary>
    /// Sets crosshair red color value, slider and text to supplied float (setRed), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setRed"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairRedSlider(float setRed, bool redraw) {
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.RED, (int)setRed, redraw);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairRedSlider, crosshairSave.crosshairRedValueText, crosshairSave.crosshairRedValueTextPlaceholder, setRed);
        CrosshairSettings.SaveRed(setRed);
    }
    /// <summary>
    /// Sets crosshair green color value, slider and text to supplied float (setGreen), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setGreen"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairGreenSlider(float setGreen, bool redraw) {
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.GREEN, (int)setGreen, redraw);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairGreenSlider, crosshairSave.crosshairGreenValueText, crosshairSave.crosshairGreenValueTextPlaceholder, setGreen);
        CrosshairSettings.SaveGreen(setGreen);
    }
    /// <summary>
    /// Sets crosshair blue color value, slider and text to supplied float (setBlue), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setBlue"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairBlueSlider(float setBlue, bool redraw) {
        //Debug.Log("set normal blue here");
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.BLUE, (int)setBlue, redraw);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairBlueSlider, crosshairSave.crosshairBlueValueText, crosshairSave.crosshairBlueValueTextPlaceholder, setBlue);
        CrosshairSettings.SaveBlue(setBlue);
    }
    /// <summary>
    /// Sets crosshair alpha value, slider and text to supplied float (setAlpha), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setAlpha"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairAlphaSlider(float setAlpha, bool redraw) {
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.ALPHA, (int)setAlpha, redraw);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairAlphaSlider, crosshairSave.crosshairAlphaValueText, crosshairSave.crosshairAlphaValueTextPlaceholder, setAlpha);
        CrosshairSettings.SaveAlpha(setAlpha);
    }
    /// <summary>
    /// Sets crosshair red outline color value, slider and text to supplied float (setRed), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setRedOutline"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairRedOutlineSlider(float setRedOutline, bool redraw) {
        crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.RED, (int)setRedOutline, redraw);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairRedOutlineSlider, crosshairSave.crosshairRedOutlineValueText, crosshairSave.crosshairRedOutlineValueTextPlaceholder, setRedOutline);
        CrosshairSettings.SaveRedOutline(setRedOutline);
    }
    /// <summary>
    /// Sets crosshair green outline color value, slider and text to supplied float (setGreen), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setGreenOutline"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairGreenOutlineSlider(float setGreenOutline, bool redraw) {
        crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.GREEN, (int)setGreenOutline, redraw);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairGreenOutlineSlider, crosshairSave.crosshairGreenOutlineValueText, crosshairSave.crosshairGreenOutlineValueTextPlaceholder, setGreenOutline);
        CrosshairSettings.SaveGreenOutline(setGreenOutline);
    }
    /// <summary>
    /// Sets crosshair blue outline color value, slider and text to supplied float (setBlue), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setBlueOutline"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairBlueOutlineSlider(float setBlueOutline, bool redraw) {
        crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.BLUE, (int)setBlueOutline, redraw);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairBlueOutlineSlider, crosshairSave.crosshairBlueOutlineValueText, crosshairSave.crosshairBlueOutlineValueTextPlaceholder, setBlueOutline);
        CrosshairSettings.SaveBlueOutline(setBlueOutline);
    }
    /// <summary>
    /// Sets crosshair alpha outline value, slider and text to supplied float (setAlpha), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setAlphaOutline"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairAlphaOutlineSlider(float setAlphaOutline, bool redraw) {
        crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.ALPHA, (int)setAlphaOutline, redraw);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairAlphaOutlineSlider, crosshairSave.crosshairAlphaOutlineValueText, crosshairSave.crosshairAlphaOutlineValueTextPlaceholder, setAlphaOutline);
        CrosshairSettings.SaveAlphaOutline(setAlphaOutline);
    }

    public static void SetAllCrosshairControls(bool setTStyle, bool setCenterDot, float setSize, float setThickness, float setGap, bool setOutline, float setRed, float setGreen, float setBlue, float setAlpha, float setRedOutline, float setGreenOutline, float setBlueOutline, float setAlphaOutline) {
        //Debug.Log("set all here");
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
