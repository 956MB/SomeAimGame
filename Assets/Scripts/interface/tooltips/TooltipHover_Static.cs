using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHover_Static : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public GameObject tooltipBackgroundStatic;

    void Start() { tooltipBackgroundStatic.SetActive(false); }

    public void OnPointerEnter(PointerEventData eventData) { tooltipBackgroundStatic.SetActive(true); }

    public void OnPointerExit(PointerEventData eventData) { tooltipBackgroundStatic.SetActive(false); }
}