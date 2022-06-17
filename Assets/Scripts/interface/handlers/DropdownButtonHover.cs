using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Utilities;
using SomeAimGame.SFX;

public class DropdownButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Image buttonImage;
    public TMP_Text buttonText;

    public void OnPointerEnter(PointerEventData pointerEventData) {
        buttonImage.color = InterfaceColors.buttonBackgroundLight_hovered;
        buttonText.color  = InterfaceColors.selectedColor;

        SFXManager.CheckPlayHover_Regular();
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        buttonImage.color = InterfaceColors.buttonBackgroundLight_Dropdown;
        buttonText.color  = InterfaceColors.widgetsNeutralColor;
    }
}
