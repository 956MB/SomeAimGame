using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
    public TMP_Text timeText, scoreText, scoreBonusText, accuracyText, streakText, ttkText, kpsText, preTipText, reactionTimeText;
    public static bool UIHidden;
    public static bool coroutinesRunning = false;
    public static bool statsLoaded       = false;
    public static string timeFormatted;
    public GameObject UICanvas, widgetsUICanvas, newTimerCanvasImage;
    Coroutine timerCoroutine, spawnScatterCoroutine;

    public int scoreUp, scoreDown, followScoreUp, followScoreDown;
    public static int timeCount, timeStart;
    public static int accuracy, streakCurrent, streakBest, scoreNum, reactionTime, newTime;
    public static List<int> reactionTimeList;

    private static WaitForSeconds timerDelay   = new WaitForSeconds(1f);
    private static WaitForSeconds scatterDelay = new WaitForSeconds(0.30f);

    private static GameUI gameUI;
    void Awake() {
        gameUI        = this;
        UIHidden      = false;
        streakCurrent = streakBest = 0;
    }

    void Start() {
        GameStartActions();
    }

    public static void GameStartActions() {
        reactionTime     = 0;
        reactionTimeList = new List<int>();

        SettingsPanel.CloseAfterActionReport();
        SettingsPanel.CloseSettingsPanel();
        SettingsPanel.afterActionReportOpen = false;
        SettingsPanel.settingsOpen          = false;

        SpawnTargets.InitSpawnTargets();

        // Load all stats objects at start.
        if (!statsLoaded) {
            StatsManager.LoadOldHighscore();
            StatsManager.LoadPreviousGameStats();
            StatsManager.LoadBestGameStats();

            statsLoaded = true;
        }

        // Set game timer to saved 'ExtraSaveSystem' time, if exists.
        timeCount     = ExtraSaveSystem.InitGameTimer();
        timeStart     = timeCount;
        timeFormatted = ReturnTimerString(timeCount);

        SetInitialWidgetValues();

        if (!ExtraSettings.hideUI) { HideWidgetsUI(); }

        StartGameCoroutines();
    }

    /// <summary>
    /// Starts game timer Coroutine, and starts 'continuousScatterSpawn' if gamemode 'Gamemode-Scatter'.
    /// </summary>
    public static void StartGameCoroutines() {
        if (SpawnTargets.gamemode == Gamemode.Scatter) { gameUI.spawnScatterCoroutine = gameUI.StartCoroutine(ContinuousScatterSpawn()); }
        gameUI.timerCoroutine = gameUI.StartCoroutine(StartGameTimerDown());

        coroutinesRunning = true;
    }

    /// <summary>
    /// Coroutine to continuously spawn targets if gamemode "Gamemode-Scatter". [EVENT]
    /// </summary>
    /// <returns></returns>
    public static IEnumerator ContinuousScatterSpawn() {
        while (true) {
            if (!MouseLook.settingsOpen) { SpawnTargets.CheckScatterSpawns(); }

            // EVENT:: for scatter game timer tick
            //if (DevEventHandler.eventsOn) { DevEventHandler.CreateGamemodeEvent($"{I18nTextTranslator.SetTranslatedText("eventgamemodescattertick")}"); }

            yield return scatterDelay;
        }
    }

    /// <summary>
    /// Coroutine to start game timer countdown. [EVENT]
    /// </summary>
    /// <returns></returns>
    public static IEnumerator StartGameTimerDown() {
        while (true) {
            // Only count timer down if settings panel not open (essentially paused).
            if (!MouseLook.settingsOpen) {
                timeCount -= 1;
            
                // Format time in seconds (120) to formatted time string (02:00)
                timeFormatted = ReturnTimerString(timeCount);
            }

            // Break out of timer if <= -1.
            if (timeCount > -1) {
                // Set timer color to red if time hits 10 seconds.
                if (timeCount == 10) {
                    if (WidgetSettings.showTime) { gameUI.timeText.color = InterfaceColors.widgetsMissColor; }
                } else if (timeCount == 0) {
                    // Wiggle timer text, set all stats in 'AfterActionReport', then stop timer.
                    if (WidgetSettings.showTime) { TextEffects.WiggleText(gameUI.timeText, 0.5f, 55); }
                    StatsManager.CheckAndSetAllStats();
                    StopEverything();
                }
            } else {
                yield break;
            }

            SetTimeText();
            SetTTKText();
            SetKPSText();

            // EVENT:: for normal game timer tick
            //if (DevEventHandler.eventsOn) { DevEventHandler.CreateTimeEvent($"{I18nTextTranslator.SetTranslatedText("eventtimerticknormal")}"); }

            yield return timerDelay;
        }
    }

    /// <summary>
    /// Starts game timer up if timer set to "∞" (currently not used).
    /// </summary>
    /// <returns></returns>
    public static IEnumerator StartGameTimerUp() {
        for (int i = 0; i < 86400; i++) {
            timeFormatted = ReturnTimerString(timeCount);

            SetTimeText();
            timeCount += 1;

            // EVENT:: for up game timer tick
            //if (DevEventHandler.eventsOn) { DevEventHandler.CreateTimeEvent($"{I18nTextTranslator.SetTranslatedText("eventtimertickup")}"); }

            yield return timerDelay;
        }
    }

    /// <summary>
    /// Returns appropriate formatted timestring based on given timespan.
    /// </summary>
    /// <param name="timerTimespan"></param>
    /// <returns></returns>
    public static string ReturnTimerString(int timeCount) {
        TimeSpan timerTimespan = TimeSpan.FromSeconds(timeCount);
        string formatted;

        // If timespan not in the hours
        if (timerTimespan.Hours == 0) {
            formatted = string.Format("{0:D2}:{1:D2}",
                timerTimespan.Minutes,
                timerTimespan.Seconds);
        } else {
            formatted = string.Format("{0:D2}:{1:D2}:{2:D2}",
                timerTimespan.Hours,
                timerTimespan.Minutes,
                timerTimespan.Seconds);
        }

        return formatted;
    }

    public static void SetInitialWidgetValues() {
        if (WidgetSettings.showTime) { gameUI.timeText.color = InterfaceColors.widgetsNeutralCOlor; }
        if (WidgetSettings.showScore) { gameUI.scoreText.SetText("0"); gameUI.scoreText.color = InterfaceColors.widgetsNeutralCOlor; }
        if (WidgetSettings.showAccuracy) { gameUI.accuracyText.SetText("100%"); gameUI.accuracyText.color = InterfaceColors.widgetsNeutralCOlor; }
        if (WidgetSettings.showStreak) { gameUI.streakText.SetText("0 / 0"); gameUI.streakText.color = InterfaceColors.widgetsNeutralCOlor; }
        if (WidgetSettings.showTTK) { gameUI.ttkText.SetText("0"); gameUI.ttkText.color = InterfaceColors.widgetsNeutralCOlor; }
        if (WidgetSettings.showKPS) { gameUI.kpsText.SetText("0"); gameUI.kpsText.color = InterfaceColors.widgetsNeutralCOlor; }
    }

    /// <summary>
    /// Increases score and sets score text/color, then checks against current best streak.
    /// </summary>
    public static void IncreaseScore() {
        scoreNum += gameUI.scoreUp;
        if (WidgetSettings.showScore) {
            gameUI.scoreText.SetText($"{string.Format("{0:n0}", scoreNum)}");
            gameUI.scoreText.color = InterfaceColors.widgetsHitColor;
        }
        CheckBestStreak(1);
    }

    /// <summary>
    /// Increases score with bonus value and sets score text/color, then checks against current best streak.
    /// </summary>
    public static void IncreaseScore_Bonus() {
        scoreNum += gameUI.scoreUp * 5;
        if (WidgetSettings.showScore) {
            gameUI.scoreText.color = InterfaceColors.widgetsBonusColor;
        }
        CheckBestStreak(1);
    }

    /// <summary>
    /// Decreases score and sets score text/color, then resets current streak.
    /// </summary>
    public static void DecreaseScore() {
        scoreNum -= gameUI.scoreDown;
        if (WidgetSettings.showScore) {
            gameUI.scoreText.SetText($"{string.Format("{0:n0}", scoreNum)}");
            gameUI.scoreText.color = InterfaceColors.widgetsMissColor;
        }
        CheckBestStreak(0);
        ResetCurrentStreak();
    }

    /// <summary>
    /// Increases score with follow value and sets score text/color, then checks against current best follow streak.
    /// </summary>
    public static void IncreaseScore_Follow() {
        scoreNum += gameUI.followScoreUp;
        if (WidgetSettings.showScore) {
            gameUI.scoreText.SetText($"{string.Format("{0:n0}", scoreNum)}");
            gameUI.scoreText.color = InterfaceColors.widgetsHitColor;
        }
        CheckBestStreak(6);
    }

    /// <summary>
    /// Decreases follow score and sets score text/color, then resets current best streak.
    /// </summary>
    public static void DecreaseScore_Follow() {
        scoreNum -= gameUI.followScoreDown;
        if (WidgetSettings.showScore) {
            gameUI.scoreText.SetText($"{string.Format("{0:n0}", scoreNum)}");
            gameUI.scoreText.color = InterfaceColors.widgetsMissColor;
        }
        CheckBestStreak(0);
        ResetCurrentStreak();
    }

    /// <summary>
    /// Updates current accuracy score based on supplied 'shotsHit' value and 'shotsTaken' value, then sets accuracy text. [EVENT]
    /// </summary>
    /// <param name="shotsHit"></param>
    /// <param name="shotsTaken"></param>
    public static void UpdateAccuracy(int shotsHit, int shotsTaken) {
        accuracy = (100 * shotsHit) / shotsTaken;
        if (WidgetSettings.showAccuracy) {
            gameUI.accuracyText.SetText($"{accuracy}%");
        }

        // EVENT:: for update reaction time
        //if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfaceaccuracyupdate")}"); }
    }

    /// <summary>
    /// Updates current reaction time score based on time between one hit target and the next, then adds reaction time (ms) to list. [EVENT]
    /// </summary>
    public static void UpdateReactionTime() {
        float currentMs = Time.time * 1000;
        newTime = (int)currentMs - reactionTime;

        reactionTime = (int)currentMs;
        reactionTimeList.Add(newTime);

        // EVENT:: for update reaction time
        //if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacereactiontimeupdate")}"); }
    }

    /// <summary>
    /// Resets current hit target streak. [EVENT]
    /// </summary>
    private static void ResetCurrentStreak() {
        streakCurrent = 0;

        // EVENT:: for reset streak
        //if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacestreakreset")}"); }
    }

    /// <summary>
    /// Checks if current hit target streak (streakNum) is higher than best streak (streakCurrent), then sets current/best streak text in UI (if exists).
    /// </summary>
    /// <param name="streakNum"></param>
    private static void CheckBestStreak(int streakNum) {
        streakCurrent += streakNum;

        if (streakCurrent > streakBest) {
            streakBest = streakCurrent;
        }
        SetStreakText();
    }

    /// <summary>
    /// Sets current/best streak text in UI (if exists). [EVENT]
    /// </summary>
    private static void SetTimeText() {
        if (WidgetSettings.showTime) {
            gameUI.timeText.SetText($"{timeFormatted}");
        }

        // EVENT:: for set streak widget text
        //if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacestreakset")} ({streakCurrent} / {streakBest})"); }
    }

    /// <summary>
    /// Sets current/best streak text in UI (if exists). [EVENT]
    /// </summary>
    private static void SetStreakText() {
        if (WidgetSettings.showStreak) {
            gameUI.streakText.SetText($"{streakCurrent} / {streakBest}");
        }

        // EVENT:: for set streak widget text
        //if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacestreakset")} ({streakCurrent} / {streakBest})"); }
    }

    /// <summary>
    /// Sets time to kill (TTK) text in UI (if exists). [EVENT]
    /// </summary>
    private static void SetTTKText() {
        if (WidgetSettings.showTTK) {
            gameUI.ttkText.SetText($"{newTime}");
        }

        // EVENT:: for set TTK widget text
        //if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacettkset")} ({newTime})"); }
    }

    /// <summary>
    /// Sets kills per second (KPS) text in UI (if exists). [EVENT]
    /// </summary>
    private static void SetKPSText() {
        if (WidgetSettings.showKPS) {
            gameUI.kpsText.SetText($"{string.Format("{0:0.00}", (double)SpawnTargets.shotsHit / timeStart)}");
        }

        // EVENT:: for set KPS widget text
        //if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacekpsset")} ({string.Format("{0:0.00}", (double)SpawnTargets.shotsHit / timeStart)})"); }
    }

    private static void ResetAllStatValues() {
        timeCount     = timeStart;
        scoreNum      = 0;
        accuracy      = 100;
        streakCurrent = 0;
        streakBest    = 0;
        newTime       = 0;
    }

    /// <summary>
    /// Restarts game with current supplied gamemode and resets all game values like timer, score and accuracy. Then targets are reset and scene reloaded.
    /// </summary>
    /// <param name="newGamemode"></param>
    public static void RestartGame(Gamemode newGamemode) {
        if (coroutinesRunning) {
            gameUI.StopCoroutine(gameUI.timerCoroutine);
            if (SpawnTargets.gamemode == Gamemode.Scatter) {
                gameUI.StopCoroutine(gameUI.spawnScatterCoroutine);
            }

            coroutinesRunning = false;
        }

        // Reset important game values.
        ResetAllStatValues();

        // reset targets and reload scene.
        SpawnTargets.ResetSpawnTargetsValues();

        if (newGamemode != SpawnTargets.gamemode) {
            ReloadScene();
        } else {
            GameStartActions();
        }
    }

    /// <summary>
    /// Resets all game UI elements text/color. [EVENT]
    /// </summary>
    public static void ResetUIElements() {
        if (WidgetSettings.showScore) {
            gameUI.scoreText.SetText($"{string.Format("{0:n0}", scoreNum)}");
            gameUI.scoreText.color = Color.white;
        }
        if (WidgetSettings.showAccuracy) {
            gameUI.accuracyText.SetText($"{accuracy}%");
        }
        if (WidgetSettings.showTime) {
            gameUI.timeText.SetText($"00:{timeCount}");
            gameUI.timeText.color = Color.white;
        }

        // EVENT:: for game UI elements reset
        //if (DevEventHandler.eventsOn) { DevEventHandler.CreateKeybindEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfaceelementsreset")}"); }
    }

    /// <summary>
    /// Stops all currently running timer/path coroutines and clears/destroys target objects.
    /// </summary>
    public static void StopEverything() {
        //gameUI.StopCoroutine(gameUI.timerCoroutine);
        if (SpawnTargets.gamemode == Gamemode.Scatter) {
            gameUI.StopCoroutine(gameUI.spawnScatterCoroutine);
        } else if (SpawnTargets.gamemode == Gamemode.Follow) {
            PathFollower.DestroyPathObj();
        }

        // Clear current target list and destroy spawned targets.
        SpawnTargets.ClearTargetLists();
        SpawnTargets.DestroyTargetObjects();
    }

    /// <summary>
    /// Reloads current scene to 'reset' game.
    /// </summary>
    public static void ReloadScene() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    /// <summary>
    /// Shows UI if hidden and hides UI is shown (toggle), then saves shown/hidden value. [EVENT]
    /// </summary>
    public static void ToggleWidgetsUI() {
        if (UIHidden) {
            ShowWidgetsUI();
            ExtraSettings.SaveHideUI(true);
        } else {
            HideWidgetsUI();
            ExtraSettings.SaveHideUI(false);
        }

        // EVENT:: for toggle world UI keybind pressed
        //DevEventHandler.CheckKeybindEvent($"'Toggle WorldUI' [H] {I18nTextTranslator.SetTranslatedText("eventkeybindpressed")}");
    }

    /// <summary>
    /// Hides UI holding timer, score and accuracy etc. [EVENT]
    /// </summary>
    public static void HideWidgetsUI() {
        gameUI.widgetsUICanvas.SetActive(false);
        UIHidden = true;

        // EVENT:: for game ui hidden
        //if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfacehideui")}"); }
    }
    /// <summary>
    /// Shows UI holding timer, score and accuracy etc. [EVENT]
    /// </summary>
    public static void ShowWidgetsUI() {
        gameUI.widgetsUICanvas.SetActive(true);
        UIHidden = false;

        // EVENT:: for game ui shown
        //if (DevEventHandler.eventsOn) { DevEventHandler.CreateInterfaceEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfaceshowui")}"); }
    }
}
