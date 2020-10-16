
[System.Serializable]
public class CrosshairDataSerial {
    public bool centerDot;
    public bool TStyle;
    public float size;
    public float thickness;
    public float gap;
    public bool outlineEnabled;
    public float outline;
    public float red;
    public float green;
    public float blue;
    public float alpha;

    public CrosshairDataSerial() {
        centerDot      = CrosshairSettings.centerDot;
        TStyle         = CrosshairSettings.TStyle;
        size           = CrosshairSettings.size;
        thickness      = CrosshairSettings.thickness;
        gap            = CrosshairSettings.gap;
        outlineEnabled = CrosshairSettings.outlineEnabled;
        outline        = CrosshairSettings.outline;
        red            = CrosshairSettings.red;
        green          = CrosshairSettings.green;
        blue           = CrosshairSettings.blue;
        alpha          = CrosshairSettings.alpha;
    }
}
