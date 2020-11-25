using UnityEngine;

public class UISound : MonoBehaviour {
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public AudioClip errorSound;

    private static AudioSource audioSrc;

    private static UISound uiSound;
    void Awake() { uiSound = this; }

    void Start() { audioSrc = GetComponent<AudioSource>(); }

    // TODO: add error sound, like for when target color is red when starting follow gamemode and notification fires

    /// <summary>
    /// Plays hover sound.
    /// </summary>
    public static void PlayUISound_Hover() {
        // TODO: if playHitSound enabled
        if (audioSrc != null) {
            audioSrc.PlayOneShot(uiSound.hoverSound);

            // EVENT:: for new hover sound triggered
            //DevEventHandler.CheckSoundEvent($"{I18nTextTranslator.SetTranslatedText("eventsoundfiredhover")}");
        }
    }

    /// <summary>
    /// Plays click sound.
    /// </summary>
    public static void PlayUISound_Click() {
        // TODO: if playHitSound enabled
        if (audioSrc != null) {
            audioSrc.PlayOneShot(uiSound.clickSound);

            // EVENT:: for new click sound triggered
            //DevEventHandler.CheckSoundEvent($"{I18nTextTranslator.SetTranslatedText("eventsoundfiredclick")}");
        }
    }

    /// <summary>
    /// Plays error sound.
    /// </summary>
    public static void PlayUISound_Error() {
        // TODO: if playHitSound enabled
        if (audioSrc != null) {
            audioSrc.PlayOneShot(uiSound.errorSound);

            // EVENT:: for new click sound triggered
            //DevEventHandler.CheckSoundEvent($"{I18nTextTranslator.SetTranslatedText("eventsoundfiredclick")}");
        }
    }
}
