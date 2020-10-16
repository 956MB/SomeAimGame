using UnityEngine;

public class UISound : MonoBehaviour {
    public AudioClip uiSoundSrc01;
    public AudioClip uiSoundSrc02;
    private static AudioSource audioSrc;

    private static UISound uiSound;
    void Awake() { uiSound = this; }

    void Start() { audioSrc = GetComponent<AudioSource>(); }

    /// <summary>
    /// Plays UI sound 01.
    /// </summary>
    public static void PlayUISound() {
        // TODO: if playHitSound enabled
        if (audioSrc != null) { audioSrc.PlayOneShot(uiSound.uiSoundSrc01); }
    }

    /// <summary>
    /// Plays UI sounds 02.
    /// </summary>
    public static void PlayUISound02() {
        // TODO: if playHitSound enabled
        if (audioSrc != null) { audioSrc.PlayOneShot(uiSound.uiSoundSrc02); }
    }

    //public static void playUISoundExtra() {
    //    // TODO: if playHitSound enabled
    //    audioSrc.PlayOneShot(uiSound.uiSoundSrc);
    //    //Debug.Log("inside PLAYHITSOUND");
    //}
}
