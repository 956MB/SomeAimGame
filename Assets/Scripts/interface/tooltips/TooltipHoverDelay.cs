using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHoverDelay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public string translateID;
    private bool delayRoutineRunning = false;
    private Coroutine delayRoutine;
    private WaitForSecondsRealtime tooltipDelay = new WaitForSecondsRealtime(2f);

    // NOTE: a little buggy with the enter/exit quickly

    public void OnPointerEnter(PointerEventData eventData) {
        if (!delayRoutineRunning) {
            delayRoutineRunning = true;
            delayRoutine = StartCoroutine(TooltipDelay());
        }
    }
    public void OnPointerExit(PointerEventData eventData) {
        if (delayRoutineRunning) {
            delayRoutineRunning = false;
            StopCoroutine(delayRoutine);
            Tooltip.HideTooltip_Static();
        }
    }

    private IEnumerator TooltipDelay() {
        yield return tooltipDelay;
        Tooltip.ShowTooltip_Static(I18nTextTranslator.SetTranslatedText(translateID));
    }
}
