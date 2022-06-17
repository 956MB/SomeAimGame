using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SomeAimGame.Utilities {
    public class ManipulatePostProcess : MonoBehaviour {
        public Camera mainCamera;
        public PostProcessVolume postProcessVolume;
        public static DepthOfField dof;
        public static ChromaticAberration ca;
        public static Vignette vig;

        private static ManipulatePostProcess PP;
        void Awake() {
            PP = this;
            postProcessVolume.profile.TryGetSettings(out dof);
            postProcessVolume.profile.TryGetSettings(out ca);
            postProcessVolume.profile.TryGetSettings(out vig);
        }

        /// <summary>
        /// Enables DepthOfField effect in post process volume (postProcessVolume).
        /// </summary>
        public static void SetDOF(bool enabled) { dof.enabled.value = enabled; }
        /// <summary>
        /// Enables ChromaticAberation effect in post process volume (postProcessVolume).
        /// </summary>
        public static void SetCA(bool enabled) { ca.enabled.value = enabled; }
        /// <summary>
        /// Enables vignette effect in post process volume (postProcessVolume).
        /// </summary>
        public static void SetVIG(bool enabled) { vig.enabled.value = enabled; }

        public static void SetPanelEffects(bool DOFenabled, bool CAenabled) {
            SetDOF(DOFenabled);
            SetCA(CAenabled);
        }

        public static void SetCameraClearFlags(CameraClearFlags setClearFlags) {
            PP.mainCamera.clearFlags = setClearFlags;
        }
    }
}
