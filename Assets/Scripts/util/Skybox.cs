using UnityEngine;

public class Skybox : MonoBehaviour {
    public Material skyboxPink, skyboxGolden, skyboxNight, skyboxGrey, skyboxBlue, skyboxSlate;
    public Cubemap skyboxPinkLighting, skyboxGoldenLighting, skyboxNightLighting, skyboxGreyLighting, skyboxBlueLighting, skyboxSlateLighting;

    private static Skybox skybox;
    void Awake() { skybox = this; }

    /// <summary>
    /// Sets new skybox and customReflection from supplied skybox string (setSkybox).
    /// </summary>
    /// <param name="setSkybox"></param>
    public static void SetNewSkybox(string setSkybox) {
        switch (setSkybox) {
            case "Skybox-Pink":
                RenderSettings.skybox           = skybox.skyboxPink;
                RenderSettings.customReflection = skybox.skyboxPinkLighting;
                break;
            case "Skybox-Golden":
                RenderSettings.skybox           = skybox.skyboxGolden;
                RenderSettings.customReflection = skybox.skyboxGoldenLighting;
                break;
            case "Skybox-Night":
                RenderSettings.skybox           = skybox.skyboxNight;
                RenderSettings.customReflection = skybox.skyboxNightLighting;
                break;
            case "Skybox-Grey":
                RenderSettings.skybox           = skybox.skyboxGrey;
                RenderSettings.customReflection = skybox.skyboxGreyLighting;
                break;
            case "Skybox-Blue":
                RenderSettings.skybox           = skybox.skyboxBlue;
                RenderSettings.customReflection = skybox.skyboxBlueLighting;
                break;
            case "Skybox-Slate":
                RenderSettings.skybox           = skybox.skyboxSlate;
                RenderSettings.customReflection = skybox.skyboxSlateLighting;
                break;
        }

        //DynamicGI.UpdateEnvironment();
    }
}
