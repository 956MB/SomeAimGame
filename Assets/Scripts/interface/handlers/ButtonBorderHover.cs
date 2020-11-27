using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBorderHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public GameObject borderObj;

    void Start() { borderObj.SetActive(false); }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        borderObj.SetActive(true);

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_HoverInner(); }
    }

    public void OnPointerExit(PointerEventData pointerEventData) { borderObj.SetActive(false); }
}
