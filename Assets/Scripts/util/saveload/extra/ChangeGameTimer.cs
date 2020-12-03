using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

using SomeAimGame.Utilities;

public class ChangeGameTimer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public TMP_Text textInfinity, text30, text60, text90, text120;
    public static string selectedTimeText = "60Text (TMP)";

    private static ChangeGameTimer gameTimer;
    void Awake() { gameTimer = this; }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        GameObject hoveredButton = pointerEventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).gameObject;

        SetHoveredTimeText_Color(hoveredButton);
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        ClearHoveredTimeText_Color();
    }

    /// <summary>
    /// Sets current game timer to supplied time string (clickedButtonText).
    /// </summary>
    /// <param name="clickedButtonText"></param>
    public void SetNewGameTimer(TMP_Text clickedButtonText) {
        ClearTimerButtons();
        clickedButtonText.color = InterfaceColors.selectedColor;

        switch (clickedButtonText.transform.name) {
            case "InfinityText (TMP)": SetNewGameTimer(0, true);   break;
            case "30Text (TMP)":       SetNewGameTimer(30, true);  break;
            case "60Text (TMP)":       SetNewGameTimer(60, true);  break;
            case "90Text (TMP)":       SetNewGameTimer(90, true);  break;
            case "120Text (TMP)":      SetNewGameTimer(120, true); break;
        }

        NotificationHandler.ShowTimedNotification_String($"{I18nTextTranslator.SetTranslatedText("eventtimerchanged")} {ReturnGameTimerString(ExtraSettings.gameTimer)}", InterfaceColors.notificationColorGreen);
    }

    /// <summary>
    /// Sets current game timer (GameUI.timeCount) to supplied time string (newGameTimer), then restarts game if restart game bool true (restartGame).
    /// </summary>
    /// <param name="newGameTimer"></param>
    /// <param name="restartGame"></param>
    public static void SetNewGameTimer(int newGameTimer, bool restartGame) {
        //GameUI.timeCount = newGameTimer;
        ExtraSettings.SaveGameTimerItem(newGameTimer);
        SetGameTimerButton(newGameTimer);

        if (restartGame) { GameUI.RestartGame(CosmeticsSettings.gamemode, false); }
    }

    /// <summary>
    /// Sets clicked time button (setButton) to active color after clearing all time buttons.
    /// </summary>
    /// <param name="setButton"></param>
    public static void SetGameTimerButton(int setButton) {
        ClearTimerButtons();

        switch (setButton) {
            case 0:
                selectedTimeText             = "InfinityText (TMP)";
                gameTimer.textInfinity.color = InterfaceColors.selectedColor;
                break;
            case 30:
                selectedTimeText       = "30Text (TMP)";
                gameTimer.text30.color = InterfaceColors.selectedColor;
                break;
            case 60:
                selectedTimeText       = "60Text (TMP)";
                gameTimer.text60.color = InterfaceColors.selectedColor;
                break;
            case 90:
                selectedTimeText       = "90Text (TMP)";
                gameTimer.text90.color = InterfaceColors.selectedColor;
                break;
            case 120:
                selectedTimeText        = "120Text (TMP)";
                gameTimer.text120.color = InterfaceColors.selectedColor;
                break;
        }
    }

    /// <summary>
    /// Clears all time buttons in settings panel (general sub-section) to unselected color.
    /// </summary>
    public static void ClearTimerButtons() {
        Util.GameObjectLoops.Util_ClearTMPTextColor(InterfaceColors.unselectedColor, gameTimer.text30, gameTimer.text60, gameTimer.text90, gameTimer.text120);
    }

    /// <summary>
    /// Sets supplied game timer button text (hoveredTimeText) to hoveredColor.
    /// </summary>
    /// <param name="hoveredTimeText"></param>
    public static void SetHoveredTimeText_Color(GameObject hoveredTimeText) {
        if (hoveredTimeText.name != selectedTimeText) {
            hoveredTimeText.GetComponent<TMP_Text>().color = InterfaceColors.hoveredColor;
            if (ToggleHandler.UISoundOn()) { UISound.PlayUISound_HoverInner(); }
        }
    }

    /// <summary>
    /// Calls ClearTimerButtons() to clear all game timer button text, then set selected timer text to selectedColor
    /// </summary>
    public static void ClearHoveredTimeText_Color() {
        ClearTimerButtons();
        GameObject.Find(selectedTimeText).GetComponent<TMP_Text>().color = InterfaceColors.selectedColor;
    }

    /// <summary>
    /// Returns infinity symbol string for supplied timer value if 0 (gameTimerValue), or just returns supplied timer value (gameTimerValue) as string if not 0.
    /// </summary>
    /// <param name="gameTimerValue"></param>
    /// <returns></returns>
    private static string ReturnGameTimerString(int gameTimerValue) {
        switch (gameTimerValue) {
            case 0: return "∞";
            default: return $"{gameTimerValue}";
        }
    }
}
