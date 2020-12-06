using UnityEngine;

namespace SomeAimGame.SFX {
    public class SFXSettings : MonoBehaviour {
        public static SFXType targetSoundClip     = SFXType.TARGET_HIT_SFX_0;
        public static SFXType targetMissSoundClip = SFXType.TARGET_MISS_SFX_0;
        public static bool targetSoundOn          = true;
        public static bool targetMissSoundOn      = false;
        public static bool uiSoundOn              = true;

        private static SFXSettings sfxSettings;
        void Awake() { sfxSettings = this; }

        /// <summary>
        /// Saves supplied target sound clip SFXType (setTargetSoundClip) to SFX settings object (SFXSettings), then saves SFX settings object.
        /// </summary>
        /// <param name="setTargetSoundClip"></param>
        public static void SaveTargetSoundClip(SFXType setTargetSoundClip) {
            targetSoundClip = setTargetSoundClip;
            sfxSettings.SaveSFXSettings();
        }
        /// <summary>
        /// Saves supplied target sound on bool (setTargetSoundOn) to SFX settings object (SFXSettings), then saves SFX settings object.
        /// </summary>
        /// <param name="setTargetSoundOn"></param>
        public static void SaveTargetSoundOn(bool setTargetSoundOn) {
            targetSoundOn = setTargetSoundOn;
            sfxSettings.SaveSFXSettings();
        }
        /// <summary>
        /// Saves supplied target miss sound on bool (setTargetMissSoundOn) to SFX settings object (SFXSettings), then saves SFX settings object.
        /// </summary>
        /// <param name="setTargetMissSoundOn"></param>
        public static void SaveTargetMissSoundOn(bool setTargetMissSoundOn) {
            targetMissSoundOn = setTargetMissSoundOn;
            sfxSettings.SaveSFXSettings();
        }
        /// <summary>
        /// Saves supplied UI sound bool (setUISoundOn) to SFX settings object (SFXSettings), then saves SFX settings object.
        /// </summary>
        /// <param name="setUISoundOn"></param>
        public static void SaveUISoundOn(bool setUISoundOn) {
            uiSoundOn = setUISoundOn;
            sfxSettings.SaveSFXSettings();
        }

        /// <summary>
        /// Calls 'SFXSaveSystem.SaveSFXSettingsData()' to save SFX settings object (SFXSettings) to file.
        /// </summary>
        public void SaveSFXSettings() { SFXSaveSystem.SaveSFXSettingsData(this); }
        public static void SaveSFXSettings_Static() { sfxSettings.SaveSFXSettings(); }

        /// <summary>
        /// Saves default SFX settings object (SFXSettings).
        /// </summary>
        /// <param name="setTargetSoundClip"></param>
        /// <param name="setTargetMissSoundClip"></param>
        /// <param name="setTargetSoundOn"></param>
        /// <param name="setTargetMissSoundOn"></param>
        /// <param name="setUISoundOn"></param>
        public static void SaveAllSFXSettingsDefaults(SFXType setTargetSoundClip, SFXType setTargetMissSoundClip, bool setTargetSoundOn, bool setTargetMissSoundOn, bool setUISoundOn) {
            targetSoundClip     = setTargetSoundClip;
            targetMissSoundClip = setTargetMissSoundClip;
            targetSoundOn       = setTargetSoundOn;
            targetMissSoundOn   = setTargetMissSoundOn;
            uiSoundOn           = setUISoundOn;

            sfxSettings.SaveSFXSettings();
        }

        /// <summary>
        /// Loads SFX settings data (SFXSettingsDataSerial) and sets values to this SFX settings object (SFXSettings).
        /// </summary>
        /// <param name="SFXData"></param>
        public static void LoadExtraSettings(SFXSettingsDataSerial SFXData) {
            targetSoundClip     = SFXData.targetSoundClip;
            targetMissSoundClip = SFXData.targetMissSoundClip;
            targetSoundOn       = SFXData.targetSoundOn;
            targetMissSoundOn   = SFXData.targetMissSoundOn;
            uiSoundOn           = SFXData.uiSoundOn;
        }
    }
}
