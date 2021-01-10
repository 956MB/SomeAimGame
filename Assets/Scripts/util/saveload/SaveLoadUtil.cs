using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using SomeAimGame.SFX;
using SomeAimGame.Core.Video;

namespace SomeAimGame.Utilities {
    public enum SaveType {
        VIDEO,
        COSMETICS,
        CROSSHAIR,
        EXTRA,
        WIDGET,
        LANGUAGE,
        SFX,
        KEYBINDS,
        HIGHSCORE
    }

    public class SaveLoadUtil : MonoBehaviour {
        public static void SaveDataSerial(string dir, string filename, object dataObject) {
            BinaryFormatter formatter = new BinaryFormatter();
            string dirPath            = Application.persistentDataPath + dir;
            string filePath           = dirPath + filename;

            DirectoryInfo dirInf = new DirectoryInfo(dirPath);
            if (!dirInf.Exists) { dirInf.Create(); }

            FileStream stream = new FileStream(filePath, FileMode.Create);
            formatter.Serialize(stream, dataObject);
            stream.Close();
        }

        public static object LoadDataSerial(string loadDir, SaveType objectType) {
            string path = Application.persistentDataPath + loadDir;
            if (File.Exists(path)) {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream         = new FileStream(path, FileMode.Open);

                object loadedData = ReturnObjectDeserialize(formatter, stream, objectType);
                stream.Close();

                return loadedData;
            } else {
                return null;
            }
        }

        private static object ReturnObjectDeserialize(BinaryFormatter formatter, FileStream stream, SaveType objectType) {
            switch (objectType) {
                case SaveType.VIDEO:     return formatter.Deserialize(stream) as VideoSettingsDataSerial;
                case SaveType.COSMETICS: return formatter.Deserialize(stream) as CosmeticsDataSerial;
                case SaveType.CROSSHAIR: return formatter.Deserialize(stream) as CrosshairDataSerial;
                case SaveType.EXTRA:     return formatter.Deserialize(stream) as ExtraSettingsDataSerial;
                case SaveType.WIDGET:    return formatter.Deserialize(stream) as WidgetSettingsDataSerial;
                case SaveType.LANGUAGE:  return formatter.Deserialize(stream) as LanguageSettingDataSerial;
                case SaveType.SFX:       return formatter.Deserialize(stream) as SFXSettingsDataSerial;
                case SaveType.KEYBINDS:  return formatter.Deserialize(stream) as KeybindDataSerial;
                case SaveType.HIGHSCORE: return formatter.Deserialize(stream) as HighscoreDataSerial;
                default:                 return formatter.Deserialize(stream) as VideoSettingsDataSerial;
            }
        }
    }
}
