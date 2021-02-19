using UnityEngine;

namespace SomeAimGame.SFX {
    public class SFXManager : MonoBehaviour {
        private static AudioSource SFXAudioSrc;
        public AudioClip UI_SFX_HOVER_TITLE, UI_SFX_HOVER_REGULAR, UI_SFX_HOVER_BUTTON, UI_SFX_HOVER_TOGGLE, UI_SFX_CLICK_TITLE, UI_SFX_CLICK_REGULAR, UI_SFX_CLICK_BUTTON, UI_SFX_CLICK_TOGGLE, UI_SFX_ERROR_0, TARGET_HIT_SFX_0, TARGET_HIT_SFX_1, TARGET_HIT_SFX_2, TARGET_MISS_SFX_0, TARGET_MISS_SFX_1, TARGET_MISS_SFX_2;
        public static AudioClip targetHitSound, targetMissSound;
        public GameObject targetSoundSelection;

        private static SFXManager sfxManager;
        void Awake() { sfxManager = this; }

        void Start() {
            SFXAudioSrc = GetComponent<AudioSource>();
            SFXSaveSystem.InitSavedSFXSettings();

            TargetSoundInit();
            TargetSoundSelect.SetTargetHitSoundText($"{SFXUtil.ReturnTargetSoundStrings(SFXSettings.targetSoundClip)}");
            TargetSoundSelect.SetTargetMissSoundText($"{SFXUtil.ReturnTargetSoundStrings(SFXSettings.targetMissSoundClip)}");
        }

        /// <summary>
        /// Inits states of sound selection containers.
        /// </summary>
        private static void TargetSoundInit() {
            TargetSoundSelect.SetSoundSelectionContainerState(SFXSettings.targetSoundOn);
            TargetSoundSelect.SetTargetMissSoundContainerState(SFXSettings.targetMissSoundOn);
        }

        /// Plays SFX clips.
        #region SFX plays

        public static void PlaySFXHover_Title() {   if (SFXAudioSrc != null) { SFXAudioSrc.PlayOneShot(sfxManager.UI_SFX_HOVER_TITLE, 1); } }
        public static void PlaySFXHover_Regular() { if (SFXAudioSrc != null) { SFXAudioSrc.PlayOneShot(sfxManager.UI_SFX_HOVER_REGULAR, 1); } }
        public static void PlaySFXHover_Button() {  if (SFXAudioSrc != null) { SFXAudioSrc.PlayOneShot(sfxManager.UI_SFX_HOVER_BUTTON, 0.45f); } }
        public static void PlaySFXHover_Toggle() {  if (SFXAudioSrc != null) { SFXAudioSrc.PlayOneShot(sfxManager.UI_SFX_HOVER_TOGGLE, 1); } }
        public static void PlaySFXClick_Title() {   if (SFXAudioSrc != null) { SFXAudioSrc.PlayOneShot(sfxManager.UI_SFX_CLICK_TITLE, 1); } }
        public static void PlaySFXClick_Regular() { if (SFXAudioSrc != null) { SFXAudioSrc.PlayOneShot(sfxManager.UI_SFX_CLICK_REGULAR, 1); } }
        public static void PlaySFXClick_Button() {  if (SFXAudioSrc != null) { SFXAudioSrc.PlayOneShot(sfxManager.UI_SFX_CLICK_BUTTON, 1); } }
        public static void PlaySFXClick_Toggle() {  if (SFXAudioSrc != null) { SFXAudioSrc.PlayOneShot(sfxManager.UI_SFX_CLICK_TOGGLE, 1); } }
        public static void PlaySFXError_0() {       if (SFXAudioSrc != null) { SFXAudioSrc.PlayOneShot(sfxManager.UI_SFX_ERROR_0, 1); } }
        public static void PlaySFXTargetHit() {     if (SFXAudioSrc != null) { SFXAudioSrc.PlayOneShot(targetHitSound, 1); } }
        public static void PlaySFXTargetMiss() {    if (SFXAudioSrc != null) { SFXAudioSrc.PlayOneShot(targetMissSound, 1); } }

        #endregion

        /// Plays specific SFX clip if UI sound/Target sound on.
        #region Checks

        public static void CheckPlayHover_Title() {   if (SFXSettings.uiSoundOn) { PlaySFXHover_Title(); } }
        public static void CheckPlayHover_Regular() { if (SFXSettings.uiSoundOn) { PlaySFXHover_Regular(); } }
        public static void CheckPlayHover_Button() {  if (SFXSettings.uiSoundOn) { PlaySFXHover_Button(); } }
        public static void CheckPlayHover_Toggle() {  if (SFXSettings.uiSoundOn) { PlaySFXHover_Toggle(); } }
        public static void CheckPlayClick_Title() {   if (SFXSettings.uiSoundOn) { PlaySFXClick_Title(); } }
        public static void CheckPlayClick_Regular() { if (SFXSettings.uiSoundOn) { PlaySFXClick_Regular(); } }
        public static void CheckPlayClick_Button() {  if (SFXSettings.uiSoundOn) { PlaySFXClick_Button(); } }
        public static void CheckPlayClick_Toggle() {  if (SFXSettings.uiSoundOn) { PlaySFXClick_Toggle(); } }
        public static void CheckPlayError() {         if (SFXSettings.uiSoundOn) { PlaySFXError_0(); } }
        // TODO: add CheckPlaySuccess
        public static void CheckPlayTargetHit() {     if (SFXSettings.targetSoundOn) {                                  PlaySFXTargetHit(); } }
        public static void CheckPlayTargetMiss() {    if (SFXSettings.targetSoundOn && SFXSettings.targetMissSoundOn) { PlaySFXTargetMiss(); } }

        #endregion

        #region Utils

        /// <summary>
        /// Returns appropriate AudioClip from supplied SFXType (sfxType).
        /// </summary>
        /// <param name="sfxType"></param>
        /// <returns></returns>
        public static AudioClip ReturnTargetSFXClip(SFXType sfxType) {
            switch (sfxType) {
                case SFXType.TARGET_HIT_SFX_0:  return sfxManager.TARGET_HIT_SFX_0;
                case SFXType.TARGET_HIT_SFX_1:  return sfxManager.TARGET_HIT_SFX_1;
                case SFXType.TARGET_HIT_SFX_2:  return sfxManager.TARGET_HIT_SFX_2;
                case SFXType.TARGET_MISS_SFX_0: return sfxManager.TARGET_MISS_SFX_0;
                case SFXType.TARGET_MISS_SFX_1: return sfxManager.TARGET_MISS_SFX_1;
                case SFXType.TARGET_MISS_SFX_2: return sfxManager.TARGET_MISS_SFX_1;
                default:                        return sfxManager.TARGET_HIT_SFX_0;
            }
        }

        #endregion
    }
}