using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// Class for handling notification gameObjects.
/// </summary>
public class NotificationHandler : MonoBehaviour {
    public static TMP_Text notificationTextContent;
    public static bool notificationOpen = false;
    private static WaitForSeconds notificationDestroyDelay = new WaitForSeconds(3.5f);

    public static Color32 notificationColorGreen  = new Color32(0, 255, 0, 255);
    public static Color32 notificationColorYellow = new Color32(255, 209, 0, 255);
    public static Color32 notificationColorRed    = new Color32(255, 0, 0, 255);
    public static Color32 notificationColorWhite  = new Color32(255, 255, 255, 255);

    public static NotificationHandler notification;
    private void Awake() {
        notification = this;
        notificationTextContent = gameObject.GetComponentInChildren<TMP_Text>();

        // Disable notification gameObject by default.
        gameObject.SetActive(false);

        // EVENT:: for notification object hidden on awake
        //DevEventHandler.CheckNotificationEvent($"{I18nTextTranslator.SetTranslatedText("eventnotificationdisabled")}");
    }

    /// <summary>
    /// Sets notification text to supplied string (notificationText), sets notification text color, then enables notification container.
    /// </summary>
    /// <param name="notificationText"></param>
    /// <param name="notificationColor"></param>
    public static void ShowNotification_String(string notificationText, Color32 notificationColor) {
        notificationTextContent.SetText($"{notificationText}");
        notificationTextContent.color = notificationColor;
        notification.gameObject.SetActive(true);
        notificationOpen = true;

        notification.StartCoroutine(HideNotification_Delay());

        // EVENT:: for new string notification
        DevEventHandler.CheckNotificationEvent($"{I18nTextTranslator.SetTranslatedText("eventnotificationcreatedstring")} \"{notificationText}\"");
    }

    /// <summary>
    /// Sets notification text to supplied I18n translated text ID (translateTextID), sets notification text color, then enables notification container.
    /// </summary>
    /// <param name="translateTextID"></param>
    /// <param name="notificationColor"></param>
    public static void ShowNotification_Translated(string translateTextID, string extraText, Color32 notificationColor) {
        string notificationContent = $"{I18nTextTranslator.SetTranslatedText(translateTextID)}{extraText}";
        notificationTextContent.SetText(notificationContent);
        notificationTextContent.color = notificationColor;
        notification.gameObject.SetActive(true);
        notificationOpen = true;

        notification.StartCoroutine(HideNotification_Delay());

        // EVENT:: for new translated notification
        DevEventHandler.CheckNotificationEvent($"{I18nTextTranslator.SetTranslatedText("eventnotificationcreatedtranslation")} \"{notificationContent}\"");
    }

    /// <summary>
    /// Hides notification container gameObject.
    /// </summary>
    public static void HideNotification() {
        notification.gameObject.SetActive(false);
        notificationOpen = false;

        // EVENT:: for active notification hidden
        DevEventHandler.CheckNotificationEvent(I18nTextTranslator.SetTranslatedText("eventnotificationhidden"));
    }

    /// <summary>
    /// Starts notification destroy delay after show.
    /// </summary>
    /// <returns></returns>
    public static IEnumerator HideNotification_Delay() {
        yield return notificationDestroyDelay;
        HideNotification();
    }
}
