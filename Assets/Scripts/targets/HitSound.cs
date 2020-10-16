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
        audioSrc.PlayOneShot(hitSound.hitSoundSrc);
    }
}
