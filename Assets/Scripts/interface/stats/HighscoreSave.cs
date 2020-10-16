using UnityEngine;

public class HighscoreSave : MonoBehaviour {
    public static string gamemodeName;
    public static int scoreValue, accuracyValue, ttkValue, bestStreakValue, targetsTotalValue, targetsHitValue, targetsMissesValue;
    public static double kpsValue;

    private static HighscoreSave highscore;
    void Awake() { highscore = this; }

    //public void saveHighscoreStats(string gamemode) { HighscoreSaveSystem.saveHighscore(gamemode); }

    /// <summary>
    /// Save highscore values to 'HighscoreSave' object for reference.
    /// </summary>
    /// <param name="setGamemode"></param>
    /// <param name="setScore"></param>
    /// <param name="setAccuracy"></param>
    /// <param name="setTTK"></param>
    /// <param name="setKPS"></param>
    /// <param name="setBestStreak"></param>
    /// <param name="setTargetsTotal"></param>
    /// <param name="setTargetsHit"></param>
    /// <param name="setTargetsMisses"></param>
    public static void SaveNewHighscoreStats(string setGamemode, int setScore, int setAccuracy, int setTTK, double setKPS, int setBestStreak, int setTargetsTotal, int setTargetsHit, int setTargetsMisses) {
        gamemodeName       = setGamemode;
        scoreValue         = setScore;
        accuracyValue      = setAccuracy;
        ttkValue           = setTTK;
        kpsValue           = setKPS;
        bestStreakValue    = setBestStreak;
        targetsTotalValue  = setTargetsTotal;
        targetsHitValue    = setTargetsHit;
        targetsMissesValue = setTargetsMisses;

        // Save 'HighscoreSave' object to highscore file.
        HighscoreSaveSystem.SaveHighscoreData(setGamemode);

        //highscore.saveHighscoreStats("Grid");
        //Debug.Log("AFTER saveHighscoreStats");
    }
}

