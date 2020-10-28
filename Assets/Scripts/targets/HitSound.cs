using UnityEngine;

public class HitSound : MonoBehaviour {
    public AudioClip hitSoundSrc;
    private static AudioSource audioSrc;

    private static HitSound hitSound;
    void Awake() { hitSound = this; }

    private void Start() { audioSrc = GetComponent<AudioSource>(); }

    /// <summary>
    /// Plays hit sound audio source.
    /// </summary>
    public static void PlayHitSound() {
        // TODO: if playHitSound enabled
        if (audioSrc != null) {
            audioSrc.PlayOneShot(hitSound.hitSoundSrc);

            // EVENT:: for new hit sound triggered
            if (DevEventHandler.eventsOn) { DevEventHandler.CreateSoundEvent($"{I18nTextTranslator.SetTranslatedText("eventsoundfiredhit")}"); }
        }
    }
}
