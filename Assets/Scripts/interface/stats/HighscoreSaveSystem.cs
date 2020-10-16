using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HighscoreSaveSystem : MonoBehaviour {

    /// <summary>
    /// Save specified gamemode 'category' to highscore file.
    /// </summary>
    /// <param name="category"></param>
    public static void SaveHighscoreData(string category) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + $"/{category}.highscore";
        FileStream stream = new FileStream(path, FileMode.Create);

        HighscoreDataSerial highscoreData = new HighscoreDataSerial();
        formatter.Serialize(stream, highscoreData);
        stream.Close();
    }

    /// <summary>
    /// Load specified gamemode 'category' from highscore file.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public static HighscoreDataSerial LoadHighscoreData(string category) {
        string path = Application.persistentDataPath + $"/{category}.highscore";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HighscoreDataSerial highscoreData = formatter.Deserialize(stream) as HighscoreDataSerial;
            stream.Close();

            return highscoreData;
        } else {
            return null;
        }
    }
}
