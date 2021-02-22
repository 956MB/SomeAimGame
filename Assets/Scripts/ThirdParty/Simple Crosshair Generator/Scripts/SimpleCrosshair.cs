using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UERandom = UnityEngine.Random;

using SomeAimGame.Utilities;

public enum CrosshairColorChannel
{
    RED,
    GREEN,
    BLUE,
    ALPHA
}

[System.Serializable]
public class Crosshair
{
    [Tooltip("Controls whether or not to draw center dot.")]
    public bool centerDot = false;

    [Tooltip("Controls whether or not to draw crosshair T style.")]
    public bool tStyle = false;

    [Tooltip("Controls whether or not to draw crosshair outline.")]
    public bool enableOutline = true;

    [Tooltip("Controls thickness of crosshair outline (if enabled).")]
    public int outlineThickness = 1;

    [Range(1, 150), Tooltip("Controls the length of each crosshair line.")]
    public int size = 6;

    [Range(1, 100), Tooltip("Controls the width of each crosshair line.")]
    public int thickness = 1;

    [Range(0, 350), Tooltip("Controls the distance between the center of the crosshair and the start of each crosshair line.")]
    public int gap = 5;

    [Tooltip("Specifies the color of the crosshair.")]
    public Color color = Color.green;

    [Tooltip("Specifies the color of the crosshairs outline (if enabled).")]
    public Color outlineColor = Color.black;

    public int SizeNeeded
    {
        private set { }
        get
        {
            int width = size + size + gap + gap;
            return width > thickness ? width : thickness;
        }
    }
}

public class SimpleCrosshair : MonoBehaviour
{
    [SerializeField, Tooltip("Contains properties that Specify how the crosshair looks.")]
    private Crosshair m_crosshair = null;

    [Tooltip("Specifies the image to draw the crosshair to. If you leave this empty, this script generates a Canvas and an Image with the correct settings for you.")]
    public Image m_crosshairImage;
    public Image m_crosshairImageSettings;
    private static string crosshairStringFull = "000601050255255255255000000000255";

    private static SimpleCrosshair simpleCrosshair;

    private void Awake()
    {
        simpleCrosshair = this;

        if (m_crosshairImage == null)
        {
            InitialiseCrosshairImage();
        }

        GenerateCrosshair();
    }

    public void InitialiseCrosshairImage()
    {
        GameObject crosshairGameObject = new GameObject();
        crosshairGameObject.name = "Crosshair Canvas";

        Canvas crosshairCanvas     = crosshairGameObject.AddComponent<Canvas>();
        crosshairCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        crosshairGameObject.AddComponent<CanvasScaler>();

        GameObject imageGameObject       = new GameObject();
        imageGameObject.name             = "Crosshair Image";
        imageGameObject.transform.parent = crosshairGameObject.transform;

        m_crosshairImage                                     = imageGameObject.AddComponent<Image>();
        m_crosshairImageSettings                             = imageGameObject.AddComponent<Image>();
        m_crosshairImage.rectTransform.localPosition         = new Vector2(0, 0);
        m_crosshairImageSettings.rectTransform.localPosition = new Vector2(0, 0);
        m_crosshairImage.raycastTarget                       = false;
        m_crosshairImageSettings.raycastTarget               = false;
    }

    public void GenerateCrosshair()
    {
        Texture2D crosshairTexture = DrawCrosshair(m_crosshair);

        m_crosshairImage.rectTransform.sizeDelta         = new Vector2(m_crosshair.SizeNeeded, m_crosshair.SizeNeeded);
        m_crosshairImageSettings.rectTransform.sizeDelta = new Vector2(m_crosshair.SizeNeeded, m_crosshair.SizeNeeded);
        Sprite crosshairSprite                           = Sprite.Create(crosshairTexture, new Rect(0, 0, crosshairTexture.width, crosshairTexture.height), Vector2.one / 2);

        m_crosshairImage.sprite         = crosshairSprite;
        m_crosshairImageSettings.sprite = crosshairSprite;
    }
    
    /// <summary>
    /// Parses given crosshair string (newCrosshairString), if all values valid SetAllCrosshairValues() called to set all values and redraw crosshair.
    /// 
    ///     │
    ///  ──   ──  Crosshair String: 000601051255255255255255000000255
    ///     │
    ///     
    /// 0        0          06    01         05   1        255  255    255   255    255         000           000          255
    /// T-Style  CenterDot  Size  Thickness  Gap  Outline  Red  Green  Blue  Alpha  RedOutline  GreenOutline  BlueOutline  AlphaOutline
    /// 
    /// </summary>
    /// <param name="newCrosshairString"></param>
    /// <returns></returns>
    public bool ParseCrosshairString(string newCrosshairString, bool setString, bool setValues = true) {
        if (newCrosshairString.Length != 33 || !Util.DigitsOnly(newCrosshairString)) { return false; }

        int tstyleValueInt, centerDotValueInt, outlineValueInt, sizeValueInt, thicknessValueInt, gapValueInt, redValueInt, greenValueInt, blueValueInt, alphaValueInt, redOutlineValueInt, greenOutlineValueInt, blueOutlineValueInt, alphaOutlineValueInt;
        string sizeValueString, thicknessValueString, gapValueString, redValueString, greenValueString, blueValueString, alphaValueString, redOutlineValueString, greenOutlineValueString, blueOutlineValueString, alphaOutlineValueString;

        tstyleValueInt              = int.Parse(newCrosshairString[0].ToString());
        centerDotValueInt           = int.Parse(newCrosshairString[1].ToString());
        sizeValueString         = newCrosshairString.Substring(2, 2);
        thicknessValueString    = newCrosshairString.Substring(4, 2);
        gapValueString          = newCrosshairString.Substring(6, 2);
        outlineValueInt             = int.Parse(newCrosshairString[8].ToString());
        redValueString          = newCrosshairString.Substring(9, 3);
        greenValueString        = newCrosshairString.Substring(12, 3);
        blueValueString         = newCrosshairString.Substring(15, 3);
        alphaValueString        = newCrosshairString.Substring(18, 3);
        redOutlineValueString   = newCrosshairString.Substring(21, 3);
        greenOutlineValueString = newCrosshairString.Substring(24, 3);
        blueOutlineValueString  = newCrosshairString.Substring(27, 3);
        alphaOutlineValueString = newCrosshairString.Substring(30, 3);

        if (sizeValueString      == "00") {      sizeValueInt = 0; } else { sizeValueInt      = int.Parse(sizeValueString.TrimStart(new char[] { '0' })); }
        if (thicknessValueString != "00") { thicknessValueInt = int.Parse(thicknessValueString.TrimStart(new char[] { '0' })); } else { return false; }
        if (gapValueString       == "00") {       gapValueInt = 0; } else { gapValueInt       = int.Parse(gapValueString.TrimStart(new char[] { '0' })); }

        if (redValueString   == "000") {   redValueInt = 0; } else { redValueInt   = int.Parse(redValueString.TrimStart(new char[] { '0' })); }
        if (greenValueString == "000") { greenValueInt = 0; } else { greenValueInt = int.Parse(greenValueString.TrimStart(new char[] { '0' })); }
        if (blueValueString  == "000") {  blueValueInt = 0; } else { blueValueInt  = int.Parse(blueValueString.TrimStart(new char[] { '0' })); }
        if (alphaValueString == "000") { alphaValueInt = 0; } else { alphaValueInt = int.Parse(alphaValueString.TrimStart(new char[] { '0' })); }

        if (redOutlineValueString   == "000") {   redOutlineValueInt = 0; } else { redOutlineValueInt   = int.Parse(redOutlineValueString.TrimStart(new char[] { '0' })); }
        if (greenOutlineValueString == "000") { greenOutlineValueInt = 0; } else { greenOutlineValueInt = int.Parse(greenOutlineValueString.TrimStart(new char[] { '0' })); }
        if (blueOutlineValueString  == "000") {  blueOutlineValueInt = 0; } else { blueOutlineValueInt  = int.Parse(blueOutlineValueString.TrimStart(new char[] { '0' })); }
        if (alphaOutlineValueString == "000") { alphaOutlineValueInt = 0; } else { alphaOutlineValueInt = int.Parse(alphaOutlineValueString.TrimStart(new char[] { '0' })); }

        #region logs
        //Debug.Log($"TStyle Value:               {tstyleValue}");
        //Debug.Log($"Center Dot Value:           {centerDotValue}");
        //Debug.Log($"Size Value:                 {sizeValue}");
        //Debug.Log($"Thickness Value:            {thicknessValue}");
        //Debug.Log($"Gap Value:                  {gapValue}");
        //Debug.Log($"Outline Value:              {outlineValue}");
        //Debug.Log($"Red Value:                  {redValue}");
        //Debug.Log($"Green Value:                {greenValue}");
        //Debug.Log($"Blue Value:                 {blueValue}");
        //Debug.Log($"Alpha Value:                {alphaValue}");
        //Debug.Log($"Red Outline Value:          {redOutlineValue}");
        //Debug.Log($"Green Outline Value:        {greenOutlineValue}");
        //Debug.Log($"Blue Outline Value:         {blueOutlineValue}");
        //Debug.Log($"Alpha Outline Value:        {alphaOutlineValue}");
        #endregion

        if (!ValidateCrosshairValues(tstyleValueInt, centerDotValueInt, sizeValueInt, thicknessValueInt, gapValueInt, outlineValueInt, redValueInt, greenValueInt, blueValueInt, alphaValueInt, redOutlineValueInt, greenOutlineValueInt, blueOutlineValueInt, alphaOutlineValueInt)) { return false; }

        bool tstyleValueBool    = tstyleValueInt    != 0;
        bool centerDotValueBool = centerDotValueInt != 0;
        bool outlineValueBool   = outlineValueInt   != 0;

        if (setString) {
            crosshairStringFull = $"{tstyleValueInt}{centerDotValueInt}{sizeValueString}{thicknessValueString}{gapValueString}{outlineValueInt}{redValueString}{greenValueString}{blueValueString}{alphaValueString}{redOutlineValueString}{greenOutlineValueString}{blueOutlineValueString}{alphaOutlineValueString}";
            //Debug.Log($"crosshaistring: {crosshairStringFull}");

            if (setValues) { SetAllCrosshairValues(tstyleValueBool, centerDotValueBool, sizeValueInt, thicknessValueInt, gapValueInt, outlineValueBool, redValueInt, greenValueInt, blueValueInt, alphaValueInt, redOutlineValueInt, greenOutlineValueInt, blueOutlineValueInt, alphaOutlineValueInt, true); }
        }

        return true;
    }

    /// <summary>
    /// Parses given csgo crosshair from Dictionary (crosshairDict), if all values valid SetAllCrosshairValues() called to set all values and redraw crosshair.
    /// 
    ///     │
    ///  ──   ──  Crosshair ShareCode: CSGO-c3YbL-vq5pC-oabtS-DJDTW-mRXXC
    ///     │
    ///     
    /// </summary>
    /// <param name="crosshairDict"></param>
    /// <param name="setString"></param>
    /// <param name="setValues"></param>
    /// <returns></returns>
    public bool ParseCSGOCrosshair(IDictionary<string, double> crosshairDict, bool setString, bool setValues = true) {
        double tstyleValueDouble, centerDotValueDouble, outlineValueDouble, sizeValueDouble, thicknessValueDouble, gapValueDouble, redValueDouble, greenValueDouble, blueValueDouble, alphaValueDouble, redOutlineValueDouble, greenOutlineValueDouble, blueOutlineValueDouble, alphaOutlineValueDouble;
        string sizeValueString, thicknessValueString, gapValueString, redValueString, greenValueString, blueValueString, alphaValueString, redOutlineValueString, greenOutlineValueString, blueOutlineValueString, alphaOutlineValueString;

        gapValueDouble          = crosshairDict["cl_crosshairgap"];
        gapValueDouble              = gapValueDouble + 5;
        // TODO: add outline thickness to SimpleCrosshair // round
        redValueDouble          = crosshairDict["cl_crosshaircolor_r"];
        greenValueDouble        = crosshairDict["cl_crosshaircolor_g"];
        blueValueDouble         = crosshairDict["cl_crosshaircolor_b"];
        alphaValueDouble        = crosshairDict["cl_crosshairalpha"];
        outlineValueDouble      = crosshairDict["cl_crosshair_drawoutline"];
        redOutlineValueDouble   = crosshairDict["cl_crosshaircolor_r_outline"];
        greenOutlineValueDouble = crosshairDict["cl_crosshaircolor_g_outline"];
        blueOutlineValueDouble  = crosshairDict["cl_crosshaircolor_b_outline"];
        alphaOutlineValueDouble = crosshairDict["cl_crosshairalpha_outline"];
        thicknessValueDouble    = crosshairDict["cl_crosshairthickness"]; // round
        thicknessValueDouble        = Math.Round(thicknessValueDouble);
        thicknessValueDouble        = thicknessValueDouble == 0 ? 1 : thicknessValueDouble;
        centerDotValueDouble    = crosshairDict["cl_crosshairdot"];
        tstyleValueDouble       = crosshairDict["cl_crosshair_t"];
        sizeValueDouble         = crosshairDict["cl_crosshairsize"]; // round
        sizeValueDouble             = (Math.Round(sizeValueDouble) * 2) + 1;

        #region logs
        //Debug.Log($"TStyle Value:               {tstyleValueDouble}");
        //Debug.Log($"Center Dot Value:           {centerDotValueDouble}");
        //Debug.Log($"Size Value:                 {sizeValueDouble}");
        //Debug.Log($"Thickness Value:            {thicknessValueDouble}");
        //Debug.Log($"Gap Value:                  {gapValueDouble}");
        //Debug.Log($"Outline Value:              {outlineValueDouble}");
        //Debug.Log($"Red Value:                  {redValueDouble}");
        //Debug.Log($"Green Value:                {greenValueDouble}");
        //Debug.Log($"Blue Value:                 {blueValueDouble}");
        //Debug.Log($"Alpha Value:                {alphaValueDouble}");
        //Debug.Log($"Red Outline Value:          {redOutlineValueDouble}");
        //Debug.Log($"Green Outline Value:        {greenOutlineValueDouble}");
        //Debug.Log($"Blue Outline Value:         {blueOutlineValueDouble}");
        //Debug.Log($"Alpha Outline Value:        {alphaOutlineValueDouble}");
        #endregion

        if (!ValidateCrosshairValues(tstyleValueDouble, centerDotValueDouble, sizeValueDouble, thicknessValueDouble, gapValueDouble, outlineValueDouble, redValueDouble, greenValueDouble, blueValueDouble, alphaValueDouble, redOutlineValueDouble, greenOutlineValueDouble, blueOutlineValueDouble, alphaOutlineValueDouble)) { return false; }

        bool tstyleValueBool    = tstyleValueDouble    != 0;
        bool centerDotValueBool = centerDotValueDouble != 0;
        bool outlineValueBool   = outlineValueDouble   != 0;

        sizeValueString         = sizeValueDouble.ToString("00");
        thicknessValueString    = thicknessValueDouble.ToString("00");
        gapValueString          = gapValueDouble.ToString("00");
        redValueString          = redValueDouble.ToString("000");
        greenValueString        = greenValueDouble.ToString("000");
        blueValueString         = blueValueDouble.ToString("000");
        alphaValueString        = alphaValueDouble.ToString("000");
        redOutlineValueString   = redOutlineValueDouble.ToString("000");
        greenOutlineValueString = greenOutlineValueDouble.ToString("000");
        blueOutlineValueString  = blueOutlineValueDouble.ToString("000");
        alphaOutlineValueString = alphaOutlineValueDouble.ToString("000");

        if (setString) {
            crosshairStringFull = $"{tstyleValueDouble}{centerDotValueDouble}{sizeValueString}{thicknessValueString}{gapValueString}{outlineValueDouble}{redValueString}{greenValueString}{blueValueString}{alphaValueString}{redOutlineValueString}{greenOutlineValueString}{blueOutlineValueString}{alphaOutlineValueString}";
            //Debug.Log($"CSGO crosshair string: {crosshairStringFull}");

            if (setValues) { SetAllCrosshairValues(tstyleValueBool, centerDotValueBool, (int)sizeValueDouble, (int)thicknessValueDouble, (int)gapValueDouble, outlineValueBool, (int)redValueDouble, (int)greenValueDouble, (int)blueValueDouble, (int)alphaValueDouble, (int)redOutlineValueDouble, (int)greenOutlineValueDouble, (int)blueOutlineValueDouble, (int)alphaOutlineValueDouble, true); }
        }

        return true;
    }

    /// <summary>
    /// Validates given crosshair string and returns true/false for valid, and sets values to current crosshair if supplied bool true (setString).
    /// </summary>
    /// <param name="newCrosshairString"></param>
    /// <param name="setString"></param>
    /// <returns></returns>
    public static bool ValidateSetCrosshairString(string newCrosshairString, bool setString) {
        if (simpleCrosshair.ParseCrosshairString(newCrosshairString, setString)) { return true; }
        return false;
    }
    /// <summary>
    /// Validates given csgo crosshair dict and returns true/false for valid, and sets values to current crosshair if supplied bool true (setString).
    /// </summary>
    /// <param name="crosshairDict"></param>
    /// <param name="setString"></param>
    /// <returns></returns>
    public static bool ValidateSetCSGOCrosshair(Dictionary<string, double> crosshairDict, bool setString) {
        if (simpleCrosshair.ParseCSGOCrosshair(crosshairDict, setString)) { return true; }
        return false;
    }

    /// <summary>
    /// Exports full crosshair string with all current crosshair values.
    /// </summary>
    /// <returns></returns>
    public static string ExportCrosshairString_Temp() {
        int tstyleInt, centerDotInt, outlineInt;
        string sizeString, thicknessString, gapString, redString, greenString, blueString, alphaString, redOutlineString, greenOutlineString, blueOutlineString, alphaOutlineString;

        tstyleInt          = simpleCrosshair.m_crosshair.tStyle ? 1 : 0;
        centerDotInt       = simpleCrosshair.m_crosshair.centerDot ? 1 : 0;
        sizeString         = simpleCrosshair.m_crosshair.size.ToString("00");
        thicknessString    = simpleCrosshair.m_crosshair.thickness.ToString("00");
        gapString          = simpleCrosshair.m_crosshair.gap.ToString("00");
        outlineInt         = simpleCrosshair.m_crosshair.enableOutline ? 1 : 0;
        redString          = (simpleCrosshair.m_crosshair.color.r * 255.0f).ToString("000");
        greenString        = (simpleCrosshair.m_crosshair.color.g * 255.0f).ToString("000");
        blueString         = (simpleCrosshair.m_crosshair.color.b * 255.0f).ToString("000");
        alphaString        = (simpleCrosshair.m_crosshair.color.a * 255.0f).ToString("000");
        redOutlineString   = (simpleCrosshair.m_crosshair.outlineColor.r * 255.0f).ToString("000");
        greenOutlineString = (simpleCrosshair.m_crosshair.outlineColor.g * 255.0f).ToString("000");
        blueOutlineString  = (simpleCrosshair.m_crosshair.outlineColor.b * 255.0f).ToString("000");
        alphaOutlineString = (simpleCrosshair.m_crosshair.outlineColor.a * 255.0f).ToString("000");

        #region log
        //Debug.Log($"EXPORTED CROSSHAIR STRING: {tstyleInt}{centerDotInt}{sizeString}{thicknessString}{gapString}{outlineInt}{redFloat}{greenFloat}{blueFloat}{alphaFloat}{redOutlineFloat}{greenOutlineFloat}{blueOutlineFloat}{alphaOutlineFloat}");
        #endregion

        string exportedString =  $"{tstyleInt}{centerDotInt}{sizeString}{thicknessString}{gapString}{outlineInt}{redString}{greenString}{blueString}{alphaString}{redOutlineString}{greenOutlineString}{blueOutlineString}{alphaOutlineString}";
        crosshairStringFull   = exportedString;
        return exportedString;
    }

    /// <summary>
    /// Saves current crosshair string to CrosshairSettings object.
    /// </summary>
    public static void SetCrosshairString_Static(bool useString) {
        //Debug.Log($"Crosshair string before save: {crosshairStringFull}");
        //CrosshairSettings.SaveCrosshairString(crosshairStringFull);
        if (useString) {
            CrosshairSettings.SaveCrosshairString(crosshairStringFull);
        } else {
            CrosshairSettings.SaveCrosshairString(ExportCrosshairString_Temp());
        }
    }

    /// <summary>
    /// Returns full crosshair string.
    /// </summary>
    /// <returns></returns>
    public string ExportCrosshairString() { return crosshairStringFull; }

    /// <summary>
    /// Returns exported crosshair string.
    /// </summary>
    /// <returns></returns>
    public static string ReturnExportedCrosshairString() { return simpleCrosshair.ExportCrosshairString(); }

    /// <summary>
    /// Sets all crosshair values and redraws.
    /// </summary>
    /// <param name="tstyle"></param>
    /// <param name="centerDot"></param>
    /// <param name="size"></param>
    /// <param name="thickness"></param>
    /// <param name="gap"></param>
    /// <param name="outlineEnable"></param>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    /// <param name="alpha"></param>
    /// <param name="redrawCrosshair"></param>
    public void SetAllCrosshairValues(bool tstyle, bool centerDot, int size, int thickness, int gap, bool outlineEnable, int red, int green, int blue, int alpha, int redOutline, int greenOutline, int blueOutline, int alphaOutline, bool redrawCrosshair) {
        m_crosshair.tStyle         = tstyle;
        m_crosshair.centerDot      = centerDot;
        m_crosshair.size           = size;
        m_crosshair.thickness      = thickness;
        m_crosshair.gap            = gap;
        m_crosshair.enableOutline  = outlineEnable;
        m_crosshair.color.r        = red;
        m_crosshair.color.g        = green;
        m_crosshair.color.b        = blue;
        m_crosshair.color.a        = alpha;
        m_crosshair.outlineColor.r = redOutline;
        m_crosshair.outlineColor.g = greenOutline;
        m_crosshair.outlineColor.b = blueOutline;
        m_crosshair.outlineColor.a = alphaOutline;

        if (redrawCrosshair) { GenerateCrosshair(); }

        CrosshairSaveSystem.SetAllCrosshairControls(tstyle, centerDot, size, thickness, gap, outlineEnable, red, green, blue, alpha, redOutline, greenOutline, blueOutline, alphaOutline);
        CrosshairSettings.SaveAllCrosshairDefaults(tstyle, centerDot, size, thickness, gap, outlineEnable, red, green, blue, alpha, redOutline, greenOutline, blueOutline, alphaOutline, crosshairStringFull);
    }

    /// <summary>
    /// Validates all supplied crosshair values and returns true if valid, false otherwise.
    /// </summary>
    /// <param name="tstyle"></param>
    /// <param name="centerDot"></param>
    /// <param name="size"></param>
    /// <param name="thickness"></param>
    /// <param name="gap"></param>
    /// <param name="outlineEnable"></param>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public bool ValidateCrosshairValues(int tstyle, int centerDot, int size, int thickness, int gap, int outlineEnable, int red, int green, int blue, int alpha, int redOutline, int greenOutline, int blueOutline, int alphaOutline) {
        if (tstyle < 0 || tstyle > 1)               { return false; }
        if (centerDot < 0 || centerDot > 1)         { return false; }
        if (size < 0 || size > 45)                  { return false; }
        if (thickness < 1 || thickness > 15)        { return false; }
        if (gap < 0 || gap > 25)                    { return false; }
        if (outlineEnable < 0 || outlineEnable > 1) { return false; }
        if (red < 0 || red > 255)                   { return false; }
        if (green < 0 || green > 255)               { return false; }
        if (blue < 0 || blue > 255)                 { return false; }
        if (alpha < 0 || alpha > 255)               { return false; }
        if (redOutline < 0 || redOutline > 255)     { return false; }
        if (greenOutline < 0 || greenOutline > 255) { return false; }
        if (blueOutline < 0 || blueOutline > 255)   { return false; }
        if (alphaOutline < 0 || alphaOutline > 255) { return false; }

        return true;
    }
    public bool ValidateCrosshairValues(double tstyle, double centerDot, double size, double thickness, double gap, double outlineEnable, double red, double green, double blue, double alpha, double redOutline, double greenOutline, double blueOutline, double alphaOutline) {
        if (tstyle < 0 || tstyle > 1)               { return false; }
        if (centerDot < 0 || centerDot > 1)         { return false; }
        if (size < 0 || size > 45)                  { return false; }
        if (thickness < 1 || thickness > 15)        { return false; }
        if (gap < 0 || gap > 25)                    { return false; }
        if (outlineEnable < 0 || outlineEnable > 1) { return false; }
        if (red < 0 || red > 255)                   { return false; }
        if (green < 0 || green > 255)               { return false; }
        if (blue < 0 || blue > 255)                 { return false; }
        if (alpha < 0 || alpha > 255)               { return false; }
        if (redOutline < 0 || redOutline > 255)     { return false; }
        if (greenOutline < 0 || greenOutline > 255) { return false; }
        if (blueOutline < 0 || blueOutline > 255)   { return false; }
        if (alphaOutline < 0 || alphaOutline > 255) { return false; }

        return true;
    }

    /// <summary>
    /// Generates random crosshair values, sets them to crosshair, then redraws.
    /// </summary>
    public static void GenerateRandomCrosshair() {
        int[,] colorsMultiArray = new int[8, 3] { { 255, 0, 0 }, { 0, 255, 0 }, { 0, 0, 255 }, { 255, 255, 0 }, { 0, 255, 255 }, { 255, 0, 255 }, { 255, 255, 255 }, { 0, 0, 0 } };

        int randomTstyleValue       = UERandom.Range(0, 3) == 0 ? 1 : 0;
        int randomCenterDotValue    = UERandom.Range(0, 3) == 0 ? 1 : 0;
        int randomOutlineValue      = UERandom.Range(0, 2);

        int randomSizeValue         = UERandom.Range(1, 11);
        int randomThicknessValue    = UERandom.Range(1, 5);
        int randomGapValue          = UERandom.Range(1, 11);

        int randomRedValue          = UERandom.Range(0, 2) == 1 ? UERandom.Range(0, 256) : colorsMultiArray[UERandom.Range(0, 8), 0];
        int randomGreenValue        = UERandom.Range(0, 2) == 1 ? UERandom.Range(0, 256) : colorsMultiArray[UERandom.Range(0, 8), 0];
        int randomBlueValue         = UERandom.Range(0, 2) == 1 ? UERandom.Range(0, 256) : colorsMultiArray[UERandom.Range(0, 8), 0];
        int randomAlphaValue        = UERandom.Range(0, 3) == 0 ? UERandom.Range(100, 256) : 255;

        int randomRedOutlineValue   = UERandom.Range(0, 2) == 1 ? UERandom.Range(0, 256) : colorsMultiArray[UERandom.Range(0, 8), 0];
        int randomGreenOutlineValue = UERandom.Range(0, 2) == 1 ? UERandom.Range(0, 256) : colorsMultiArray[UERandom.Range(0, 8), 0];
        int randomBlueOutlineValue  = UERandom.Range(0, 2) == 1 ? UERandom.Range(0, 256) : colorsMultiArray[UERandom.Range(0, 8), 0];
        randomRedOutlineValue       = UERandom.Range(0, 3) != 0 ? 0 : randomRedOutlineValue;
        randomGreenOutlineValue     = UERandom.Range(0, 3) != 0 ? 0 : randomGreenOutlineValue;
        randomBlueOutlineValue      = UERandom.Range(0, 3) != 0 ? 0 : randomBlueOutlineValue;
        int randomAlphaOutlineBool  = UERandom.Range(0, 2);
        int randomAlphaOutlineValue = randomAlphaOutlineBool == 1 ? UERandom.Range(100, 256) : 255;
        randomAlphaOutlineValue     = UERandom.Range(0, 3) != 0 ? 255 : randomAlphaOutlineValue;

        bool randomTstyleValueBool    = randomTstyleValue    != 0;
        bool randomCenterDotValueBool = randomCenterDotValue != 0;
        bool randomOutlineValueBool   = randomOutlineValue   != 0;

        string sizeValueString         = randomSizeValue.ToString("00");
        string thicknessValueString    = randomThicknessValue.ToString("00");
        string gapValueString          = randomGapValue.ToString("00");
        string redValueString          = randomRedValue.ToString("000");
        string greenValueString        = randomGreenValue.ToString("000");
        string blueValueString         = randomBlueValue.ToString("000");
        string alphaValueString        = randomAlphaValue.ToString("000");
        string redOutlineValueString   = randomRedOutlineValue.ToString("000");
        string greenOutlineValueString = randomGreenOutlineValue.ToString("000");
        string blueOutlineValueString  = randomBlueOutlineValue.ToString("000");
        string alphaOutlineValueString = randomAlphaOutlineValue.ToString("000");

        crosshairStringFull = $"{randomTstyleValue}{randomCenterDotValue}{sizeValueString}{thicknessValueString}{gapValueString}{randomOutlineValue}{redValueString}{greenValueString}{blueValueString}{alphaValueString}{redOutlineValueString}{greenOutlineValueString}{blueOutlineValueString}{alphaOutlineValueString}";

        simpleCrosshair.SetAllCrosshairValues(randomTstyleValueBool, randomCenterDotValueBool, randomSizeValue, randomThicknessValue, randomGapValue, randomOutlineValueBool, randomRedValue, randomGreenValue, randomBlueValue, randomAlphaValue, randomRedOutlineValue, randomGreenOutlineValue, randomBlueOutlineValue, randomAlphaOutlineValue, true);
    }

    public void SetColor(CrosshairColorChannel channel, int value, bool redrawCrosshair)  // Set between 0 and 255
    {
        switch (channel)
        {
            case CrosshairColorChannel.RED:   m_crosshair.color.r = value / 255.0f; break;
            case CrosshairColorChannel.GREEN: m_crosshair.color.g = value / 255.0f; break;
            case CrosshairColorChannel.BLUE:  m_crosshair.color.b = value / 255.0f; break;
            case CrosshairColorChannel.ALPHA: m_crosshair.color.a = value / 255.0f; break;
        }

        if (redrawCrosshair) { GenerateCrosshair(); }
    }

    public void SetOutlineColor(CrosshairColorChannel channel, int value, bool redrawCrosshair)  // Set between 0 and 255
    {
        switch (channel)
        {
            case CrosshairColorChannel.RED:   m_crosshair.outlineColor.r = value / 255.0f; break;
            case CrosshairColorChannel.GREEN: m_crosshair.outlineColor.g = value / 255.0f; break;
            case CrosshairColorChannel.BLUE:  m_crosshair.outlineColor.b = value / 255.0f; break;
            case CrosshairColorChannel.ALPHA: m_crosshair.outlineColor.a = value / 255.0f; break;
        }

        if (redrawCrosshair) { GenerateCrosshair(); }
    }

    public void SetColor(Color color, bool redrawCrosshair)
    {
        m_crosshair.color = color;
        if (redrawCrosshair) { GenerateCrosshair(); }
    }

    public void SetOutlineColor(Color color, bool redrawCrosshair)
    {
        m_crosshair.outlineColor = color;
        if (redrawCrosshair) { GenerateCrosshair(); }
    }

    public void SetTStyle(bool enableTStyle, bool redrawCrosshair) {
        m_crosshair.tStyle = enableTStyle;
        if (redrawCrosshair) { GenerateCrosshair(); }
    }

    public void SetCenterDot(bool centerDot, bool redrawCrosshair) {
        m_crosshair.centerDot = centerDot;
        if (redrawCrosshair) { GenerateCrosshair(); }
    }

    public void SetOutlineEnabled(bool enableOutline, bool redrawCrosshair) {
        m_crosshair.enableOutline = enableOutline;
        if (redrawCrosshair) { GenerateCrosshair(); }
    }

    public void SetOutlineThickness(int outlineThickness, bool redrawCrosshair) {
        m_crosshair.outlineThickness = outlineThickness;
        if (redrawCrosshair) { GenerateCrosshair(); }
    }

    public void SetThickness(int newThickness, bool redrawCrosshair)
    {
        m_crosshair.thickness = newThickness;
        if (m_crosshair.thickness < 1) { m_crosshair.thickness = 1; }
        if (redrawCrosshair) { GenerateCrosshair(); }
    }

    public void SetSize(int newSize, bool redrawCrosshair)
    {
        m_crosshair.size = newSize;
        //if(m_crosshair.size < 1) { m_crosshair.size = 1; }
        if (redrawCrosshair) { GenerateCrosshair(); }
    }

    public void SetGap(int newGap, bool redrawCrosshair)
    {
        m_crosshair.gap = newGap;
        if (m_crosshair.gap < 0) { m_crosshair.gap = 0; }
        if (redrawCrosshair) { GenerateCrosshair(); }
    }

    #region Getters
    public int GetSize() { return m_crosshair.size; }
    public int GetThickness() { return m_crosshair.thickness; }
    public int GetGap() { return m_crosshair.gap; }
    public Color GetColor() { return m_crosshair.color; }
    public Crosshair GetCrosshair() { return m_crosshair; }
    #endregion

    #region Draw Crosshair Texture
    public Texture2D DrawCrosshair(Crosshair crosshair = null)
    {
        if(crosshair == null) { crosshair = m_crosshair; }

        //int outlineTemp = 1;
        int sizeNeeded = crosshair.SizeNeeded;
        int centerBias = sizeNeeded / 2;

        Texture2D crosshairTexture  = new Texture2D(sizeNeeded + (m_crosshair.outlineThickness + 1), sizeNeeded + (m_crosshair.outlineThickness + 1), TextureFormat.RGBA32, false);
        crosshairTexture.wrapMode   = TextureWrapMode.Clamp;
        crosshairTexture.filterMode = FilterMode.Point;

        //DrawBox(0, 0, crosshairTexture.width, crosshairTexture.height, crosshairTexture, new Color(255, 0, 0, 50));
        DrawBox(0, 0, crosshairTexture.width, crosshairTexture.height, crosshairTexture, new Color(0, 0, 0, 0));

        #region Original crosshair drawing

        /*
        int leftExtra  = 1;
        int startGapShort = Mathf.CeilToInt(crosshair.thickness / 2.0f);

        // Top
        DrawBox(centerBias - startGapShort,
            (centerBias + crosshair.gap) - extra,
            crosshair.thickness,
            crosshair.size,
            crosshairTexture,
            crosshair.color);

        // Right
        DrawBox((centerBias + crosshair.gap) - extra,
            centerBias - startGapShort,
            crosshair.size,
            crosshair.thickness,
            crosshairTexture,
            crosshair.color);


        // Bottom
        DrawBox((centerBias - startGapShort),
            (centerBias - crosshair.gap - crosshair.size),
            crosshair.thickness,
            crosshair.size,
            crosshairTexture,
            crosshair.color);

        // Left
        DrawBox((centerBias - crosshair.gap - crosshair.size),
           (centerBias - startGapShort),
           crosshair.size,
           crosshair.thickness,
           crosshairTexture,
           crosshair.color);
        */

        #endregion

        #region My current crosshair drawing

        int leftExtra     = 0;
        int startGapShort = Mathf.CeilToInt(crosshair.thickness / 2.0f);
        if (crosshair.thickness % 2 == 0) {
            leftExtra = 1;
        }

        if (m_crosshair.enableOutline) {
            // top outline
            if (!m_crosshair.tStyle) {
                if (crosshair.size >= 1) {
                    DrawBox((centerBias - startGapShort),
                        (centerBias + crosshair.gap) - (m_crosshair.outlineThickness),
                        crosshair.thickness + (m_crosshair.outlineThickness + 1),
                        crosshair.size + (m_crosshair.outlineThickness+1),
                        crosshairTexture,
                        crosshair.outlineColor);
                }
            }

            // Center dot outline
            if (m_crosshair.centerDot) {
                DrawBox((centerBias - startGapShort),
                    (centerBias - (crosshair.thickness / 2) - 1 ) + leftExtra,
                    crosshair.thickness + 2,
                    crosshair.thickness + 2,
                    crosshairTexture,
                    crosshair.outlineColor);
            }

            // Right outline
            if (crosshair.size >= 1) {
                DrawBox((centerBias + crosshair.gap) - m_crosshair.outlineThickness,
                    (centerBias - startGapShort),
                    crosshair.size + (m_crosshair.outlineThickness + 1),
                    crosshair.thickness + (m_crosshair.outlineThickness + 1),
                    crosshairTexture,
                    crosshair.outlineColor);
            }


            if (crosshair.thickness % 2 == 0) {
                // Left outline
                if (crosshair.size >= 1) {
                    DrawBox((centerBias - crosshair.gap - crosshair.size) + 1,
                        (centerBias - startGapShort),
                        crosshair.size + (m_crosshair.outlineThickness + 1),
                        crosshair.thickness + (m_crosshair.outlineThickness + 1),
                        crosshairTexture,
                        crosshair.outlineColor);
                }

                // Bottom outline
                if (crosshair.size >= 1) {
                    DrawBox((centerBias - startGapShort),
                        (centerBias - crosshair.gap - crosshair.size) + 1,
                        crosshair.thickness + (m_crosshair.outlineThickness + 1),
                        crosshair.size + (m_crosshair.outlineThickness + 1),
                        crosshairTexture,
                        crosshair.outlineColor);
                }

            } else {
                // Left outline
                if (crosshair.size >= 1) {
                    DrawBox((centerBias - crosshair.gap - crosshair.size),
                        (centerBias - startGapShort),
                        crosshair.size + (m_crosshair.outlineThickness + 1),
                        crosshair.thickness + (m_crosshair.outlineThickness + 1),
                        crosshairTexture,
                        crosshair.outlineColor);
                }
            
                // Bottom outline
                if (crosshair.size >= 1) {
                    DrawBox((centerBias - startGapShort),
                        (centerBias - crosshair.gap - crosshair.size),
                        crosshair.thickness + (m_crosshair.outlineThickness + 1),
                        crosshair.size + (m_crosshair.outlineThickness+1),
                        crosshairTexture,
                        crosshair.outlineColor);
                }
            }

        }

            
        if (!m_crosshair.tStyle) {
            // top
            if (crosshair.size >= 1) {
                DrawBox((centerBias - startGapShort) + m_crosshair.outlineThickness,
                    (centerBias + crosshair.gap),
                    crosshair.thickness,
                    crosshair.size,
                    crosshairTexture,
                    crosshair.color);
            }
        }

        if (crosshair.thickness == 1) {
            // Left
            if (crosshair.size >= 1) {
                DrawBox((centerBias - crosshair.gap - crosshair.size) + (m_crosshair.outlineThickness),
                    (centerBias - startGapShort) + 1,
                    crosshair.size - leftExtra,
                    crosshair.thickness + (m_crosshair.outlineThickness) - 1,
                    crosshairTexture,
                    crosshair.color);
            }
            
            // Right
            if (crosshair.size >= 1) {
                DrawBox((centerBias + crosshair.gap),
                    (centerBias - startGapShort) + 1,
                    crosshair.size,
                    crosshair.thickness,
                    crosshairTexture,
                    crosshair.color);
            }

            // Bottom
            if (crosshair.size >= 1) {
                DrawBox((centerBias - startGapShort) + (m_crosshair.outlineThickness),
                    (centerBias - crosshair.gap - crosshair.size) + (m_crosshair.outlineThickness),
                    crosshair.thickness,
                    crosshair.size,
                    crosshairTexture,
                    crosshair.color);
            }

        } else {
            if (crosshair.thickness % 2 == 0) {
                // Left
                if (crosshair.size >= 1) {
                    DrawBox((centerBias - crosshair.gap - crosshair.size) + (m_crosshair.outlineThickness) + 1,
                        (centerBias - startGapShort) + 1,
                        (crosshair.size - leftExtra) + 1,
                        crosshair.thickness,
                        crosshairTexture,
                        crosshair.color);
                }

                // Right
                if (crosshair.size >= 1) {
                    DrawBox((centerBias + crosshair.gap),
                        (centerBias - startGapShort) + 1,
                        (crosshair.size - leftExtra) + 1,
                        crosshair.thickness,
                        crosshairTexture,
                        crosshair.color);
                }

                // Bottom
                if (crosshair.size >= 1) {
                    DrawBox((centerBias - startGapShort) + (m_crosshair.outlineThickness),
                        (centerBias - crosshair.gap - crosshair.size) + (m_crosshair.outlineThickness + 1),
                        crosshair.thickness,
                        crosshair.size,
                        crosshairTexture,
                        crosshair.color);
                }

            } else {
                // Right
                if (crosshair.size >= 1) {
                    DrawBox((centerBias + crosshair.gap),
                        (centerBias - startGapShort) + 1,
                        crosshair.size - leftExtra,
                        crosshair.thickness,
                        crosshairTexture,
                        crosshair.color);
                }

                // Left
                if (crosshair.size >= 1) {
                    DrawBox((centerBias - crosshair.gap - crosshair.size) + (m_crosshair.outlineThickness),
                        (centerBias - startGapShort) + 1,
                        crosshair.size - leftExtra,
                        crosshair.thickness,
                        crosshairTexture,
                        crosshair.color);
                }

                // Bottom
                if (crosshair.size >= 1) {
                    DrawBox((centerBias - startGapShort) + (m_crosshair.outlineThickness),
                        (centerBias - crosshair.gap - crosshair.size) + (m_crosshair.outlineThickness),
                        crosshair.thickness,
                        crosshair.size,
                        crosshairTexture,
                        crosshair.color);
                }
            }
            //if (m_crosshair.enableOutline) {
            //    // Left
            //    DrawBox((centerBias - crosshair.gap - crosshair.size) + m_crosshair.outlineThickness,
            //       centerBias - startGapShort,
            //       crosshair.size,
            //       crosshair.thickness,
            //       crosshairTexture,
            //       crosshair.color);
            //} else {
            //    // Left
            //    DrawBox((centerBias - crosshair.gap - crosshair.size),
            //       centerBias - startGapShort,
            //       crosshair.size,
            //       crosshair.thickness,
            //       crosshairTexture,
            //       crosshair.color);
            //}
        }

        // Center dot
        //int startGapShort = Mathf.CeilToInt(crosshair.thickness / 2.0f);
        if (m_crosshair.centerDot) {
            DrawBox((centerBias - startGapShort) + 1,
                (centerBias - (crosshair.thickness / 2)) + leftExtra,
                crosshair.thickness,
                crosshair.thickness,
                crosshairTexture,
                crosshair.color);
        }

        #endregion

        crosshairTexture.Apply();
        return crosshairTexture;
    }

    private void DrawBox(int startX, int startY, int width, int height, Texture2D target, Color color)
    {
        if (startX + width > target.width ||
            startY + height > target.height) {
            Debug.LogWarning("Crosshair box is out of range.");
            return;
        }
        for (int x = startX; x < startX + width; ++x)
        {
            for (int y = startY; y < startY + height; ++y)
            {
                target.SetPixel(x, y, color);
            }
        }
    }
    #endregion
}
