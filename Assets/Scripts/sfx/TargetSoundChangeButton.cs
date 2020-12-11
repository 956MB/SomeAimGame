using UnityEngine;
using UnityEngine.EventSystems;

namespace SomeAimGame.SFX {
    public class TargetSoundChangeButton : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData pointerEventData) {
            GameObject changeButton = pointerEventData.pointerCurrentRaycast.gameObject;
            
            switch (changeButton.tag) {
                case "TargetHitSoundButton":  TargetSoundSelect.SetTargetHitSound($"{changeButton.transform.name}");  break;
                case "TargetMissSoundButton": TargetSoundSelect.SetTargetMissSound($"{changeButton.transform.name}"); break;
            }

            TargetSoundSelect.CheckCloseTargetSoundDropdowns();
        }
    }
}
