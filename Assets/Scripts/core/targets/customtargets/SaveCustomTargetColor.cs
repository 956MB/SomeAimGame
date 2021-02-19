using UnityEngine;
using UnityEngine.EventSystems;

namespace SomeAimGame.Targets {
    public class SaveCustomTargetColor : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData pointerEventData) {
            EditCustomTarget.SaveNewCustomTarget();
        }
    }
}