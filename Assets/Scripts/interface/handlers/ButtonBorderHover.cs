using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

using SomeAimGame.SFX;
using SomeAimGame.Utilities;

public class ButtonBorderHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public GameObject borderObj;
    public TMP_Text buttonText;
    private Color buttonTextColor;

    void Start() {
        borderObj.SetActive(false);
        if (buttonText != null) { buttonTextColor = buttonText.color; }
    }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        borderObj.SetActive(true);
        if (buttonText != null) { buttonText.color = InterfaceColors.hoveredColor; }

        SFXManager.CheckPlayHover_Regular();
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        borderObj.SetActive(false);
        if (buttonText != null) { buttonText.color = buttonTextColor; }
    }
}
