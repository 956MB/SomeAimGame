using SomeAimGame.Skybox;
using SomeAimGame.Gamemode;
using SomeAimGame.Targets;

[System.Serializable]
public class CosmeticsDataSerial {
    public GamemodeType gamemode;
    public TargetType   targetColor;
    public SkyboxType   skybox;
    public float        afterActionReportPanelX;
    public float        afterActionReportPanelY;
    public float        extraStatsPanelX;
    public float        extraStatsPanelY;
    public bool         quickStartGame;

    public CosmeticsDataSerial() {
        gamemode                = CosmeticsSettings.gamemode;
        targetColor             = CosmeticsSettings.targetColor;
        skybox                  = CosmeticsSettings.skybox;
        afterActionReportPanelX = CosmeticsSettings.afterActionReportPanelX;
        afterActionReportPanelY = CosmeticsSettings.afterActionReportPanelY;
        extraStatsPanelX        = CosmeticsSettings.extraStatsPanelX;
        extraStatsPanelY        = CosmeticsSettings.extraStatsPanelY;
        quickStartGame          = CosmeticsSettings.quickStartGame;
    }
}
