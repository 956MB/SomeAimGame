
[System.Serializable]
public class CrosshairDataSerial {
    public bool   centerDot;
    public bool   TStyle;
    public float  size;
    public float  thickness;
    public float  gap;
    public bool   outlineEnabled;
    public float  red;
    public float  green;
    public float  blue;
    public float  alpha;
    public float  outlineRed;
    public float  outlineGreen;
    public float  outlineBlue;
    public float  outlineAlpha;
    public string crosshairString;

    public CrosshairDataSerial() {
        centerDot       = CrosshairSettings.centerDot;
        TStyle          = CrosshairSettings.TStyle;
        size            = CrosshairSettings.size;
        thickness       = CrosshairSettings.thickness;
        gap             = CrosshairSettings.gap;
        outlineEnabled  = CrosshairSettings.outlineEnabled;
        red             = CrosshairSettings.red;
        green           = CrosshairSettings.green;
        blue            = CrosshairSettings.blue;
        alpha           = CrosshairSettings.alpha;
        outlineRed      = CrosshairSettings.outlineRed;
        outlineGreen    = CrosshairSettings.outlineGreen;
        outlineBlue     = CrosshairSettings.outlineBlue;
        outlineAlpha    = CrosshairSettings.outlineAlpha;
        crosshairString = CrosshairSettings.crosshairString;
    }
}
