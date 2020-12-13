using UnityEngine;
using System.IO;

namespace SomeAimGame.Stats {
    /// <summary>
    /// Holds previous games stats in 'LastGameStats' object.
    /// </summary>
    public class PreviousGameStats {
        public string gamemodeName;
        public int scoreValue, accuracyValue, ttkValue, bestStreakValue, targetsTotalValue, targetsHitValue, targetsMissesValue;
        public double kpsValue;
    }

    /// <summary>
    /// Holds best games stats in 'LastGameStats' object.
    /// </summary>
    public class BestGameStats {
        public string gamemodeName;
        public int scoreValue, accuracyValue, ttkValue, bestStreakValue, targetsTotalValue, targetsHitValue, targetsMissesValue;
        public double kpsValue;
    }

    public class StatsJsonSaveSystem : MonoBehaviour {
        public static PreviousGameStats previousGameStatsLoaded;
        public static BestGameStats bestGameStatsLoaded;

        /// <summary>
        /// Loads previous game stats from json file.
        /// </summary>
        /// <param name="loadedGamemode"></param>
        /// <returns></returns>
        public static PreviousGameStats LoadLastGameData(string loadedGamemode) {
            string jsonPath = Application.persistentDataPath + $"/stats/previous/{loadedGamemode}_stats_previous.json";
            if (File.Exists(jsonPath)) {
                previousGameStatsLoaded = JsonUtility.FromJson<PreviousGameStats>(File.ReadAllText(jsonPath));
                return previousGameStatsLoaded;
            } else {
                return null;
            }
        }

        /// <summary>
        /// Loads best game stats from json file.
        /// </summary>
        /// <param name="loadedGamemode"></param>
        /// <returns></returns>
        public static BestGameStats LoadBestGameStatsData(string loadedGamemode) {
            string jsonPath = Application.persistentDataPath + $"/stats/best/{loadedGamemode}_stats_best.json";
            if (File.Exists(jsonPath)) {
                bestGameStatsLoaded = JsonUtility.FromJson<BestGameStats>(File.ReadAllText(jsonPath));
                return bestGameStatsLoaded;
            } else {
                return null;
            }
        }

        /// <summary>
        /// Saves previous game (setGamemode) stats to json file.
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
        public static void SavePreviousGameData(string setGamemode, int setScore, int setAccuracy, int setTTK, double setKPS, int setBestStreak, int setTargetsTotal, int setTargetsHit, int setTargetsMisses) {
            PreviousGameStats previousGameStats = new PreviousGameStats();
            previousGameStats                   = SetPreviousGameStats(previousGameStats, setGamemode, setScore, setAccuracy, setTTK, setKPS, setBestStreak, setTargetsTotal, setTargetsHit, setTargetsMisses);

            string dirPath       = Application.persistentDataPath + "/stats/previous";
            DirectoryInfo dirInf = new DirectoryInfo(dirPath);
            if (!dirInf.Exists) { dirInf.Create(); }

            string jsonData = JsonUtility.ToJson(previousGameStats, true);
            File.WriteAllText(Application.persistentDataPath + $"/stats/previous/{setGamemode}_stats_previous.json", jsonData);
        }

        /// <summary>
        /// Saves best game (setGamemode) stats to json file.
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
        public static void SaveBestGameStatsData(string setGamemode, int setScore, int setAccuracy, int setTTK, double setKPS, int setBestStreak, int setTargetsTotal, int setTargetsHit, int setTargetsMisses) {
            BestGameStats bestGameStatsLoaded = new BestGameStats();
            bestGameStatsLoaded               = SetBestGameStats(bestGameStatsLoaded, setGamemode, setScore, setAccuracy, setTTK, setKPS, setBestStreak, setTargetsTotal, setTargetsHit, setTargetsMisses);

            string dirPath       = Application.persistentDataPath + "/stats/best";
            DirectoryInfo dirInf = new DirectoryInfo(dirPath);
            if (!dirInf.Exists) { dirInf.Create(); }

            string jsonData = JsonUtility.ToJson(bestGameStatsLoaded, true);
            File.WriteAllText(Application.persistentDataPath + $"/stats/best/{setGamemode}_stats_best.json", jsonData);
        }

        /// <summary>
        /// Sets previous games stats to 'PreviousGameStats' object, then returns.
        /// </summary>
        /// <param name="previousGameStatsObj"></param>
        /// <param name="setGamemode"></param>
        /// <param name="setScore"></param>
        /// <param name="setAccuracy"></param>
        /// <param name="setTTK"></param>
        /// <param name="setKPS"></param>
        /// <param name="setBestStreak"></param>
        /// <param name="setTargetsTotal"></param>
        /// <param name="setTargetsHit"></param>
        /// <param name="setTargetsMisses"></param>
        /// <returns></returns>
        private static PreviousGameStats SetPreviousGameStats(PreviousGameStats previousGameStatsObj, string setGamemode, int setScore, int setAccuracy, int setTTK, double setKPS, int setBestStreak, int setTargetsTotal, int setTargetsHit, int setTargetsMisses) {
            previousGameStatsObj.gamemodeName       = setGamemode;
            previousGameStatsObj.scoreValue         = setScore;
            previousGameStatsObj.accuracyValue      = setAccuracy;
            previousGameStatsObj.ttkValue           = setTTK;
            previousGameStatsObj.kpsValue           = setKPS;
            previousGameStatsObj.bestStreakValue    = setBestStreak;
            previousGameStatsObj.targetsTotalValue  = setTargetsTotal;
            previousGameStatsObj.targetsHitValue    = setTargetsHit;
            previousGameStatsObj.targetsMissesValue = setTargetsMisses;

            return previousGameStatsObj;
        }

        /// <summary>
        /// Sets best games stats to 'BestGameStats' object, then returns.
        /// </summary>
        /// <param name="bestGameStatsObj"></param>
        /// <param name="setScore"></param>
        /// <param name="setAccuracy"></param>
        /// <param name="setTTK"></param>
        /// <param name="setKPS"></param>
        /// <param name="setBestStreak"></param>
        /// <param name="setTargetsTotal"></param>
        /// <param name="setTargetsHit"></param>
        /// <param name="setTargetsMisses"></param>
        /// <returns></returns>
        private static BestGameStats SetBestGameStats(BestGameStats bestGameStatsObj, string setGamemode, int setScore, int setAccuracy, int setTTK, double setKPS, int setBestStreak, int setTargetsTotal, int setTargetsHit, int setTargetsMisses) {
            bestGameStatsObj.gamemodeName       = setGamemode;
            bestGameStatsObj.scoreValue         = setScore;
            bestGameStatsObj.accuracyValue      = setAccuracy;
            bestGameStatsObj.ttkValue           = setTTK;
            bestGameStatsObj.kpsValue           = setKPS;
            bestGameStatsObj.bestStreakValue    = setBestStreak;
            bestGameStatsObj.targetsTotalValue  = setTargetsTotal;
            bestGameStatsObj.targetsHitValue    = setTargetsHit;
            bestGameStatsObj.targetsMissesValue = setTargetsMisses;

            return bestGameStatsObj;
        }
    }
}
