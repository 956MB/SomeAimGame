using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DevEventHandler : MonoBehaviour {
    public VerticalLayoutGroup eventLayoutGroup;
    public GameObject gamemodeEventPrefab, timeEventPrefab, crosshairEventPrefab, targetsEventPrefab, interfaceEventPrefab, saveEventPrefab, skyboxEventPrefab, languageEventPrefab, keybindEventPrefab, soundEventPrefab, notificationEventPrefab;

    private static WaitForSeconds cardDestroyDelay = new WaitForSeconds(5f);
    public static bool selfDestructCard = true;
    public static int cardLimit = 10;

    public static bool eventsOn = false;

    public static string longestCardTypeText, gamemodeCardSpaces, timeCardSpaces, crosshairCardSpaces, targetsCardSpaces, interfaceCardSpaces, saveCardSpaces, skyboxCardSpaces, languageCardSpaces, keybindCardSpaces, soundCardSpaces, notificationCardSpaces;

    public static DevEventHandler devEvents;
    private void Awake() { devEvents = this; }

    private void Start() {
        ClearDevEventLayoutGroup();
        PopulateExtraSpaces();
    }

    public static void CreateGamemodeEvent(string textContent) { NewEventCard($"{gamemodeCardSpaces}", devEvents.gamemodeEventPrefab, textContent); }
    public static void CreateTimeEvent(string textContent) { NewEventCard($"{timeCardSpaces}", devEvents.timeEventPrefab, textContent); }
    public static void CreateCrosshairEvent(string textContent) { NewEventCard($"{crosshairCardSpaces}", devEvents.crosshairEventPrefab, textContent); }
    public static void CreateTargetsEvent(string textContent) { NewEventCard($"{targetsCardSpaces}", devEvents.targetsEventPrefab, textContent); }
    public static void CreateInterfaceEvent(string textContent) { NewEventCard($"{interfaceCardSpaces}", devEvents.interfaceEventPrefab, textContent); }
    public static void CreateSaveEvent(string textContent) { NewEventCard($"{saveCardSpaces}", devEvents.saveEventPrefab, textContent); }
    public static void CreateSkyboxEvent(string textContent) { NewEventCard($"{skyboxCardSpaces}", devEvents.skyboxEventPrefab, textContent); }
    public static void CreateLanguageEvent(string textContent) { NewEventCard($"{languageCardSpaces}", devEvents.languageEventPrefab, textContent); } // DONE
    public static void CreateKeybindEvent(string textContent) { NewEventCard($"{keybindCardSpaces}", devEvents.keybindEventPrefab, textContent); } // DONE
    public static void CreateSoundEvent(string textContent) { NewEventCard($"{soundCardSpaces}", devEvents.soundEventPrefab, textContent); } // DONE
    public static void CreateNotificationEvent(string textContent) { NewEventCard($"{notificationCardSpaces}", devEvents.notificationEventPrefab, textContent); } // DONE

    /// <summary>
    /// Creates new card with supplied card prefab (cardPrefab), gives new card self destroy timer (DestroyCardAfterDelay), then "flattens" or destroys card thats above card limit.
    /// </summary>
    /// <param name="cardPrefab"></param>
    /// <param name="textContent"></param>
    public static void NewEventCard(string typeTranslated, GameObject cardPrefab, string textContent) {
        GameObject createdCard = CreateEventCard(typeTranslated, cardPrefab, textContent);
        devEvents.StartCoroutine(DestroyCardAfterDelay(createdCard));

        if (!CheckEventGroupCount()) {
            if (selfDestructCard) {
                FlattenTopCard();
            } else {
                DestroyEventCard_Top();
            }
        }
    }

    /// <summary>
    /// Waits delay (cardDestroyDelay), then destroys single supplied card (cardToDestroy).
    /// </summary>
    /// <param name="cardToDestroy"></param>
    /// <returns></returns>
    public static IEnumerator DestroyCardAfterDelay(GameObject cardToDestroy) {
        yield return cardDestroyDelay;
        DestroySingleCard(cardToDestroy);
    }

    /// <summary>
    /// Creates new dev event card with supplied card prefab (cardPrefab) and sets text content (textContent).
    /// </summary>
    /// <param name="cardPrefab"></param>
    /// <param name="textContent"></param>
    public static GameObject CreateEventCard(string typeTranslated, GameObject cardPrefab, string textContent) {
        GameObject eventCard = Instantiate(cardPrefab);
        TMP_Text typeText    = eventCard.GetComponentsInChildren<TMP_Text>()[0];
        TMP_Text timeText    = eventCard.GetComponentsInChildren<TMP_Text>()[1];
        TMP_Text contentText = eventCard.GetComponentsInChildren<TMP_Text>()[2];

        // Sets type, time and content text.
        typeText.SetText($"{typeTranslated}");
        timeText.SetText($"{System.DateTime.Now:HH:mm:ss}");
        contentText.SetText($"{textContent}");

        // Sets new created card as child of dev event vertical layout group.
        eventCard.transform.SetParent(devEvents.eventLayoutGroup.transform);

        return eventCard;
    }

    /// <summary>
    /// Checks child count of dev event vertical layout group, then deletes top element if over 7.
    /// </summary>
    private static bool CheckEventGroupCount() {
        if (devEvents.eventLayoutGroup.transform.childCount > cardLimit + 1) { return false; }
        return true;
    }

    /// <summary>
    /// Destroys single supplied card (newCard).
    /// </summary>
    /// <param name="newCard"></param>
    public static void DestroySingleCard(GameObject newCard) {
        if (!newCard.CompareTag("GradientBackground")) { Destroy(newCard); }
        Util.RefreshRootLayoutGroup(devEvents.eventLayoutGroup.gameObject);
    }

    /// <summary>
    /// Destroys top level child of dev event vertical layout group that is above card limit.
    /// </summary>
    public static void DestroyEventCard_Top() {
        int count = devEvents.eventLayoutGroup.transform.childCount;
        DestroySingleCard(devEvents.eventLayoutGroup.transform.GetChild(count - (cardLimit + 1)).gameObject);
    }

    /// <summary>
    /// Temporarily "flattens" (hides) card that is above card limit.
    /// </summary>
    public static void FlattenTopCard() {
        int count = devEvents.eventLayoutGroup.transform.childCount;
        devEvents.eventLayoutGroup.transform.GetChild(count - (cardLimit + 1)).gameObject.SetActive(false);
        Util.RefreshRootLayoutGroup(devEvents.eventLayoutGroup.gameObject);
    }

    /// <summary>
    /// Clears all cards from dev event layout group.
    /// </summary>
    public static void ClearDevEventLayoutGroup() {
        foreach (Transform layoutChild in devEvents.eventLayoutGroup.transform) {
            if (!layoutChild.CompareTag("GradientBackground")) { Destroy(layoutChild.gameObject); }
        }
    }

    /// <summary>
    /// Prints child count of dev event vertical layout group.
    /// </summary>
    public static void PrintEventGroupCount() { Debug.Log($"{devEvents.eventLayoutGroup.transform.childCount}"); }

    public static void PopulateExtraSpaces() {
        gamemodeCardSpaces     = GetDifference(I18nTextTranslator.SetTranslatedText("cardtypegamemode"));
        timeCardSpaces         = GetDifference(I18nTextTranslator.SetTranslatedText("cardtypetime"));
        crosshairCardSpaces    = GetDifference(I18nTextTranslator.SetTranslatedText("cardtypecrosshair"));
        targetsCardSpaces      = GetDifference(I18nTextTranslator.SetTranslatedText("cardtypetargets"));
        interfaceCardSpaces    = GetDifference(I18nTextTranslator.SetTranslatedText("cardtypeinterface"));
        saveCardSpaces         = GetDifference(I18nTextTranslator.SetTranslatedText("cardtypesave"));
        skyboxCardSpaces       = GetDifference(I18nTextTranslator.SetTranslatedText("cardtypeskybox"));
        languageCardSpaces     = GetDifference(I18nTextTranslator.SetTranslatedText("cardtypelanguage"));
        keybindCardSpaces      = GetDifference(I18nTextTranslator.SetTranslatedText("cardtypekeybind"));
        soundCardSpaces        = GetDifference(I18nTextTranslator.SetTranslatedText("cardtypesound"));
        notificationCardSpaces = GetDifference(I18nTextTranslator.SetTranslatedText("cardtypenotification"));
    }

    public static string GetDifference(string translatedText) {
        int diff = longestCardTypeText.Length - translatedText.Length;
        if (diff == 0) { return $"<mspace=mspace=7>{translatedText}\u00A0</mspace>"; }

        for (int i = 0; i < diff + 1; i++) { translatedText += "\u00A0"; } // \u00A0

        return $"<mspace=mspace=7>{translatedText}</mspace>";
    }
}