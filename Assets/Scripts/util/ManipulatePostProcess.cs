using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SomeAimGame.Utilities {
    public class ManipulatePostProcess : MonoBehaviour {
        public PostProcessVolume postProcessVolume;
        public static DepthOfField dof;
        public static ChromaticAberration ca;

        void Awake() {
            postProcessVolume.profile.TryGetSettings(out dof);
            postProcessVolume.profile.TryGetSettings(out ca);
        }

        /// <summary>
        /// Enables DepthOfField effect in post process volume (postProcessVolume).
        /// </summary>
        public static void EnableDOF() { dof.enabled.value = true; }
        /// <summary>
        /// Disables DepthOfField effect in post process volume (postProcessVolume).
        /// </summary>
        public static void DisableDOF() { dof.enabled.value = false; }
        /// <summary>
        /// Enables ChromaticAberation effect in post process volume (postProcessVolume).
        /// </summary>
        public static void EnableCA() { ca.enabled.value = true; }
        /// <summary>
        /// Disables ChromaticAberation effect in post process volume (postProcessVolume).
        /// </summary>
        public static void DisableCA() { ca.enabled.value = false; }

        public static void EnableEffects() {
            EnableDOF();
            EnableCA();
        }

        public static void DisableEffects() {
            DisableDOF();
            DisableCA();
        }
    }
}
