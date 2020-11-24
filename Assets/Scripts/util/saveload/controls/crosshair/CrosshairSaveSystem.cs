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

            if (!crosshairSave.simpleCrosshair.ParseCrosshairString(loadedCrosshairData.crosshairString, false)) {
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

            //CrosshairOptionsObject.LoadNewCrosshairString("100602051255200050255");
            //CrosshairOptionsObject.ExportCurrentCrosshairString();
        }
        else {
            //Debug.Log("failed to init cosmetics in 'initSettingsDefaults', cosmetics: " + loadedCosmeticsData);
            InitCrosshairSettingsDefaults();
        }
    }

    /// <summary>
    /// Inits default crosshair values and saves to file on first launch.
    /// </summary>
    public static void InitCrosshairSettingsDefaults() {
        // All crosshair values.
        crosshairSave.simpleCrosshair.SetTStyle(false, false);
        crosshairSave.simpleCrosshair.SetCenterDot(false, false);
        crosshairSave.simpleCrosshair.SetOutlineEnabled(true, false);
        crosshairSave.simpleCrosshair.SetOutlineThickness(1, false);
        crosshairSave.simpleCrosshair.SetSize(6, false);
        crosshairSave.simpleCrosshair.SetThickness(1, false);
        crosshairSave.simpleCrosshair.SetGap(5, false);
        // Crosshair color
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.RED, 255, false);
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.GREEN, 255, false);
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.BLUE, 255, false);
        crosshairSave.simpleCrosshair.SetColor(CrosshairColorChannel.ALPHA, 255, false);
        // Crosshair outline color
        crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.RED, 0, false);
        crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.GREEN, 0, false);
        crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.BLUE, 0, false);
        crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.ALPHA, 255, false);

        crosshairSave.simpleCrosshair.GenerateCrosshair();

        // Sets all default crosshair values to their text placeholders.
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairOutlineValueText, crosshairSave.crosshairOutlineValueTextPlaceholder, 1f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairSizeValueText, crosshairSave.crosshairSizeValueTextPlaceholder, 6f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairThicknessValueText, crosshairSave.crosshairThicknessValueTextPlaceholder, 1f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairGapValueText, crosshairSave.crosshairGapValueTextPlaceholder, 5f);
        // Crosshair color
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairRedValueText, crosshairSave.crosshairRedValueTextPlaceholder, 255f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairGreenValueText, crosshairSave.crosshairGreenValueTextPlaceholder, 255f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairBlueValueText, crosshairSave.crosshairBlueValueTextPlaceholder, 255f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairAlphaValueText, crosshairSave.crosshairAlphaValueTextPlaceholder, 255f);
        // Crosshair outline color
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairRedOutlineValueText, crosshairSave.crosshairRedOutlineValueTextPlaceholder, 0f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairGreenOutlineValueText, crosshairSave.crosshairGreenOutlineValueTextPlaceholder, 0f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairBlueOutlineValueText, crosshairSave.crosshairBlueOutlineValueTextPlaceholder, 0f);
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairAlphaOutlineValueText, crosshairSave.crosshairAlphaOutlineValueTextPlaceholder, 255f);

        // Sets all default crosshair values to their toggles/sliders.
        CrosshairOptionsObject.SetCrosshairOptionToggle(crosshairSave.TStyleToggle, false);
        CrosshairOptionsObject.SetCrosshairOptionToggle(crosshairSave.centerDotToggle, false);
        CrosshairOptionsObject.SetCrosshairOptionToggle(crosshairSave.OutlineEnabledToggle, true);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairOutlineSlider, 1f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairSizeSlider, 6f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairThicknessSlider, 1f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairGapSlider, 5f);
        // Crosshair color
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairRedSlider, 255f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairGreenSlider, 255f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairBlueSlider, 255f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairAlphaSlider, 255f);
        // Crosshair outline color
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairRedOutlineSlider, 0f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairGreenOutlineSlider, 0f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairBlueOutlineSlider, 0f);
        CrosshairOptionsObject.SetCrosshairOptionSlider(crosshairSave.crosshairAlphaOutlineSlider, 255f);

        CrosshairOptionsObject.SetOutlineContainerState(true);

        // Saves defaults to new 'crosshair.settings' file.
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
        crosshairSave.crosshairSizeSlider.value = setSize;
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairSizeValueText, crosshairSave.crosshairSizeValueTextPlaceholder, setSize);
        CrosshairSettings.SaveSize(setSize);
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
        CrosshairSettings.SaveThickness(setThickness);
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
        CrosshairSettings.SaveGap(setGap);
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
        CrosshairSettings.SaveRed(setRed);
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
        CrosshairSettings.SaveGreen(setGreen);
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
        CrosshairSettings.SaveBlue(setBlue);
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
        CrosshairSettings.SaveAlpha(setAlpha);
    }
    /// <summary>
    /// Sets crosshair red outline color value, slider and text to supplied float (setRed), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setRedOutline"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairRedOutlineSlider(float setRedOutline, bool redraw) {
        crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.RED, (int)setRedOutline, redraw);
        crosshairSave.crosshairRedOutlineSlider.value = setRedOutline;
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairRedOutlineValueText, crosshairSave.crosshairRedOutlineValueTextPlaceholder, setRedOutline);
        CrosshairSettings.SaveRedOutline(setRedOutline);
    }
    /// <summary>
    /// Sets crosshair green outline color value, slider and text to supplied float (setGreen), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setGreenOutline"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairGreenOutlineSlider(float setGreenOutline, bool redraw) {
        crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.GREEN, (int)setGreenOutline, redraw);
        crosshairSave.crosshairGreenOutlineSlider.value = setGreenOutline;
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairGreenOutlineValueText, crosshairSave.crosshairGreenOutlineValueTextPlaceholder, setGreenOutline);
        CrosshairSettings.SaveGreenOutline(setGreenOutline);
    }
    /// <summary>
    /// Sets crosshair blue outline color value, slider and text to supplied float (setBlue), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setBlueOutline"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairBlueOutlineSlider(float setBlueOutline, bool redraw) {
        crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.BLUE, (int)setBlueOutline, redraw);
        crosshairSave.crosshairBlueOutlineSlider.value = setBlueOutline;
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairBlueOutlineValueText, crosshairSave.crosshairBlueOutlineValueTextPlaceholder, setBlueOutline);
        CrosshairSettings.SaveBlueOutline(setBlueOutline);
    }
    /// <summary>
    /// Sets crosshair alpha outline value, slider and text to supplied float (setAlpha), and redraws crosshair if bool true (redraw).
    /// </summary>
    /// <param name="setAlphaOutline"></param>
    /// <param name="redraw"></param>
    public static void SetCrosshairAlphaOutlineSlider(float setAlphaOutline, bool redraw) {
        crosshairSave.simpleCrosshair.SetOutlineColor(CrosshairColorChannel.ALPHA, (int)setAlphaOutline, redraw);
        crosshairSave.crosshairAlphaOutlineSlider.value = setAlphaOutline;
        CrosshairOptionsObject.SetCrosshairOptionText(crosshairSave.crosshairAlphaOutlineValueText, crosshairSave.crosshairAlphaOutlineValueTextPlaceholder, setAlphaOutline);
        CrosshairSettings.SaveAlphaOutline(setAlphaOutline);
    }
}
