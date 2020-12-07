using UnityEngine;
using UnityEngine.EventSystems;

using SomeAimGame.Stats;

public class TooltipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public string translateTextID;

    /// <summary>
    /// Shows tooltip with corresponding difference text based on hovered item (pointerEnter).
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) {
        string tooltipName = eventData.pointerCurrentRaycast.gameObject.name;

        // Show tooltip with corresponding difference text based on which stats item in 'AfterActionReport' is hovered.
        switch (tooltipName) {
            case "ScoreItem":         Tooltip.ShowTooltip_Static($"{StatsDiff.scoreDiffStringDisplay}");                      break;
            case "AccuracyItem":      Tooltip.ShowTooltip_Static($"{StatsDiff.accuracyDiffStringDisplay}");                   break;
            case "TTKItem":           Tooltip.ShowTooltip_Static($"{StatsDiff.ttkDiffStringDisplay}");                        break;
            case "KPSItem":           Tooltip.ShowTooltip_Static($"{StatsDiff.kpsDiffStringDisplay}");                        break;
            case "BestStreakItem":    Tooltip.ShowTooltip_Static($"{StatsDiff.bestStreakDiffStringDisplay}");                 break;
            case "TargetsTotalItem":  Tooltip.ShowTooltip_Static($"{StatsDiff.targetsTotalDiffStringDisplay}");               break;
            case "TargetsHitItem":    Tooltip.ShowTooltip_Static($"{StatsDiff.targetHitDiffStringDisplay}");                  break;
            case "TargetsMissesItem": Tooltip.ShowTooltip_Static($"{StatsDiff.targetsMissesDiffStringDisplay}");              break;
            default:                  Tooltip.ShowTooltip_Static($"{I18nTextTranslator.SetTranslatedText(translateTextID)}"); break;
        }
    }

    /// <summary>
    /// Hides tooltip when no stats item hovered (pointerExit).
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) { Tooltip.HideTooltip_Static(); }
}
