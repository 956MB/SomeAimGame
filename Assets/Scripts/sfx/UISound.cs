using UnityEngine;

public class UISound : MonoBehaviour {
    public AudioClip uiSoundSrc01;
    public AudioClip uiSoundSrc02;
    private static AudioSource audioSrc;

    private static UISound uiSound;
    void Awake() { uiSound = this; }

    void Start() { audioSrc = GetComponent<AudioSource>(); }

    // TODO: add error sound, like for when target color is red when starting follow gamemode and notification fires

    /// <summary>
    /// Plays UI sound 01.
    /// </summary>
    public static void PlayUISound_Hover() {
        // TODO: if playHitSound enabled
        if (audioSrc != null) {
            audioSrc.PlayOneShot(uiSound.uiSoundSrc01);

            // EVENT:: for new hover sound triggered
            //DevEventHandler.CheckSoundEvent($"{I18nTextTranslator.SetTranslatedText("eventsoundfiredhover")}");
        }
    }

    /// <summary>
    /// Plays UI sounds 02.
    /// </summary>
    public static void PlayUISound_Click() {
        // TODO: if playHitSound enabled
        if (audioSrc != null) {
            audioSrc.PlayOneShot(uiSound.uiSoundSrc02);

            // EVENT:: for new click sound triggered
            //DevEventHandler.CheckSoundEvent($"{I18nTextTranslator.SetTranslatedText("eventsoundfiredclick")}");
        }
    }

    //public static void playUISoundExtra() {
    //    // TODO: if playHitSound enabled
    //    audioSrc.PlayOneShot(uiSound.uiSoundSrc);
    //    //Debug.Log("inside PLAYHITSOUND");
    //}
}
