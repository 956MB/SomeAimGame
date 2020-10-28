using UnityEngine;
using TMPro;

/// <summary>
/// Class for handling notification gameObjects.
/// </summary>
public class NotificationHandler : MonoBehaviour {
    public static TMP_Text notificationTextContent;
    public static bool notificationOpen = false;

    public static NotificationHandler notification;
    private void Awake() {
        notification = this;
        notificationTextContent = gameObject.GetComponentInChildren<TMP_Text>();

        // Disable notification gameObject by default.
        gameObject.SetActive(false);

        // EVENT:: for notification object hidden on awake
        if (DevEventHandler.eventsOn) { DevEventHandler.CreateNotificationEvent($"{I18nTextTranslator.SetTranslatedText("eventnotificationdisabled")}"); }
    }

    /// <summary>
    /// Sets notification text to supplied string (notificationText), sets notification text color, then enables notification container.
    /// </summary>
    /// <param name="notificationText"></param>
    /// <param name="notificationColor"></param>
    public static void ShowNotification_String(string notificationText, Color notificationColor) {
        notificationTextContent.SetText($"{notificationText}");
        notificationTextContent.color = notificationColor;
        notification.gameObject.SetActive(true);
        notificationOpen = true;

        // EVENT:: for new string notification
        if (DevEventHandler.eventsOn) { DevEventHandler.CreateNotificationEvent($"{I18nTextTranslator.SetTranslatedText("eventnotificationcreatedstring")} \"{notificationText}\""); }
    }

    /// <summary>
    /// Sets notification text to supplied I18n translated text ID (translateTextID), sets notification text color, then enables notification container.
    /// </summary>
    /// <param name="translateTextID"></param>
    /// <param name="notificationColor"></param>
    public static void ShowNotification_Translated(string translateTextID, string extraText, Color notificationColor) {
        string notificationContent = $"{I18nTextTranslator.SetTranslatedText(translateTextID)}{extraText}";
        notificationTextContent.SetText(notificationContent);
        notificationTextContent.color = notificationColor;
        notification.gameObject.SetActive(true);
        notificationOpen = true;

        // EVENT:: for new translated notification
        if (DevEventHandler.eventsOn) { DevEventHandler.CreateNotificationEvent($"{I18nTextTranslator.SetTranslatedText("eventnotificationcreatedtranslation")} \"{notificationContent}\""); }
    }

    /// <summary>
    /// Hides notification container gameObject.
    /// </summary>
    public static void HideNotification() {
        notification.gameObject.SetActive(false);
        notificationOpen = false;

        // EVENT:: for active notification hidden
        if (DevEventHandler.eventsOn) { DevEventHandler.CreateNotificationEvent(I18nTextTranslator.SetTranslatedText("eventnotificationhidden")); }
    }
}
