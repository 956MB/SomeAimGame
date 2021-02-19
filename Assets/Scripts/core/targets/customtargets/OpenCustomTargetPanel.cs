using UnityEngine;
using UnityEngine.EventSystems;

namespace SomeAimGame.Targets {
    public class OpenCustomTargetPanel : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData pointerEventData) {
            CustomTargetPanel.OpenCustomTargetPanel();
        }
    }
}
