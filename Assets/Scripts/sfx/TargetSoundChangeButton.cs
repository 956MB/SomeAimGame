using UnityEngine;
using UnityEngine.EventSystems;

namespace SomeAimGame.SFX {
    public class TargetSoundChangeButton : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData pointerEventData) {
            GameObject changeButton = pointerEventData.pointerCurrentRaycast.gameObject;
            
            if (changeButton.CompareTag("TargetHitSoundButton")) {
                TargetSoundSelect.SetTargetHitSound($"{changeButton.transform.name}");
            } else if (changeButton.CompareTag("TargetMissSoundButton")) {
                TargetSoundSelect.SetTargetMissSound($"{changeButton.transform.name}");
            }

            TargetSoundSelect.CheckCloseTargetSoundDropdowns();
        }
    }
}
