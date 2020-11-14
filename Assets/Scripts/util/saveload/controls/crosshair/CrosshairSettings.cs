﻿using UnityEngine;

public class CrosshairSettings : MonoBehaviour {
    public static bool  centerDot      = false;
    public static bool  TStyle         = false;
    public static float size           = 1f;
    public static float thickness      = 12f;
    public static float gap            = 0f;
    public static bool  outlineEnabled = false;
    public static float outline        = 1f;
    public static float red            = 255f;
    public static float green          = 255f;
    public static float blue           = 255f;
    public static float alpha          = 255f;

    private static CrosshairSettings crosshairSettings;
    void Awake() { crosshairSettings = this; }

    /// <summary>
    /// Saves supplied center dot bool (setCenterDot) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setCenterDot"></param>
    public static void SaveCenterDot(bool setCenterDot) {
        centerDot = setCenterDot;
        crosshairSettings.SaveCrosshairSettings();
    }
    /// <summary>
    /// Saves supplied t-style bool (setTStyle) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setTStyle"></param>
    public static void SaveTStyle(bool setTStyle) {
        TStyle = setTStyle;
        crosshairSettings.SaveCrosshairSettings();
    }
    /// <summary>
    /// Saves supplied size float (setSize) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setSize"></param>
    public static void SaveSize(float setSize) {
        size = setSize;
        crosshairSettings.SaveCrosshairSettings();
    }
    /// <summary>
    /// Saves supplied thickness float (setThickness) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setThickness"></param>
    public static void SaveThickness(float setThickness) {
        thickness = setThickness;
        crosshairSettings.SaveCrosshairSettings();
    }
    /// <summary>
    /// Saves supplied gap float (setGap) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setGap"></param>
    public static void SaveGap(float setGap) {
        gap = setGap;
        crosshairSettings.SaveCrosshairSettings();
    }
    /// <summary>
    /// Saves supplied outline enabled bool (setOutlineEnabled) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setOutlineEnabled"></param>
    public static void SaveOutlineEnabled(bool setOutlineEnabled) {
        outlineEnabled = setOutlineEnabled;
        crosshairSettings.SaveCrosshairSettings();
    }
    /// <summary>
    /// Saves supplied outline float (setOutline) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setOutline"></param>
    public static void SaveOutline (float setOutline) {
        outline = setOutline;
        crosshairSettings.SaveCrosshairSettings();
    }
    /// <summary>
    /// Saves supplied red color float (setRed) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setRed"></param>
    public static void SaveRed(float setRed) {
        red = setRed;
        crosshairSettings.SaveCrosshairSettings();
    }
    /// <summary>
    /// Saves supplied green color float (setGreen) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setGreen"></param>
    public static void SaveGreen(float setGreen) {
        green = setGreen;
        crosshairSettings.SaveCrosshairSettings();
    }
    /// <summary>
    /// Saves supplied blue color float (setBlue) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setBlue"></param>
    public static void SaveBlue(float setBlue) {
        blue = setBlue;
        crosshairSettings.SaveCrosshairSettings();
    }
    /// <summary>
    /// Saves supplied alpha float (setAlpha) to crosshair settings object (CrosshairSettings), then saves crosshair settings object.
    /// </summary>
    /// <param name="setAlpha"></param>
    public static void SaveAlpha(float setAlpha) {
        alpha = setAlpha;
        crosshairSettings.SaveCrosshairSettings();
    }

    /// <summary>
    /// Calls 'CrosshairSaveSystem.SaveCrosshairItem()' to save crosshair settings object (CrosshairSettings) to file.
    /// </summary>
    public void SaveCrosshairSettings() { CrosshairSaveSystem.SaveCrosshairSettingsData(this); }

    /// <summary>
    /// Saves default crosshair settings object (CrosshairSettings).
    /// </summary>
    /// <param name="setCenterDot"></param>
    /// <param name="setTStyle"></param>
    /// <param name="setSize"></param>
    /// <param name="setThickness"></param>
    /// <param name="setGap"></param>
    /// <param name="setOutlineEnabled"></param>
    /// <param name="setOutline"></param>
    /// <param name="setRed"></param>
    /// <param name="setGreen"></param>
    /// <param name="setBlue"></param>
    /// <param name="setAlpha"></param>
    public static void SaveAllCrosshairDefaults(bool setCenterDot, bool setTStyle, float setSize, float setThickness, float setGap, bool setOutlineEnabled, float setOutline, float setRed, float setGreen, float setBlue, float setAlpha) {
        centerDot      = setCenterDot;
        TStyle         = setTStyle;
        size           = setSize;
        thickness      = setThickness;
        gap            = setGap;
        outlineEnabled = setOutlineEnabled;
        outline        = setOutline;
        red            = setRed;
        green          = setGreen;
        blue           = setBlue;
        alpha          = setAlpha;

        crosshairSettings.SaveCrosshairSettings();
    }

    /// <summary>
    /// Loads crosshair data (CrosshairDataSerial) and sets values to this crosshair settings object (CrosshairSettings).
    /// </summary>
    /// <param name="crosshairData"></param>
    public static void LoadCrosshairSettings(CrosshairDataSerial crosshairData) {
        centerDot      = crosshairData.centerDot;
        TStyle         = crosshairData.TStyle;
        size           = crosshairData.size;
        thickness      = crosshairData.thickness;
        gap            = crosshairData.gap;
        outlineEnabled = crosshairData.outlineEnabled;
        outline        = crosshairData.outline;
        red            = crosshairData.red;
        green          = crosshairData.green;
        blue           = crosshairData.blue;
        alpha          = crosshairData.alpha;
    }
}
