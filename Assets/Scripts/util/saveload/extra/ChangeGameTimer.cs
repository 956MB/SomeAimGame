using UnityEngine;
using TMPro;

public class ChangeGameTimer : MonoBehaviour {
    public TMP_Text text30, text60, text90, text120;
    private static Color32 selectedTextColor   = new Color32(255, 255, 255, 255);
    private static Color32 unselectedTextColor = new Color32(255, 255, 255, 120);

    private static ChangeGameTimer gameTimer;
    void Awake() { gameTimer = this; }

    /// <summary>
    /// Sets current game timer to supplied time string (clickedButtonText).
    /// </summary>
    /// <param name="clickedButtonText"></param>
    public void SetNewGameTimer(TMP_Text clickedButtonText) {
        ClearTimerButtons();
        clickedButtonText.color = selectedTextColor;

        switch (clickedButtonText.transform.name) {
            case "30Text (TMP)":  SetNewGameTimer(30, true);  break;
            case "60Text (TMP)":  SetNewGameTimer(60, true);  break;
            case "90Text (TMP)":  SetNewGameTimer(90, true);  break;
            case "120Text (TMP)": SetNewGameTimer(120, true); break;
        }
    }

    /// <summary>
    /// Sets current game timer (GameUI.timeCount) to supplied time string (newGameTimer), then restarts game if restart game bool true (restartGame).
    /// </summary>
    /// <param name="newGameTimer"></param>
    /// <param name="restartGame"></param>
    public static void SetNewGameTimer(int newGameTimer, bool restartGame) {
        GameUI.timeCount = newGameTimer;
        ExtraSettings.SaveGameTimerItem(newGameTimer);

        if (restartGame) { GameUI.RestartGame(SpawnTargets.gamemode); }
    }

    /// <summary>
    /// Sets clicked time button (setButton) to active color after clearing all time buttons.
    /// </summary>
    /// <param name="setButton"></param>
    public static void SetGameTimerButton(int setButton) {
        ClearTimerButtons();

        switch (setButton) {
            case 30:  gameTimer.text30.color = selectedTextColor;  break;
            case 60:  gameTimer.text60.color = selectedTextColor;  break;
            case 90:  gameTimer.text90.color = selectedTextColor;  break;
            case 120: gameTimer.text120.color = selectedTextColor; break;
        }
    }

    /// <summary>
    /// Clears all time buttons in settings panel (general sub-section) to unselected color.
    /// </summary>
    public static void ClearTimerButtons() {
        gameTimer.text30.color  = unselectedTextColor;
        gameTimer.text60.color  = unselectedTextColor;
        gameTimer.text90.color  = unselectedTextColor;
        gameTimer.text120.color = unselectedTextColor;
    }
}
