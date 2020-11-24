using UnityEngine;
using TMPro;

public class TempValues : MonoBehaviour {
    public TMP_Text timerRunning, settingsOpen, timeCount;

    private static TempValues tempValues;
    void Awake() { tempValues = this; }

    public static void SetTimerRunningTemp(bool running) {
        if (running) {
            tempValues.timerRunning.SetText("true");
            tempValues.timerRunning.color = InterfaceColors.notificationColorGreen;
        } else {
            tempValues.timerRunning.SetText("false");
            tempValues.timerRunning.color = InterfaceColors.notificationColorRed;
        }
    }

    public static void SetSettingsOpenTemp(bool open) {
        if (open) {
            tempValues.settingsOpen.SetText("true");
            tempValues.settingsOpen.color = InterfaceColors.notificationColorGreen;
        } else {
            tempValues.settingsOpen.SetText("false");
            tempValues.settingsOpen.color = InterfaceColors.notificationColorRed;
        }
    }

    public static void SetTimeCountTemp(int count) {
        tempValues.timeCount.SetText($"{count}");
        //tempValues.settingsOpen.color = InterfaceColors.widgetsNeutralColor;
    }
}
