using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Gamemode;
using SomeAimGame.Targets;
using SomeAimGame.Utilities;

namespace SomeAimGame.Stats {
    public class StatsManager : MonoBehaviour {
        public TMP_Text AARTitleText, scoreTitleText, newHighscoreTitleText, accuracyTitleText, ttkTitleText, kpsTitleText, bestStreakTitleText, targetsTotalTitleText, targetsHitsTitleText, targetsMissesTitleText;

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
        private static Color32[] backgroundsSaves;

        private static int highscoreScore;
        private static HighscoreDataSerial highscoreData;
        public static PreviousGameStats previousGameStats;
        public static BestGameStats bestGameStats;

        private static string gamemodeStat;
        private static int scoreStat, accuracyStat, ttkStat, bestStreakStat, targetTotalStat, targetHitStat, targetMissesStat;
        private static double kpsStat;
        private static WaitForSeconds setStatRowDelay = new WaitForSeconds(0.1f);

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
            ClearEntireAAR();                               // Clear AAR
            SetStatValues();                                // Load stat values
            SaveExtraStatsBackgrounds();                    // Save stat backgrounds
            LoadStatDiffs();                                // Load stat diff strings
            SettingsPanel.OpenAfterActionReport();          // Open AAR
            CheckHighscore(scoreStat);                      // Check highscore
            statsManager.StartCoroutine(SetAllStatRows());  // Set all stat rows
        }

        /// <summary>
        /// Coroutine that sets full stat rows in AAR with slight delay after each.
        /// </summary>
        /// <returns></returns>
        private static IEnumerator SetAllStatRows() {
            SetScoreRow();
            yield return setStatRowDelay;
            SetAccuracyRow();
            yield return setStatRowDelay;
            SetTTKRow();
            yield return setStatRowDelay;
            SetKPSRow();
            yield return setStatRowDelay;
            SetStreakRow();
            yield return setStatRowDelay;
            SetTargetsTotalRow();
            yield return setStatRowDelay;
            SetTargetsHitRow();
            yield return setStatRowDelay;
            SetTargetsMissesRow();

            SettingsPanel.afterActionReportSet = true;
            SetExtraStatsPanel();
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

        #region Extra stats panel

        /// <summary>
        /// Sets full previous game stat column in AAR extra stats panel if PreviousGameStats previousGameStats not null.
        /// </summary>
        private static void SetPreviousStatText() {
            // Sets last game stats text in 'ExtraStats' panel.
            if (previousGameStats != null) {
                statsManager.scoreTextPrevious.SetText($"{string.Format("{0:0,.0}K", previousGameStats.scoreValue)}");
                statsManager.accuracyTextPrevious.SetText($"{previousGameStats.accuracyValue}%");
                statsManager.ttkTextPrevious.SetText($"{string.Format("{0:n0}", previousGameStats.ttkValue)}ms");
                statsManager.kpsTextPrevious.SetText(string.Format("{0:0.00}/s", previousGameStats.kpsValue));
                statsManager.bestStreakTextPrevious.SetText($"{string.Format("{0:n0}", previousGameStats.bestStreakValue)}");
                statsManager.targetsTotalTextPrevious.SetText($"{previousGameStats.targetsTotalValue}");
                statsManager.taretsHitTextPrevious.SetText($"{previousGameStats.targetsHitValue}");
                statsManager.targetsMissesTextPrevious.SetText($"{previousGameStats.targetsMissesValue}");
            }
        }

        /// <summary>
        /// Sets full highscore stat column in AAR extra stats panel if HighscoreDataSerial highscoreData not null.
        /// </summary>
        private static void SetHighscoreStatText() {
            // Set highscore run stats text in 'ExtraStats' panel.
            if (highscoreData != null) {
                statsManager.scoreTextHighscore.SetText($"{string.Format("{0:0,.0}K", highscoreData.scoreValue)}");
                statsManager.accuracyTextHighscore.SetText($"{highscoreData.accuracyValue}%");
                statsManager.ttkTextHighscore.SetText($"{string.Format("{0:n0}", highscoreData.ttkValue)}ms");
                statsManager.kpsTextHighscore.SetText(string.Format("{0:0.00}/s", highscoreData.kpsValue));
                statsManager.bestStreakTextHighscore.SetText($"{string.Format("{0:n0}", highscoreData.bestStreakValue)}");
                statsManager.targetsTotalTextHighscore.SetText($"{highscoreData.targetsTotalValue}");
                statsManager.taretsHitTextHighscore.SetText($"{highscoreData.targetsHitValue}");
                statsManager.targetsMissesTextHighscore.SetText($"{highscoreData.targetsMissesValue}");
            }
        }

        /// <summary>
        /// Compares current game stats against saved best stats (if file exists), then sets each new/same best stat in 'ExtraStats' panel.
        /// </summary>
        public static void SetNewBestGameStats() {
            // If saved best stats file exists, compare new to old and set each best stat.
            if (bestGameStats != null) {
                if (StatsUtil.CheckHigherStatValue(scoreStat, bestGameStats.scoreValue)) {                     bestGameStats.scoreValue         = scoreStat; }
                if (StatsUtil.CheckHigherStatValue(accuracyStat, bestGameStats.accuracyValue)) {               bestGameStats.accuracyValue      = accuracyStat; }
                if (StatsUtil.CheckHigherStatValue_Flip(ttkStat, bestGameStats.ttkValue)) {                    bestGameStats.ttkValue           = ttkStat; }
                if (StatsUtil.CheckHigherStatValue(kpsStat, bestGameStats.kpsValue)) {                         bestGameStats.kpsValue           = kpsStat; }
                if (StatsUtil.CheckHigherStatValue(bestStreakStat, bestGameStats.bestStreakValue)) {           bestGameStats.bestStreakValue    = bestStreakStat; }
                if (StatsUtil.CheckHigherStatValue(targetTotalStat, bestGameStats.targetsTotalValue)) {        bestGameStats.targetsTotalValue  = targetTotalStat; }
                if (StatsUtil.CheckHigherStatValue(targetHitStat, bestGameStats.targetsHitValue)) {            bestGameStats.targetsHitValue    = targetHitStat; }
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

        #endregion

        #region Individual rows

        /// <summary>
        /// Sets entire score stat row (diff/stat/item/background).
        /// </summary>
        private static void SetScoreRow() {
            statsManager.scoreStatText.SetText($"{string.Format("{0:n0}", scoreStat)}");
            SetIndivivualStatItem(statsManager.scoreItem, StatsUtil.GetItemText(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue), StatsUtil.GetItemColor(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue), statsManager.scoreContainerBackground, 0);
        }
        /// <summary>
        /// Sets entire accuracy stat row (diff/stat/item/background).
        /// </summary>
        private static void SetAccuracyRow() {
            statsManager.accuracyStatText.SetText($"{accuracyStat}%");
            CheckStatMatch(accuracyStat, previousGameStats.accuracyValue, statsManager.accuracyExtraInner, StatsDiff.accuracyDiffStringDisplay);
            SetIndivivualStatItem(statsManager.accuracyItem, StatsUtil.GetItemText(accuracyStat, previousGameStats.accuracyValue, bestGameStats.accuracyValue), StatsUtil.GetItemColor(accuracyStat, previousGameStats.accuracyValue, bestGameStats.accuracyValue), statsManager.accuracyContainerBackground, 1);
        }
        /// <summary>
        /// Sets entire TTK stat row (diff/stat/item/background).
        /// </summary>
        private static void SetTTKRow() {
            statsManager.ttkStatText.SetText($"{string.Format("{0:n0}", ttkStat)}ms");
            CheckStatMatch(ttkStat, previousGameStats.ttkValue, statsManager.ttkExtraInner, StatsDiff.ttkDiffStringDisplay);
            SetIndivivualStatItem(statsManager.ttkItem, StatsUtil.GetItemText_Flip(ttkStat, previousGameStats.ttkValue, bestGameStats.ttkValue), StatsUtil.GetItemColor_Flip(ttkStat, previousGameStats.ttkValue, bestGameStats.ttkValue), statsManager.ttkContainerBackground, 2);
        }
        /// <summary>
        /// Sets entire KPS stat row (diff/stat/item/background).
        /// </summary>
        private static void SetKPSRow() {
            double kps = kpsStat;
            statsManager.kpsStatText.SetText(string.Format("{0:0.00}/s", kps));
            CheckStatMatch(kpsStat, previousGameStats.kpsValue, statsManager.kpsExtraInner, StatsDiff.kpsDiffStringDisplay);
            SetIndivivualStatItem(statsManager.kpsItem, StatsUtil.GetItemText(kpsStat, previousGameStats.kpsValue, bestGameStats.kpsValue), StatsUtil.GetItemColor(kpsStat, previousGameStats.kpsValue, bestGameStats.kpsValue), statsManager.kpsContainerBackground, 3);
        }
        /// <summary>
        /// Sets entire best streak stat row (diff/stat/item/background).
        /// </summary>
        private static void SetStreakRow() {
            statsManager.bestStreakStatText.SetText($"{string.Format("{0:n0}", bestStreakStat)}");
            CheckStatMatch(bestStreakStat, previousGameStats.bestStreakValue, statsManager.bestStreakExtraInner, StatsDiff.bestStreakDiffStringDisplay);
            SetIndivivualStatItem(statsManager.bestStreakItem, StatsUtil.GetItemText(bestStreakStat, previousGameStats.bestStreakValue, bestGameStats.bestStreakValue), StatsUtil.GetItemColor(bestStreakStat, previousGameStats.bestStreakValue, bestGameStats.bestStreakValue), statsManager.bestStreakContainerBackground, 4);
        }
        /// <summary>
        /// Sets entire targets total stat row (diff/stat/item/background).
        /// </summary>
        private static void SetTargetsTotalRow() {
            statsManager.targetsTotalStatText.SetText($"{targetTotalStat}");
            CheckStatMatch(targetTotalStat, previousGameStats.targetsTotalValue, statsManager.targetsTotalExtraInner, StatsDiff.targetsTotalDiffStringDisplay);
            SetIndivivualStatItem(statsManager.targetsTotalItem, StatsUtil.GetItemText(targetTotalStat, previousGameStats.targetsTotalValue, bestGameStats.targetsTotalValue), StatsUtil.GetItemColor(targetTotalStat, previousGameStats.targetsTotalValue, bestGameStats.targetsTotalValue), statsManager.targetsTotalContainerBackground, 5);
        }
        /// <summary>
        /// Sets entire targets hit stat row (diff/stat/item/background).
        /// </summary>
        private static void SetTargetsHitRow() {
            statsManager.targetsHitsStatText.SetText($"{targetHitStat}");
            CheckStatMatch(targetHitStat, previousGameStats.targetsHitValue, statsManager.taretsHitExtraInner, StatsDiff.targetHitDiffStringDisplay);
            SetIndivivualStatItem(statsManager.taretsHitItem, StatsUtil.GetItemText(targetHitStat, previousGameStats.targetsHitValue, bestGameStats.targetsHitValue), StatsUtil.GetItemColor(targetHitStat, previousGameStats.targetsHitValue, bestGameStats.targetsHitValue), statsManager.taretsHitContainerBackground, 6);
        }
        /// <summary>
        /// Sets entire targets missed stat row (diff/stat/item/background).
        /// </summary>
        private static void SetTargetsMissesRow() {
            statsManager.targetsMissesStatText.SetText($"{targetMissesStat}");
            CheckStatMatch(targetMissesStat, previousGameStats.targetsMissesValue, statsManager.targetsMissesExtraInner, StatsDiff.targetsMissesDiffStringDisplay);
            SetIndivivualStatItem(statsManager.targetsMissesItem, StatsUtil.GetItemText_Flip(targetMissesStat, previousGameStats.targetsMissesValue, bestGameStats.targetsMissesValue), StatsUtil.GetItemColor_Flip(targetMissesStat, previousGameStats.targetsMissesValue, bestGameStats.targetsMissesValue), statsManager.targetsMissesContainerBackground, 7);
        }

        #endregion

        #region Utils

        /// <summary>
        /// Calls all methods to clear entire AAR stat rows (diff/stat/item/background).
        /// </summary>
        private static void ClearEntireAAR() {
            statsManager.AARTitleText.SetText($"{I18nTextTranslator.SetTranslatedText("afteractionreporttitle")} - {I18nTextTranslator.SetTranslatedText(GamemodeUtil.ReturnGamemodeType_StringShort(CosmeticsSettings.gamemode))}");
            ClearStatBackgrounds();
            StatsUtil.FillStatBackgrounds(backgroundsSaves, StatsUtil.neutralBackgroundColor);
            //ClearTitlesText();
            ClearStatDiffs();
            ClearStatsText();
            SetStatsNeutralItems();
        }

        /// <summary>
        /// Sets entire AAR extra stats panel stat values (previous/average/best/highscore).
        /// </summary>
        private static void SetExtraStatsPanel() {
            SetPreviousStatText();
            SetHighscoreStatText();
            SetNewBestGameStats();

            //if (ExtraSettings.showExtraStats) { ShowExtraStatsPanel(); }
        }

        /// <summary>
        /// Compares current vs previous game stats, then sets stat differences in 'StatsDiff' object for respective tooltips.
        /// </summary>
        private static void LoadStatDiffs() {
            if (previousGameStats != null) {
                StatsDiff.LoadStatDiffs(scoreStat, accuracyStat, ttkStat, kpsStat, bestStreakStat, targetTotalStat, targetHitStat, targetMissesStat, previousGameStats);
            }
        }

        /// <summary>
        /// Saves appropriate stat container background colors to backgroundsSaves color array.
        /// </summary>
        public static void SaveExtraStatsBackgrounds() {
            if (previousGameStats != null && bestGameStats != null) {
                backgroundsSaves[0] = StatsUtil.GetItemBackgroundColor(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue);
                backgroundsSaves[1] = StatsUtil.GetItemBackgroundColor(accuracyStat, previousGameStats.accuracyValue, bestGameStats.accuracyValue);
                backgroundsSaves[2] = StatsUtil.GetItemBackgroundColor_Flip(ttkStat, previousGameStats.ttkValue, bestGameStats.ttkValue);
                backgroundsSaves[3] = StatsUtil.GetItemBackgroundColor(kpsStat, previousGameStats.kpsValue, bestGameStats.kpsValue);
                backgroundsSaves[4] = StatsUtil.GetItemBackgroundColor(bestStreakStat, previousGameStats.bestStreakValue, bestGameStats.bestStreakValue);
                backgroundsSaves[5] = StatsUtil.GetItemBackgroundColor(targetTotalStat, previousGameStats.targetsTotalValue, bestGameStats.targetsTotalValue);
                backgroundsSaves[6] = StatsUtil.GetItemBackgroundColor(targetHitStat, previousGameStats.targetsHitValue, bestGameStats.targetsHitValue);
                backgroundsSaves[7] = StatsUtil.GetItemBackgroundColor_Flip(targetMissesStat, previousGameStats.targetsMissesValue, bestGameStats.targetsMissesValue);
            }
        }

        /// <summary>
        /// Enables "NEW HIGHSCORE!" text in 'AfterActionReport' UI;
        /// </summary>
        private static void EnableNewHighscoreText() {
            statsManager.newHighscoreTitleText.enabled = true;
            statsManager.scoreTitleText.enabled        = false;

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
        /// Checks if supplied current stat (currentStat) and previous stat (previousStat) match, then calls SetIndivivualStatDiff(TMP_Text statDiffText, string statDiffDisplay) to set diff if they dont.
        /// </summary>
        /// <param name="currentStat"></param>
        /// <param name="previousStat"></param>
        /// <param name="diffText"></param>
        /// <param name="setDiff"></param>
        private static void CheckStatMatch(double currentStat, double previousStat, TMP_Text diffText, string setDiff) {
            if (previousGameStats != null) {
                if (!StatsUtil.CheckMatchingStatValue(currentStat, previousStat)) { SetIndivivualStatDiff(diffText, setDiff); }
            }
        }
        /// <summary>
        /// Sets supplied stat diff TMP_Text (statDiffText) to supplied diff display string (statDiffDisplay).
        /// </summary>
        /// <param name="statDiffText"></param>
        /// <param name="statDiffDisplay"></param>
        private static void SetIndivivualStatDiff(TMP_Text statDiffText, string statDiffDisplay) {
            if (previousGameStats != null) {
                statDiffText.SetText($"{statDiffDisplay}");
                statDiffText.transform.parent.gameObject.GetComponent<Image>().color = StatsUtil.statDiffEnabled;
            }
        }
        /// <summary>
        /// Sets supplied stat item text from supplied string (statItem), and color from supplied Color32 (setItemColor). Also sets stat row container background from supplied backgroundSaves index (backgroundIdx).
        /// </summary>
        /// <param name="statItemText"></param>
        /// <param name="statItem"></param>
        /// <param name="setItemColor"></param>
        /// <param name="containerObject"></param>
        /// <param name="backgroundIdx"></param>
        private static void SetIndivivualStatItem(TMP_Text statItemText, string statItem, Color32 setItemColor, GameObject containerObject, int backgroundIdx) {
            if (previousGameStats != null) {
                statItemText.SetText($"{statItem}");
                statItemText.color = setItemColor;
                if (showBackgrounds) containerObject.GetComponent<Image>().color = backgroundsSaves[backgroundIdx];
            }
        }

        /// <summary>
        /// Refreshes all stat groups after stat text/items are populated.
        /// </summary>
        private static void RefreshDiffGroups() {
            Util.RefreshRootLayoutGroup(statsManager.parentStatGroup);
        }
        /// <summary>
        /// Clears all AAR stat rows title texts.
        /// </summary>
        private static void ClearTitlesText() {
            StatsUtil.ClearTMPText("", statsManager.accuracyTitleText, statsManager.ttkTitleText, statsManager.kpsTitleText, statsManager.bestStreakTitleText, statsManager.targetsTotalTitleText, statsManager.targetsHitsTitleText, statsManager.targetsMissesTitleText);
        }
        /// <summary>
        /// Clears all AAR stat rows stat texts.
        /// </summary>
        private static void ClearStatsText() {
            StatsUtil.ClearTMPText("", statsManager.accuracyStatText, statsManager.ttkStatText, statsManager.kpsStatText, statsManager.bestStreakStatText, statsManager.targetsTotalStatText, statsManager.targetsHitsStatText, statsManager.targetsMissesStatText);
        }
        /// <summary>
        /// Sets all stat diffs text to empty ("").
        /// </summary>
        private static void ClearStatDiffs() {
            StatsUtil.ClearTMPTextAndColor("", statsManager.accuracyExtraInner, statsManager.ttkExtraInner, statsManager.kpsExtraInner, statsManager.bestStreakExtraInner, statsManager.targetsTotalExtraInner, statsManager.taretsHitExtraInner, statsManager.targetsMissesExtraInner);
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
        public static void SetStatBackgrounds() {
            StatsUtil.ClearStatBackgrounds(backgroundsSaves, statsManager.scoreContainerBackground, statsManager.accuracyContainerBackground, statsManager.ttkContainerBackground, statsManager.kpsContainerBackground, statsManager.bestStreakContainerBackground, statsManager.targetsTotalContainerBackground, statsManager.taretsHitContainerBackground, statsManager.targetsMissesContainerBackground);
        }

        /// <summary>
        /// Resets all stat container background colors to original background colors.
        /// </summary>
        public static void ClearStatBackgrounds() {
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

        /// <summary>
        /// Sets state of AAR extra stats panel (opened/closed).
        /// </summary>
        /// <param name="enabled"></param>
        public static void SetExtraStatsState(bool enabled) { if (enabled) { ShowExtraStatsPanel(); } else { HideExtraStatsPanel(); } }
        /// <summary>
        /// Sets/clears backgrounds of stat rows in AAR.
        /// </summary>
        /// <param name="enabled"></param>
        public static void SetStatsBackgroundState(bool enabled) { if (enabled) { SetStatBackgrounds(); } else { ClearStatBackgrounds(); } }
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

        #endregion
    }
}
