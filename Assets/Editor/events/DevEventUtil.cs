using UnityEngine;

public class DevEventUtil : MonoBehaviour {
    public static Texture2D gamemodeBarLeft, timeBarLeft, crosshairBarLeft, targetsBarLeft, interfaceBarLeft, saveBarLeft, skyboxBarLeft, languageBarLeft, keybindBarLeft, soundBarLeft, notificationBarLeft, statsBarLeft, defaultBarLeft;

    public static void LoadIconResources() {
        // Load bar left icons from resources
        gamemodeBarLeft     = Resources.Load<Texture2D>("images/gamemodeBarLeft");
        timeBarLeft         = Resources.Load<Texture2D>("images/timeBarLeft");
        crosshairBarLeft    = Resources.Load<Texture2D>("images/crosshairBarLeft");
        targetsBarLeft      = Resources.Load<Texture2D>("images/targetsBarLeft");
        interfaceBarLeft    = Resources.Load<Texture2D>("images/interfaceBarLeft");
        saveBarLeft         = Resources.Load<Texture2D>("images/saveBarLeft");
        skyboxBarLeft       = Resources.Load<Texture2D>("images/skyboxBarLeft");
        languageBarLeft     = Resources.Load<Texture2D>("images/languageBarLeft");
        keybindBarLeft      = Resources.Load<Texture2D>("images/keybindBarLeft");
        soundBarLeft        = Resources.Load<Texture2D>("images/soundBarLeft");
        notificationBarLeft = Resources.Load<Texture2D>("images/notificationBarLeft");
        statsBarLeft        = Resources.Load<Texture2D>("images/statsBarLeft");
        defaultBarLeft      = Resources.Load<Texture2D>("images/defaultBarLeft");
    }

    public static string ReturnDevEventTypeColor(DevEventType devEventType) {
        switch (devEventType) {
            case DevEventType.Gamemode:     return "#FF9100";
            case DevEventType.Time:         return "#FFB700";
            case DevEventType.Crosshair:    return "#00B8FF";
            case DevEventType.Targets:      return "#B100FF";
            case DevEventType.Interface:    return "#00FF00";
            case DevEventType.Save:         return "#FF0000";
            case DevEventType.Skybox:       return "#6E00FF";
            case DevEventType.Language:     return "#00FFE3";
            case DevEventType.Keybind:      return "#FF0088";
            case DevEventType.Sound:        return "#0055FF";
            case DevEventType.Notification: return "#C1FF00";
            case DevEventType.Stats:        return "#FFFFFF";
            default:                        return "#DBDBDB";
        }
    }

    public static string ReturnDevEventTypeText(DevEventType devEventType) {
        switch (devEventType) {
            case DevEventType.Gamemode:     return "GAMEMODE";
            case DevEventType.Time:         return "TIME             ";
            case DevEventType.Crosshair:    return "CROSSHAIR ";
            case DevEventType.Targets:      return "TARGETS     ";
            case DevEventType.Interface:    return "INTERFACE  ";
            case DevEventType.Save:         return "SAVE            ";
            case DevEventType.Skybox:       return "SKYBOX";
            case DevEventType.Language:     return "LANGUAGE";
            case DevEventType.Keybind:      return "KEYBIND";
            case DevEventType.Sound:        return "SOUND";
            case DevEventType.Notification: return "NOTIFICATION";
            case DevEventType.Stats:        return "STATS";
            default:                        return "DEFAULT";
        }
    }

    public static Texture2D ReturnDevEventTypeBarLeft(DevEventType devEventType) {
        switch (devEventType) {
            case DevEventType.Gamemode:     return gamemodeBarLeft;
            case DevEventType.Time:         return timeBarLeft;
            case DevEventType.Crosshair:    return crosshairBarLeft;
            case DevEventType.Targets:      return targetsBarLeft;
            case DevEventType.Interface:    return interfaceBarLeft;
            case DevEventType.Save:         return saveBarLeft;
            case DevEventType.Skybox:       return skyboxBarLeft;
            case DevEventType.Language:     return languageBarLeft;
            case DevEventType.Keybind:      return keybindBarLeft;
            case DevEventType.Sound:        return soundBarLeft;
            case DevEventType.Notification: return notificationBarLeft;
            case DevEventType.Stats:        return statsBarLeft;
            default:                        return defaultBarLeft;
        }
    }
}
