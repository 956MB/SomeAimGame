using UnityEngine;

namespace SomeAimGame.Skybox {

    public class SkyboxHandler : MonoBehaviour {
        public Material skyboxPink, skyboxGolden, skyboxNight, skyboxGrey, skyboxBlue, skyboxSlate;
        public Cubemap skyboxPinkLighting, skyboxGoldenLighting, skyboxNightLighting, skyboxGreyLighting, skyboxBlueLighting, skyboxSlateLighting;

        private static SkyboxHandler skyboxHandler;
        void Awake() { skyboxHandler = this; }

        /// <summary>
        /// Sets new skybox and customReflection from supplied skybox string (setSkybox).
        /// </summary>
        /// <param name="setSkybox"></param>
        public static void SetNewSkybox(SkyboxType setSkybox) {
            switch (setSkybox) {
                case SkyboxType.PINK:
                    RenderSettings.skybox           = skyboxHandler.skyboxPink;
                    RenderSettings.customReflection = skyboxHandler.skyboxPinkLighting;
                    break;
                case SkyboxType.GOLDEN:
                    RenderSettings.skybox           = skyboxHandler.skyboxGolden;
                    RenderSettings.customReflection = skyboxHandler.skyboxGoldenLighting;
                    break;
                case SkyboxType.NIGHT:
                    RenderSettings.skybox           = skyboxHandler.skyboxNight;
                    RenderSettings.customReflection = skyboxHandler.skyboxNightLighting;
                    break;
                case SkyboxType.GREY:
                    RenderSettings.skybox           = skyboxHandler.skyboxGrey;
                    RenderSettings.customReflection = skyboxHandler.skyboxGreyLighting;
                    break;
                case SkyboxType.BLUE:
                    RenderSettings.skybox           = skyboxHandler.skyboxBlue;
                    RenderSettings.customReflection = skyboxHandler.skyboxBlueLighting;
                    break;
                case SkyboxType.SLATE:
                    RenderSettings.skybox           = skyboxHandler.skyboxSlate;
                    RenderSettings.customReflection = skyboxHandler.skyboxSlateLighting;
                    break;
            }

            //DynamicGI.UpdateEnvironment();
        }
    }
}
