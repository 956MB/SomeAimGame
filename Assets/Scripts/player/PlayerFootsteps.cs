using UnityEngine;

public class PlayerFootsteps : MonoBehaviour {
    CharacterController cc;
    AudioSource audioData;

    void Start() {
        cc        = GetComponent<CharacterController>();
        audioData = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        if (cc.isGrounded == true && cc.velocity.magnitude > .3f && audioData.isPlaying == false) {
            audioData.volume = Random.Range(0.25f, 0.45f);
            audioData.pitch  = Random.Range(2f, 2.2f); // Floot_Step15
            audioData.Play();
        }
    }
}
