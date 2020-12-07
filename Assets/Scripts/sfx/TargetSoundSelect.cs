using UnityEngine;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Utilities;

namespace SomeAimGame.SFX {
    public class TargetSoundSelect : MonoBehaviour {
        public TMP_Text targetHitSoundText, targetMissSoundText, arrowTextHit, arrowTextMiss;
        public GameObject targetHitSoundDropdownBody, targetMissSoundDropdownBody, targetSoundSelectionContainer;
        public CanvasGroup soundSelectionCanvasGroup;
        public Image uiSoundBackground;
        public static bool targetHitSoundSelectOpen          = false;
        public static bool targetMissSoundSelectOpen         = false;
        public static bool targetSoundSelectionContainerOpen = false;
        public static bool targetSoundsSaveReady             = false;

        public static TargetSoundSelect targetSoundSelect;
        private void Awake() { targetSoundSelect = this; }

        private void Start() {
            CloseTargetHitSoundSelect();
            CloseTargetMissSoundSelect();
        }

        public static void CheckSaveTargetSoundSelection() {
            if (targetSoundsSaveReady) {
                SFXSettings.SaveSFXSettings_Static();
                targetSoundsSaveReady = false;
            }
        }

        public static void CheckCloseTargetSoundDropdowns() {
            if (targetHitSoundSelectOpen) { CloseTargetHitSoundSelect(); }
            if (targetMissSoundSelectOpen) { CloseTargetMissSoundSelect(); }
        }

        public static void SetTargetHitSoundText(string soundName) { targetSoundSelect.targetHitSoundText.SetText($"{soundName}"); }
        public static void SetTargetMissSoundText(string soundName) { targetSoundSelect.targetMissSoundText.SetText($"{soundName}"); }

        public static void TestTargetHitSound() { SFXManager.PlaySFXTargetHit(); }
        public static void TestTargetMissSound() { SFXManager.PlaySFXTargetMiss(); }

        public static void ToggleTargetHitSoundSelect_Static() {
            if (!targetHitSoundSelectOpen) {
                OpenTargetHitSoundSelect();
                SFXManager.CheckPlayClick_Button();
            } else {
                CloseTargetHitSoundSelect();
            }
        }

        public static void ToggleTargetMissSoundSelect_Static() {
            if (!targetMissSoundSelectOpen) {
                OpenTargetMissSoundSelect();
                SFXManager.CheckPlayClick_Button();
            } else {
                CloseTargetMissSoundSelect();
            }
        }

        public static void SetSoundSelectionContainerContainerState(bool soundSelectionState) {
            Util.CanvasGroupState(targetSoundSelect.soundSelectionCanvasGroup, soundSelectionState);
        }

        public static void TargetSound_OpenAction(GameObject dropdownBody, TMP_Text arrowText, ref bool targetSoundSelectBool) {
            dropdownBody.transform.localScale = DropdownUtils.dropdownBodyOpenScale;
            Util.RefreshRootLayoutGroup(dropdownBody);
            arrowText.transform.localScale = DropdownUtils.arrowupScale;
            targetSoundSelectBool          = true;
        }

        public static void TargetSound_CloseAction(GameObject dropdownBody, TMP_Text arrowText, ref bool targetSoundSelectBool) {
            dropdownBody.transform.localScale = DropdownUtils.dropdownBodyClosedScale;
            arrowText.transform.localScale    = DropdownUtils.arrowDownScale;
            targetSoundSelectBool             = false;
        }

        public static void OpenTargetHitSoundSelect() {
            TargetSound_OpenAction(targetSoundSelect.targetHitSoundDropdownBody, targetSoundSelect.arrowTextHit, ref targetHitSoundSelectOpen);
        }

        public static void CloseTargetHitSoundSelect() {
            TargetSound_CloseAction(targetSoundSelect.targetHitSoundDropdownBody, targetSoundSelect.arrowTextHit, ref targetHitSoundSelectOpen);
        }

        public static void OpenTargetMissSoundSelect() {
            TargetSound_OpenAction(targetSoundSelect.targetMissSoundDropdownBody, targetSoundSelect.arrowTextMiss, ref targetMissSoundSelectOpen);
        }

        public static void CloseTargetMissSoundSelect() {
            TargetSound_CloseAction(targetSoundSelect.targetMissSoundDropdownBody, targetSoundSelect.arrowTextMiss, ref targetMissSoundSelectOpen);
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
