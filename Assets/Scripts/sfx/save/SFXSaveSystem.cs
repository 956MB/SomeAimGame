using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SomeAimGame.SFX {
    public class SFXSaveSystem : MonoBehaviour {
        public Toggle targetSoundOnToggleObject, targetMissSoundOnToggleObject, UISoundOnToggleObject;

        private static SFXSaveSystem sfxSave;
        void Awake() { sfxSave = this; }

        /// <summary>
        /// Saves supplied SFX settings object (SFXSettings) to file.
        /// </summary>
        /// <param name="sfxSettings"></param>
        public static void SaveSFXSettingsData(SFXSettings sfxSettings) {
            BinaryFormatter formatter = new BinaryFormatter();
            string dirPath            = Application.persistentDataPath + "/prefs";
            string filePath           = dirPath + "/sag_sfx.prefs";

            DirectoryInfo dirInf = new DirectoryInfo(dirPath);
            if (!dirInf.Exists) { dirInf.Create(); }

            FileStream stream = new FileStream(filePath, FileMode.Create);
            SFXSettingsDataSerial sfxData = new SFXSettingsDataSerial();
            formatter.Serialize(stream, sfxData);
            stream.Close();
        }

        /// <summary>
        /// Loads SFX settings data (SFXSettingsDataSerial) from file.
        /// </summary>
        /// <returns></returns>
        public static SFXSettingsDataSerial LoadSFXSettingsData() {
            string path = Application.persistentDataPath + "/prefs/sag_sfx.prefs";
            if (File.Exists(path)) {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream         = new FileStream(path, FileMode.Open);

                SFXSettingsDataSerial sfxData = formatter.Deserialize(stream) as SFXSettingsDataSerial;
                stream.Close();

                return sfxData;
            } else {
                return null;
            }
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
        private static void SetTargetSoundClip(SFXType targetSoundClip) {
            SFXManager.targetHitSound = SFXManager.ReturnTargetSFXClip(targetSoundClip);
        }
        /// <summary>
        /// Sets target miss sound clip to supplied SFXType (targetMissSoundClip).
        /// </summary>
        /// <param name="targetMissSoundClip"></param>
        private static void SetTargetMissSoundClip(SFXType targetMissSoundClip) {
            SFXManager.targetMissSound = SFXManager.ReturnTargetSFXClip(targetMissSoundClip);
        }
        /// <summary>
        /// Sets target sound on toggle to supplied bool (targetSoundToggle).
        /// </summary>
        /// <param name="targetSoundToggle"></param>
        private static void SetTargetSoundOnToggle(bool targetSoundToggle) {
            sfxSave.targetSoundOnToggleObject.isOn = targetSoundToggle;
        }
        /// <summary>
        /// Sets target miss sound on toggle to supplied bool (targetMissSoundToggle).
        /// </summary>
        /// <param name="targetMissSoundToggle"></param>
        private static void SetTargetMissSoundOnToggle(bool targetMissSoundToggle) {
            sfxSave.targetMissSoundOnToggleObject.isOn = targetMissSoundToggle;
        }
        /// <summary>
        /// Sets UI sound on toggle to supplied bool (UISoundToggle).
        /// </summary>
        /// <param name="UISoundToggle"></param>
        private static void SetUISoundOnToggle(bool UISoundToggle) {
            sfxSave.UISoundOnToggleObject.isOn = UISoundToggle;
        }
    }
}
