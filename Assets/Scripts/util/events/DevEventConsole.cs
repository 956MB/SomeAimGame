using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

// 0. Title bar
// 1. Menu bar
// 2. Upper panel scroll view
// 3. Lower panel
//
//      I---------------I---------------------------------------------------------------I
//  0.  I   0.0         I                                                               I
//      I---------------I---------------------------------------------------------------I
//      I-----I-I-------I-------I-------I-----------------------I-------I-------I-------I
//  1.  I 1.0 I I  1.1  I  1.2  I  1.3  I     <-   1.4   -->    I  1.5  I  1.6  I  1.7  I
//      I-----I-I-------I-------I-------I-----------------------I-------I-------I-------I
//      I  2.0                                                                    I     I
//      I-------------------------------------------------------------------------I     I
//      I  2.1                                                                    I     I
//      I-------------------------------------------------------------------------I     I
//      I  2.1                                                                    I     I
//  2.  I-------------------------------------------------------------------------I 2.3 I
//      I  2.2                                                                    I     I
//      I-------------------------------------------------------------------------I     I
//      I                                                                         I     I
//      I                                                                         I     I
//      I                                                                         I     I
//      I-------------------------------------------------------------------------------I
//      I------------------I--------------------I--------------------I------------------I
//      I    <-- 3.0 -->   I     <-- 3.1 -->    I     <-- 3.2 -->    I    <-- 3.3 -->   I
//      I------------------I--------------------I--------------------I------------------I
//      I------------------I--------------------I--------------------I------------------I
//  3.  I    <-- 3.4 -->   I     <-- 3.5 -->    I     <-- 3.6 -->    I    <-- 3.7 -->   I
//      I------------------I--------------------I--------------------I------------------I
//      I------------------I--------------------I--------------------I------------------I
//      I    <-- 3.8 -->   I     <-- 3.9 -->    I    <-- 3.10 -->    I   <-- 3.11 -->   I
//      I------------------I--------------------I--------------------I------------------I
//      I------------------I--------------------I--------------------I------------------I

public class DevEventConsole : EditorWindow {
    private Rect upperPanel, lowerPanel1, lowerPanel2, lowerPanel3, resizer, menuBar;
    private Rect filterBar1, filterBar2, filterBar3;
    private GUIStyle resizerStyle, boxStyle, textAreaStyle;

    private float sizeRatio     = 0.5f;
    private float resizerHeight = 5f;
    private float barHeight     = 20f;
    private bool isResizing;

    // Util toggles
    private bool collapse, clearOnPlay, errorPause, showLog, showWarnings, showErrors;

    // Filter toggles
    private bool showGamemode, showTime, showCrosshair, showTargets, showInterface, showSave, showSkybox, showLanguage, showKeybind, showSound, showNotification, showStats, showAll;

    private Vector2 upperPanelScroll, lowerPanelScroll;

    private Texture2D boxBgOdd, boxBgEven, boxBgSelected, icon, errorIcon, errorIconSmall, warningIcon, warningIconSmall, infoIcon, infoIconSmall;

    public static List<DevEvent> devEvents;
    public DevEvent selectedDevEvent;
    private int devEventCount = 5;

    public static DevEventConsole devEventsConsole;
    private void Awake() {
        devEventsConsole = this;
    }

        [MenuItem("Window/DevEvent Console")]
    private static void OpenWindow() {
        DevEventConsole devEventWindow = GetWindow<DevEventConsole>();
        // 0.0
        devEventWindow.titleContent    = new GUIContent("DevEvent Console");
    }

    private void OnEnable() {
        DevEventUtil.LoadIconResources();

        // Load icon resource
        errorIcon        = EditorGUIUtility.Load("icons/console.erroricon.png") as Texture2D;
        warningIcon      = EditorGUIUtility.Load("icons/console.warnicon.png") as Texture2D;
        infoIcon         = EditorGUIUtility.Load("icons/console.infoicon.png") as Texture2D;
        errorIconSmall   = EditorGUIUtility.Load("icons/console.erroricon.sml.png") as Texture2D;
        warningIconSmall = EditorGUIUtility.Load("icons/console.warnicon.sml.png") as Texture2D;
        infoIconSmall    = EditorGUIUtility.Load("icons/console.infoicon.sml.png") as Texture2D;

        // 
        resizerStyle                   = new GUIStyle();
        resizerStyle.normal.background = EditorGUIUtility.Load("icons/d_AvatarBlendBackground.png") as Texture2D;

        boxStyle                  = new GUIStyle();
        boxStyle.normal.textColor = new Color(0.8f, 0.8f, 0.8f);

        // Load panel background resources
        boxBgOdd      = EditorGUIUtility.Load("builtin skins/darkskin/images/cn entrybackodd.png") as Texture2D;
        boxBgEven     = EditorGUIUtility.Load("builtin skins/darkskin/images/cnentrybackeven.png") as Texture2D;
        boxBgSelected = EditorGUIUtility.Load("builtin skins/darkskin/images/menuitemhover.png") as Texture2D;

        textAreaStyle                   = new GUIStyle();
        textAreaStyle.normal.textColor  = new Color(0.9f, 0.9f, 0.9f);
        textAreaStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/projectbrowsericonareabg.png") as Texture2D;

        devEvents        = new List<DevEvent>();
        selectedDevEvent = null;
    }

    private void OnGUI() {
        DrawAllConsole();
    }
    public void OnInspectorUpdate() {
        // This will only get called 10 times per second.
        if (DevEventHandler.logsOn) { Repaint(); }
    }

    private void DrawAllConsole() {
        DrawMenuBar();
        //DrawFilterBar1();
        DrawUpperPanel();
        DrawLowerPanel();
        DrawResizer();

        ProcessEvents(Event.current);

        if (GUI.changed) Repaint();
    }

    /// <summary>
    /// Draws top menu bar holding normal buttons and toggles
    /// </summary>
    private void DrawMenuBar() {
        menuBar = new Rect(0, 0, position.width, barHeight);

        GUILayout.BeginArea(menuBar, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();

        // 1.0
        GUILayout.Button(new GUIContent("Clear"), EditorStyles.toolbarButton);
        GUILayout.Space(5f);

        // Left side toggle group
        collapse    = GUILayout.Toggle(collapse, new GUIContent("Collapse"), EditorStyles.toolbarButton); // 1.1
        clearOnPlay = GUILayout.Toggle(clearOnPlay, new GUIContent("Clear On Play"), EditorStyles.toolbarButton); // 1.2
        //errorPause  = GUILayout.Toggle(errorPause, new GUIContent("Error Pause"), EditorStyles.toolbarButton); // 1.3
        //showAll = GUILayout.Toggle(showAll, new GUIContent("Show All"), EditorStyles.toolbarButton, GUILayout.Width(60));

        // 1.4 Middle flexible space
        GUILayout.FlexibleSpace();

        // Right side toggle group
        showLog      = GUILayout.Toggle(showLog, new GUIContent($"{devEventCount}", infoIconSmall), EditorStyles.toolbarButton, GUILayout.Width(30)); // 1.5
        showWarnings = GUILayout.Toggle(showWarnings, new GUIContent("5", warningIconSmall), EditorStyles.toolbarButton, GUILayout.Width(30)); // 1.6
        showErrors   = GUILayout.Toggle(showErrors, new GUIContent("0", errorIconSmall), EditorStyles.toolbarButton, GUILayout.Width(30)); // 1.7

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    /// <summary>
    /// Draws top filter bar holding toggles for showing/hiding different events in console.
    /// </summary>
    private void DrawFilterBar1() {
        //filterBar1 = new Rect(0, barHeight, position.width, barHeight);
        filterBar1 = new Rect(0, ((position.height * sizeRatio) + resizerHeight) + barHeight*2, position.width, (position.height * (1 - sizeRatio)) - resizerHeight);
        filterBar2 = new Rect(0, ((position.height * sizeRatio) + resizerHeight), position.width, (position.height * (1 - sizeRatio)) - resizerHeight);

        // 1.
        GUILayout.BeginArea(filterBar1, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();
        showGamemode  = GUILayout.Toggle(showGamemode, new GUIContent("Gamemode", DevEventUtil.gamemodeBarLeft), EditorStyles.toolbarButton);
        showTime      = GUILayout.Toggle(showTime, new GUIContent("Time", DevEventUtil.timeBarLeft), EditorStyles.toolbarButton);
        showCrosshair = GUILayout.Toggle(showCrosshair, new GUIContent("Crossahair", DevEventUtil.crosshairBarLeft), EditorStyles.toolbarButton);
        showTargets   = GUILayout.Toggle(showTargets, new GUIContent("Gamemode", DevEventUtil.targetsBarLeft), EditorStyles.toolbarButton);
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        // 2.
        GUILayout.BeginArea(filterBar2, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();
        showInterface = GUILayout.Toggle(showInterface, new GUIContent("Time", DevEventUtil.interfaceBarLeft), EditorStyles.toolbarButton);
        showSave      = GUILayout.Toggle(showSave, new GUIContent("Crossahair", DevEventUtil.crosshairBarLeft), EditorStyles.toolbarButton);
        showSkybox    = GUILayout.Toggle(showSkybox, new GUIContent("Skybox", DevEventUtil.skyboxBarLeft), EditorStyles.toolbarButton);
        showLanguage  = GUILayout.Toggle(showLanguage, new GUIContent("Language", DevEventUtil.languageBarLeft), EditorStyles.toolbarButton);
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void DrawFilterBar2() {
        filterBar2 = new Rect(0, barHeight, position.width, barHeight);

        GUILayout.BeginArea(filterBar2, EditorStyles.toolbar);

        // 1.
        GUILayout.BeginHorizontal();
        showInterface = GUILayout.Toggle(showInterface, new GUIContent("Time", DevEventUtil.interfaceBarLeft), EditorStyles.toolbarButton);
        showSave      = GUILayout.Toggle(showSave, new GUIContent("Crossahair", DevEventUtil.saveBarLeft), EditorStyles.toolbarButton);
        GUILayout.EndHorizontal();

        //GUILayout.FlexibleSpace();
        GUILayout.EndArea();
    }

    /// <summary>
    /// Draws upper panel holding console output content
    /// </summary>
    private void DrawUpperPanel() {
        upperPanel = new Rect(0, barHeight, position.width, (position.height * sizeRatio) - barHeight * 2);

        // 2. upper panel
        GUILayout.BeginArea(upperPanel);
        //GUILayout.Label("Upper Panel");

        // 2.3 Upper panel right scroll bar
        upperPanelScroll = GUILayout.BeginScrollView(upperPanelScroll);
        //upperPanelScroll = GUILayout.BeginScrollView(new Vector2(0, upperPanel.height));

        // Loop events in dev events object list

        // Draw boxed test:
        //DrawDevEventBox("[23:47:14]", DevEventType.Stats, "TEST LOG HERE", "", true, false); // 2.0
        LoopDevEvents();

        //DrawDevEventBox("[23:47:14]", DevEventType.Gamemode, "'Follow' GAMEMODE SELECTED", "Hello, World EXTRA!", true, false); // 2.0
        //DrawDevEventBox("[23:47:14]", DevEventType.Time, "GAME TIMER CHANGED TO '00:60'", "EXTRA", false, false); // 2.1
        //DrawDevEventBox("[23:47:14]", DevEventType.Crosshair, "CROSSHAIR GAP CHANGED TO '6", "CROSSHAIR EXTRA", true, false); // 2.2
        //DrawDevEventBox("[23:47:14]", DevEventType.Targets, "[Scatter] NEW PRIMARY TARGET SPAWN: (13,42,90).", "Targets EXTRA", false, false); // 2.3
        //DrawDevEventBox("[23:47:14]", DevEventType.Interface, "FPS Counter WIDGET TOGGLE SET", "Interface EXTRA", true, false); // 2.4
        //DrawDevEventBox("[23:47:14]", DevEventType.Save, "'Cosmetics' SETTINGS SAVED", "Save EXTRA", false, false); // 2.5

        GUILayout.EndScrollView();

        GUILayout.EndArea();
    }

    /// <summary>
    /// Draws lower panel holding console item extended text content
    /// </summary>
    private void DrawLowerPanel() {
        lowerPanel1 = new Rect(0, (position.height * sizeRatio) + resizerHeight, position.width, (position.height * (1 - sizeRatio)) - resizerHeight);
        lowerPanel2 = new Rect(0, ((position.height * sizeRatio) + resizerHeight) + barHeight + resizerHeight, position.width, (position.height * (1 - sizeRatio)) - resizerHeight);
        lowerPanel3 = new Rect(0, ((position.height * sizeRatio) + resizerHeight) + (barHeight*2) + (resizerHeight*2), position.width, (position.height * (1 - sizeRatio)) - resizerHeight);

        lowerPanelScroll = GUILayout.BeginScrollView(lowerPanelScroll);

        // 1. lower panel: gamemode, time, crosshair, targets
        GUILayout.BeginArea(lowerPanel1);
        GUILayout.BeginHorizontal();
        showGamemode  = GUILayout.Toggle(showGamemode, new GUIContent("Gamemode", DevEventUtil.gamemodeBarLeft), EditorStyles.toolbarButton); // 3.0
        GUILayout.Space(5f);
        showTime      = GUILayout.Toggle(showTime, new GUIContent("Time", DevEventUtil.timeBarLeft), EditorStyles.toolbarButton); // 3.1
        GUILayout.Space(5f);
        showCrosshair = GUILayout.Toggle(showCrosshair, new GUIContent("Crossahair", DevEventUtil.crosshairBarLeft), EditorStyles.toolbarButton); // 3.2
        GUILayout.Space(5f);
        showTargets   = GUILayout.Toggle(showTargets, new GUIContent("Targets", DevEventUtil.targetsBarLeft), EditorStyles.toolbarButton); // 3.3
        //GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        // 2. lower panel: interface, save, skybox, save
        GUILayout.BeginArea(lowerPanel2, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();
        showInterface = GUILayout.Toggle(showInterface, new GUIContent("Interface", DevEventUtil.interfaceBarLeft), EditorStyles.toolbarButton); // 3.4
        GUILayout.Space(5f);
        showSave      = GUILayout.Toggle(showSave, new GUIContent("Save", DevEventUtil.saveBarLeft), EditorStyles.toolbarButton); // 3.5
        GUILayout.Space(5f);
        showSkybox    = GUILayout.Toggle(showSkybox, new GUIContent("Skybox", DevEventUtil.skyboxBarLeft), EditorStyles.toolbarButton); // 3.6
        GUILayout.Space(5f);
        showLanguage  = GUILayout.Toggle(showLanguage, new GUIContent("Language", DevEventUtil.languageBarLeft), EditorStyles.toolbarButton); // 3.7
        //GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        // 3. lower panel: keybind, sound, notification, stats
        GUILayout.BeginArea(lowerPanel3, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();
        showKeybind      = GUILayout.Toggle(showKeybind, new GUIContent("Keybind", DevEventUtil.keybindBarLeft), EditorStyles.toolbarButton); // 3.8
        GUILayout.Space(5f);
        showSound        = GUILayout.Toggle(showSound, new GUIContent("Sound", DevEventUtil.soundBarLeft), EditorStyles.toolbarButton); // 3.9
        GUILayout.Space(5f);
        showNotification = GUILayout.Toggle(showNotification, new GUIContent("Notification", DevEventUtil.notificationBarLeft), EditorStyles.toolbarButton); // 3.10
        GUILayout.Space(5f);
        showStats        = GUILayout.Toggle(showStats, new GUIContent("Stats", DevEventUtil.statsBarLeft), EditorStyles.toolbarButton); // 3.11
        //GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        //GUILayout.TextArea("It is working now!", textAreaStyle);
        GUILayout.EndScrollView();
    }

    /// <summary>
    /// Draws resizer line above lower panel.
    /// </summary>
    private void DrawResizer() {
        resizer = new Rect(0, (position.height * sizeRatio) - resizerHeight*2.65f, position.width, resizerHeight * 2);

        GUILayout.BeginArea(new Rect(resizer.position + (Vector2.up * resizerHeight), new Vector2(position.width, 2)), resizerStyle);
        GUILayout.EndArea();

        EditorGUIUtility.AddCursorRect(resizer, MouseCursor.ResizeVertical);
    }

    /// <summary>
    /// Draws console item box in upper panel with supplied text content (content), log type (boxType), where box item is odd (isOdd) and isselected (isSelected).
    /// </summary>
    /// <param name="devEventType"></param>
    /// <param name="eventTime"></param>
    /// <param name="content"></param>
    /// <param name="isOdd"></param>
    /// <param name="isSelected"></param>
    /// <returns></returns>
    private bool DrawDevEventBox(string eventTime, DevEventType devEventType, string content, string contentExtra, bool isOdd, bool isSelected) {
        if (isSelected) {
            boxStyle.normal.background = boxBgSelected;
        } else {
            if (isOdd) {
                boxStyle.normal.background = boxBgOdd;
            } else {
                boxStyle.normal.background = boxBgEven;
            }
        }

        // DevEvent type color
        //var boxStyleCopy = boxStyle;
        string eventTypeColor      = DevEventUtil.ReturnDevEventTypeColor(devEventType);
        string eventTypeText       = DevEventUtil.ReturnDevEventTypeText(devEventType);
        Texture2D eventTypeBarLeft = DevEventUtil.ReturnDevEventTypeBarLeft(devEventType);

        //if (eventCount <= 999) { eventCount++; }

        return GUILayout.Button(new GUIContent($"<size=11> <color={eventTypeColor}><b>{eventTypeText}    </b></color></size>{eventTime}    {content}", eventTypeBarLeft), boxStyle, GUILayout.ExpandWidth(true), GUILayout.Height(25));
    }

    /// <summary>
    /// Processes input events like resize
    /// </summary>
    /// <param name="e"></param>
    private void ProcessEvents(Event e) {
        switch (e.type) {
            case EventType.MouseDown:
                if (e.button == 0 && resizer.Contains(e.mousePosition)) { isResizing = true; }
                break;

            case EventType.MouseUp:
                isResizing = false;
                //sizeRatio -= 0.0100000f;
                break;
        }

        Resize(e);
    }

    /// <summary>
    /// Resizes panel being interacted with, the repaints window
    /// </summary>
    /// <param name="e"></param>
    private void Resize(Event e) {
        if (isResizing) {
            sizeRatio = e.mousePosition.y / position.height;
            //Debug.Log($"sizeRatio: {sizeRatio}");
            Repaint();
        }
    }

    /// <summary>
    /// Loop dev events object and draw boxes for events.
    /// </summary>
    private void LoopDevEvents() {
        if (devEvents.Count <= 0) { return; }

        for (int i = 0; i < devEvents.Count; i++) {
            if (DrawDevEventBox(devEvents[i].eventTime, devEvents[i].eventType, devEvents[i].eventContent, devEvents[i].eventExtra, i % 2 == 0, devEvents[i].isSelected)) {
                if (selectedDevEvent != null) { selectedDevEvent.isSelected = false; }

                devEvents[i].isSelected = true;
                selectedDevEvent = devEvents[i];
                GUI.changed = true;
            }
        }
    }

    public static void AddNewDevEventLog(DevEvent newEventLog) {
        devEvents.Add(newEventLog);

        //try {
        //    GUI.changed = true;
        //    devEventsConsole.DrawAllConsole();
        //} catch (NullReferenceException) { }
    }
}