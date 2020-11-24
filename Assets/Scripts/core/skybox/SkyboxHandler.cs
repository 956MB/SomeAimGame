using UnityEngine;

public class SkyboxHandler : MonoBehaviour {
    public Material skyboxPink, skyboxGolden, skyboxNight, skyboxGrey, skyboxBlue, skyboxSlate;
    public Cubemap skyboxPinkLighting, skyboxGoldenLighting, skyboxNightLighting, skyboxGreyLighting, skyboxBlueLighting, skyboxSlateLighting;

    private static SkyboxHandler skyboxHandler;
    void Awake() { skyboxHandler = this; }

    /// <summary>
    /// Sets new skybox and customReflection from supplied skybox string (setSkybox).
    /// </summary>
    /// <param name="setSkybox"></param>
    public static void SetNewSkybox(Skybox setSkybox) {
        switch (setSkybox) {
            case Skybox.PINK:
                RenderSettings.skybox           = skyboxHandler.skyboxPink;
                RenderSettings.customReflection = skyboxHandler.skyboxPinkLighting;
                break;
            case Skybox.GOLDEN:
                RenderSettings.skybox           = skyboxHandler.skyboxGolden;
                RenderSettings.customReflection = skyboxHandler.skyboxGoldenLighting;
                break;
            case Skybox.NIGHT:
                RenderSettings.skybox           = skyboxHandler.skyboxNight;
                RenderSettings.customReflection = skyboxHandler.skyboxNightLighting;
                break;
            case Skybox.GREY:
                RenderSettings.skybox           = skyboxHandler.skyboxGrey;
                RenderSettings.customReflection = skyboxHandler.skyboxGreyLighting;
                break;
            case Skybox.BLUE:
                RenderSettings.skybox           = skyboxHandler.skyboxBlue;
                RenderSettings.customReflection = skyboxHandler.skyboxBlueLighting;
                break;
            case Skybox.SLATE:
                RenderSettings.skybox           = skyboxHandler.skyboxSlate;
                RenderSettings.customReflection = skyboxHandler.skyboxSlateLighting;
                break;
        }

        //DynamicGI.UpdateEnvironment();
    }
}
