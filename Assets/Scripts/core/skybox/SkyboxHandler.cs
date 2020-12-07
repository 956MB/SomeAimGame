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
                case SkyboxType.PINK:   SetRenderSettings(skyboxHandler.skyboxPink, skyboxHandler.skyboxPinkLighting);     break;
                case SkyboxType.GOLDEN: SetRenderSettings(skyboxHandler.skyboxGolden, skyboxHandler.skyboxGoldenLighting); break;
                case SkyboxType.NIGHT:  SetRenderSettings(skyboxHandler.skyboxNight, skyboxHandler.skyboxNightLighting);   break;
                case SkyboxType.GREY:   SetRenderSettings(skyboxHandler.skyboxGrey, skyboxHandler.skyboxGreyLighting);     break;
                case SkyboxType.BLUE:   SetRenderSettings(skyboxHandler.skyboxBlue, skyboxHandler.skyboxBlueLighting);     break;
                case SkyboxType.SLATE:  SetRenderSettings(skyboxHandler.skyboxSlate, skyboxHandler.skyboxSlateLighting);   break;
            }

            //DynamicGI.UpdateEnvironment();
        }

        /// <summary>
        /// Sets RenderSettings from supplied Material (setSkyboxMaterial) and Cubemap (setCubeMap).
        /// </summary>
        /// <param name="setSkyboxMaterial"></param>
        /// <param name="setCubeMap"></param>
        private static void SetRenderSettings(Material setSkyboxMaterial, Cubemap setCubeMap) {
            RenderSettings.skybox           = setSkyboxMaterial;
            RenderSettings.customReflection = setCubeMap;
        }
    }
}
