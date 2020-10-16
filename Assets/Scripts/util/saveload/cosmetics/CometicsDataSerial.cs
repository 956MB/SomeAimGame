
[System.Serializable]
public class CosmeticsDataSerial {
    public string gamemode;
    public string targetColor;
    public string skybox;
    public float afterActionReportPanelX;
    public float afterActionReportPanelY;
    public float extraStatsPanelX;
    public float extraStatsPanelY;
    public bool quickStartGame;

    public CosmeticsDataSerial() {
        gamemode    = CosmeticsSettings.gamemode;
        targetColor = CosmeticsSettings.targetColor;
        skybox      = CosmeticsSettings.skybox;
        afterActionReportPanelX = CosmeticsSettings.afterActionReportPanelX;
        afterActionReportPanelY = CosmeticsSettings.afterActionReportPanelY;
        extraStatsPanelX = CosmeticsSettings.extraStatsPanelX;
        extraStatsPanelY = CosmeticsSettings.extraStatsPanelY;
        quickStartGame   = CosmeticsSettings.quickStartGame;
    }
}
