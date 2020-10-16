using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public string translateTextID;
    //private bool hovering = false;
    //private float currentTime;

    /// <summary>
    /// Shows tooltip with corresponding difference text based on hovered item (pointerEnter).
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) {
        // Show tooltip with corresponding difference text based on which stats item in 'AfterActionReport' is hovered.
        switch (eventData.pointerCurrentRaycast.gameObject.name) {
            case "ScoreItem":
                Tooltip.ShowTooltip_Static($"{StatsDiff.scoreDiffSymbol}{string.Format("{0:n0}", StatsDiff.scoreDiff)} ({(int)StatsDiff.scoreDiffPercent}%)");
                break;
            case "AccuracyItem":
                Tooltip.ShowTooltip_Static($"{StatsDiff.accuracyDiffSymbol}{StatsDiff.accuracyDiff}%");
                break;
            case "TTKItem":
                Tooltip.ShowTooltip_Static($"{StatsDiff.ttkDiffSymbol}{StatsDiff.ttkDiff}ms ({(int)StatsDiff.ttkDiffPercent}%)");
                break;
            case "KPSItem":
                Tooltip.ShowTooltip_Static($"{StatsDiff.kpsDiffSymbol}{string.Format("{0:0.00}/s", StatsDiff.kpsDiff)} ({(int)StatsDiff.kpsDiffPercent}%)");
                break;
            case "BestStreakItem":
                Tooltip.ShowTooltip_Static($"{StatsDiff.bestStreakDiffSymbol}{StatsDiff.bestStreakDiff} ({(int)StatsDiff.bestStreakDiffPercent}%)");
                break;
            case "TargetsTotalItem":
                Tooltip.ShowTooltip_Static($"{StatsDiff.targetsTotalDiffSymbol}{StatsDiff.targetsTotalDiff} ({(int)StatsDiff.targetsTotalDiffPercent}%)");
                break;
            case "TargetsHitItem":
                Tooltip.ShowTooltip_Static($"{StatsDiff.targetHitDiffSymbol}{StatsDiff.targetHitDiff} ({(int)StatsDiff.targetHitDiffPercent}%)");
                break;
            case "TargetsMissesItem":
                Tooltip.ShowTooltip_Static($"{StatsDiff.targetsMissesDiffSymbol}{StatsDiff.targetsMissesDiff} ({(int)StatsDiff.targetsMissesDiffPercent}%)");
                break;
            default:
                Tooltip.ShowTooltip_Static($"{I18nTextTranslator.SetTranslatedText(translateTextID)}");
                break;
        }
    }

    /// <summary>
    /// Hides tooltip when no stats item hovered (pointerExit).
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) {
        //hovering = false;
        Tooltip.HideTooltip_Static();
    }
}
