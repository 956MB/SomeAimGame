using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
    public TMP_Text timeText, scoreText, scoreBonusText, accuracyText, streakText, ttkText, kpsText, preTipText, reactionTimeText;
    public static bool UIHidden, triggerRestart;
    public GameObject UICanvas, newUICanvas, newTimerCanvasImage;
    Coroutine timerCoroutine;
    Coroutine spawnScatterCoroutine;

    public int scoreUp, scoreDown, followScoreUp, followScoreDown;
    public static int timeCount, timeStart;
    public static int accuracy, streakCurrent, streakBest, scoreNum, reactionTime, newTime;
    public static List<int> reactionTimeList;

    private static Color32 hitColor   = new Color32(0, 255, 0, 255);
    private static Color32 missColor  = new Color32(255, 0, 0, 255);
    private static Color32 bonusColor = new Color32(255, 209, 0, 255);

    private static GameUI gameUI;
    void Awake() {
        gameUI = this;
        UIHidden = false;
        triggerRestart = false;
        streakCurrent = streakBest = 0;
    }

    void Start() {
        //spawnScatterCoroutine = continuousScatterSpawn();
        //timerCoroutine = startGameTimerDown();
        reactionTime = 0;
        reactionTimeList = new List<int>();

        //yield return StartCoroutine(spawnScatterCoroutine);
        //yield return StartCoroutine(timerCoroutine);
        SettingsPanel.CloseAfterActionReport();
        SettingsPanel.afterActionReportOpen = false;
        SettingsPanel.CloseSettingsPanel();
        SettingsPanel.settingsOpen = false;

        SpawnTargets.InitSpawnTargets();
        // Load all stats objects at start.
        StatsManager.LoadOldHighscore();
        StatsManager.LoadPreviousGameStats();
        StatsManager.LoadBestGameStats();

        // Set game timer to saved 'ExtraSaveSystem' time, if exists.
        timeCount = ExtraSaveSystem.InitGameTimer();
        timeStart = timeCount;

        //if (SpawnTargets.gamemode == "Gamemode-Scatter") {
        //    spawnScatterCoroutine = StartCoroutine(continuousScatterSpawn());
        //}
        //timerCoroutine = StartCoroutine(startGameTimerDown());
        //StartCoroutine(startGameTimer());
        if (!ExtraSettings.hideUI) {
            HideUI();
        }
        StartGame();
    }

    /// <summary>
    /// Starts game timer Coroutine, and starts 'continuousScatterSpawn' if gamemode 'Gamemode-Scatter'.
    /// </summary>
    public static void StartGame() {
        //gameUI.preTipText.SetText("''R'' to restart");
        if (SpawnTargets.gamemode == "Gamemode-Scatter") {
            gameUI.spawnScatterCoroutine = gameUI.StartCoroutine(ContinuousScatterSpawn());
        }
        gameUI.timerCoroutine = gameUI.StartCoroutine(StartGameTimerDown());
    }

    /// <summary>
    /// Coroutine to continuously spawn targets if gamemode "Gamemode-Scatter".
    /// </summary>
    /// <returns></returns>
    public static IEnumerator ContinuousScatterSpawn() {
        while (true) {
            //Debug.Log("continuousScatterSpawn called??? -----------------");
            if (!MouseLook.settingsOpen) {
                SpawnTargets.CheckScatterSpawns();
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 0.45f));
            //yield return new WaitForSeconds(UnityEngine.Random.Range(0.6f, 0.85f));
        }
    }

    /// <summary>
    /// Coroutine to start game timer countdown.
    /// </summary>
    /// <returns></returns>
    public static IEnumerator StartGameTimerDown() {
        while (true) {
            // Only count timer down if settings panel not open (essentially paused).
            if (!MouseLook.settingsOpen) {
                timeCount -= 1;
            }
            // Disable bonus score UI text if previously set active.
            //if (gameUI.scoreBonusText.gameObject.activeSelf == true) {
            //    gameUI.scoreBonusText.gameObject.SetActive(false);
            //}

            // Format time in seconds (120) to formatted time string (02:00)
            TimeSpan t = TimeSpan.FromSeconds(timeCount);
            string timeFormatted = ReturnTimerString(t);

            // Break out of timer if <= -1.
            if (timeCount > -1) {
                // Set timer color to red if time hits 10 seconds.
                if (timeCount == 10) {
                    gameUI.timeText.color = missColor;
                    //gameUI.newTimerCanvasImage.GetComponent<Image>().color = new Color32(255, 0, 0, 20);
                } else if (timeCount == 0) {
                    // Wiggle timer text, set all stats in 'AfterActionReport', then stop timer.
                    TextEffects.WiggleText(gameUI.timeText, 0.5f, 55);
                    StatsManager.CheckAndSetAllStats();
                    //StatsManager.setAfterActionStats();
                    //SettingsPanel.ToggleAfterActionReportPanel();
                    StopEverything();
                }
            } else {
                yield break;
            }

            gameUI.timeText.SetText($"{timeFormatted}");
            SetTTKText();
            SetKPSText();
            yield return new WaitForSeconds(1f);
        }
        //Debug.Log($"coroutine ended?? {timeCount}");
        //triggerRestart = false;
        //restartGame();
    }

    /// <summary>
    /// Starts game timer up if timer set to "∞" (currently not used).
    /// </summary>
    /// <returns></returns>
    public static IEnumerator StartGameTimerUp() {
        for (int i = 0; i < 86400; i++) {
            TimeSpan t = TimeSpan.FromSeconds(timeCount);
            string timeFormatted = ReturnTimerString(t);

            gameUI.timeText.SetText($"{timeFormatted}");
            timeCount += 1;
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Returns appropriate formatted timestring based on given timespan.
    /// </summary>
    /// <param name="timerTimespan"></param>
    /// <returns></returns>
    public static string ReturnTimerString(TimeSpan timerTimespan) {
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

    /// <summary>
    /// Increases score and sets score text/color, then checks against current best streak.
    /// </summary>
    public static void IncreaseScore() {
        scoreNum += gameUI.scoreUp;
        gameUI.scoreText.SetText($"{string.Format("{0:n0}", scoreNum)}");
        gameUI.scoreText.color = hitColor;
        CheckBestStreak(1);
    }

    /// <summary>
    /// Increases score with bonus value and sets score text/color, then checks against current best streak.
    /// </summary>
    public static void IncreaseScore_Bonus() {
        scoreNum += gameUI.scoreUp * 5;
        gameUI.scoreText.color = bonusColor;
        CheckBestStreak(1);
    }

    /// <summary>
    /// Decreases score and sets score text/color, then resets current streak.
    /// </summary>
    public static void DecreaseScore() {
        scoreNum -= gameUI.scoreDown;
        gameUI.scoreText.SetText($"{string.Format("{0:n0}", scoreNum)}");
        gameUI.scoreText.color = missColor;
        CheckBestStreak(0);
        ResetCurrentStreak();
    }

    /// <summary>
    /// Increases score with follow value and sets score text/color, then checks against current best follow streak.
    /// </summary>
    public static void IncreaseScore_Follow() {
        scoreNum += gameUI.followScoreUp;
        gameUI.scoreText.SetText($"{string.Format("{0:n0}", scoreNum)}");
        gameUI.scoreText.color = hitColor;
        CheckBestStreak(6);
    }

    /// <summary>
    /// Decreases follow score and sets score text/color, then resets current best streak.
    /// </summary>
    public static void DecreaseScore_Follow() {
        scoreNum -= gameUI.followScoreDown;
        gameUI.scoreText.SetText($"{string.Format("{0:n0}", scoreNum)}");
        gameUI.scoreText.color = missColor;
        CheckBestStreak(0);
        ResetCurrentStreak();
    }

    /// <summary>
    /// Updates current accuracy score based on supplied 'shotsHit' value and 'shotsTaken' value, then sets accuracy text.
    /// </summary>
    /// <param name="shotsHit"></param>
    /// <param name="shotsTaken"></param>
    public static void UpdateAccuracy(int shotsHit, int shotsTaken) {
        //Debug.Log($"hit:{shotsHit} taken:{shotsTaken}");
        accuracy = (100 * shotsHit) / shotsTaken;
        gameUI.accuracyText.SetText($"{accuracy}%");
    }

    /// <summary>
    /// Updates current reaction time score based on time between one hit target and the next, then adds reaction time (ms) to list.
    /// </summary>
    public static void UpdateReactionTime() {
        float currentMs = Time.time * 1000;
        newTime = (int)currentMs - reactionTime;
        //gameUI.reactionTimeText.SetText($"{newTime}ms");

        reactionTime = (int)currentMs;
        reactionTimeList.Add(newTime);
    }

    /// <summary>
    /// Resets current hit target streak.
    /// </summary>
    private static void ResetCurrentStreak() {
        streakCurrent = 0;
        //setStreakText();
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
    /// Sets current/best streak text in UI (if exists).
    /// </summary>
    private static void SetStreakText() { gameUI.streakText.SetText($"{streakCurrent} / {streakBest}"); }
    /// <summary>
    /// Sets time to kill (TTK) text in UI (if exists).
    /// </summary>
    private static void SetTTKText() { gameUI.ttkText.SetText($"{newTime}"); }
    /// <summary>
    /// Sets kills per second (KPS) text in UI (if exists).
    /// </summary>
    private static void SetKPSText() { gameUI.kpsText.SetText($"{string.Format("{0:0.00}", (double)SpawnTargets.shotsHit / timeStart)}"); }
    //private static void SetKPSText() { gameUI.kpsText.SetText($"{(double)SpawnTargets.shotsHit / timeStart}"); }

    /// <summary>
    /// Restarts game with current supplied gamemode and resets all game values like timer, score and accuracy. Then targets are reset and scene reloaded.
    /// </summary>
    /// <param name="newGamemode"></param>
    public static void RestartGame(string newGamemode) {
        gameUI.StopCoroutine(gameUI.timerCoroutine);
        if (SpawnTargets.gamemode == "Gamemode-Scatter") {
            gameUI.StopCoroutine(gameUI.spawnScatterCoroutine);
        }

        // Reset important game values.
        timeCount = timeStart;
        scoreNum  = 0;
        accuracy  = 100;

        // reset targets and reload scene.
        SpawnTargets.ResetSpawnTargets();
        //resetUIText();
        ReloadScene();
    }

    /// <summary>
    /// Resets all game UI elements text/color.
    /// </summary>
    public static void ResetUIElements() {
        gameUI.scoreText.SetText($"{string.Format("{0:n0}", scoreNum)}");
        gameUI.scoreText.color = Color.white;
        gameUI.accuracyText.SetText($"{accuracy}%");
        gameUI.timeText.SetText($"00:{timeCount}");
        gameUI.timeText.color = Color.white;
    }

    /// <summary>
    /// Stops all currently running timer/path coroutines and clears/destroys target objects.
    /// </summary>
    public static void StopEverything() {
        //gameUI.StopCoroutine(gameUI.timerCoroutine);
        if (SpawnTargets.gamemode == "Gamemode-Scatter") {
            gameUI.StopCoroutine(gameUI.spawnScatterCoroutine);
        } else if (SpawnTargets.gamemode == "Gamemode-Follow") {
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
        // TODO: Maybe save mouse look position and load it on scene reload to reduce jolt back to the center.
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    /// <summary>
    /// Shows UI if hidden and hides UI is shown (toggle), then saves shown/hidden value.
    /// </summary>
    public static void ToggleWorldUI() {
        if (UIHidden) {
            ShowUI();
            ExtraSettings.SaveHideUI(true);
        } else {
            HideUI();
            ExtraSettings.SaveHideUI(false);
        }
    }

    /// <summary>
    /// Hides UI holding timer, score and accuracy etc.
    /// </summary>
    public static void HideUI() {
        //Debug.Log("hideUI");
        //gameUI.UICanvas.layer = 1;
        gameUI.newUICanvas.SetActive(false);
        UIHidden = true;
    }
    /// <summary>
    /// Shows UI holding timer, score and accuracy etc.
    /// </summary>
    public static void ShowUI() {
        //Debug.Log("showUI");
        //gameUI.UICanvas.layer = 15;
        gameUI.newUICanvas.SetActive(true);
        UIHidden = false;
    }
}
