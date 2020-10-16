
[System.Serializable]
public class HighscoreDataSerial {
    public string gamemodeName;
    public int scoreValue;
    public int accuracyValue;
    public int ttkValue;
    public double kpsValue;
    public int bestStreakValue;
    public int targetsTotalValue;
    public int targetsHitValue;
    public int targetsMissesValue;

    public HighscoreDataSerial() {
        gamemodeName       = HighscoreSave.gamemodeName;
        scoreValue         = HighscoreSave.scoreValue;
        accuracyValue      = HighscoreSave.accuracyValue;
        ttkValue           = HighscoreSave.ttkValue;
        kpsValue           = HighscoreSave.kpsValue;
        bestStreakValue    = HighscoreSave.bestStreakValue;
        targetsTotalValue  = HighscoreSave.targetsTotalValue;
        targetsHitValue    = HighscoreSave.targetsHitValue;
        targetsMissesValue = HighscoreSave.targetsMissesValue;
    }
}
