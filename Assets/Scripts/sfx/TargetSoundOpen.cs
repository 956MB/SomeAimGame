using UnityEngine;

namespace SomeAimGame.SFX {
    public class TargetSoundOpen : MonoBehaviour {
        public void OpenTargetSoundDropdown(int dropdownType) {
            switch ((TargetSoundDropdowns)dropdownType) {
                case TargetSoundDropdowns.TARGET_HIT:  TargetSoundSelect.ToggleTargetHitSoundSelect_Static();  break;
                case TargetSoundDropdowns.TARGET_MISS: TargetSoundSelect.ToggleTargetMissSoundSelect_Static(); break;
            }

            SFXManager.CheckPlayClick_Button();
        }
    }
}
