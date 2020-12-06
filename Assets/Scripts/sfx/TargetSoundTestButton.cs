using UnityEngine;
using UnityEngine.EventSystems;

namespace SomeAimGame.SFX {
    public class TargetSoundTestButton : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData pointerEventData) {
            GameObject testButton = pointerEventData.pointerCurrentRaycast.gameObject;

            if (testButton.CompareTag("TargetHitTestButton")) {
                TargetSoundSelect.TestTargetHitSound();
            } else if (testButton.CompareTag("TargetMissTestButton")) {
                TargetSoundSelect.TestTargetMissSound();
            }
        }
    }
}