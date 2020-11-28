using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Utilities;

public class ButtonLanguageHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Image buttonImage;
    public TMP_Text buttonText;

    public void OnPointerEnter(PointerEventData pointerEventData) {
        buttonImage.color = InterfaceColors.buttonBackgroundLight_hovered;
        buttonText.color  = InterfaceColors.selectedColor;

        if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_HoverInner(); }
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        buttonImage.color = InterfaceColors.buttonBackgroundLight;
        buttonText.color  = InterfaceColors.widgetsNeutralColor;
    }
}
