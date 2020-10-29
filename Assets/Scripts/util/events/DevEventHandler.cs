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

    public static bool gamemodeSignalRunning, timeSignalRunning, crosshairSignalRunning, targetsSignalRunning, interfaceSignalRunning, saveSignalRunning, skyboxSignalRunning, languageSignalRunning, keybindSignalRunning, soundSignalRunning, notificationSignalRunning, statsSignalRunning;

    private static WaitForSeconds cardDestroyDelay = new WaitForSeconds(2f);
    private static WaitForSeconds signalDestroyDelay = new WaitForSeconds(0.5f);
    public static bool selfDestructCard = true;
    public static int cardLimit = 50;
    public static int eventCount;

    public static bool cardsOn = false;
    public static bool signalsOn = true;

    public static string longestCardTypeText, gamemodeCardSpaces, timeCardSpaces, crosshairCardSpaces, targetsCardSpaces, interfaceCardSpaces, saveCardSpaces, skyboxCardSpaces, languageCardSpaces, keybindCardSpaces, soundCardSpaces, notificationCardSpaces, statsCardSpaces;

    public static Color signalGamemodeColor     = new Color(255f, 130f, 0f, 255f);
    public static Color signalTimeColor         = new Color(255f, 183f, 0f, 255f);
    public static Color signalCrosshairColor    = new Color(0f, 184f, 255f, 255f);
    public static Color signalTargetsColor      = new Color(117f, 0f, 255f, 255f);
    public static Color signalInterfaceColor    = new Color(0f, 255f, 0f, 255f);
    public static Color signalSaveColor         = new Color(255f, 130f, 0f, 255f);
    public static Color signalSkyboxColor       = new Color(255f, 0f, 0f, 255f);
    public static Color signalLanguageColor     = new Color(110f, 0f, 255f, 255f);
    public static Color signalKeybindColor      = new Color(0f, 255f, 227f, 255f);
    public static Color signalSoundColor        = new Color(255f, 0f, 136f, 255f);
    public static Color signalNotificationColor = new Color(0f, 85f, 255f, 255f);
    public static Color signalStatsColor        = new Color(255f, 255f, 255f, 255f);
    public static Color signalDefaultColor      = new Color(255f, 255f, 255f, 200f);

    public static DevEventHandler devEvents;
    private void Awake() { devEvents = this; }

    // Cards vs signals check
    public static void CheckGamemodeEvent(string eventContent) { if (cardsOn) {     CreateGamemodeCard(eventContent); } else if (signalsOn) {     CreateGamemodeSignal(); } }
    public static void CheckTimeEvent(string eventContent) { if (cardsOn) {         CreateTimeCard(eventContent); } else if (signalsOn) {         CreateTimeSignal(); } }
    public static void CheckCrossahairEvent(string eventContent) { if (cardsOn) {   CreateCrosshairCard(eventContent); } else if (signalsOn) {    CreateCrosshairSignal(); } }
    public static void CheckTargetsEvent(string eventContent) { if (cardsOn) {      CreateTargetsCard(eventContent); } else if (signalsOn) {      CreateTargetsSignal(); } }
    public static void CheckInterfaceEvent(string eventContent) { if (cardsOn) {    CreateInterfaceCard(eventContent); } else if (signalsOn) {    CreateInterfaceSignal(); } }
    public static void CheckSaveEvent(string eventContent) { if (cardsOn) {         CreateSaveCard(eventContent); } else if (signalsOn) {         CreateSaveSignal(); } }
    public static void CheckSkyboxEvent(string eventContent) { if (cardsOn) {       CreateSkyboxCard(eventContent); } else if (signalsOn) {       CreateSkyboxSignal(); } }
    public static void CheckLanguageEvent(string eventContent) { if (cardsOn) {     CreateLanguageCard(eventContent); } else if (signalsOn) {     CreateLanguageSignal(); } }
    public static void CheckKeybindEvent(string eventContent) { if (cardsOn) {      CreateKeybindCard(eventContent); } else if (signalsOn) {      CreateKeybindSignal(); } }
    public static void CheckSoundEvent(string eventContent) { if (cardsOn) {        CreateSoundCard(eventContent); } else if (signalsOn) {        CreateSoundSignal(); } }
    public static void CheckNotificationEvent(string eventContent) { if (cardsOn) { CreateNotificationCard(eventContent); } else if (signalsOn) { CreateNotificationSignal(); } }
    public static void CheckStatsEvent(string eventContent) { if (cardsOn) {        CreateStatsCard(eventContent); } else if (signalsOn) {        CreateStatsSignal(); } }

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
    public static void CreateGamemodeSignal() {
        if (!gamemodeSignalRunning) {
            gamemodeSignalRunning = true;
            NewSignalEvent(devEvents.gamemodeSignalPrefabImage, signalGamemodeColor);
            devEvents.StartCoroutine(DisableGamemodeSignal_Delay(devEvents.gamemodeSignalPrefabImage));
        }
    }
    public static void CreateTimeSignal() {
        if (!timeSignalRunning) {
            timeSignalRunning = true;
            NewSignalEvent(devEvents.timeSignalPrefabImage, signalTimeColor);
            devEvents.StartCoroutine(DisableTimeSignal_Delay(devEvents.timeSignalPrefabImage));
        }
    }
    public static void CreateCrosshairSignal() {
        if (!crosshairSignalRunning) {
            timeSignalRunning = true;
            NewSignalEvent(devEvents.crosshairSignalPrefabImage, signalCrosshairColor);
            devEvents.StartCoroutine(DisableCrossahirSignal_Delay(devEvents.crosshairSignalPrefabImage));
        }
    }
    public static void CreateTargetsSignal() {
        if (!targetsSignalRunning) {
            targetsSignalRunning = true;
            NewSignalEvent(devEvents.targetsSignalPrefabImage, signalTargetsColor);
            devEvents.StartCoroutine(DisableTargetsSignal_Delay(devEvents.targetsSignalPrefabImage));
        }
    }
    public static void CreateInterfaceSignal() {
        if (!interfaceSignalRunning) {
            interfaceSignalRunning = true;
            NewSignalEvent(devEvents.interfaceSignalPrefabImage, signalInterfaceColor);
            devEvents.StartCoroutine(DisableInterfaceSignal_Delay(devEvents.interfaceSignalPrefabImage));
        }
    }
    public static void CreateSaveSignal() {
        if (!saveSignalRunning) {
            saveSignalRunning = true;
            NewSignalEvent(devEvents.saveSignalPrefabImage, signalSaveColor);
            devEvents.StartCoroutine(DisableSaveSignal_Delay(devEvents.saveSignalPrefabImage));
        }
    }
    public static void CreateSkyboxSignal() {
        if (!skyboxSignalRunning) {
            skyboxSignalRunning = true;
            NewSignalEvent(devEvents.skyboxSignalPrefabImage, signalSkyboxColor);
            devEvents.StartCoroutine(DisableSkyboxSignal_Delay(devEvents.skyboxSignalPrefabImage));
        }
    }
    public static void CreateLanguageSignal() {
        if (!languageSignalRunning) {
            languageSignalRunning = true;
            NewSignalEvent(devEvents.languageSignalPrefabImage, signalLanguageColor);
            devEvents.StartCoroutine(DisableLanguageSignal_Delay(devEvents.languageSignalPrefabImage));
        }
    }
    public static void CreateKeybindSignal() {
        if (!keybindSignalRunning) {
            keybindSignalRunning = true;
            NewSignalEvent(devEvents.keybindSignalPrefabImage, signalKeybindColor);
            devEvents.StartCoroutine(DisableKeybindSignal_Delay(devEvents.keybindSignalPrefabImage));
        }
    }
    public static void CreateSoundSignal() {
        if (!soundSignalRunning) {
            soundSignalRunning = true;
            NewSignalEvent(devEvents.soundSignalPrefabImage, signalSoundColor);
            devEvents.StartCoroutine(DisableSoundSignal_Delay(devEvents.soundSignalPrefabImage));
        }
    }
    public static void CreateNotificationSignal() {
        if (!notificationSignalRunning) {
            notificationSignalRunning = true;
            NewSignalEvent(devEvents.notificationSignalPrefabImage, signalNotificationColor);
            devEvents.StartCoroutine(DisableNotificationSignal_Delay(devEvents.notificationSignalPrefabImage));
        }
    }
    public static void CreateStatsSignal() {
        if (!statsSignalRunning) {
            statsSignalRunning = true;
            NewSignalEvent(devEvents.statsSignalPrefabImage, signalStatsColor);
            devEvents.StartCoroutine(DisableStatsSignal_Delay(devEvents.statsSignalPrefabImage));
        }
    }

    public static void NewSignalEvent(Image signalPrefabImage, Color signalColor) {
        signalPrefabImage.color = signalColor;
        eventCount++;
        devEvents.eventCountText.SetText($"{eventCount}");
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
    public static IEnumerator DisableGamemodeSignal_Delay(Image signalImage) {
        yield return signalDestroyDelay;
        signalImage.color     = new Color(signalImage.color.r, signalImage.color.g, signalImage.color.b, 0.15f);
        gamemodeSignalRunning = false;
    }
    public static IEnumerator DisableTimeSignal_Delay(Image signalImage) {
        yield return signalDestroyDelay;
        signalImage.color = new Color(signalImage.color.r, signalImage.color.g, signalImage.color.b, 0.15f);
        timeSignalRunning = false;
    }
    public static IEnumerator DisableCrossahirSignal_Delay(Image signalImage) {
        yield return signalDestroyDelay;
        signalImage.color      = new Color(signalImage.color.r, signalImage.color.g, signalImage.color.b, 0.15f);
        crosshairSignalRunning = false;
    }
    public static IEnumerator DisableTargetsSignal_Delay(Image signalImage) {
        yield return signalDestroyDelay;
        signalImage.color    = new Color(signalImage.color.r, signalImage.color.g, signalImage.color.b, 0.15f);
        targetsSignalRunning = false;
    }
    public static IEnumerator DisableInterfaceSignal_Delay(Image signalImage) {
        yield return signalDestroyDelay;
        signalImage.color      = new Color(signalImage.color.r, signalImage.color.g, signalImage.color.b, 0.15f);
        interfaceSignalRunning = false;
    }
    public static IEnumerator DisableSaveSignal_Delay(Image signalImage) {
        yield return signalDestroyDelay;
        signalImage.color = new Color(signalImage.color.r, signalImage.color.g, signalImage.color.b, 0.15f);
        saveSignalRunning = false;
    }
    public static IEnumerator DisableSkyboxSignal_Delay(Image signalImage) {
        yield return signalDestroyDelay;
        signalImage.color   = new Color(signalImage.color.r, signalImage.color.g, signalImage.color.b, 0.15f);
        skyboxSignalRunning = false;
    }
    public static IEnumerator DisableLanguageSignal_Delay(Image signalImage) {
        yield return signalDestroyDelay;
        signalImage.color     = new Color(signalImage.color.r, signalImage.color.g, signalImage.color.b, 0.15f);
        languageSignalRunning = false;
    }
    public static IEnumerator DisableKeybindSignal_Delay(Image signalImage) {
        yield return signalDestroyDelay;
        signalImage.color    = new Color(signalImage.color.r, signalImage.color.g, signalImage.color.b, 0.15f);
        keybindSignalRunning = false;
    }
    public static IEnumerator DisableSoundSignal_Delay(Image signalImage) {
        yield return signalDestroyDelay;
        signalImage.color  = new Color(signalImage.color.r, signalImage.color.g, signalImage.color.b, 0.15f);
        soundSignalRunning = false;
    }
    public static IEnumerator DisableNotificationSignal_Delay(Image signalImage) {
        yield return signalDestroyDelay;
        signalImage.color         = new Color(signalImage.color.r, signalImage.color.g, signalImage.color.b, 0.15f);
        notificationSignalRunning = false;
    }
    public static IEnumerator DisableStatsSignal_Delay(Image signalImage) {
        yield return signalDestroyDelay;
        signalImage.color  = new Color(signalImage.color.r, signalImage.color.g, signalImage.color.b, 0.15f);
        statsSignalRunning = false;
    }
}