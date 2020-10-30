using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEditor;

public class DevEventHandler : MonoBehaviour {
    public VerticalLayoutGroup eventLayoutGroup;
    public GameObject gamemodeEventPrefab, timeEventPrefab, crosshairEventPrefab, targetsEventPrefab, interfaceEventPrefab, saveEventPrefab, skyboxEventPrefab, languageEventPrefab, keybindEventPrefab, soundEventPrefab, notificationEventPrefab, statsEventPrefab;
    public Image gamemodeSignalPrefabImage, timeSignalPrefabImage, crosshairSignalPrefabImage, targetsSignalPrefabImage, interfaceSignalPrefabImage, saveSignalPrefabImage, skyboxSignalPrefabImage, languageSignalPrefabImage, keybindSignalPrefabImage, soundSignalPrefabImage, notificationSignalPrefabImage, statsSignalPrefabImage;
    public TMP_Text eventCountText;
    private Image currentSignalImage;
    public GameObject devEventSignalsObject;

    public static bool gamemodeSignalRunning, timeSignalRunning, crosshairSignalRunning, targetsSignalRunning, interfaceSignalRunning, saveSignalRunning, skyboxSignalRunning, languageSignalRunning, keybindSignalRunning, soundSignalRunning, notificationSignalRunning, statsSignalRunning;

    private static WaitForSeconds cardDestroyDelay   = new WaitForSeconds(2f);
    private static WaitForSeconds signalDestroyDelay = new WaitForSeconds(0.5f);
    public static bool selfDestructCard = true;
    public static int cardLimit = 50;
    public static int eventCount;

    public static bool cardsOn   = false;
    public static bool signalsOn = false;
    public static bool logsOn = false;

    public static string longestCardTypeText, gamemodeCardSpaces, timeCardSpaces, crosshairCardSpaces, targetsCardSpaces, interfaceCardSpaces, saveCardSpaces, skyboxCardSpaces, languageCardSpaces, keybindCardSpaces, soundCardSpaces, notificationCardSpaces, statsCardSpaces;

    public static Color signalGamemodeColor, signalTimeColor, signalCrosshairColor, signalTargetsColor, signalInterfaceColor, signalSaveColor, signalSkyboxColor, signalLanguageColor, signalKeybindColor, signalSoundColor, signalNotificationColor, signalStatsColor, signalDefaultColor;

    public static DevEventHandler devEvents;
    private void Awake() {
        devEvents = this;
        
        if (cardsOn || signalsOn || logsOn) {
            signalGamemodeColor     = gamemodeSignalPrefabImage.GetComponent<Image>().color;
            signalTimeColor         = timeSignalPrefabImage.GetComponent<Image>().color;
            signalCrosshairColor    = crosshairSignalPrefabImage.GetComponent<Image>().color;
            signalTargetsColor      = targetsSignalPrefabImage.GetComponent<Image>().color;
            signalInterfaceColor    = interfaceSignalPrefabImage.GetComponent<Image>().color;
            signalSaveColor         = saveSignalPrefabImage.GetComponent<Image>().color;
            signalSkyboxColor       = skyboxSignalPrefabImage.GetComponent<Image>().color;
            signalLanguageColor     = languageSignalPrefabImage.GetComponent<Image>().color;
            signalKeybindColor      = keybindSignalPrefabImage.GetComponent<Image>().color;
            signalSoundColor        = soundSignalPrefabImage.GetComponent<Image>().color;
            signalNotificationColor = notificationSignalPrefabImage.GetComponent<Image>().color;
            signalStatsColor        = statsSignalPrefabImage.GetComponent<Image>().color;
            signalDefaultColor      = statsSignalPrefabImage.GetComponent<Image>().color;
        } else {
            devEventSignalsObject.SetActive(false);
        }
    }

    // Cards vs signals check
    public static void CheckGamemodeEvent(string eventContent) {
        if (cardsOn) { CreateGamemodeCard(eventContent); } else if (signalsOn) { CreateSignal(DevEventType.Gamemode, ref gamemodeSignalRunning); } else if (logsOn) { NewEventLog(DevEventType.Gamemode, eventContent); }
    }
    public static void CheckTimeEvent(string eventContent) {
        if (cardsOn) { CreateTimeCard(eventContent); } else if (signalsOn) { CreateSignal(DevEventType.Time, ref timeSignalRunning); } else if (logsOn) { NewEventLog(DevEventType.Time, eventContent); }
    }
    public static void CheckCrossahairEvent(string eventContent) {
        if (cardsOn) { CreateCrosshairCard(eventContent); } else if (signalsOn) { CreateSignal(DevEventType.Crosshair, ref crosshairSignalRunning); } else if (logsOn) { NewEventLog(DevEventType.Crosshair, eventContent); }
    }
    public static void CheckTargetsEvent(string eventContent) {
        if (cardsOn) { CreateTargetsCard(eventContent); } else if (signalsOn) { CreateSignal(DevEventType.Targets, ref targetsSignalRunning); } else if (logsOn) { NewEventLog(DevEventType.Targets, eventContent); }
    }
    public static void CheckInterfaceEvent(string eventContent) {
        if (cardsOn) { CreateInterfaceCard(eventContent); } else if (signalsOn) { CreateSignal(DevEventType.Interface, ref interfaceSignalRunning); } else if (logsOn) { NewEventLog(DevEventType.Interface, eventContent); }
    }
    public static void CheckSaveEvent(string eventContent) {
        if (cardsOn) { CreateSaveCard(eventContent); } else if (signalsOn) { CreateSignal(DevEventType.Save, ref saveSignalRunning); } else if (logsOn) { NewEventLog(DevEventType.Save, eventContent); }
    }
    public static void CheckSkyboxEvent(string eventContent) {
        if (cardsOn) { CreateSkyboxCard(eventContent); } else if (signalsOn) { CreateSignal(DevEventType.Skybox, ref skyboxSignalRunning); } else if (logsOn) { NewEventLog(DevEventType.Skybox, eventContent); }
    }
    public static void CheckLanguageEvent(string eventContent) {
        if (cardsOn) { CreateLanguageCard(eventContent); } else if (signalsOn) { CreateSignal(DevEventType.Language, ref languageSignalRunning); } else if (logsOn) { NewEventLog(DevEventType.Language, eventContent); }
    }
    public static void CheckKeybindEvent(string eventContent) {
        if (cardsOn) { CreateKeybindCard(eventContent); } else if (signalsOn) { CreateSignal(DevEventType.Keybind, ref keybindSignalRunning); } else if (logsOn) { NewEventLog(DevEventType.Keybind, eventContent); }
    }
    public static void CheckSoundEvent(string eventContent) {
        if (cardsOn) { CreateSoundCard(eventContent); } else if (signalsOn) { CreateSignal(DevEventType.Sound, ref soundSignalRunning); } else if (logsOn) { NewEventLog(DevEventType.Sound, eventContent); }
    }
    public static void CheckNotificationEvent(string eventContent) {
        if (cardsOn) { CreateNotificationCard(eventContent); } else if (signalsOn) { CreateSignal(DevEventType.Notification, ref notificationSignalRunning); } else if (logsOn) { NewEventLog(DevEventType.Notification, eventContent); }
    }
    public static void CheckStatsEvent(string eventContent) {
        if (cardsOn) { CreateStatsCard(eventContent); } else if (signalsOn) { CreateSignal(DevEventType.Stats, ref statsSignalRunning); } else if (logsOn) { NewEventLog(DevEventType.Stats, eventContent); }
    }

    // Event cards
    public static void CreateGamemodeCard(string textContent) {     NewEventCard($"{gamemodeCardSpaces}", devEvents.gamemodeEventPrefab, textContent); }
    public static void CreateTimeCard(string textContent) {         NewEventCard($"{timeCardSpaces}", devEvents.timeEventPrefab, textContent); }
    public static void CreateCrosshairCard(string textContent) {    NewEventCard($"{crosshairCardSpaces}", devEvents.crosshairEventPrefab, textContent); }
    public static void CreateTargetsCard(string textContent) {      NewEventCard($"{targetsCardSpaces}", devEvents.targetsEventPrefab, textContent); }
    public static void CreateInterfaceCard(string textContent) {    NewEventCard($"{interfaceCardSpaces}", devEvents.interfaceEventPrefab, textContent); }
    public static void CreateSaveCard(string textContent) {         NewEventCard($"{saveCardSpaces}", devEvents.saveEventPrefab, textContent); }
    public static void CreateSkyboxCard(string textContent) {       NewEventCard($"{skyboxCardSpaces}", devEvents.skyboxEventPrefab, textContent); } // DONE
    public static void CreateLanguageCard(string textContent) {     NewEventCard($"{languageCardSpaces}", devEvents.languageEventPrefab, textContent); } // DONE
    public static void CreateKeybindCard(string textContent) {      NewEventCard($"{keybindCardSpaces}", devEvents.keybindEventPrefab, textContent); } // DONE
    public static void CreateSoundCard(string textContent) {        NewEventCard($"{soundCardSpaces}", devEvents.soundEventPrefab, textContent); } // DONE
    public static void CreateNotificationCard(string textContent) { NewEventCard($"{notificationCardSpaces}", devEvents.notificationEventPrefab, textContent); } // DONE
    public static void CreateStatsCard(string textContent) {        NewEventCard($"{statsCardSpaces}", devEvents.statsEventPrefab, textContent); }

    // Event signals

    public static void CreateSignal(DevEventType devEventType, ref bool signalRunning) {
        if (!signalRunning) { signalRunning = true; }

        switch (devEventType) {
            case DevEventType.Gamemode:
                NewSignalEvent(devEvents.gamemodeSignalPrefabImage, signalGamemodeColor);
                devEvents.currentSignalImage = devEvents.gamemodeSignalPrefabImage; break;
            case DevEventType.Time:
                NewSignalEvent(devEvents.timeSignalPrefabImage, signalTimeColor);
                devEvents.currentSignalImage = devEvents.timeSignalPrefabImage; break;
            case DevEventType.Crosshair:
                NewSignalEvent(devEvents.crosshairSignalPrefabImage, signalCrosshairColor);
                devEvents.currentSignalImage = devEvents.crosshairSignalPrefabImage; break;
            case DevEventType.Targets:
                NewSignalEvent(devEvents.targetsSignalPrefabImage, signalTargetsColor);
                devEvents.currentSignalImage = devEvents.targetsSignalPrefabImage; break;
            case DevEventType.Interface:
                NewSignalEvent(devEvents.interfaceSignalPrefabImage, signalInterfaceColor);
                devEvents.currentSignalImage = devEvents.interfaceSignalPrefabImage; break;
            case DevEventType.Save:
                NewSignalEvent(devEvents.saveSignalPrefabImage, signalSaveColor);
                devEvents.currentSignalImage = devEvents.saveSignalPrefabImage; break;
            case DevEventType.Skybox:
                NewSignalEvent(devEvents.skyboxSignalPrefabImage, signalSkyboxColor);
                devEvents.currentSignalImage = devEvents.skyboxSignalPrefabImage; break;
            case DevEventType.Language:
                NewSignalEvent(devEvents.languageSignalPrefabImage, signalLanguageColor);
                devEvents.currentSignalImage = devEvents.languageSignalPrefabImage; break;
            case DevEventType.Keybind:
                NewSignalEvent(devEvents.keybindSignalPrefabImage, signalKeybindColor);
                devEvents.currentSignalImage = devEvents.keybindSignalPrefabImage; break;
            case DevEventType.Sound:
                NewSignalEvent(devEvents.soundSignalPrefabImage, signalSoundColor);
                devEvents.currentSignalImage = devEvents.soundSignalPrefabImage; break;
            case DevEventType.Notification:
                NewSignalEvent(devEvents.notificationSignalPrefabImage, signalNotificationColor);
                devEvents.currentSignalImage = devEvents.notificationSignalPrefabImage; break;
            case DevEventType.Stats:
                NewSignalEvent(devEvents.statsSignalPrefabImage, signalStatsColor);
                devEvents.currentSignalImage = devEvents.statsSignalPrefabImage; break;
        }
        
        devEvents.StartCoroutine(DisableSignal_Delay(devEvents.currentSignalImage, devEventType));
    }

    public static void NewSignalEvent(Image signalPrefabImage, Color signalColor) {
        signalPrefabImage.color = signalColor;
        eventCount++;
        //devEvents.eventCountText.SetText($"{eventCount}");
    }

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

    public static void NewEventLog(DevEventType devEventType, string textContent) {
        DevEvent newDevEvent = new DevEvent($"{System.DateTime.Now:HH:mm:ss}", devEventType, textContent, "", false);
        DevEventConsole.AddNewDevEventLog(newDevEvent);
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
        statsCardSpaces        = GetDifference(I18nTextTranslator.SetTranslatedText("cardtypestats"));
    }

    public static string GetDifference(string translatedText) {
        int diff = longestCardTypeText.Length - translatedText.Length;
        if (diff == 0) { return $"<mspace=mspace=7>{translatedText}\u00A0</mspace>"; }

        for (int i = 0; i < diff + 1; i++) { translatedText += "\u00A0"; } // \u00A0

        return $"<mspace=mspace=7>{translatedText}</mspace>";
    }

    // Signal delays
    public static IEnumerator DisableSignal_Delay(Image signalImage, DevEventType signalRunning) {
        yield return signalDestroyDelay;
        signalImage.color = new Color(signalImage.color.r, signalImage.color.g, signalImage.color.b, 0.15f);
        DisableRunningSignal(signalRunning);
    }

    /// <summary>
    /// Disables supplied currently ran dev event bool (runningDevEventType).
    /// </summary>
    /// <param name="runningDevEventType"></param>
    public static void DisableRunningSignal(DevEventType runningDevEventType) {
        switch (runningDevEventType) {
            case DevEventType.Gamemode:     gamemodeSignalRunning     = false; break;
            case DevEventType.Time:         timeSignalRunning         = false; break;
            case DevEventType.Crosshair:    crosshairSignalRunning    = false; break;
            case DevEventType.Targets:      targetsSignalRunning      = false; break;
            case DevEventType.Interface:    interfaceSignalRunning    = false; break;
            case DevEventType.Save:         saveSignalRunning         = false; break;
            case DevEventType.Skybox:       skyboxSignalRunning       = false; break;
            case DevEventType.Language:     languageSignalRunning     = false; break;
            case DevEventType.Keybind:      keybindSignalRunning      = false; break;
            case DevEventType.Sound:        soundSignalRunning        = false; break;
            case DevEventType.Notification: notificationSignalRunning = false; break;
            case DevEventType.Stats:        statsSignalRunning        = false; break;
        }
    }
}