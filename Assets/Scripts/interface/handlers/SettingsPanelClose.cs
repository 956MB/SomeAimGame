using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsPanelClose : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData pointerEventData) {
        SettingsPanel.CloseSettingsPanel();
    }
}
