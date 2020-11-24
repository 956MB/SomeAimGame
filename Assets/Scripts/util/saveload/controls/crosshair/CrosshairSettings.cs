using UnityEngine;

public class CrosshairSettings : MonoBehaviour {
    public static bool  centerDot        = false;
    public static bool  TStyle           = false;
    public static float size             = 6f;
    public static float thickness        = 1f;
    public static float gap              = 5f;
    public static bool  outlineEnabled   = true;
    public static float red              = 255f;
    public static float green            = 255f;
    public static float blue             = 255f;
    public static float alpha            = 255f;
    public static float outlineRed       = 0f;
    public static float outlineGreen     = 0f;
    public static float outlineBlue      = 0f;
    public static float outlineAlpha     = 255f;
    public static string crosshairString = "000601050255255255255000000000255";

    private static CrosshairSettings crosshairSettings;
    void Awake() { crosshairSettings = this; }

    /// <summary>
    /// Saves supplied center dot bool (setCenterDot) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setCenterDot"></param>
    public static void SaveCenterDot(bool setCenterDot) { centerDot = setCenterDot; }
    /// <summary>
    /// Saves supplied t-style bool (setTStyle) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setTStyle"></param>
    public static void SaveTStyle(bool setTStyle) { TStyle = setTStyle; }
    /// <summary>
    /// Saves supplied size float (setSize) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setSize"></param>
    public static void SaveSize(float setSize) { size = setSize; }
    /// <summary>
    /// Saves supplied thickness float (setThickness) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setThickness"></param>
    public static void SaveThickness(float setThickness) { thickness = setThickness; }
    /// <summary>
    /// Saves supplied gap float (setGap) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setGap"></param>
    public static void SaveGap(float setGap) { gap = setGap; }
    /// <summary>
    /// Saves supplied outline enabled bool (setOutlineEnabled) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setOutlineEnabled"></param>
    public static void SaveOutlineEnabled(bool setOutlineEnabled) { outlineEnabled = setOutlineEnabled; }
    /// <summary>
    /// Saves supplied red color float (setRed) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setRed"></param>
    public static void SaveRed(float setRed) { red = setRed; }
    /// <summary>
    /// Saves supplied green color float (setGreen) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setGreen"></param>
    public static void SaveGreen(float setGreen) { green = setGreen; }
    /// <summary>
    /// Saves supplied blue color float (setBlue) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setBlue"></param>
    public static void SaveBlue(float setBlue) { blue = setBlue; }
    /// <summary>
    /// Saves supplied alpha float (setAlpha) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setAlpha"></param>
    public static void SaveAlpha(float setAlpha) { alpha = setAlpha; }
    /// <summary>
    /// Saves supplied red outline color float (setRedOutline) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setRedOutline"></param>
    public static void SaveRedOutline(float setRedOutline) { outlineRed = setRedOutline; }
    /// <summary>
    /// Saves supplied green outline color float (setGreenOutline) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setGreenOutline"></param>
    public static void SaveGreenOutline(float setGreenOutline) { outlineGreen = setGreenOutline; }
    /// <summary>
    /// Saves supplied blue outline color float (setBlueOutline) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setBlueOutline"></param>
    public static void SaveBlueOutline(float setBlueOutline) { outlineBlue = setBlueOutline; }
    /// <summary>
    /// Saves supplied alpha outline float (setAlphaOutline) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setAlphaOutline"></param>
    public static void SaveAlphaOutline(float setAlphaOutline) { outlineAlpha = setAlphaOutline; }

    public static void SaveCrosshairString(string newCrosshairString) { crosshairString = newCrosshairString; }

    /// <summary>
    /// Calls 'CrosshairSaveSystem.SaveCrosshairItem()' to save crosshair settings object (CrosshairSettings) to file.
    /// </summary>
    public void SaveCrosshairSettings() { CrosshairSaveSystem.SaveCrosshairSettingsData(this); }

    public static void SaveCrosshairSettings_Static() { crosshairSettings.SaveCrosshairSettings(); }

    /// <summary>
    /// Saves default crosshair settings object (CrosshairSettings).
    /// </summary>
    /// <param name="setCenterDot"></param>
    /// <param name="setTStyle"></param>
    /// <param name="setSize"></param>
    /// <param name="setThickness"></param>
    /// <param name="setGap"></param>
    /// <param name="setOutlineEnabled"></param>
    /// <param name="setRed"></param>
    /// <param name="setGreen"></param>
    /// <param name="setBlue"></param>
    /// <param name="setAlpha"></param>
    /// <param name="setRedOutline"></param>
    /// <param name="setGreenOutline"></param>
    /// <param name="setBlueOutline"></param>
    /// <param name="setAlphaOutline"></param>
    public static void SaveAllCrosshairDefaults(bool setCenterDot, bool setTStyle, float setSize, float setThickness, float setGap, bool setOutlineEnabled, float setRed, float setGreen, float setBlue, float setAlpha, float setRedOutline, float setGreenOutline, float setBlueOutline, float setAlphaOutline, string setCrosshairString) {
        centerDot       = setCenterDot;
        TStyle          = setTStyle;
        size            = setSize;
        thickness       = setThickness;
        gap             = setGap;
        outlineEnabled  = setOutlineEnabled;
        red             = setRed;
        green           = setGreen;
        blue            = setBlue;
        alpha           = setAlpha;
        outlineRed      = setRedOutline;
        outlineGreen    = setGreenOutline;
        outlineBlue     = setBlueOutline;
        outlineAlpha    = setAlphaOutline;
        crosshairString = setCrosshairString;

        crosshairSettings.SaveCrosshairSettings();
    }

    /// <summary>
    /// Loads crosshair data (CrosshairDataSerial) and sets values to this crosshair settings object (CrosshairSettings).
    /// </summary>
    /// <param name="crosshairData"></param>
    public static void LoadCrosshairSettings(CrosshairDataSerial crosshairData) {
        centerDot       = crosshairData.centerDot;
        TStyle          = crosshairData.TStyle;
        size            = crosshairData.size;
        thickness       = crosshairData.thickness;
        gap             = crosshairData.gap;
        outlineEnabled  = crosshairData.outlineEnabled;
        red             = crosshairData.red;
        green           = crosshairData.green;
        blue            = crosshairData.blue;
        alpha           = crosshairData.alpha;
        outlineRed      = crosshairData.outlineRed;
        outlineGreen    = crosshairData.outlineGreen;
        outlineBlue     = crosshairData.outlineBlue;
        outlineAlpha    = crosshairData.outlineAlpha;
        crosshairString = crosshairData.crosshairString;
    }
}
