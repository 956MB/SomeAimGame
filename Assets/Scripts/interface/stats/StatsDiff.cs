using UnityEngine;

/// <summary>
/// Holds differences between previous and current run stats in 'StatsDiff' object.
/// </summary>
public class StatsDiff : MonoBehaviour {
    public static double scoreDiff, accuracyDiff, ttkDiff, kpsDiff, bestStreakDiff, targetsTotalDiff, targetHitDiff, targetsMissesDiff;
    public static string scoreDiffSymbol, accuracyDiffSymbol, ttkDiffSymbol, kpsDiffSymbol, bestStreakDiffSymbol, targetsTotalDiffSymbol, targetHitDiffSymbol, targetsMissesDiffSymbol;
    public static double scoreDiffPercent, accuracyDiffPercent, ttkDiffPercent, kpsDiffPercent, bestStreakDiffPercent, targetsTotalDiffPercent, targetHitDiffPercent, targetsMissesDiffPercent;
}
