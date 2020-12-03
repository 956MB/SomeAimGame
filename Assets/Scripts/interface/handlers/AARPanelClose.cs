using UnityEngine;
using UnityEngine.EventSystems;

public class AARPanelClose : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData pointerEventData) {
        SettingsPanel.CloseAfterActionReport();
    }
}
