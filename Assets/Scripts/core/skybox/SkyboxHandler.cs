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
            case Skybox.Pink:
                RenderSettings.skybox           = skyboxHandler.skyboxPink;
                RenderSettings.customReflection = skyboxHandler.skyboxPinkLighting;
                break;
            case Skybox.Golden:
                RenderSettings.skybox           = skyboxHandler.skyboxGolden;
                RenderSettings.customReflection = skyboxHandler.skyboxGoldenLighting;
                break;
            case Skybox.Night:
                RenderSettings.skybox           = skyboxHandler.skyboxNight;
                RenderSettings.customReflection = skyboxHandler.skyboxNightLighting;
                break;
            case Skybox.Grey:
                RenderSettings.skybox           = skyboxHandler.skyboxGrey;
                RenderSettings.customReflection = skyboxHandler.skyboxGreyLighting;
                break;
            case Skybox.Blue:
                RenderSettings.skybox           = skyboxHandler.skyboxBlue;
                RenderSettings.customReflection = skyboxHandler.skyboxBlueLighting;
                break;
            case Skybox.Slate:
                RenderSettings.skybox           = skyboxHandler.skyboxSlate;
                RenderSettings.customReflection = skyboxHandler.skyboxSlateLighting;
                break;
        }

        //DynamicGI.UpdateEnvironment();
    }
}
