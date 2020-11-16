using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class CrosshairSaveSystem : MonoBehaviour {
    public Slider crosshairSizeSlider, crosshairThicknessSlider, crosshairGapSlider, crosshairOutlineSlider, crosshairRedSlider, crosshairGreenSlider, crosshairBlueSlider, crosshairAlphaSlider;
    public TMP_Text crosshairSizeValueText, crosshairThicknessValueText, crosshairGapValueText, crosshairOutlineValueText, crosshairRedValueText, crosshairGreenValueText, crosshairBlueValueText, crosshairAlphaValueText;
    public TMP_Text crosshairSizeValueTextPlaceholder, crosshairThicknessValueTextPlaceholder, crosshairGapValueTextPlaceholder, crosshairOutlineValueTextPlaceholder, crosshairRedValueTextPlaceholder, crosshairGreenValueTextPlaceholder, crosshairBlueValueTextPlaceholder, crosshairAlphaValueTextPlaceholder;
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
        string dirPath            = Application.persistentDataPath + "/settings";
        string filePath           = dirPath + "/crosshair.settings";

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
        string path = Application.persistentDataPath + "/settings/crosshair.settings";
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
            CrosshairSettings.LoadCrosshairSettings(loadedCrosshairData);

            SetCrosshairTStyleToggle(loadedCrosshairData.TStyle, true);
            SetCrosshairOutlineToggle(loadedCrosshairData.outlineEnabled, true);
            SetCrosshairOutlineThickness(loadedCrosshairData.outline, true);
            SetCrosshairCenterDotToggle(loadedCrosshairData.centerDot, true);
            SetCrosshairSizeSlider(loadedCrosshairData.size, true);
            SetCrosshairThicknessSlider(loadedCrosshairData.thickness, true);
            SetCrosshairGapSlider(loadedCrosshairData.gap, true);
            SetCrosshairOutlineSlider(loadedCrosshairData.outline, true);
            SetCrosshairRedSlider(loadedCrosshairData.red, true);
            SetCrosshairGreenSlider(loadedCrosshairData.green, true);
            SetCrosshairBlueSlider(loadedCrosshairData.blue, true);
            SetCrosshairAlphaSlider(loadedCrosshairData.alpha, true);

            //CrosshairOptionsObject.LoadNewCrosshairString("100602051255200050255");
            //CrosshairOptionsObject.ExportCurrentCrosshairString();
        }
        else {
            //Debug.Log("failed to init cosmetics in 'initSettingsDefaults', cosmetics: " + loadedCosmeticsData);
            InitSettingsDefaults();
        }
    }

    /// <summary>
    /// Inits default crosshair values and saves to file on first launch.
    /// </summary>
    public static void InitSettingsDefaults() {
        // Crosshair toggles.
        crosshairSave.TStyleToggle.isOn         = false;
        crosshairSave.centerDotToggle.isOn      = false;
        crosshairSave.OutlineEnabledToggle.isOn = true;

        // All crosshair values.
        crosshairSave.simpleCrosshair.SetTStyle(false, false);
        crosshairSave.simpleCrosshair.SetCenterDot(false, false);
        crosshairSave.simpleCrosshair.SetOutlineEnabled(true, false);
        crosshairSave.simpleCrosshair.SetOutlineThickness(1, false);
        crosshairSave.simpleCrosshair.SetSize(9, false);
        crosshairSave.simpleCrosshair.SetThickness(2, false);
        crosshairSave.simpleCrosshair.SetGap(5, false);
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.RED, 255, false);
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.GREEN, 255, false);
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.BLUE, 255, false);
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.ALPHA, 255, false);
        crosshairSave.simpleCrosshair.GenerateCrosshair();

        // Sets all default crosshair values to their text placeholders.
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairOutlineValueText, crosshairSave.crosshairOutlineValueTextPlaceholder, 1f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairSizeValueText, crosshairSave.crosshairSizeValueTextPlaceholder, 9f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairThicknessValueText, crosshairSave.crosshairThicknessValueTextPlaceholder, 2f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairGapValueText, crosshairSave.crosshairGapValueTextPlaceholder, 5f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairRedValueText, crosshairSave.crosshairRedValueTextPlaceholder, 255f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairGreenValueText, crosshairSave.crosshairGreenValueTextPlaceholder, 255f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairBlueValueText, crosshairSave.crosshairBlueValueTextPlaceholder, 255f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairAlphaValueText, crosshairSave.crosshairAlphaValueTextPlaceholder, 255f);

        // Sets all default crosshair values to their toggles/sliders.
        CrosshairOptionsObject.SetCrosshairOptionToggle(crosshairSave.centerDotToggle, false);
        CrosshairOptionsObject.SetCrosshairOptionToggle(crosshairSave.OutlineEnabledToggle, true);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairOutlineSlider, 1f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairSizeSlider, 9f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairThicknessSlider, 2f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairGapSlider, 5f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairRedSlider, 255f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairGreenSlider, 255f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairBlueSlider, 255f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairAlphaSlider, 255f);

        // Saves defaults to new 'crosshair.settings' file.
        CrosshairSettings.SaveAllCrosshairDefaults(false, false, 9f, 2f, 5f, true, 1f, 255f, 255f, 255f, 255f);
    }

    /// <summary>
    /// Sets crosshair tstyle value and toggle to supplied bool (setTStyle), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setTStyle"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairTStyleToggle(bool setTStyle, bool redraw) {
        crosshairSave.simpleCrosshair.SetTStyle(setTStyle, redraw);
        crosshairSave.TStyleToggle.isOn = setTStyle;
        CrosshairSettings.TStyle        = setTStyle;
    }
    /// <summary>
    /// Sets crosshair center dot value and toggle to supplied bool (setCenterDot), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setCenterDot"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairCenterDotToggle(bool setCenterDot, bool redraw) {
        crosshairSave.simpleCrosshair.SetCenterDot(setCenterDot, redraw);
        crosshairSave.centerDotToggle.isOn = setCenterDot;
        CrosshairSettings.centerDot        = setCenterDot;
    }
    /// <summary>
    /// Sets crosshair outline enable value and toggle to supplied bool (setOutlineEnable), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setOutlineEnable"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairOutlineToggle(bool setOutlineEnable, bool redraw) {
        crosshairSave.simpleCrosshair.SetOutlineEnabled(setOutlineEnable, redraw);
        crosshairSave.OutlineEnabledToggle.isOn = setOutlineEnable;
        CrosshairSettings.outlineEnabled        = setOutlineEnable;
    }
    /// <summary>
    /// Sets crosshair thickness value to supplied float (setOutlineThickness), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setOutlineThickness"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairOutlineThickness(float setOutlineThickness, bool redraw) {
        crosshairSave.simpleCrosshair.SetOutlineThickness((int)setOutlineThickness, redraw);
        CrosshairSettings.outline = setOutlineThickness;
    }
    /// <summary>
    /// Sets crosshair size value, slider and text to supplied float (setSize), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setSize"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairSizeSlider(float setSize, bool redraw) {
        crosshairSave.simpleCrosshair.SetSize((int)setSize, redraw);
        crosshairSave.crosshairSizeSlider.value = setSize;
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairSizeValueText, crosshairSave.crosshairSizeValueTextPlaceholder, setSize);
        CrosshairSettings.size = setSize;
    }
    /// <summary>
    /// Sets crosshair thickness value, slider and text to supplied float (setThickness), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setThickness"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairThicknessSlider(float setThickness, bool redraw) {
        crosshairSave.simpleCrosshair.SetThickness((int)setThickness, redraw);
        crosshairSave.crosshairThicknessSlider.value = setThickness;
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairThicknessValueText, crosshairSave.crosshairThicknessValueTextPlaceholder, setThickness);
        CrosshairSettings.thickness = setThickness;
    }
    /// <summary>
    /// Sets crosshair gap value, slider and text to supplied float (setGap), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setGap"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairGapSlider(float setGap, bool redraw) {
        crosshairSave.simpleCrosshair.SetGap((int)setGap, redraw);
        crosshairSave.crosshairGapSlider.value = setGap;
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairGapValueText, crosshairSave.crosshairGapValueTextPlaceholder, setGap);
        CrosshairSettings.gap = setGap;
    }
    public static void SetCrosshairOutlineSlider(float setOutline, bool redraw) {
        crosshairSave.crosshairOutlineSlider.value = setOutline;
        CrosshairSettings.outline = setOutline;
    }
    /// <summary>
    /// Sets crosshair red color value, slider and text to supplied float (setRed), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setRed"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairRedSlider(float setRed, bool redraw) {
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.RED, (int)setRed, redraw);
        crosshairSave.crosshairRedSlider.value = setRed;
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairRedValueText, crosshairSave.crosshairRedValueTextPlaceholder, setRed);
        CrosshairSettings.red = setRed;
    }
    /// <summary>
    /// Sets crosshair green color value, slider and text to supplied float (setGreen), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setGreen"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairGreenSlider(float setGreen, bool redraw) {
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.GREEN, (int)setGreen, redraw);
        crosshairSave.crosshairGreenSlider.value = setGreen;
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairGreenValueText, crosshairSave.crosshairGreenValueTextPlaceholder, setGreen);
        CrosshairSettings.green = setGreen;
    }
    /// <summary>
    /// Sets crosshair blue color value, slider and text to supplied float (setBlue), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setBlue"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairBlueSlider(float setBlue, bool redraw) {
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.BLUE, (int)setBlue, redraw);
        crosshairSave.crosshairBlueSlider.value = setBlue;
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairBlueValueText, crosshairSave.crosshairBlueValueTextPlaceholder, setBlue);
        CrosshairSettings.blue = setBlue;
    }
    /// <summary>
    /// Sets crosshair alpha value, slider and text to supplied float (setAlpha), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setAlpha"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairAlphaSlider(float setAlpha, bool redraw) {
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.ALPHA, (int)setAlpha, redraw);
        crosshairSave.crosshairAlphaSlider.value = setAlpha;
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairAlphaValueText, crosshairSave.crosshairAlphaValueTextPlaceholder, setAlpha);
        CrosshairSettings.alpha = setAlpha;
    }
}
