
namespace SomeAimGame.Stats {
    /// <summary>
    /// Holds differences between previous and current run stats in 'StatsDiff' object.
    /// </summary>
    public class StatsDiff {
        public static double scoreDiff, accuracyDiff, ttkDiff, kpsDiff, bestStreakDiff, targetsTotalDiff, targetHitDiff, targetsMissesDiff;
        public static string scoreDiffSymbol, accuracyDiffSymbol, ttkDiffSymbol, kpsDiffSymbol, bestStreakDiffSymbol, targetsTotalDiffSymbol, targetHitDiffSymbol, targetsMissesDiffSymbol;
        public static double scoreDiffPercent, accuracyDiffPercent, ttkDiffPercent, kpsDiffPercent, bestStreakDiffPercent, targetsTotalDiffPercent, targetHitDiffPercent, targetsMissesDiffPercent;

        public static string scoreDiffStringDisplay, accuracyDiffStringDisplay, ttkDiffStringDisplay, kpsDiffStringDisplay, bestStreakDiffStringDisplay, targetsTotalDiffStringDisplay, targetHitDiffStringDisplay, targetsMissesDiffStringDisplay;

        /// <summary>
        /// Compares all current vs previous game stats, then sets stat differences in 'StatsDiff' object and strings for display.
        /// </summary>
        public static void LoadStatDiffs(int scoreStat, int accuracyStat, int ttkStat, double kpsStat, int bestStreakStat, int targetTotalStat, int targetHitStat, int targetMissesStat, PreviousGameStats previousGameStats) {
            if (previousGameStats != null) {
                // Actual stat diff number.
                scoreDiff         = StatsUtil.CheckDifference(scoreStat, previousGameStats.scoreValue);
                accuracyDiff      = StatsUtil.CheckDifference(accuracyStat, previousGameStats.accuracyValue);
                ttkDiff           = StatsUtil.CheckDifference(ttkStat, previousGameStats.ttkValue);
                kpsDiff           = StatsUtil.CheckDifference(kpsStat, previousGameStats.kpsValue);
                bestStreakDiff    = StatsUtil.CheckDifference(bestStreakStat, previousGameStats.bestStreakValue);
                targetsTotalDiff  = StatsUtil.CheckDifference(targetTotalStat, previousGameStats.targetsTotalValue);
                targetHitDiff     = StatsUtil.CheckDifference(targetHitStat, previousGameStats.targetsHitValue);
                targetsMissesDiff = StatsUtil.CheckDifference(targetMissesStat, previousGameStats.targetsMissesValue);
                // Percent of stat diff (higher/lower).
                scoreDiffPercent         = StatsUtil.CheckDifference_Percent(scoreStat, previousGameStats.scoreValue);
                accuracyDiffPercent      = StatsUtil.CheckDifference_Percent(accuracyStat, previousGameStats.accuracyValue);
                ttkDiffPercent           = StatsUtil.CheckDifference_Percent(ttkStat, previousGameStats.ttkValue);
                kpsDiffPercent           = StatsUtil.CheckDifference_Percent(kpsStat, previousGameStats.kpsValue);
                bestStreakDiffPercent    = StatsUtil.CheckDifference_Percent(bestStreakStat, previousGameStats.bestStreakValue);
                targetsTotalDiffPercent  = StatsUtil.CheckDifference_Percent(targetTotalStat, previousGameStats.targetsTotalValue);
                targetHitDiffPercent     = StatsUtil.CheckDifference_Percent(targetHitStat, previousGameStats.targetsHitValue);
                targetsMissesDiffPercent = StatsUtil.CheckDifference_Percent(targetMissesStat, previousGameStats.targetsMissesValue);
                // Whether stat diff is "+" or "-" (higher/lower).
                scoreDiffSymbol         = StatsUtil.CheckDifference_Symbol(scoreStat, previousGameStats.scoreValue);
                accuracyDiffSymbol      = StatsUtil.CheckDifference_Symbol(accuracyStat, previousGameStats.accuracyValue);
                ttkDiffSymbol           = StatsUtil.CheckDifference_Symbol(ttkStat, previousGameStats.ttkValue);
                kpsDiffSymbol           = StatsUtil.CheckDifference_Symbol(kpsStat, previousGameStats.kpsValue);
                bestStreakDiffSymbol    = StatsUtil.CheckDifference_Symbol(bestStreakStat, previousGameStats.bestStreakValue);
                targetsTotalDiffSymbol  = StatsUtil.CheckDifference_Symbol(targetTotalStat, previousGameStats.targetsTotalValue);
                targetHitDiffSymbol     = StatsUtil.CheckDifference_Symbol(targetHitStat, previousGameStats.targetsHitValue);
                targetsMissesDiffSymbol = StatsUtil.CheckDifference_Symbol(targetMissesStat, previousGameStats.targetsMissesValue);
                // Formatted stat diff string "+5 (5%)".
                accuracyDiffStringDisplay      = $"{accuracyDiffSymbol}{accuracyDiff}%";
                ttkDiffStringDisplay           = $"{ttkDiffSymbol}{ttkDiff}ms ({(int)ttkDiffPercent}%)";
                kpsDiffStringDisplay           = $"{kpsDiffSymbol}{string.Format("{0:0.00}/s", kpsDiff)} ({(int)kpsDiffPercent}%)";
                bestStreakDiffStringDisplay    = $"{bestStreakDiffSymbol}{bestStreakDiff} ({(int)bestStreakDiffPercent}%)";
                targetsTotalDiffStringDisplay  = $"{targetsTotalDiffSymbol}{targetsTotalDiff} ({(int)targetsTotalDiffPercent}%)";
                targetHitDiffStringDisplay     = $"{targetHitDiffSymbol}{targetHitDiff} ({(int)targetHitDiffPercent}%)";
                targetsMissesDiffStringDisplay = $"{targetsMissesDiffSymbol}{targetsMissesDiff} ({(int)targetsMissesDiffPercent}%)";
            }
        }
    }
}
