using UnityEngine;
using UnityEngine.EventSystems;

namespace SomeAimGame.Targets {
    public class CloseCustomTargetPanel : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData pointerEventData) {
            EditCustomTarget.CancelNewCustomTarget();
            CustomTargetPanel.CloseCustomTargetPanel();
        }
    }
}
