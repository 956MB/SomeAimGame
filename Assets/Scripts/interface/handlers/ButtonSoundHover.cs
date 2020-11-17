using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundHover : MonoBehaviour, IPointerEnterHandler {
    public void OnPointerEnter(PointerEventData pointerEventData) {
        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_Hover(); }
    }
}
