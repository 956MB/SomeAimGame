using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour, IPointerClickHandler {
    /// <summary>
    /// Calls "OnDoubleClick" when gameObject double clicked.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) {
        int clickCount = eventData.clickCount;
        if (clickCount == 2) { OnDoubleClick(); }
    }

    /// <summary>
    /// Returns 'AfterActionReport' and 'ExtraStats' panels to original positions on double click.
    /// </summary>
    void OnDoubleClick() {
        switch (transform.name) {
            case "StatsPage":
                transform.position = new Vector3(960f, 540f, 0f);
                CosmeticsSettings.resetAfterActionReportPanelCenter();
                break;
            case "ExtraStats":
                transform.position = new Vector3(1608.53f, 585.93f, 0f);
                CosmeticsSettings.resetExtraStatsPanelCenter();
                break;
        }
    }
}