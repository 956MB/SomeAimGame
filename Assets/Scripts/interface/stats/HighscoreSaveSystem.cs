using UnityEngine;

using SomeAimGame.Utilities;

public class HighscoreSaveSystem : MonoBehaviour {

    /// <summary>
    /// Save specified gamemode 'category' to highscore file.
    /// </summary>
    /// <param name="category"></param>
    public static void SaveHighscoreData(string category) {
        HighscoreDataSerial highscoreData = new HighscoreDataSerial();
        SaveLoadUtil.SaveDataSerial("/stats/highscores", $"/{category}.highscore", highscoreData);
    }

    /// <summary>
    /// Load specified gamemode 'category' from highscore file.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public static HighscoreDataSerial LoadHighscoreData(string category) {
        HighscoreDataSerial highscoreData = (HighscoreDataSerial)SaveLoadUtil.LoadDataSerial($"/stats/highscores/{category}.highscore", SaveType.HIGHSCORE);
        return highscoreData;
    }
}
