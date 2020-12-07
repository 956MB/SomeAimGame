﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Gamemode;
using SomeAimGame.Targets;
using SomeAimGame.Utilities;

namespace SomeAimGame.Stats {
    public class StatsManager : MonoBehaviour {
        public TMP_Text AARTitleText, scoreTitleText, newHighscoreTitleText;

        public TMP_Text scoreStatText, gamemodeStatText, accuracyStatText, ttkStatText, kpsStatText, bestStreakStatText, targetsTotalStatText, targetsHitsStatText, targetsMissesStatText;

        public TMP_Text scoreItem, accuracyItem, ttkItem, kpsItem, bestStreakItem, targetsTotalItem, taretsHitItem, targetsMissesItem;

        public TMP_Text scoreTextBest, accuracyTextBest, ttkTextBest, kpsTextBest, bestStreakTextBest, targetsTotalTextBest, taretsHitTextBest, targetsMissesTextBest;

        public TMP_Text scoreTextPrevious, accuracyTextPrevious, ttkTextPrevious, kpsTextPrevious, bestStreakTextPrevious, targetsTotalTextPrevious, taretsHitTextPrevious, targetsMissesTextPrevious;

        public TMP_Text scoreTextHighscore, accuracyTextHighscore, ttkTextHighscore, kpsTextHighscore, bestStreakTextHighscore, targetsTotalTextHighscore, taretsHitTextHighscore, targetsMissesTextHighscore;

        public TMP_Text scoreExtraInner, accuracyExtraInner, ttkExtraInner, kpsExtraInner, bestStreakExtraInner, targetsTotalExtraInner, taretsHitExtraInner, targetsMissesExtraInner;

        public GameObject scoreContainerBackground, accuracyContainerBackground, ttkContainerBackground, kpsContainerBackground, bestStreakContainerBackground, targetsTotalContainerBackground, taretsHitContainerBackground, targetsMissesContainerBackground;

        public GameObject parentStatGroup;
        //public GameObject accuracyStatGroup, ttkStatGroup, kpsStatGroup, bestStreakStatGroup, targetsTotalStatGroup, taretsHitStatGroup, targetsMissesStatGroup;

        public GameObject extraStatsPanel, aarScrollView;

        public Image highscoreLineTop, highscoreLineBottom;
        public static bool showBackgrounds  = true;
        public static bool backgroundsSaved = false;
        private static Color32[] backgroundsSaves;

        private static int highscoreScore;
        private static HighscoreDataSerial highscoreData;
        public static PreviousGameStats previousGameStats;
        public static BestGameStats bestGameStats;

        private static string gamemodeStat;
        private static int scoreStat, accuracyStat, ttkStat, bestStreakStat, targetTotalStat, targetHitStat, targetMissesStat;
        private static double kpsStat;

        private static StatsManager statsManager;
        void Awake() { statsManager = this; }

        private void Start() {
            scoreTitleText.enabled        = true;
            newHighscoreTitleText.enabled = false;
            backgroundsSaves              = new Color32[8];
        }

        /// <summary>
        /// Loads highscore stats of current gamemode into 'highscoreData' object, then sets 'highscoreScore' from highscore stats.
        /// </summary>
        public static void LoadOldHighscore() {
            highscoreData = HighscoreSaveSystem.LoadHighscoreData(GamemodeUtil.ReturnGamemodeType_StringShort(SpawnTargets.gamemode));
            if (highscoreData != null) {
                highscoreScore = highscoreData.scoreValue;
            }
        }

        /// <summary>
        /// Loads previous stats of current gamemode into 'previousGameStats' object.
        /// </summary>
        public static void LoadPreviousGameStats() {
            previousGameStats = StatsJsonSaveSystem.LoadLastGameData(GamemodeUtil.ReturnGamemodeType_StringShort(SpawnTargets.gamemode));
        }

        /// <summary>
        /// Loads best stats of current gamemode into 'bestGameStats' object.
        /// </summary>
        public static void LoadBestGameStats() {
            bestGameStats = StatsJsonSaveSystem.LoadBestGameStatsData(GamemodeUtil.ReturnGamemodeType_StringShort(SpawnTargets.gamemode));
        }

        /// <summary>
        /// Sets current gamemodes stats after finish to be compared.
        /// </summary>
        public static void SetStatValues() {
            gamemodeStat     = GamemodeUtil.ReturnGamemodeType_StringShort(CosmeticsSettings.gamemode);
            scoreStat        = GameUI.scoreNum;
            accuracyStat     = GameUI.accuracy;
            ttkStat          = GameUI.newTime;
            bestStreakStat   = GameUI.streakBest;
            targetTotalStat  = SpawnTargets.totalCount;
            targetHitStat    = SpawnTargets.shotsHit;
            targetMissesStat = SpawnTargets.shotMisses;
            kpsStat          = (double)targetHitStat / GameUI.timeStart;
        }

        /// <summary>
        /// After game finish, sets global stat values (SetStatValues) for compare, sets current games stats inside 'AfterActionReport' UI, then checks for any new best stats to save.
        /// </summary>
        public static void CheckAndSetAllStats() {
            SetStatValues();
            SetAfterActionStats();
            SetNewBestGameStats();
        }

        /// <summary>
        /// Checks if current games stats after finish are a new highscore run.
        /// </summary>
        public static void CheckHighscore(int newScore) {
            // If highscore stats exist in saved file.
            if (highscoreData != null) {
                // If current games score is new highscore.
                if (newScore > highscoreScore) {
                    HighscoreSave.SaveNewHighscoreStats(gamemodeStat, scoreStat, accuracyStat, ttkStat, kpsStat, bestStreakStat, targetTotalStat, targetHitStat, targetMissesStat);

                    // Sets "NEW HIGHSCORE!" text.
                    EnableNewHighscoreText();
                    LoadOldHighscore();
                } else {
                    SetScoreLine(StatsUtil.GetLineColor(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue));
                }
            } else {
                // No saved highscore on first run, sets current games run as new highscore.
                SetScoreLine(StatsUtil.neutralLineColor);
                statsManager.newHighscoreTitleText.transform.parent.gameObject.GetComponent<Image>().color = StatsUtil.neutralBackgroundColor;
                HighscoreSave.SaveNewHighscoreStats(GamemodeUtil.ReturnGamemodeType_StringShort(CosmeticsSettings.gamemode), scoreStat, accuracyStat, ttkStat, kpsStat, bestStreakStat, targetTotalStat, targetHitStat, targetMissesStat);
            }

            // Saves current games stats as new 'previous' for next run.
            StatsJsonSaveSystem.SavePreviousGameData(gamemodeStat, scoreStat, accuracyStat, ttkStat, kpsStat, bestStreakStat, targetTotalStat, targetHitStat, targetMissesStat);
        }

        /// <summary>
        /// Sets stats in 'AfterActionReport' UI.
        /// </summary>
        public static void SetAfterActionStats() {
            double kps = kpsStat;

            statsManager.AARTitleText.SetText($"{I18nTextTranslator.SetTranslatedText("afteractionreporttitle")} - {I18nTextTranslator.SetTranslatedText(GamemodeUtil.ReturnGamemodeType_StringShort(CosmeticsSettings.gamemode))}");
            statsManager.scoreStatText.SetText($"{string.Format("{0:n0}", scoreStat)}");
            statsManager.accuracyStatText.SetText($"{accuracyStat}%");
            statsManager.ttkStatText.SetText($"{string.Format("{0:n0}", ttkStat)}ms");
            statsManager.kpsStatText.SetText(string.Format("{0:0.00}/s", kps));
            statsManager.bestStreakStatText.SetText($"{string.Format("{0:n0}", bestStreakStat)}");
            statsManager.targetsTotalStatText.SetText($"{targetTotalStat}");
            statsManager.targetsHitsStatText.SetText($"{targetHitStat}");
            statsManager.targetsMissesStatText.SetText($"{targetMissesStat}");

            CheckHighscore(scoreStat);
            SetStatItems();
        }

        /// <summary>
        /// Enables "NEW HIGHSCORE!" text in 'AfterActionReport' UI;
        /// </summary>
        private static void EnableNewHighscoreText() {
            statsManager.newHighscoreTitleText.enabled = true;
            statsManager.scoreTitleText.enabled         = false;

            statsManager.newHighscoreTitleText.transform.parent.gameObject.GetComponent<Image>().color = StatsUtil.newHighscoreBackgroundColor;

            SetScoreLine(StatsUtil.highscoreLineColor);
        }

        /// <summary>
        /// Sets color of top/bottom score lines.
        /// </summary>
        /// <param name="lineColor"></param>
        private static void SetScoreLine(Color32 lineColor) {
            statsManager.highscoreLineTop.color    = lineColor;
            statsManager.highscoreLineBottom.color = lineColor;
        }

        /// <summary>
        /// Sets all stat items neutral, then sets stat items for previous and highscore in 'AfterActionReport' and 'ExtraStats' panel.
        /// </summary>
        private static void SetStatItems() {
            ClearExtraStatsBackgrounds();
            SetStatsNeutralItems();
            ClearStatDiffs();

            if (previousGameStats != null) {
                SetStatsItemsUpDown();

                // Sets last game stats text in 'ExtraStats' panel.
                statsManager.scoreTextPrevious.SetText($"{string.Format("{0:0,.0}K", previousGameStats.scoreValue)}");
                statsManager.accuracyTextPrevious.SetText($"{previousGameStats.accuracyValue}%");
                statsManager.ttkTextPrevious.SetText($"{string.Format("{0:n0}", previousGameStats.ttkValue)}ms");
                statsManager.kpsTextPrevious.SetText(string.Format("{0:0.00}/s", previousGameStats.kpsValue));
                statsManager.bestStreakTextPrevious.SetText($"{string.Format("{0:n0}", previousGameStats.bestStreakValue)}");
                statsManager.targetsTotalTextPrevious.SetText($"{previousGameStats.targetsTotalValue}");
                statsManager.taretsHitTextPrevious.SetText($"{previousGameStats.targetsHitValue}");
                statsManager.targetsMissesTextPrevious.SetText($"{previousGameStats.targetsMissesValue}");
 
                SetStatDiffs();
                SetStatDiffsText();
                //RefreshDiffGroups();
            }

            if (highscoreData != null) {
                // Set highscore run stats text in 'ExtraStats' panel.
                statsManager.scoreTextHighscore.SetText($"{string.Format("{0:0,.0}K", highscoreData.scoreValue)}");
                statsManager.accuracyTextHighscore.SetText($"{highscoreData.accuracyValue}%");
                statsManager.ttkTextHighscore.SetText($"{string.Format("{0:n0}", highscoreData.ttkValue)}ms");
                statsManager.kpsTextHighscore.SetText(string.Format("{0:0.00}/s", highscoreData.kpsValue));
                statsManager.bestStreakTextHighscore.SetText($"{string.Format("{0:n0}", highscoreData.bestStreakValue)}");
                statsManager.targetsTotalTextHighscore.SetText($"{highscoreData.targetsTotalValue}");
                statsManager.taretsHitTextHighscore.SetText($"{highscoreData.targetsHitValue}");
                statsManager.targetsMissesTextHighscore.SetText($"{highscoreData.targetsMissesValue}");
            }


            SettingsPanel.afterActionReportSet = true;
        }

        /// <summary>
        /// Compares current vs previous game stats, then sets stat differences in 'StatsDiff' object for respective tooltips.
        /// </summary>
        private static void SetStatDiffs() {
            // Actual stat diff number.
            StatsDiff.scoreDiff         = StatsUtil.CheckDifference(scoreStat, previousGameStats.scoreValue);
            StatsDiff.accuracyDiff      = StatsUtil.CheckDifference(accuracyStat, previousGameStats.accuracyValue);
            StatsDiff.ttkDiff           = StatsUtil.CheckDifference(ttkStat, previousGameStats.ttkValue);
            StatsDiff.kpsDiff           = StatsUtil.CheckDifference(kpsStat, previousGameStats.kpsValue);
            StatsDiff.bestStreakDiff    = StatsUtil.CheckDifference(bestStreakStat, previousGameStats.bestStreakValue);
            StatsDiff.targetsTotalDiff  = StatsUtil.CheckDifference(targetTotalStat, previousGameStats.targetsTotalValue);
            StatsDiff.targetHitDiff     = StatsUtil.CheckDifference(targetHitStat, previousGameStats.targetsHitValue);
            StatsDiff.targetsMissesDiff = StatsUtil.CheckDifference(targetMissesStat, previousGameStats.targetsMissesValue);
            // Percent of stat diff (higher/lower).
            StatsDiff.scoreDiffPercent         = StatsUtil.CheckDifference_Percent(scoreStat, previousGameStats.scoreValue);
            StatsDiff.accuracyDiffPercent      = StatsUtil.CheckDifference_Percent(accuracyStat, previousGameStats.accuracyValue);
            StatsDiff.ttkDiffPercent           = StatsUtil.CheckDifference_Percent(ttkStat, previousGameStats.ttkValue);
            StatsDiff.kpsDiffPercent           = StatsUtil.CheckDifference_Percent(kpsStat, previousGameStats.kpsValue);
            StatsDiff.bestStreakDiffPercent    = StatsUtil.CheckDifference_Percent(bestStreakStat, previousGameStats.bestStreakValue);
            StatsDiff.targetsTotalDiffPercent  = StatsUtil.CheckDifference_Percent(targetTotalStat, previousGameStats.targetsTotalValue);
            StatsDiff.targetHitDiffPercent     = StatsUtil.CheckDifference_Percent(targetHitStat, previousGameStats.targetsHitValue);
            StatsDiff.targetsMissesDiffPercent = StatsUtil.CheckDifference_Percent(targetMissesStat, previousGameStats.targetsMissesValue);
            // Whether stat diff is "+" or "-" (higher/lower).
            StatsDiff.scoreDiffSymbol         = StatsUtil.CheckDifference_Symbol(scoreStat, previousGameStats.scoreValue);
            StatsDiff.accuracyDiffSymbol      = StatsUtil.CheckDifference_Symbol(accuracyStat, previousGameStats.accuracyValue);
            StatsDiff.ttkDiffSymbol           = StatsUtil.CheckDifference_Symbol(ttkStat, previousGameStats.ttkValue);
            StatsDiff.kpsDiffSymbol           = StatsUtil.CheckDifference_Symbol(kpsStat, previousGameStats.kpsValue);
            StatsDiff.bestStreakDiffSymbol    = StatsUtil.CheckDifference_Symbol(bestStreakStat, previousGameStats.bestStreakValue);
            StatsDiff.targetsTotalDiffSymbol  = StatsUtil.CheckDifference_Symbol(targetTotalStat, previousGameStats.targetsTotalValue);
            StatsDiff.targetHitDiffSymbol     = StatsUtil.CheckDifference_Symbol(targetHitStat, previousGameStats.targetsHitValue);
            StatsDiff.targetsMissesDiffSymbol = StatsUtil.CheckDifference_Symbol(targetMissesStat, previousGameStats.targetsMissesValue);
            // Formatted stat diff string "+5 (5%)".
            StatsDiff.accuracyDiffStringDisplay      = $"{StatsDiff.accuracyDiffSymbol}{StatsDiff.accuracyDiff}%";
            StatsDiff.ttkDiffStringDisplay           = $"{StatsDiff.ttkDiffSymbol}{StatsDiff.ttkDiff}ms ({(int)StatsDiff.ttkDiffPercent}%)";
            StatsDiff.kpsDiffStringDisplay           = $"{StatsDiff.kpsDiffSymbol}{string.Format("{0:0.00}/s", StatsDiff.kpsDiff)} ({(int)StatsDiff.kpsDiffPercent}%)";
            StatsDiff.bestStreakDiffStringDisplay    = $"{StatsDiff.bestStreakDiffSymbol}{StatsDiff.bestStreakDiff} ({(int)StatsDiff.bestStreakDiffPercent}%)";
            StatsDiff.targetsTotalDiffStringDisplay  = $"{StatsDiff.targetsTotalDiffSymbol}{StatsDiff.targetsTotalDiff} ({(int)StatsDiff.targetsTotalDiffPercent}%)";
            StatsDiff.targetHitDiffStringDisplay     = $"{StatsDiff.targetHitDiffSymbol}{StatsDiff.targetHitDiff} ({(int)StatsDiff.targetHitDiffPercent}%)";
            StatsDiff.targetsMissesDiffStringDisplay = $"{StatsDiff.targetsMissesDiffSymbol}{StatsDiff.targetsMissesDiff} ({(int)StatsDiff.targetsMissesDiffPercent}%)";
        }

        /// <summary>
        /// Populates all stat diffs values in text/item groups.
        /// </summary>
        private static void SetStatDiffsText() {
            // If current and previous stats match, dont set diff string.
            if (!StatsUtil.CheckMatchingStatValue(accuracyStat, previousGameStats.accuracyValue)) {          SetIndivivualStatDiff(statsManager.accuracyExtraInner, StatsDiff.accuracyDiffStringDisplay); }
            if (!StatsUtil.CheckMatchingStatValue(ttkStat, previousGameStats.ttkValue)) {                    SetIndivivualStatDiff(statsManager.ttkExtraInner, StatsDiff.ttkDiffStringDisplay); }
            if (!StatsUtil.CheckMatchingStatValue(kpsStat, previousGameStats.kpsValue)) {                    SetIndivivualStatDiff(statsManager.kpsExtraInner, StatsDiff.kpsDiffStringDisplay); }
            if (!StatsUtil.CheckMatchingStatValue(bestStreakStat, previousGameStats.bestStreakValue)) {      SetIndivivualStatDiff(statsManager.bestStreakExtraInner, StatsDiff.bestStreakDiffStringDisplay); }
            if (!StatsUtil.CheckMatchingStatValue(targetTotalStat, previousGameStats.targetsTotalValue)) {   SetIndivivualStatDiff(statsManager.targetsTotalExtraInner, StatsDiff.targetsTotalDiffStringDisplay); }
            if (!StatsUtil.CheckMatchingStatValue(targetHitStat, previousGameStats.targetsHitValue)) {       SetIndivivualStatDiff(statsManager.taretsHitExtraInner, StatsDiff.targetHitDiffStringDisplay); }
            if (!StatsUtil.CheckMatchingStatValue(targetMissesStat, previousGameStats.targetsMissesValue)) { SetIndivivualStatDiff(statsManager.targetsMissesExtraInner, StatsDiff.targetsMissesDiffStringDisplay); }
        }

        /// <summary>
        /// Refreshes all stat groups after stat text/items are populated.
        /// </summary>
        private static void RefreshDiffGroups() {
            Util.RefreshRootLayoutGroup(statsManager.parentStatGroup);
        }

        /// <summary>
        /// Compares current vs previous game stats to determine item text, color and background color to be set in 'AfterActionReport', then sets everything.
        /// </summary>
        private static void SetStatsItemsUpDown() {
            statsManager.scoreItem.SetText($"{StatsUtil.GetItemText(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue)}");
            statsManager.accuracyItem.SetText($"{StatsUtil.GetItemText(accuracyStat, previousGameStats.accuracyValue, bestGameStats.accuracyValue)}");
            statsManager.ttkItem.SetText($"{StatsUtil.GetItemText_Flip(ttkStat, previousGameStats.ttkValue, bestGameStats.ttkValue)}");
            statsManager.kpsItem.SetText($"{StatsUtil.GetItemText(kpsStat, previousGameStats.kpsValue, bestGameStats.kpsValue)}");
            statsManager.bestStreakItem.SetText($"{StatsUtil.GetItemText(bestStreakStat, previousGameStats.bestStreakValue, bestGameStats.bestStreakValue)}");
            statsManager.targetsTotalItem.SetText($"{StatsUtil.GetItemText(targetTotalStat, previousGameStats.targetsTotalValue, bestGameStats.targetsTotalValue)}");
            statsManager.taretsHitItem.SetText($"{StatsUtil.GetItemText(targetHitStat, previousGameStats.targetsHitValue, bestGameStats.targetsHitValue)}");
            statsManager.targetsMissesItem.SetText($"{StatsUtil.GetItemText_Flip(targetMissesStat, previousGameStats.targetsMissesValue, bestGameStats.targetsMissesValue)}");

            statsManager.scoreItem.color         = StatsUtil.GetItemColor(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue);
            statsManager.accuracyItem.color      = StatsUtil.GetItemColor(accuracyStat, previousGameStats.accuracyValue, bestGameStats.accuracyValue);
            statsManager.ttkItem.color           = StatsUtil.GetItemColor_Flip(ttkStat, previousGameStats.ttkValue, bestGameStats.ttkValue);
            statsManager.kpsItem.color           = StatsUtil.GetItemColor(kpsStat, previousGameStats.kpsValue, bestGameStats.kpsValue);
            statsManager.bestStreakItem.color    = StatsUtil.GetItemColor(bestStreakStat, previousGameStats.bestStreakValue, bestGameStats.bestStreakValue);
            statsManager.targetsTotalItem.color  = StatsUtil.GetItemColor(targetTotalStat, previousGameStats.targetsTotalValue, bestGameStats.targetsTotalValue);
            statsManager.taretsHitItem.color     = StatsUtil.GetItemColor(targetHitStat, previousGameStats.targetsHitValue, bestGameStats.targetsHitValue);
            statsManager.targetsMissesItem.color = StatsUtil.GetItemColor_Flip(targetMissesStat, previousGameStats.targetsMissesValue, bestGameStats.targetsMissesValue);

            SaveExtraStatsBackgrounds();

            if (showBackgrounds) { SetExtraStatsBackgrounds(); }
        }

        /// <summary>
        /// Compares current game stats against saved best stats (if file exists), then sets each new/same best stat in 'ExtraStats' panel.
        /// </summary>
        public static void SetNewBestGameStats() {
            // If saved best stats file exists, compare new to old and set each best stat.
            if (bestGameStats != null) {
                if (StatsUtil.CheckHigherStatValue(scoreStat, bestGameStats.scoreValue)) { bestGameStats.scoreValue = scoreStat; }
                if (StatsUtil.CheckHigherStatValue(accuracyStat, bestGameStats.accuracyValue)) { bestGameStats.accuracyValue = accuracyStat; }
                if (StatsUtil.CheckHigherStatValue_Flip(ttkStat, bestGameStats.ttkValue)) { bestGameStats.ttkValue = ttkStat; }
                if (StatsUtil.CheckHigherStatValue(kpsStat, bestGameStats.kpsValue)) { bestGameStats.kpsValue = kpsStat; }
                if (StatsUtil.CheckHigherStatValue(bestStreakStat, bestGameStats.bestStreakValue)) { bestGameStats.bestStreakValue = bestStreakStat; }
                if (StatsUtil.CheckHigherStatValue(targetTotalStat, bestGameStats.targetsTotalValue)) { bestGameStats.targetsTotalValue = targetTotalStat; }
                if (StatsUtil.CheckHigherStatValue(targetHitStat, bestGameStats.targetsHitValue)) { bestGameStats.targetsHitValue = targetHitStat; }
                if (StatsUtil.CheckHigherStatValue_Flip(targetMissesStat, bestGameStats.targetsMissesValue)) { bestGameStats.targetsMissesValue = targetMissesStat; }

                // Set best stats text.
                statsManager.scoreTextBest.SetText($"{string.Format("{0:0,.0}K", bestGameStats.scoreValue)}");
                statsManager.accuracyTextBest.SetText($"{bestGameStats.accuracyValue}%");
                statsManager.ttkTextBest.SetText($"{string.Format("{0:n0}", bestGameStats.ttkValue)}ms");
                statsManager.kpsTextBest.SetText(string.Format("{0:0.00}/s", bestGameStats.kpsValue));
                statsManager.bestStreakTextBest.SetText($"{string.Format("{0:n0}", bestGameStats.bestStreakValue)}");
                statsManager.targetsTotalTextBest.SetText($"{bestGameStats.targetsTotalValue}");
                statsManager.taretsHitTextBest.SetText($"{bestGameStats.targetsHitValue}");
                statsManager.targetsMissesTextBest.SetText($"{bestGameStats.targetsMissesValue}");

                // Save all best stats back to file.
                StatsJsonSaveSystem.SaveBestGameStatsData(gamemodeStat, bestGameStats.scoreValue, bestGameStats.accuracyValue, bestGameStats.ttkValue, bestGameStats.kpsValue, bestGameStats.bestStreakValue, bestGameStats.targetsTotalValue, bestGameStats.targetsHitValue, bestGameStats.targetsMissesValue);
            } else {
                // If best stats file doesnt exist, save current run stats to new best file.
                StatsJsonSaveSystem.SaveBestGameStatsData(gamemodeStat, scoreStat, accuracyStat, ttkStat, kpsStat, bestStreakStat, targetTotalStat, targetHitStat, targetMissesStat);
            }
        }

        /// <summary>
        /// Saves appropriate stat container background colors to backgroundsSaves color array.
        /// </summary>
        public static void SaveExtraStatsBackgrounds() {
            backgroundsSaves[0] = StatsUtil.GetItemBackgroundColor(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue);
            backgroundsSaves[1] = StatsUtil.GetItemBackgroundColor(accuracyStat, previousGameStats.accuracyValue, bestGameStats.accuracyValue);
            backgroundsSaves[2] = StatsUtil.GetItemBackgroundColor_Flip(ttkStat, previousGameStats.ttkValue, bestGameStats.ttkValue);
            backgroundsSaves[3] = StatsUtil.GetItemBackgroundColor(kpsStat, previousGameStats.kpsValue, bestGameStats.kpsValue);
            backgroundsSaves[4] = StatsUtil.GetItemBackgroundColor(bestStreakStat, previousGameStats.bestStreakValue, bestGameStats.bestStreakValue);
            backgroundsSaves[5] = StatsUtil.GetItemBackgroundColor(targetTotalStat, previousGameStats.targetsTotalValue, bestGameStats.targetsTotalValue);
            backgroundsSaves[6] = StatsUtil.GetItemBackgroundColor(targetHitStat, previousGameStats.targetsHitValue, bestGameStats.targetsHitValue);
            backgroundsSaves[7] = StatsUtil.GetItemBackgroundColor_Flip(targetMissesStat, previousGameStats.targetsMissesValue, bestGameStats.targetsMissesValue);

            backgroundsSaved = true;
        }

        private static void SetIndivivualStatDiff(TMP_Text statDiffText, string statDiffDisplay) {
            statDiffText.SetText($"{statDiffDisplay}");
            statDiffText.transform.parent.gameObject.GetComponent<Image>().color = StatsUtil.statDiffEnabled;
        }

        /// <summary>
        /// Sets all stat diffs text to empty ("").
        /// </summary>
        private static void ClearStatDiffs() {
            StatsUtil.ClearStatDiffs("", statsManager.accuracyExtraInner, statsManager.ttkExtraInner, statsManager.kpsExtraInner, statsManager.bestStreakExtraInner, statsManager.targetsTotalExtraInner, statsManager.taretsHitExtraInner, statsManager.targetsMissesExtraInner);
        }

        /// <summary>
        /// Sets all items in 'AfterActionReport' neutral text and color.
        /// </summary>
        private static void SetStatsNeutralItems() {
            StatsUtil.SetNeutralItems(StatsUtil.itemNeutral, StatsUtil.itemColorGrey, statsManager.scoreItem, statsManager.accuracyItem, statsManager.ttkItem, statsManager.kpsItem, statsManager.bestStreakItem, statsManager.targetsTotalItem, statsManager.taretsHitItem, statsManager.targetsMissesItem);
        }

        /// <summary>
        /// Sets all stat container background colors to corresponding backgroundsSaves item.
        /// </summary>
        public static void SetExtraStatsBackgrounds() {
            StatsUtil.ClearStatBackgrounds(backgroundsSaves, statsManager.scoreContainerBackground, statsManager.accuracyContainerBackground, statsManager.ttkContainerBackground, statsManager.kpsContainerBackground, statsManager.bestStreakContainerBackground, statsManager.targetsTotalContainerBackground, statsManager.taretsHitContainerBackground, statsManager.targetsMissesContainerBackground);
        }

        /// <summary>
        /// Resets all stat container background colors to original background colors.
        /// </summary>
        public static void ClearExtraStatsBackgrounds() {
            // Alternating grey stat backgrounds
            statsManager.scoreContainerBackground.GetComponent<Image>().color         = StatsUtil.clearBackgroundLight;
            statsManager.accuracyContainerBackground.GetComponent<Image>().color      = StatsUtil.clearBackgroundLight;
            statsManager.ttkContainerBackground.GetComponent<Image>().color           = StatsUtil.clearBackgroundDark;
            statsManager.kpsContainerBackground.GetComponent<Image>().color           = StatsUtil.clearBackgroundLight;
            statsManager.bestStreakContainerBackground.GetComponent<Image>().color    = StatsUtil.clearBackgroundDark;
            statsManager.targetsTotalContainerBackground.GetComponent<Image>().color  = StatsUtil.clearBackgroundLight;
            statsManager.taretsHitContainerBackground.GetComponent<Image>().color     = StatsUtil.clearBackgroundDark;
            statsManager.targetsMissesContainerBackground.GetComponent<Image>().color = StatsUtil.clearBackgroundLight;

            // Solid grey stat backgrounds
            //StatsUtil.ClearStatBackgrounds(StatsUtil.clearBackgroundLight, statsManager.scoreContainerBackground, statsManager.accuracyContainerBackground, statsManager.ttkContainerBackground, statsManager.kpsContainerBackground, statsManager.bestStreakContainerBackground, statsManager.targetsTotalContainerBackground, statsManager.taretsHitContainerBackground, statsManager.targetsMissesContainerBackground);
        }

        public static void SetExtraStatsState(bool enabled) { if (enabled) { ShowExtraStatsPanel(); } else { HideExtraStatsPanel(); } }
        public static void SetExtraStatsBackgroundsState(bool enabled) { if (enabled) { SetExtraStatsBackgrounds(); } else { ClearExtraStatsBackgrounds(); } }
        /// <summary>
        /// Show 'ExtraStats' panel;
        /// </summary>
        public static void ShowExtraStatsPanel() { statsManager.extraStatsPanel.SetActive(true); }
        /// <summary>
        /// Hides 'ExtraStats' panel;
        /// </summary>
        public static void HideExtraStatsPanel() { statsManager.extraStatsPanel.SetActive(false); }
        /// <summary>
        /// Resets AAR scrollview to top.
        /// </summary>
        public static void ResetAARScrollView() { ScrollRectExtension.ScrollToTop(statsManager.aarScrollView.GetComponent<ScrollRect>()); }
    }
}
