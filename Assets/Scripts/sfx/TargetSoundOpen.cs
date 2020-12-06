using UnityEngine;

namespace SomeAimGame.SFX {
    public class TargetSoundOpen : MonoBehaviour {
        public void OpenTargetSoundDropdown(GameObject openButton) {
            if (openButton.CompareTag("TargetHitOpen")) {
                TargetSoundSelect.ToggleTargetHitSoundSelect_Static();
            } else if (openButton.CompareTag("TargetMissOpen")) {
                TargetSoundSelect.ToggleTargetMissSoundSelect_Static();
            }
        }
    }
}
