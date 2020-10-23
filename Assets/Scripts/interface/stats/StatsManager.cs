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
    public GameObject extraStatsPanel;
    public Image highscoreLineTop, highscoreLineBottom;
    public static bool showBackgrounds = true;
    public static bool backgroundsSaved = false;
    private static Color32[] backgroundsSaves;

    private static int highscoreScore;
    private static HighscoreDataSerial highscoreData;
    public static PreviousGameStats previousGameStats;
    public static BestGameStats bestGameStats;

    private static string itemHighscore     = "▲▲";
    private static string itemHighscoreFlip = "▼▼";
    private static string itemUp            = "▲";
    private static string itemDown          = "▼";
    private static string itemNeutral       = "-";
    private static Color32 newHighscoreBackgroundColor = new Color32(255, 209, 0, 55);
    private static Color32 upBackgroundColor           = new Color32(0, 255, 0, 20);
    private static Color32 downBackgroundColor         = new Color32(255, 0, 0, 20);
    private static Color32 neutralBackgroundColor      = new Color32(255, 255, 255, 15);
    private static Color32 itemColorRed       = new Color32(255, 0, 0, 255);
    private static Color32 itemColorGreen     = new Color32(0, 255, 0, 255);
    private static Color32 itemColorGrey      = new Color32(255, 255, 255, 85);
    private static Color32 itemColorHighscore = new Color32(255, 209, 0, 255);
    private static Color32 upLineColor        = new Color32(0, 255, 0, 150);
    private static Color32 downLineColor      = new Color32(255, 0, 0, 150);
    private static Color32 neutralLineColor   = new Color32(255, 255, 255, 35);
    private static Color32 highscoreLineColor = new Color32(255, 209, 0, 255);
    private static Color32 clearBackgroundLight = new Color32(255, 255, 255, 15);
    private static Color32 clearBackgroundDark  = new Color32(0, 0, 0, 0);

    private static string gamemodeStat;
    private static int scoreStat, accuracyStat, ttkStat, bestStreakStat, targetTotalStat, targetHitStat, targetMissesStat;
    private static double kpsStat;

    private static StatsManager stats;
    void Awake() { stats = this; }

    private void Start() {
        //loadHighscore();
        scoreTitleText.enabled = true;
        newHighscoreEffectText.enabled = false;
        backgroundsSaves = new Color32[8];
    }

    /// <summary>
    /// Loads highscore stats of current gamemode into 'highscoreData' object, then sets 'highscoreScore' from highscore stats.
    /// </summary>
    public static void LoadOldHighscore() {
        highscoreData = HighscoreSaveSystem.LoadHighscoreData(SpawnTargets.gamemode.Split('-')[1]);
        if (highscoreData != null) {
            highscoreScore = highscoreData.scoreValue;
        }
    }

    /// <summary>
    /// Loads previous stats of current gamemode into 'previousGameStats' object.
    /// </summary>
    public static void LoadPreviousGameStats() {
        previousGameStats = StatsJsonSaveSystem.LoadLastGameData(SpawnTargets.gamemode.Split('-')[1]);
    }

    /// <summary>
    /// Loads best stats of current gamemode into 'bestGameStats' object.
    /// </summary>
    public static void LoadBestGameStats() {
        bestGameStats = StatsJsonSaveSystem.LoadBestGameStatsData(SpawnTargets.gamemode.Split('-')[1]);
    }

    /// <summary>
    /// Sets current gamemodes stats after finish to be compared.
    /// </summary>
    public static void SetStatValues() {
        gamemodeStat     = CosmeticsSettings.gamemode.Split('-')[1];
        scoreStat        = GameUI.scoreNum;
        accuracyStat     = GameUI.accuracy;
        ttkStat          = GameUI.newTime;
        bestStreakStat   = GameUI.streakBest;
        targetTotalStat  = SpawnTargets.totalCount;
        targetHitStat    = SpawnTargets.shotsHit;
        targetMissesStat = SpawnTargets.shotMisses;
        kpsStat          = (double)targetHitStat / GameUI.timeStart;
        //Debug.Log($"scoreStat?? :: {scoreStat}");
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
                //Debug.Log($"{scoreStat} {previousGameStats.scoreValue} {bestGameStats.scoreValue}");
                stats.highscoreLineTop.color = GetLineColor(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue);
                stats.highscoreLineBottom.color = GetLineColor(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue);
            }
        } else {
            // No saved highscore on first run, sets current games run as new highscore.
            stats.highscoreLineTop.color = neutralLineColor;
            stats.highscoreLineBottom.color = neutralLineColor;
            stats.newHighscoreEffectText.transform.parent.gameObject.GetComponent<Image>().color = neutralBackgroundColor;
            HighscoreSave.SaveNewHighscoreStats(CosmeticsSettings.gamemode.Split('-')[1], scoreStat, accuracyStat, ttkStat, kpsStat, bestStreakStat, targetTotalStat, targetHitStat, targetMissesStat);
        }

        // Saves current games stats as new 'previous' for next run.
        StatsJsonSaveSystem.SavePreviousGameData(gamemodeStat, scoreStat, accuracyStat, ttkStat, kpsStat, bestStreakStat, targetTotalStat, targetHitStat, targetMissesStat);
    }

    /// <summary>
    /// Sets stats in 'AfterActionReport' UI.
    /// </summary>
    public static void SetAfterActionStats() {
        double kps = kpsStat;

        stats.titleText.SetText($"{I18nTextTranslator.SetTranslatedText("afteractionreporttitle")} - {I18nTextTranslator.SetTranslatedText(CosmeticsSettings.gamemode.Split('-')[1])}");
        //stats.showExtraGamemodeText.SetText($"({I18nTextTranslator.SetTranslatedText(CosmeticsSettings.gamemode.Split('-')[1])})");
        //stats.scoreText.SetText($"{string.Format("{0:n0}", scoreStat)}");
        stats.scoreEffectText.SetText($"{string.Format("{0:n0}", scoreStat)}");
        //stats.gamemodeText.SetText($"{CosmeticsSettings.gamemode.Split('-')[1]}");
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
        stats.scoreTitleText.enabled = false;
        stats.newHighscoreEffectText.transform.parent.gameObject.GetComponent<Image>().color = newHighscoreBackgroundColor;
        stats.highscoreLineTop.color = highscoreLineColor;
        stats.highscoreLineBottom.color = highscoreLineColor;
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
        StatsDiff.scoreDiff         = CheckDifference(scoreStat, previousGameStats.scoreValue);
        StatsDiff.accuracyDiff      = CheckDifference(accuracyStat, previousGameStats.accuracyValue);
        StatsDiff.ttkDiff           = CheckDifference(ttkStat, previousGameStats.ttkValue);
        StatsDiff.kpsDiff           = CheckDifference(kpsStat, previousGameStats.kpsValue);
        StatsDiff.bestStreakDiff    = CheckDifference(bestStreakStat, previousGameStats.bestStreakValue);
        StatsDiff.targetsTotalDiff  = CheckDifference(targetTotalStat, previousGameStats.targetsTotalValue);
        StatsDiff.targetHitDiff     = CheckDifference(targetHitStat, previousGameStats.targetsHitValue);
        StatsDiff.targetsMissesDiff = CheckDifference(targetMissesStat, previousGameStats.targetsMissesValue);

        StatsDiff.scoreDiffPercent         = CheckDifference_Percent(scoreStat, previousGameStats.scoreValue);
        StatsDiff.accuracyDiffPercent      = CheckDifference_Percent(accuracyStat, previousGameStats.accuracyValue);
        StatsDiff.ttkDiffPercent           = CheckDifference_Percent(ttkStat, previousGameStats.ttkValue);
        StatsDiff.kpsDiffPercent           = CheckDifference_Percent(kpsStat, previousGameStats.kpsValue);
        StatsDiff.bestStreakDiffPercent    = CheckDifference_Percent(bestStreakStat, previousGameStats.bestStreakValue);
        StatsDiff.targetsTotalDiffPercent  = CheckDifference_Percent(targetTotalStat, previousGameStats.targetsTotalValue);
        StatsDiff.targetHitDiffPercent     = CheckDifference_Percent(targetHitStat, previousGameStats.targetsHitValue);
        StatsDiff.targetsMissesDiffPercent = CheckDifference_Percent(targetMissesStat, previousGameStats.targetsMissesValue);
        
        StatsDiff.scoreDiffSymbol         = CheckDifference_Symbol(scoreStat, previousGameStats.scoreValue);
        StatsDiff.accuracyDiffSymbol      = CheckDifference_Symbol(accuracyStat, previousGameStats.accuracyValue);
        StatsDiff.ttkDiffSymbol           = CheckDifference_Symbol(ttkStat, previousGameStats.ttkValue);
        StatsDiff.kpsDiffSymbol           = CheckDifference_Symbol(kpsStat, previousGameStats.kpsValue);
        StatsDiff.bestStreakDiffSymbol    = CheckDifference_Symbol(bestStreakStat, previousGameStats.bestStreakValue);
        StatsDiff.targetsTotalDiffSymbol  = CheckDifference_Symbol(targetTotalStat, previousGameStats.targetsTotalValue);
        StatsDiff.targetHitDiffSymbol     = CheckDifference_Symbol(targetHitStat, previousGameStats.targetsHitValue);
        StatsDiff.targetsMissesDiffSymbol = CheckDifference_Symbol(targetMissesStat, previousGameStats.targetsMissesValue);
    }

    /// <summary>
    /// Compares current vs previous game stats to determine item text, color and background color to be set in 'AfterActionReport', then sets everything.
    /// </summary>
    private static void SetStatsItemsUpDown() {
        stats.scoreItem.SetText($"{GetItemText(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue)}");
        stats.accuracyItem.SetText($"{GetItemText(accuracyStat, previousGameStats.accuracyValue, bestGameStats.accuracyValue)}");
        stats.ttkItem.SetText($"{GetItemText_Flip(ttkStat, previousGameStats.ttkValue, bestGameStats.ttkValue)}");
        stats.kpsItem.SetText($"{GetItemText(kpsStat, previousGameStats.kpsValue, bestGameStats.kpsValue)}");
        stats.bestStreakItem.SetText($"{GetItemText(bestStreakStat, previousGameStats.bestStreakValue, bestGameStats.bestStreakValue)}");
        stats.targetsTotalItem.SetText($"{GetItemText(targetTotalStat, previousGameStats.targetsTotalValue, bestGameStats.targetsTotalValue)}");
        stats.taretsHitItem.SetText($"{GetItemText(targetHitStat, previousGameStats.targetsHitValue, bestGameStats.targetsHitValue)}");
        stats.targetsMissesItem.SetText($"{GetItemText_Flip(targetMissesStat, previousGameStats.targetsMissesValue, bestGameStats.targetsMissesValue)}");

        stats.scoreItem.color         = GetItemColor(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue);
        stats.accuracyItem.color      = GetItemColor(accuracyStat, previousGameStats.accuracyValue, bestGameStats.accuracyValue);
        stats.ttkItem.color           = GetItemColor_Flip(ttkStat, previousGameStats.ttkValue, bestGameStats.ttkValue);
        stats.kpsItem.color           = GetItemColor(kpsStat, previousGameStats.kpsValue, bestGameStats.kpsValue);
        stats.bestStreakItem.color    = GetItemColor(bestStreakStat, previousGameStats.bestStreakValue, bestGameStats.bestStreakValue);
        stats.targetsTotalItem.color  = GetItemColor(targetTotalStat, previousGameStats.targetsTotalValue, bestGameStats.targetsTotalValue);
        stats.taretsHitItem.color     = GetItemColor(targetHitStat, previousGameStats.targetsHitValue, bestGameStats.targetsHitValue);
        stats.targetsMissesItem.color = GetItemColor_Flip(targetMissesStat, previousGameStats.targetsMissesValue, bestGameStats.targetsMissesValue);

        SaveExtraStatsBackgrounds();

        if (showBackgrounds) { SetExtraStatsBackgrounds(); }
    }

    /// <summary>
    /// Sets all items in 'AfterActionReport' neutral text and color.
    /// </summary>
    private static void SetStatsNeutralItems() {
        stats.accuracyItem.SetText($"{itemNeutral}");
        stats.ttkItem.SetText($"{itemNeutral}");
        stats.kpsItem.SetText($"{itemNeutral}");
        stats.bestStreakItem.SetText($"{itemNeutral}");
        stats.targetsTotalItem.SetText($"{itemNeutral}");
        stats.taretsHitItem.SetText($"{itemNeutral}");
        stats.targetsMissesItem.SetText($"{itemNeutral}");

        stats.accuracyItem.color      = itemColorGrey;
        stats.ttkItem.color           = itemColorGrey;
        stats.kpsItem.color           = itemColorGrey;
        stats.bestStreakItem.color    = itemColorGrey;
        stats.targetsTotalItem.color  = itemColorGrey;
        stats.taretsHitItem.color     = itemColorGrey;
        stats.targetsMissesItem.color = itemColorGrey;
    }

    /// <summary>
    /// Compares current game stats against saved best stats (if file exists), then sets each new/same best stat in 'ExtraStats' panel.
    /// </summary>
    public static void SetNewBestGameStats() {
        // If saved best stats file exists, compare new to old and set each best stat.
        if (bestGameStats != null) {
            if (CheckHighestStatValue(scoreStat, bestGameStats.scoreValue)) { bestGameStats.scoreValue = scoreStat; }
            if (CheckHighestStatValue(accuracyStat, bestGameStats.accuracyValue)) { bestGameStats.accuracyValue = accuracyStat; }
            if (CheckHighestStatValue_Flip(ttkStat, bestGameStats.ttkValue)) { bestGameStats.ttkValue = ttkStat; }
            if (CheckHighestStatValue(kpsStat, bestGameStats.kpsValue)) { bestGameStats.kpsValue = kpsStat; }
            if (CheckHighestStatValue(bestStreakStat, bestGameStats.bestStreakValue)) { bestGameStats.bestStreakValue = bestStreakStat; }
            if (CheckHighestStatValue(targetTotalStat, bestGameStats.targetsTotalValue)) { bestGameStats.targetsTotalValue = targetTotalStat; }
            if (CheckHighestStatValue(targetHitStat, bestGameStats.targetsHitValue)) { bestGameStats.targetsHitValue = targetHitStat; }
            if (CheckHighestStatValue_Flip(targetMissesStat, bestGameStats.targetsMissesValue)) { bestGameStats.targetsMissesValue = targetMissesStat; }

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
        backgroundsSaves[0] = GetItemBackgroundColor(scoreStat, previousGameStats.scoreValue, bestGameStats.scoreValue);
        backgroundsSaves[1] = GetItemBackgroundColor(accuracyStat, previousGameStats.accuracyValue, bestGameStats.accuracyValue);
        backgroundsSaves[2] = GetItemBackgroundColor_Flip(ttkStat, previousGameStats.ttkValue, bestGameStats.ttkValue);
        backgroundsSaves[3] = GetItemBackgroundColor(kpsStat, previousGameStats.kpsValue, bestGameStats.kpsValue);
        backgroundsSaves[4] = GetItemBackgroundColor(bestStreakStat, previousGameStats.bestStreakValue, bestGameStats.bestStreakValue);
        backgroundsSaves[5] = GetItemBackgroundColor(targetTotalStat, previousGameStats.targetsTotalValue, bestGameStats.targetsTotalValue);
        backgroundsSaves[6] = GetItemBackgroundColor(targetHitStat, previousGameStats.targetsHitValue, bestGameStats.targetsHitValue);
        backgroundsSaves[7] = GetItemBackgroundColor_Flip(targetMissesStat, previousGameStats.targetsMissesValue, bestGameStats.targetsMissesValue);

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
        stats.newHighscoreEffectText.transform.parent.gameObject.GetComponent<Image>().color = clearBackgroundLight;
        stats.accuracyItem.transform.parent.gameObject.GetComponent<Image>().color           = clearBackgroundLight;
        stats.ttkItem.transform.parent.gameObject.GetComponent<Image>().color                = clearBackgroundDark;
        stats.kpsItem.transform.parent.gameObject.GetComponent<Image>().color                = clearBackgroundLight;
        stats.bestStreakItem.transform.parent.gameObject.GetComponent<Image>().color         = clearBackgroundDark;
        stats.targetsTotalItem.transform.parent.gameObject.GetComponent<Image>().color       = clearBackgroundLight;
        stats.taretsHitItem.transform.parent.gameObject.GetComponent<Image>().color          = clearBackgroundDark;
        stats.targetsMissesItem.transform.parent.gameObject.GetComponent<Image>().color      = clearBackgroundLight;
    }

    /// <summary>
    /// Show 'ExtraStats' panel;
    /// </summary>
    public static void ShowExtraStatsPanel() { stats.extraStatsPanel.SetActive(true); }
    /// <summary>
    /// Hides 'ExtraStats' panel;
    /// </summary>
    public static void HideExtraStatsPanel() { stats.extraStatsPanel.SetActive(false); }

    // TODO: do summary comments for all these utility methods:

    private static string GetItemText(double newValue, double oldValue, double highscoreValue) {
        if (newValue < oldValue) {
            return itemDown;
        } else if (newValue > oldValue) {
            if (newValue > highscoreValue) {
                return itemHighscore;
            } else {
                return itemUp;
            }
        } else {
            return itemNeutral;
        }
    }
    private static string GetItemText_Flip(double newValue, double oldValue, double highscoreValue) {
        if (newValue > oldValue) {
            return itemUp;
        } else if (newValue < oldValue) {
            if (newValue < highscoreValue) {
                return itemHighscoreFlip;
            } else {
                return itemDown;
            }
        } else {
            return itemNeutral;
        }
    }

    private static Color32 GetItemColor(double newValue, double oldValue, double highscoreValue) {
        if (newValue < oldValue) {
            return itemColorRed;
        } else if (newValue > oldValue) {
            if (newValue > highscoreValue) {
                return itemColorHighscore;
            } else {
                return itemColorGreen;
            }
        } else {
            return itemColorGrey;
        }
    }

    private static Color32 GetLineColor(double newValue, double oldValue, double highscoreValue) {
        if (newValue < oldValue) {
            return downLineColor;
        } else if (newValue > oldValue) {
            if (newValue > highscoreValue) {
                return highscoreLineColor;
            } else {
                return upLineColor;
            }
        } else {
            return neutralLineColor;
        }
    }

    private static Color32 GetItemColor_Flip(double newValue, double oldValue, double highscoreValue) {
        if (newValue < oldValue) {
            if (newValue < highscoreValue) {
                return itemColorHighscore;
            } else {
                return itemColorGreen;
            }
        } else if (newValue > oldValue) {
            return itemColorRed;
        } else {
            return itemColorGrey;
        }
    }

    private static Color32 GetItemBackgroundColor(double newValue, double oldValue, double highscoreValue) {
        if (newValue < oldValue) {
            return downBackgroundColor;
        } else if (newValue > oldValue) {
            if (newValue > highscoreValue) {
                return newHighscoreBackgroundColor;
            } else {
                return upBackgroundColor;
            }
        }

        return neutralBackgroundColor;
    }
    private static Color32 GetItemBackgroundColor_Flip(int newValue, int oldValue, int highscoreValue) {
        if (newValue < oldValue) {
            if (newValue < highscoreValue) {
                return newHighscoreBackgroundColor;
            } else {
                return upBackgroundColor;
            }
        } else if (newValue > oldValue) {
            return downBackgroundColor;
        }

        return neutralBackgroundColor;
    }

    private static bool CheckHighestStatValue(double newValue, double highestValue) {
        if (newValue > highestValue) {
            return true;
        } else {
            return false;
        }
    }
    private static bool CheckHighestStatValue_Flip(int newValue, int highestValue) {
        if (newValue > highestValue) {
            return false;
        } else if (newValue == 0) {
            return false;
        } else {
            return true;
        }
    }

    private static double CheckDifference(double newValue, double oldValue) {
        if (newValue > oldValue) {
            return newValue - oldValue;
        } else if (newValue < oldValue) {
            return oldValue - newValue;
        } else {
            return newValue - oldValue;
        }
    }
    private static double CheckDifference_Percent(double newValue, double oldValue) {
        if (newValue > oldValue) {
            return (int)((newValue - oldValue) * 100) / oldValue;
        } else if (newValue < oldValue) {
            return (int)((oldValue - newValue) * 100) / newValue;
        } else {
            return 0;
        }
    }
    private static string CheckDifference_Symbol(double newValue, double oldValue) {
        if (newValue > oldValue) {
            return "+ ";
        } else if (newValue < oldValue) {
            return "- ";
        } else {
            return "";
        }
    }
}
