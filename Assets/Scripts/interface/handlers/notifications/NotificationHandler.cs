using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// Class for handling notification gameObjects.
/// </summary>
public class NotificationHandler : MonoBehaviour {
    public static TMP_Text notificationTextContent;
    public static bool notificationOpen                            = false;
    private static WaitForSecondsRealtime notificationDestroyDelay = new WaitForSecondsRealtime(3.5f);

    public static NotificationHandler notification;
    private void Awake() {
        // TODO: move notification banner to new location and uncomment all "ShowTimedNotification_String" methods not currently being used.
        notification            = this;
        notificationTextContent = gameObject.GetComponentInChildren<TMP_Text>();

        // Disable notification gameObject by default.
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets notification text to supplied string (notificationText), sets notification text color, then enables notification container.
    /// </summary>
    /// <param name="notificationText"></param>
    /// <param name="notificationColor"></param>
    public static void ShowTimedNotification_String(string notificationText, Color32 notificationColor) {
        SetShowNotification(notificationText, notificationColor);
    }

    /// <summary>
    /// Sets notification text to supplied I18n translated text ID (translateTextID), sets notification text color, then enables notification container.
    /// </summary>
    /// <param name="translateTextID"></param>
    /// <param name="notificationColor"></param>
    public static void ShowTimedNotification_Translated(string translateTextID, string extraText, Color32 notificationColor) {
        string notificationContent = $"{I18nTextTranslator.SetTranslatedText(translateTextID)}{extraText}";
        SetShowNotification(notificationContent, notificationColor);
    }

    /// <summary>
    /// Shows new delay notification with supplied text string (setText) and color (notificationColor).
    /// </summary>
    /// <param name="setText"></param>
    /// <param name="notificationColor"></param>
    public static void SetShowNotification(string setText, Color32 notificationColor) {
        notificationTextContent.SetText(setText);
        notificationTextContent.color = notificationColor;
        notification.gameObject.SetActive(true);
        notificationOpen = true;

        notification.StartCoroutine(HideNotification_Delay());
    }

    /// <summary>
    /// Hides notification object if one currently active.
    /// </summary>
    public static void CheckHideNotificationObject() {
        if (notificationOpen) { HideNotification(); }
    }

    /// <summary>
    /// Hides notification container gameObject.
    /// </summary>
    public static void HideNotification() {
        notification.gameObject.SetActive(false);
        notificationOpen = false;
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
