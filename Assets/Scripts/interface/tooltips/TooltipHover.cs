using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public string translateID;

    /// <summary>
    /// Shows translated text tooltip on mouse enter.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) {
        Tooltip.ShowTooltip_Static(I18nTextTranslator.SetTranslatedText(translateID));
    }
    /// <summary>
    /// Hides translated text tooltip on mouse exit.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.HideTooltip_Static();
    }
}
