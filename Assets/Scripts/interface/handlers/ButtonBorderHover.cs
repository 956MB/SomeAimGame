using UnityEngine;
using UnityEngine.EventSystems;

using SomeAimGame.SFX;

public class ButtonBorderHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public GameObject borderObj;

    void Start() { borderObj.SetActive(false); }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        borderObj.SetActive(true);

        SFXManager.CheckPlayHover_Regular();
    }

    public void OnPointerExit(PointerEventData pointerEventData) { borderObj.SetActive(false); }
}
