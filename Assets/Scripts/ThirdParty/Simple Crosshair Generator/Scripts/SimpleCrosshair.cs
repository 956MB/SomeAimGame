using UnityEngine;
using UnityEngine.UI;

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
        Texture2D crosshairTexture =  DrawCrosshair(m_crosshair);

        m_crosshairImage.rectTransform.sizeDelta         = new Vector2(m_crosshair.SizeNeeded, m_crosshair.SizeNeeded);
        m_crosshairImageSettings.rectTransform.sizeDelta = new Vector2(m_crosshair.SizeNeeded, m_crosshair.SizeNeeded);
        Sprite crosshairSprite                           = Sprite.Create(crosshairTexture, new Rect(0, 0, crosshairTexture.width, crosshairTexture.height), Vector2.one / 2);

        m_crosshairImage.sprite         = crosshairSprite;
        m_crosshairImageSettings.sprite = crosshairSprite;
    }

    /// <summary>
    /// Parses given crosshair string (newCrosshairString), if all values valid SetAllCrosshairValues() called to set all values and redraw crosshair.
    /// 
    /// Crosshair String: 000601051255255255255255000000255
    /// 
    /// 0        0          06    01         05   1        255  255    255   255    255         000           000          255
    /// T-Style  CenterDot  Size  Thickness  Gap  Outline  Red  Green  Blue  Alpha  RedOutline  GreenOutline  BlueOutline  AlphaOutline
    /// 
    /// </summary>
    /// <param name="newCrosshairString"></param>
    /// <returns></returns>
    public bool ParseCrosshairString(string newCrosshairString, bool setValues) {
        if (newCrosshairString.Length != 33 || !Util.DigitsOnly(newCrosshairString)) { return false; }

        int tstyleValue, centerDotValue, outlineValue, sizeValueInt, thicknessValueInt, gapValueInt, redValueInt, greenValueInt, blueValueInt, alphaValueInt, redOutlineValueInt, greenOutlineValueInt, blueOutlineValueInt, alphaOutlineValueInt;
        string sizeValue, thicknessValue, gapValue, redValue, greenValue, blueValue, alphaValue, redOutlineValue, greenOutlineValue, blueOutlineValue, alphaOutlineValue;

        tstyleValue       = int.Parse(newCrosshairString[0].ToString());
        centerDotValue    = int.Parse(newCrosshairString[1].ToString());
        sizeValue         = newCrosshairString.Substring(2, 2);
        thicknessValue    = newCrosshairString.Substring(4, 2);
        gapValue          = newCrosshairString.Substring(6, 2);
        outlineValue      = int.Parse(newCrosshairString[8].ToString());
        redValue          = newCrosshairString.Substring(9, 3);
        greenValue        = newCrosshairString.Substring(12, 3);
        blueValue         = newCrosshairString.Substring(15, 3);
        alphaValue        = newCrosshairString.Substring(18, 3);
        redOutlineValue   = newCrosshairString.Substring(21, 3);
        greenOutlineValue = newCrosshairString.Substring(24, 3);
        blueOutlineValue  = newCrosshairString.Substring(27, 3);
        alphaOutlineValue = newCrosshairString.Substring(30, 3);

        if (sizeValue == "00") {      sizeValueInt      = 0; } else { sizeValueInt      = int.Parse(sizeValue.TrimStart(new char[] { '0' })); }
        if (thicknessValue != "00") { thicknessValueInt = int.Parse(thicknessValue.TrimStart(new char[] { '0' })); } else { return false; }
        if (gapValue == "00") {       gapValueInt       = 0; } else { gapValueInt       = int.Parse(gapValue.TrimStart(new char[] { '0' })); }

        if (redValue == "000") {   redValueInt   = 0; } else { redValueInt   = int.Parse(redValue.TrimStart(new char[] { '0' })); }
        if (greenValue == "000") { greenValueInt = 0; } else { greenValueInt = int.Parse(greenValue.TrimStart(new char[] { '0' })); }
        if (blueValue == "000") {  blueValueInt  = 0; } else { blueValueInt  = int.Parse(blueValue.TrimStart(new char[] { '0' })); }
        if (alphaValue == "000") { alphaValueInt = 0; } else { alphaValueInt = int.Parse(alphaValue.TrimStart(new char[] { '0' })); }

        if (redOutlineValue == "000") {   redOutlineValueInt   = 0; } else { redOutlineValueInt   = int.Parse(redOutlineValue.TrimStart(new char[] { '0' })); }
        if (greenOutlineValue == "000") { greenOutlineValueInt = 0; } else { greenOutlineValueInt = int.Parse(greenOutlineValue.TrimStart(new char[] { '0' })); }
        if (blueOutlineValue == "000") {  blueOutlineValueInt  = 0; } else { blueOutlineValueInt  = int.Parse(blueOutlineValue.TrimStart(new char[] { '0' })); }
        if (alphaOutlineValue == "000") { alphaOutlineValueInt = 0; } else { alphaOutlineValueInt = int.Parse(alphaOutlineValue.TrimStart(new char[] { '0' })); }

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

        if (!ValidateCrosshairValues(tstyleValue, centerDotValue, sizeValueInt, thicknessValueInt, gapValueInt, outlineValue, redValueInt, greenValueInt, blueValueInt, alphaValueInt, redOutlineValueInt, greenOutlineValueInt, blueOutlineValueInt, alphaOutlineValueInt)) { return false; }

        bool tstyleValueBool    = tstyleValue    != 0;
        bool centerDotValueBool = centerDotValue != 0;
        bool outlineValueBool   = outlineValue   != 0;

        if (setValues) {
            crosshairStringFull = $"{tstyleValue}{centerDotValue}{sizeValue}{thicknessValue}{gapValue}{outlineValue}{redValue}{greenValue}{blueValue}{alphaValue}{redOutlineValue}{greenOutlineValue}{blueOutlineValue}{alphaOutlineValue}";

            SetAllCrosshairValues(tstyleValueBool, centerDotValueBool, sizeValueInt, thicknessValueInt, gapValueInt, outlineValueBool, redValueInt, greenValueInt, blueValueInt, alphaValueInt, redOutlineValueInt, greenOutlineValueInt, blueOutlineValueInt, alphaOutlineValueInt, true);
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

    /* No longer needed export crosshair string.
    /// <summary>
    /// Exports full crosshair string with all current crosshair values.
    /// </summary>
    /// <returns></returns>
    public string ExportCrosshairString_Temp() {
        int tstyleInt, centerDotInt, outlineInt;
        string sizeString, thicknessString, gapString, redFloat, greenFloat, blueFloat, alphaFloat, redOutlineFloat, greenOutlineFloat, blueOutlineFloat, alphaOutlineFloat;

        tstyleInt         = m_crosshair.tStyle ? 1 : 0;
        centerDotInt      = m_crosshair.centerDot ? 1 : 0;
        sizeString        = m_crosshair.size.ToString("00");
        thicknessString   = m_crosshair.thickness.ToString("00");
        gapString         = m_crosshair.gap.ToString("00");
        outlineInt        = m_crosshair.enableOutline ? 1 : 0;
        redFloat          = (m_crosshair.color.r * 255.0f).ToString("000");
        greenFloat        = (m_crosshair.color.g * 255.0f).ToString("000");
        blueFloat         = (m_crosshair.color.b * 255.0f).ToString("000");
        alphaFloat        = (m_crosshair.color.a * 255.0f).ToString("000");
        redOutlineFloat   = (m_crosshair.outlineColor.r * 255.0f).ToString("000");
        greenOutlineFloat = (m_crosshair.outlineColor.g * 255.0f).ToString("000");
        blueOutlineFloat  = (m_crosshair.outlineColor.b * 255.0f).ToString("000");
        alphaOutlineFloat = (m_crosshair.outlineColor.a * 255.0f).ToString("000");

        //Debug.Log($"EXPORTED CROSSHAIR STRING: {tstyleInt}{centerDotInt}{sizeString}{thicknessString}{gapString}{outlineInt}{redFloat}{greenFloat}{blueFloat}{alphaFloat}{redOutlineFloat}{greenOutlineFloat}{blueOutlineFloat}{alphaOutlineFloat}");
        return $"{tstyleInt}{centerDotInt}{sizeString}{thicknessString}{gapString}{outlineInt}{redFloat}{greenFloat}{blueFloat}{alphaFloat}{redOutlineFloat}{greenOutlineFloat}{blueOutlineFloat}{alphaOutlineFloat}";
    }
    */

    public static void SetCrosshairString_Static() { simpleCrosshair.SetCrosshairString(); }

    public void SetCrosshairString() {
        int tstyleInt, centerDotInt, outlineInt;
        string sizeString, thicknessString, gapString, redFloat, greenFloat, blueFloat, alphaFloat, redOutlineFloat, greenOutlineFloat, blueOutlineFloat, alphaOutlineFloat;

        tstyleInt           = m_crosshair.tStyle ? 1 : 0;
        centerDotInt        = m_crosshair.centerDot ? 1 : 0;
        sizeString          = m_crosshair.size.ToString("00");
        thicknessString     = m_crosshair.thickness.ToString("00");
        gapString           = m_crosshair.gap.ToString("00");
        outlineInt          = m_crosshair.enableOutline ? 1 : 0;
        redFloat            = (m_crosshair.color.r * 255.0f).ToString("000");
        greenFloat          = (m_crosshair.color.g * 255.0f).ToString("000");
        blueFloat           = (m_crosshair.color.b * 255.0f).ToString("000");
        alphaFloat          = (m_crosshair.color.a * 255.0f).ToString("000");
        redOutlineFloat     = (m_crosshair.outlineColor.r * 255.0f).ToString("000");
        greenOutlineFloat   = (m_crosshair.outlineColor.g * 255.0f).ToString("000");
        blueOutlineFloat    = (m_crosshair.outlineColor.b * 255.0f).ToString("000");
        alphaOutlineFloat   = (m_crosshair.outlineColor.a * 255.0f).ToString("000");

        //Debug.Log($"EXPORTED CROSSHAIR STRING: {tstyleInt}{centerDotInt}{sizeString}{thicknessString}{gapString}{outlineInt}{redFloat}{greenFloat}{blueFloat}{alphaFloat}{redOutlineFloat}{greenOutlineFloat}{blueOutlineFloat}{alphaOutlineFloat}");
        crosshairStringFull = $"{tstyleInt}{centerDotInt}{sizeString}{thicknessString}{gapString}{outlineInt}{redFloat}{greenFloat}{blueFloat}{alphaFloat}{redOutlineFloat}{greenOutlineFloat}{blueOutlineFloat}{alphaOutlineFloat}";
        CrosshairSettings.SaveCrosshairString(crosshairStringFull);
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

    /// <summary>
    /// Generates random crosshair values, sets them to crosshair, then redraws.
    /// </summary>
    public void GenerateRandomCrosshair() {
        int randomTstyleValue       = UnityEngine.Random.Range(0, 2);
        int randomCenterDotValue    = UnityEngine.Random.Range(0, 2);
        int randomSizeValue         = UnityEngine.Random.Range(1, 46);
        int randomThicknessValue    = UnityEngine.Random.Range(1, 16);
        int randomGapValue          = UnityEngine.Random.Range(1, 26);
        int randomOutlineValue      = UnityEngine.Random.Range(0, 2);
        int randomRedValue          = UnityEngine.Random.Range(0, 256);
        int randomGreenValue        = UnityEngine.Random.Range(0, 256);
        int randomBlueValue         = UnityEngine.Random.Range(0, 256);
        int randomAlphaValue        = UnityEngine.Random.Range(0, 256);
        int randomRedOutlineValue   = UnityEngine.Random.Range(0, 256);
        int randomGreenOutlineValue = UnityEngine.Random.Range(0, 256);
        int randomBlueOutlineValue  = UnityEngine.Random.Range(0, 256);
        int randomAlphaOutlineValue = UnityEngine.Random.Range(0, 256);

        bool randomTstyleValueBool    = randomTstyleValue    != 0;
        bool randomCenterDotValueBool = randomCenterDotValue != 0;
        bool randomOutlineValueBool   = randomOutlineValue   != 0;

        SetAllCrosshairValues(randomTstyleValueBool, randomCenterDotValueBool, randomSizeValue, randomThicknessValue, randomGapValue, randomOutlineValueBool, randomRedValue, randomGreenValue, randomBlueValue, randomAlphaValue, randomRedOutlineValue, randomGreenOutlineValue, randomBlueOutlineValue, randomAlphaOutlineValue, true);
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
