using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using SomeAimGame.Stats;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoSettingsSaveSystem : MonoBehaviour {

            private static VideoSettingsSaveSystem videoSave;
            void Awake() { videoSave = this; }

            public static void SaveVideoSettingsData(VideoSettings videoSettings) {
                BinaryFormatter formatter = new BinaryFormatter();
                string dirPath            = Application.persistentDataPath + "/prefs";
                string filePath           = dirPath + "/sag_video.prefs";

                DirectoryInfo dirInf = new DirectoryInfo(dirPath);
                if (!dirInf.Exists) { dirInf.Create(); }

                FileStream stream                 = new FileStream(filePath, FileMode.Create);
                VideoSettingsDataSerial videoData = new VideoSettingsDataSerial();
                formatter.Serialize(stream, videoData);
                stream.Close();
            }

            public static VideoSettingsDataSerial LoadVideoSettingsData() {
                string path = Application.persistentDataPath + "/prefs/sag_video.prefs";
                if (File.Exists(path)) {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream stream         = new FileStream(path, FileMode.Open);

                    VideoSettingsDataSerial videodata = formatter.Deserialize(stream) as VideoSettingsDataSerial;
                    stream.Close();

                    return videodata;
                } else {
                    return null;
                }
            }

            public static void InitSavedVideoSettings() {
                VideoSettingsDataSerial loadedVideoSettingsData = LoadVideoSettingsData();
                if (loadedVideoSettingsData != null) {
                    VideoSettings.LoadVideoSettings(loadedVideoSettingsData);
                } else {
                    //Debug.Log("failed to init extra in 'initSavedSettings', extra: " + loadedExtraData);
                    InitVideoSettingsDefaults();
                }
            }

            public static void InitVideoSettingsDefaults() {
            
            }
        }
    }
}
