
public class DevEvent {
    public bool isSelected;
    public DevEventType eventType;
    public string eventTime;
    public string eventContent;
    public string eventExtra;

    public DevEvent(string eventTime, DevEventType eventType, string eventContent, string eventExtra, bool isSelected) {
        this.eventTime    = eventTime;
        this.eventType    = eventType;
        this.eventContent = eventContent;
        this.eventExtra   = eventExtra;
        this.isSelected   = isSelected;
    }
}

public enum DevEventType {
    Gamemode     = 0,
    Time         = 1,
    Crosshair    = 2,
    Targets      = 3,
    Interface    = 4,
    Save         = 5,
    Skybox       = 6,
    Language     = 7,
    Keybind      = 8,
    Sound        = 9,
    Notification = 10,
    Stats        = 11
}
