using UnityEngine;
using UnityEngine.EventSystems;

using SomeAimGame.SFX;

public class ButtonSoundHover : MonoBehaviour, IPointerEnterHandler {
    public void OnPointerEnter(PointerEventData pointerEventData) {
        SFXManager.CheckPlayHover_Button();
    }
}
