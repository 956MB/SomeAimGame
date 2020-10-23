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
    }

    /// <summary>
    /// Sets notification text to supplied I18n translated text ID (translateTextID), sets notification text color, then enables notification container.
    /// </summary>
    /// <param name="translateTextID"></param>
    /// <param name="notificationColor"></param>
    public static void ShowNotification_Translated(string translateTextID, Color notificationColor) {
        notificationTextContent.SetText($"{I18nTextTranslator.SetTranslatedText(translateTextID)}");
        notificationTextContent.color = notificationColor;
        notification.gameObject.SetActive(true);
        notificationOpen = true;
    }

    /// <summary>
    /// Hides notification container gameObject.
    /// </summary>
    public static void HideNotification() {
        notification.gameObject.SetActive(false);
        notificationOpen = false;
    }
}
