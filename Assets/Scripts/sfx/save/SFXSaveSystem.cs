using UnityEngine;
using UnityEngine.UI;

using SomeAimGame.Utilities;

namespace SomeAimGame.SFX {
    public class SFXSaveSystem : MonoBehaviour {
        public Toggle targetSoundOnToggleObject, targetMissSoundOnToggleObject, UISoundOnToggleObject;

        private static SFXSaveSystem sfxSave;
        void Awake() { sfxSave = this; }

        /// <summary>
        /// Saves supplied SFX settings object (SFXSettings) to file.
        /// </summary>
        /// <param name="sfxSettings"></param>
        public static void SaveSFXSettingsData() {
            SFXSettingsDataSerial sfxData = new SFXSettingsDataSerial();
            SaveLoadUtil.SaveDataSerial("/prefs", "/sag_sfx.prefs", sfxData);
        }

        /// <summary>
        /// Loads SFX settings data (SFXSettingsDataSerial) from file.
        /// </summary>
        /// <returns></returns>
        public static SFXSettingsDataSerial LoadSFXSettingsData() {
            SFXSettingsDataSerial sfxData = (SFXSettingsDataSerial)SaveLoadUtil.LoadDataSerial("/prefs/sag_sfx.prefs", SaveType.SFX);
            return sfxData;
        }

        /// <summary>
        /// Inits saved SFX settings object and sets all SFX settings values.
        /// </summary>
        public static void InitSavedSFXSettings() {
            SFXSettingsDataSerial loadedSFXData = LoadSFXSettingsData();
            if (loadedSFXData != null) {
                SFXSettings.LoadExtraSettings(loadedSFXData);

                SetTargetSoundClip(loadedSFXData.targetSoundClip);
                SetTargetMissSoundClip(loadedSFXData.targetMissSoundClip);
                SetTargetSoundOnToggle(loadedSFXData.targetSoundOn);
                SetUISoundOnToggle(loadedSFXData.uiSoundOn);
                SetTargetMissSoundOnToggle(loadedSFXData.targetMissSoundOn);
            } else {
                //Debug.Log("failed to init extra in 'initSavedSettings', extra: " + loadedExtraData);
                InitSFXSettingsDefaults();
            }
        }

        /// <summary>
        /// Inits default SFX settings values and saves to file on first launch.
        /// </summary>
        public static void InitSFXSettingsDefaults() {
            SetTargetSoundOnToggle(true);
            SetUISoundOnToggle(true);
            SetTargetSoundClip(SFXType.TARGET_HIT_SFX_0);
            SetTargetMissSoundClip(SFXType.TARGET_MISS_SFX_0);
            SetTargetMissSoundOnToggle(false);

            SFXSettings.SaveAllSFXSettingsDefaults(SFXType.TARGET_HIT_SFX_0, SFXType.TARGET_MISS_SFX_0, true, false, true);
        }

        /// <summary>
        /// Sets target sound clip to supplied SFXType (targetSoundClip).
        /// </summary>
        /// <param name="targetSoundClip"></param>
        private static void SetTargetSoundClip(SFXType targetSoundClip) {            SFXManager.targetHitSound                  = SFXManager.ReturnTargetSFXClip(targetSoundClip); }
        /// <summary>
        /// Sets target miss sound clip to supplied SFXType (targetMissSoundClip).
        /// </summary>
        /// <param name="targetMissSoundClip"></param>
        private static void SetTargetMissSoundClip(SFXType targetMissSoundClip) {    SFXManager.targetMissSound                 = SFXManager.ReturnTargetSFXClip(targetMissSoundClip); }
        /// <summary>
        /// Sets target sound on toggle to supplied bool (targetSoundToggle).
        /// </summary>
        /// <param name="targetSoundToggle"></param>
        private static void SetTargetSoundOnToggle(bool targetSoundToggle) {         sfxSave.targetSoundOnToggleObject.isOn     = targetSoundToggle; }
        /// <summary>
        /// Sets target miss sound on toggle to supplied bool (targetMissSoundToggle).
        /// </summary>
        /// <param name="targetMissSoundToggle"></param>
        private static void SetTargetMissSoundOnToggle(bool targetMissSoundToggle) { sfxSave.targetMissSoundOnToggleObject.isOn = targetMissSoundToggle; }
        /// <summary>
        /// Sets UI sound on toggle to supplied bool (UISoundToggle).
        /// </summary>
        /// <param name="UISoundToggle"></param>
        private static void SetUISoundOnToggle(bool UISoundToggle) {                 sfxSave.UISoundOnToggleObject.isOn         = UISoundToggle; }
    }
}