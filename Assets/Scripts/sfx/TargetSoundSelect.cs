using UnityEngine;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Utilities;

namespace SomeAimGame.SFX {
    public class TargetSoundSelect : MonoBehaviour {
        public TMP_Text targetHitSoundText, targetMissSoundText, arrowTextHit, arrowTextMiss;
        public GameObject targetHitSoundDropdownBody, targetMissSoundDropdownBody, targetSoundSelectionContainer;
        public CanvasGroup soundSelectionCanvasGroup, targetMissContainerCanvasGroup;
        public Image uiSoundBackground;
        public static bool targetHitSoundSelectOpen          = false;
        public static bool targetMissSoundSelectOpen         = false;
        public static bool targetSoundSelectionContainerOpen = false;
        public static bool targetSoundsSaveReady             = false;

        public static TargetSoundSelect targetSoundSelect;
        private void Awake() { targetSoundSelect = this; }

        private void Start() {
            SetTargetHitSelect(false);
            SetTargetMissSelect(false);
        }

        public static void CheckSaveTargetSoundSelection() {
            if (targetSoundsSaveReady) {
                SFXSettings.SaveSFXSettings_Static();
                targetSoundsSaveReady = false;
            }
        }

        public static void CheckCloseTargetSoundDropdowns() {
            if (targetHitSoundSelectOpen) {  SetTargetHitSelect(false); }
            if (targetMissSoundSelectOpen) { SetTargetMissSelect(false); }
        }

        public static void SetTargetHitSoundText(string soundName) {  targetSoundSelect.targetHitSoundText.SetText($"{soundName}"); }
        public static void SetTargetMissSoundText(string soundName) { targetSoundSelect.targetMissSoundText.SetText($"{soundName}"); }

        public static void TestTargetHitSound() {  SFXManager.PlaySFXTargetHit(); }
        public static void TestTargetMissSound() { SFXManager.PlaySFXTargetMiss(); }

        public static void ToggleTargetHitSoundSelect_Static() {  SetTargetHitSelect(!targetHitSoundSelectOpen); }
        public static void ToggleTargetMissSoundSelect_Static() { SetTargetMissSelect(!targetMissSoundSelectOpen); }

        public static void SetTargetHitSelect(bool state) {
            DropdownUtils.SetDropdownState(state, targetSoundSelect.targetHitSoundDropdownBody, targetSoundSelect.arrowTextHit, ref targetHitSoundSelectOpen);
        }

        public static void SetTargetMissSelect(bool state) {
            DropdownUtils.SetDropdownState(state, targetSoundSelect.targetMissSoundDropdownBody, targetSoundSelect.arrowTextMiss, ref targetMissSoundSelectOpen);
        }

        public static void SetSoundSelectionContainerState(bool soundSelectionState) {
            Util.SetCanvasGroupState_DisableHover(targetSoundSelect.soundSelectionCanvasGroup, soundSelectionState);
        }

        public static void SetTargetMissSoundContainerState(bool targetMissSoundState) {
            Util.SetCanvasGroupState_DisableHover(targetSoundSelect.targetMissContainerCanvasGroup, targetMissSoundState);
        }

        private static void SaveNewTargetHitSound(AudioClip newAudioClip, SFXType newAudioClipType) {
            SFXManager.targetHitSound   = newAudioClip;
            SFXSettings.targetSoundClip = newAudioClipType;
            targetSoundsSaveReady       = true;
        }
        private static void SaveNewTargetMissSound(AudioClip newAudioClip, SFXType newAudioClipType) {
            SFXManager.targetMissSound      = newAudioClip;
            SFXSettings.targetMissSoundClip = newAudioClipType;
            targetSoundsSaveReady           = true;
        }

        public static void SetTargetHitSound(string hitName) {
            SetTargetHitSoundText(hitName);
            
            switch (hitName) {
                case "00": SaveNewTargetHitSound(SFXManager.ReturnTargetSFXClip(SFXType.TARGET_HIT_SFX_0), SFXType.TARGET_HIT_SFX_0); break;
                case "01": SaveNewTargetHitSound(SFXManager.ReturnTargetSFXClip(SFXType.TARGET_HIT_SFX_1), SFXType.TARGET_HIT_SFX_1); break;
                case "02": SaveNewTargetHitSound(SFXManager.ReturnTargetSFXClip(SFXType.TARGET_HIT_SFX_2), SFXType.TARGET_HIT_SFX_2); break;
            }
        }

        public static void SetTargetMissSound(string hitName) {
            SetTargetMissSoundText(hitName);
            
            switch (hitName) {
                case "00": SaveNewTargetMissSound(SFXManager.ReturnTargetSFXClip(SFXType.TARGET_MISS_SFX_0), SFXType.TARGET_MISS_SFX_0); break;
                case "01": SaveNewTargetMissSound(SFXManager.ReturnTargetSFXClip(SFXType.TARGET_MISS_SFX_1), SFXType.TARGET_MISS_SFX_1); break;
                case "02": SaveNewTargetMissSound(SFXManager.ReturnTargetSFXClip(SFXType.TARGET_MISS_SFX_2), SFXType.TARGET_MISS_SFX_2); break;
            }
        }
    }
}
