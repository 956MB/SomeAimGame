using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsManager : MonoBehaviour {
    public TMP_Text titleText, showExtraGamemodeText, scoreText, gamemodeText, accuracyText, ttkText, kpsText, bestStreakText, targetsTotalText, targetsHitsText, targetsMissesText, scoreTitleText;
    public TMP_Text scoreItem, accuracyItem, ttkItem, kpsItem, bestStreakItem, targetsTotalItem, taretsHitItem, targetsMissesItem;
    public TMP_Text scoreTextBest, accuracyTextBest, ttkTextBest, kpsTextBest, bestStreakTextBest, targetsTotalTextBest, taretsHitTextBest, targetsMissesTextBest;
    public TMP_Text scoreTextPrevious, accuracyTextPrevious, ttkTextPrevious, kpsTextPrevious, bestStreakTextPrevious, targetsTotalTextPrevious, taretsHitTextPrevious, targetsMissesTextPrevious;
    public TMP_Text scoreTextHighscore, accuracyTextHighscore, ttkTextHighscore, kpsTextHighscore, bestStreakTextHighscore, targetsTotalTextHighscore, taretsHitTextHighscore, targetsMissesTextHighscore;
    public TMP_Text scoreEffectText, newHighscoreEffectText;
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

    private static StatsManager stats;
    void Awake() { stats = this; }

    private void Start() {
        scoreTitleText.enabled         = true;
        newHighscoreEffectText.enabled = false;
        backgroundsSaves               = new Color32[8];
    }

    /// <summary>
    /// Loads highscore stats of current gamemode into 'highscoreData' object, then sets 'highscoreScore' from highscore stats.
    /// </summary>
    public static void LoadOldHighscore() {
        highscoreData = HighscoreSaveSystem.LoadHighscoreData(GamemodeType.ReturnGamemodeType_StringShort(SpawnTargets.gamemode));
        if (highscoreData != null) {
            highscoreScore = highscoreData.scoreValue;
        }
    }

    /// <summary>
    /// Loads previous stats of current gamemode into 'previousGameStats' object.
    /// </summary>
    public static void LoadPreviousGameStats() {
        previousGameStats = StatsJsonSaveSystem.LoadLastGameData(GamemodeType.ReturnGamemodeType_StringShort(SpawnTargets.gamemode));
    }

    /// <summary>
    /// Loads best stats of current gamemode into 'bestGameStats' object.
    /// </summary>
    public static void LoadBestGameStats() {
        bestGameStats = StatsJsonSaveSystem.LoadBestGameStatsData(GamemodeType.ReturnGamemodeType_StringShort(SpawnTargets.gamemode));
    }

    /// <summary>
    /// Sets current gamemodes stats after finish to be compared.
    /// </summary>
    public static void SetStatValues() {
        gamemodeStat     = GamemodeType.ReturnGamemodeType_StringShort(CosmeticsSettings.gamemode);
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
                stats.highscoreLineTop.color    = StatsUtil.GetLineColor(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue);
                stats.highscoreLineBottom.color = StatsUtil.GetLineColor(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue);
            }
        } else {
            // No saved highscore on first run, sets current games run as new highscore.
            stats.highscoreLineTop.color    = StatsUtil.neutralLineColor;
            stats.highscoreLineBottom.color = StatsUtil.neutralLineColor;
            stats.newHighscoreEffectText.transform.parent.gameObject.GetComponent<Image>().color = StatsUtil.neutralBackgroundColor;
            HighscoreSave.SaveNewHighscoreStats(GamemodeType.ReturnGamemodeType_StringShort(CosmeticsSettings.gamemode), scoreStat, accuracyStat, ttkStat, kpsStat, bestStreakStat, targetTotalStat, targetHitStat, targetMissesStat);
        }

        // Saves current games stats as new 'previous' for next run.
        StatsJsonSaveSystem.SavePreviousGameData(gamemodeStat, scoreStat, accuracyStat, ttkStat, kpsStat, bestStreakStat, targetTotalStat, targetHitStat, targetMissesStat);
    }

    /// <summary>
    /// Sets stats in 'AfterActionReport' UI.
    /// </summary>
    public static void SetAfterActionStats() {
        double kps = kpsStat;

        stats.titleText.SetText($"{I18nTextTranslator.SetTranslatedText("afteractionreporttitle")} - {I18nTextTranslator.SetTranslatedText(GamemodeType.ReturnGamemodeType_StringShort(CosmeticsSettings.gamemode))}");
        stats.scoreEffectText.SetText($"{string.Format("{0:n0}", scoreStat)}");
        stats.accuracyText.SetText($"{accuracyStat}%");
        stats.ttkText.SetText($"{string.Format("{0:n0}", ttkStat)}ms");
        stats.kpsText.SetText(string.Format("{0:0.00}/s", kps));
        stats.bestStreakText.SetText($"{string.Format("{0:n0}", bestStreakStat)}");
        stats.targetsTotalText.SetText($"{targetTotalStat}");
        stats.targetsHitsText.SetText($"{targetHitStat}");
        stats.targetsMissesText.SetText($"{targetMissesStat}");

        CheckHighscore(scoreStat);
        SetStatItems();
    }

    /// <summary>
    /// Enables "NEW HIGHSCORE!" text in 'AfterActionReport' UI;
    /// </summary>
    private static void EnableNewHighscoreText() {
        stats.newHighscoreEffectText.enabled = true;
        stats.scoreTitleText.enabled         = false;

        stats.newHighscoreEffectText.transform.parent.gameObject.GetComponent<Image>().color = StatsUtil.newHighscoreBackgroundColor;

        stats.highscoreLineTop.color    = StatsUtil.highscoreLineColor;
        stats.highscoreLineBottom.color = StatsUtil.highscoreLineColor;
    }

    /// <summary>
    /// Sets all stat items neutral, then sets stat items for previous and highscore in 'AfterActionReport' and 'ExtraStats' panel.
    /// </summary>
    private static void SetStatItems() {
        ClearExtraStatsBackgrounds();
        SetStatsNeutralItems();

        if (previousGameStats != null) {
            SetStatsItemsUpDown();

            // Sets last game stats text in 'ExtraStats' panel.
            stats.scoreTextPrevious.SetText($"{string.Format("{0:0,.0}K", previousGameStats.scoreValue)}");
            stats.accuracyTextPrevious.SetText($"{previousGameStats.accuracyValue}%");
            stats.ttkTextPrevious.SetText($"{string.Format("{0:n0}", previousGameStats.ttkValue)}ms");
            stats.kpsTextPrevious.SetText(string.Format("{0:0.00}/s", previousGameStats.kpsValue));
            stats.bestStreakTextPrevious.SetText($"{string.Format("{0:n0}", previousGameStats.bestStreakValue)}");
            stats.targetsTotalTextPrevious.SetText($"{previousGameStats.targetsTotalValue}");
            stats.taretsHitTextPrevious.SetText($"{previousGameStats.targetsHitValue}");
            stats.targetsMissesTextPrevious.SetText($"{previousGameStats.targetsMissesValue}");
 
            SetStatItemDiffs();
        }

        if (highscoreData != null) {
            // Set highscore run stats text in 'ExtraStats' panel.
            stats.scoreTextHighscore.SetText($"{string.Format("{0:0,.0}K", highscoreData.scoreValue)}");
            stats.accuracyTextHighscore.SetText($"{highscoreData.accuracyValue}%");
            stats.ttkTextHighscore.SetText($"{string.Format("{0:n0}", highscoreData.ttkValue)}ms");
            stats.kpsTextHighscore.SetText(string.Format("{0:0.00}/s", highscoreData.kpsValue));
            stats.bestStreakTextHighscore.SetText($"{string.Format("{0:n0}", highscoreData.bestStreakValue)}");
            stats.targetsTotalTextHighscore.SetText($"{highscoreData.targetsTotalValue}");
            stats.taretsHitTextHighscore.SetText($"{highscoreData.targetsHitValue}");
            stats.targetsMissesTextHighscore.SetText($"{highscoreData.targetsMissesValue}");
        }


        SettingsPanel.afterActionReportSet = true;
    }

    /// <summary>
    /// Compares current vs previous game stats, then sets stat differences in 'StatsDiff' object for respective tooltips.
    /// </summary>
    private static void SetStatItemDiffs() {
        StatsDiff.scoreDiff         = StatsUtil.CheckDifference(scoreStat, previousGameStats.scoreValue);
        StatsDiff.accuracyDiff      = StatsUtil.CheckDifference(accuracyStat, previousGameStats.accuracyValue);
        StatsDiff.ttkDiff           = StatsUtil.CheckDifference(ttkStat, previousGameStats.ttkValue);
        StatsDiff.kpsDiff           = StatsUtil.CheckDifference(kpsStat, previousGameStats.kpsValue);
        StatsDiff.bestStreakDiff    = StatsUtil.CheckDifference(bestStreakStat, previousGameStats.bestStreakValue);
        StatsDiff.targetsTotalDiff  = StatsUtil.CheckDifference(targetTotalStat, previousGameStats.targetsTotalValue);
        StatsDiff.targetHitDiff     = StatsUtil.CheckDifference(targetHitStat, previousGameStats.targetsHitValue);
        StatsDiff.targetsMissesDiff = StatsUtil.CheckDifference(targetMissesStat, previousGameStats.targetsMissesValue);

        StatsDiff.scoreDiffPercent         = StatsUtil.CheckDifference_Percent(scoreStat, previousGameStats.scoreValue);
        StatsDiff.accuracyDiffPercent      = StatsUtil.CheckDifference_Percent(accuracyStat, previousGameStats.accuracyValue);
        StatsDiff.ttkDiffPercent           = StatsUtil.CheckDifference_Percent(ttkStat, previousGameStats.ttkValue);
        StatsDiff.kpsDiffPercent           = StatsUtil.CheckDifference_Percent(kpsStat, previousGameStats.kpsValue);
        StatsDiff.bestStreakDiffPercent    = StatsUtil.CheckDifference_Percent(bestStreakStat, previousGameStats.bestStreakValue);
        StatsDiff.targetsTotalDiffPercent  = StatsUtil.CheckDifference_Percent(targetTotalStat, previousGameStats.targetsTotalValue);
        StatsDiff.targetHitDiffPercent     = StatsUtil.CheckDifference_Percent(targetHitStat, previousGameStats.targetsHitValue);
        StatsDiff.targetsMissesDiffPercent = StatsUtil.CheckDifference_Percent(targetMissesStat, previousGameStats.targetsMissesValue);
        
        StatsDiff.scoreDiffSymbol         = StatsUtil.CheckDifference_Symbol(scoreStat, previousGameStats.scoreValue);
        StatsDiff.accuracyDiffSymbol      = StatsUtil.CheckDifference_Symbol(accuracyStat, previousGameStats.accuracyValue);
        StatsDiff.ttkDiffSymbol           = StatsUtil.CheckDifference_Symbol(ttkStat, previousGameStats.ttkValue);
        StatsDiff.kpsDiffSymbol           = StatsUtil.CheckDifference_Symbol(kpsStat, previousGameStats.kpsValue);
        StatsDiff.bestStreakDiffSymbol    = StatsUtil.CheckDifference_Symbol(bestStreakStat, previousGameStats.bestStreakValue);
        StatsDiff.targetsTotalDiffSymbol  = StatsUtil.CheckDifference_Symbol(targetTotalStat, previousGameStats.targetsTotalValue);
        StatsDiff.targetHitDiffSymbol     = StatsUtil.CheckDifference_Symbol(targetHitStat, previousGameStats.targetsHitValue);
        StatsDiff.targetsMissesDiffSymbol = StatsUtil.CheckDifference_Symbol(targetMissesStat, previousGameStats.targetsMissesValue);
    }

    /// <summary>
    /// Compares current vs previous game stats to determine item text, color and background color to be set in 'AfterActionReport', then sets everything.
    /// </summary>
    private static void SetStatsItemsUpDown() {
        stats.scoreItem.SetText($"{StatsUtil.GetItemText(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue)}");
        stats.accuracyItem.SetText($"{StatsUtil.GetItemText(accuracyStat, previousGameStats.accuracyValue, bestGameStats.accuracyValue)}");
        stats.ttkItem.SetText($"{StatsUtil.GetItemText_Flip(ttkStat, previousGameStats.ttkValue, bestGameStats.ttkValue)}");
        stats.kpsItem.SetText($"{StatsUtil.GetItemText(kpsStat, previousGameStats.kpsValue, bestGameStats.kpsValue)}");
        stats.bestStreakItem.SetText($"{StatsUtil.GetItemText(bestStreakStat, previousGameStats.bestStreakValue, bestGameStats.bestStreakValue)}");
        stats.targetsTotalItem.SetText($"{StatsUtil.GetItemText(targetTotalStat, previousGameStats.targetsTotalValue, bestGameStats.targetsTotalValue)}");
        stats.taretsHitItem.SetText($"{StatsUtil.GetItemText(targetHitStat, previousGameStats.targetsHitValue, bestGameStats.targetsHitValue)}");
        stats.targetsMissesItem.SetText($"{StatsUtil.GetItemText_Flip(targetMissesStat, previousGameStats.targetsMissesValue, bestGameStats.targetsMissesValue)}");

        stats.scoreItem.color         = StatsUtil.GetItemColor(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue);
        stats.accuracyItem.color      = StatsUtil.GetItemColor(accuracyStat, previousGameStats.accuracyValue, bestGameStats.accuracyValue);
        stats.ttkItem.color           = StatsUtil.GetItemColor_Flip(ttkStat, previousGameStats.ttkValue, bestGameStats.ttkValue);
        stats.kpsItem.color           = StatsUtil.GetItemColor(kpsStat, previousGameStats.kpsValue, bestGameStats.kpsValue);
        stats.bestStreakItem.color    = StatsUtil.GetItemColor(bestStreakStat, previousGameStats.bestStreakValue, bestGameStats.bestStreakValue);
        stats.targetsTotalItem.color  = StatsUtil.GetItemColor(targetTotalStat, previousGameStats.targetsTotalValue, bestGameStats.targetsTotalValue);
        stats.taretsHitItem.color     = StatsUtil.GetItemColor(targetHitStat, previousGameStats.targetsHitValue, bestGameStats.targetsHitValue);
        stats.targetsMissesItem.color = StatsUtil.GetItemColor_Flip(targetMissesStat, previousGameStats.targetsMissesValue, bestGameStats.targetsMissesValue);

        SaveExtraStatsBackgrounds();

        if (showBackgrounds) { SetExtraStatsBackgrounds(); }
    }

    /// <summary>
    /// Sets all items in 'AfterActionReport' neutral text and color.
    /// </summary>
    private static void SetStatsNeutralItems() {
        stats.accuracyItem.SetText($"{StatsUtil.itemNeutral}");
        stats.ttkItem.SetText($"{StatsUtil.itemNeutral}");
        stats.kpsItem.SetText($"{StatsUtil.itemNeutral}");
        stats.bestStreakItem.SetText($"{StatsUtil.itemNeutral}");
        stats.targetsTotalItem.SetText($"{StatsUtil.itemNeutral}");
        stats.taretsHitItem.SetText($"{StatsUtil.itemNeutral}");
        stats.targetsMissesItem.SetText($"{StatsUtil.itemNeutral}");

        stats.accuracyItem.color      = StatsUtil.itemColorGrey;
        stats.ttkItem.color           = StatsUtil.itemColorGrey;
        stats.kpsItem.color           = StatsUtil.itemColorGrey;
        stats.bestStreakItem.color    = StatsUtil.itemColorGrey;
        stats.targetsTotalItem.color  = StatsUtil.itemColorGrey;
        stats.taretsHitItem.color     = StatsUtil.itemColorGrey;
        stats.targetsMissesItem.color = StatsUtil.itemColorGrey;
    }

    /// <summary>
    /// Compares current game stats against saved best stats (if file exists), then sets each new/same best stat in 'ExtraStats' panel.
    /// </summary>
    public static void SetNewBestGameStats() {
        // If saved best stats file exists, compare new to old and set each best stat.
        if (bestGameStats != null) {
            if (StatsUtil.CheckHighestStatValue(scoreStat, bestGameStats.scoreValue)) { bestGameStats.scoreValue = scoreStat; }
            if (StatsUtil.CheckHighestStatValue(accuracyStat, bestGameStats.accuracyValue)) { bestGameStats.accuracyValue = accuracyStat; }
            if (StatsUtil.CheckHighestStatValue_Flip(ttkStat, bestGameStats.ttkValue)) { bestGameStats.ttkValue = ttkStat; }
            if (StatsUtil.CheckHighestStatValue(kpsStat, bestGameStats.kpsValue)) { bestGameStats.kpsValue = kpsStat; }
            if (StatsUtil.CheckHighestStatValue(bestStreakStat, bestGameStats.bestStreakValue)) { bestGameStats.bestStreakValue = bestStreakStat; }
            if (StatsUtil.CheckHighestStatValue(targetTotalStat, bestGameStats.targetsTotalValue)) { bestGameStats.targetsTotalValue = targetTotalStat; }
            if (StatsUtil.CheckHighestStatValue(targetHitStat, bestGameStats.targetsHitValue)) { bestGameStats.targetsHitValue = targetHitStat; }
            if (StatsUtil.CheckHighestStatValue_Flip(targetMissesStat, bestGameStats.targetsMissesValue)) { bestGameStats.targetsMissesValue = targetMissesStat; }

            // Set best stats text.
            stats.scoreTextBest.SetText($"{string.Format("{0:0,.0}K", bestGameStats.scoreValue)}");
            stats.accuracyTextBest.SetText($"{bestGameStats.accuracyValue}%");
            stats.ttkTextBest.SetText($"{string.Format("{0:n0}", bestGameStats.ttkValue)}ms");
            stats.kpsTextBest.SetText(string.Format("{0:0.00}/s", bestGameStats.kpsValue));
            stats.bestStreakTextBest.SetText($"{string.Format("{0:n0}", bestGameStats.bestStreakValue)}");
            stats.targetsTotalTextBest.SetText($"{bestGameStats.targetsTotalValue}");
            stats.taretsHitTextBest.SetText($"{bestGameStats.targetsHitValue}");
            stats.targetsMissesTextBest.SetText($"{bestGameStats.targetsMissesValue}");

            // Save all best stats back to file.
            StatsJsonSaveSystem.SaveBestGameStatsData(gamemodeStat, bestGameStats.scoreValue, bestGameStats.accuracyValue, bestGameStats.ttkValue, bestGameStats.kpsValue, bestGameStats.bestStreakValue, bestGameStats.targetsTotalValue, bestGameStats.targetsHitValue, bestGameStats.targetsMissesValue);
        } else {
            // If best stats file doesnt exist, save current run stats to new best file.
            StatsJsonSaveSystem.SaveBestGameStatsData(gamemodeStat, scoreStat, accuracyStat, ttkStat, kpsStat, bestStreakStat, targetTotalStat, targetHitStat, targetMissesStat);
        }
    }

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

    public static void SetExtraStatsBackgrounds() {
        stats.newHighscoreEffectText.transform.parent.gameObject.GetComponent<Image>().color = backgroundsSaves[0];
        stats.accuracyItem.transform.parent.gameObject.GetComponent<Image>().color           = backgroundsSaves[1];
        stats.ttkItem.transform.parent.gameObject.GetComponent<Image>().color                = backgroundsSaves[2];
        stats.kpsItem.transform.parent.gameObject.GetComponent<Image>().color                = backgroundsSaves[3];
        stats.bestStreakItem.transform.parent.gameObject.GetComponent<Image>().color         = backgroundsSaves[4];
        stats.targetsTotalItem.transform.parent.gameObject.GetComponent<Image>().color       = backgroundsSaves[5];
        stats.taretsHitItem.transform.parent.gameObject.GetComponent<Image>().color          = backgroundsSaves[6];
        stats.targetsMissesItem.transform.parent.gameObject.GetComponent<Image>().color      = backgroundsSaves[7];
    }

    public static void ClearExtraStatsBackgrounds() {
        stats.newHighscoreEffectText.transform.parent.gameObject.GetComponent<Image>().color = StatsUtil.clearBackgroundLight;
        stats.accuracyItem.transform.parent.gameObject.GetComponent<Image>().color           = StatsUtil.clearBackgroundLight;
        stats.ttkItem.transform.parent.gameObject.GetComponent<Image>().color                = StatsUtil.clearBackgroundDark;
        stats.kpsItem.transform.parent.gameObject.GetComponent<Image>().color                = StatsUtil.clearBackgroundLight;
        stats.bestStreakItem.transform.parent.gameObject.GetComponent<Image>().color         = StatsUtil.clearBackgroundDark;
        stats.targetsTotalItem.transform.parent.gameObject.GetComponent<Image>().color       = StatsUtil.clearBackgroundLight;
        stats.taretsHitItem.transform.parent.gameObject.GetComponent<Image>().color          = StatsUtil.clearBackgroundDark;
        stats.targetsMissesItem.transform.parent.gameObject.GetComponent<Image>().color      = StatsUtil.clearBackgroundLight;
    }

    /// <summary>
    /// Show 'ExtraStats' panel;
    /// </summary>
    public static void ShowExtraStatsPanel() { stats.extraStatsPanel.SetActive(true); }
    /// <summary>
    /// Hides 'ExtraStats' panel;
    /// </summary>
    public static void HideExtraStatsPanel() { stats.extraStatsPanel.SetActive(false); }
    /// <summary>
    /// Resets AAR scrollview to top.
    /// </summary>
    public static void ResetAARScrollView() { ScrollRectExtension.ScrollToTop(stats.aarScrollView.GetComponent<ScrollRect>()); }
}
