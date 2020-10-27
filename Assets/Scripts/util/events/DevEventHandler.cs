using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DevEventHandler : MonoBehaviour {
    public VerticalLayoutGroup eventLayoutGroup;
    public GameObject gamemodeEventPrefab, timeEventPrefab, crosshairEventPrefab, targetsEventPrefab, interfaceEventPrefab, saveEventPrefab, skyboxEventPrefab, languageEventPrefab, keybindEventPrefab, soundEventPrefab;

    private static WaitForSeconds cardDestroyDelay = new WaitForSeconds(3f);
    public static bool selfDestructCard = true;
    public static int cardLimit = 5;

    public static DevEventHandler devEvents;
    private void Awake() { devEvents = this; }

    private void Start() {
        ClearDevEventLayoutGroup();
    }

    public static void CreateGamemodeEvent(string textContent) { NewEventCard(devEvents.gamemodeEventPrefab, textContent); }
    public static void CreateTimeEvent(string textContent) { NewEventCard(devEvents.timeEventPrefab, textContent); }
    public static void CreateCrosshairEvent(string textContent) { NewEventCard(devEvents.crosshairEventPrefab, textContent); }
    public static void CreateTargetsEvent(string textContent) { NewEventCard(devEvents.targetsEventPrefab, textContent); }
    public static void CreateInterfaceEvent(string textContent) { NewEventCard(devEvents.interfaceEventPrefab, textContent); }
    public static void CreateSaveEvent(string textContent) { NewEventCard(devEvents.saveEventPrefab, textContent); }
    public static void CreateSkyboxEvent(string textContent) { NewEventCard(devEvents.skyboxEventPrefab, textContent); }
    public static void CreateLanguageEvent(string textContent) { NewEventCard(devEvents.languageEventPrefab, textContent); }
    public static void CreateKeybindEvent(string textContent) { NewEventCard(devEvents.keybindEventPrefab, textContent); }
    public static void CreateSoundEvent(string textContent) { NewEventCard(devEvents.soundEventPrefab, textContent); }

    /// <summary>
    /// Creates new card with supplied card prefab (cardPrefab), gives new card self destroy timer (DestroyCardAfterDelay), then "flattens" or destroys card thats above card limit.
    /// </summary>
    /// <param name="cardPrefab"></param>
    /// <param name="textContent"></param>
    public static void NewEventCard(GameObject cardPrefab, string textContent) {
        GameObject createdCard = CreateEventCard(cardPrefab, textContent);
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
    public static GameObject CreateEventCard(GameObject cardPrefab, string textContent) {
        GameObject eventCard = Instantiate(cardPrefab);
        TMP_Text timeText    = eventCard.GetComponentsInChildren<TMP_Text>()[1];
        TMP_Text contentText = eventCard.GetComponentsInChildren<TMP_Text>()[2];

        // Sets time and text content.
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
}