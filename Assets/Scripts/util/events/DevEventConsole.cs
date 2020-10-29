using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// 0. Title bar
// 1. Menu bar
// 2. Upper panel scroll view
// 3. Lower panel text
//      I---------------I---------------------------------------------------------------I
//  0.  I   0.0         I                                                               I
//      I---------------I---------------------------------------------------------------I
//      I-----I-I-------I-------I-------I-----------------------I-------I-------I-------I
//  1.  I 1.0 I I  1.1  I  1.2  I  1.3  I     <-   1.4   -->    I  1.5  I  1.6  I  1.7  I
//      I-----I-I-------I-------I-------I-----------------------I-------I-------I-------I
//      I                                                                         I     I
//      I                                                                         I     I
//      I                                                                         I     I
//  2.  I                                      2.0                                I 2.1 I
//      I                                                                         I     I
//      I                                                                         I     I
//      I                                                                         I     I
//      I-------------------------------------------------------------------------------I
//      I                                                                               I
//  3.  I                                      3.0                                      I
//      I                                                                               I
//      I-------------------------------------------------------------------------------I

public class DevEventConsole : EditorWindow {
    private Rect upperPanel, lowerPanel, resizer, menuBar, filterBar;
    private GUIStyle resizerStyle, boxStyle, textAreaStyle;

    private float sizeRatio     = 0.5f;
    private float resizerHeight = 5f;
    private float menuBarHeight = 20f;
    private float filterBarHeight = 20f;
    private bool isResizing;

    // Util toggles
    private bool collapse     = false;
    private bool clearOnPlay  = false;
    private bool errorPause   = false;
    private bool showLog      = false;
    private bool showWarnings = false;
    private bool showErrors   = false;

    // Filters
    private bool showGamemode  = false;
    private bool showTime      = false;
    private bool showCrosshair = false;

    private bool showAll = false;

    private Vector2 upperPanelScroll;
    private Vector2 lowerPanelScroll;

    private Texture2D boxBgOdd, boxBgEven, boxBgSelected, icon, errorIcon, errorIconSmall, warningIcon, warningIconSmall, infoIcon, infoIconSmall;

    public Texture2D gamemodeBarLeft, timeBarLeft, crosshairBarLeft, targetsBarLeft, interfaceBarLeft, saveBarLeft, skyboxBarLeft, languageBarLeft, keybindBarLeft, soundBarLeft, notificationBarLeft, statsBarLeft, defaultBarLeft;

    private List<DevEvent> devEvents;
    private DevEvent selectedDevEvent;

    [MenuItem("Window/DevEvent Console")]
    private static void OpenWindow() {
        DevEventConsole devEventWindow = GetWindow<DevEventConsole>();
        devEventWindow.titleContent    = new GUIContent("DevEvent Console");
    }

    private void OnEnable() {
        // Load icon resource
        errorIcon        = EditorGUIUtility.Load("icons/console.erroricon.png") as Texture2D;
        warningIcon      = EditorGUIUtility.Load("icons/console.warnicon.png") as Texture2D;
        infoIcon         = EditorGUIUtility.Load("icons/console.infoicon.png") as Texture2D;
        errorIconSmall   = EditorGUIUtility.Load("icons/console.erroricon.sml.png") as Texture2D;
        warningIconSmall = EditorGUIUtility.Load("icons/console.warnicon.sml.png") as Texture2D;
        infoIconSmall    = EditorGUIUtility.Load("icons/console.infoicon.sml.png") as Texture2D;

        gamemodeBarLeft     = Resources.Load<Texture2D>("Images/gamemodeBarLeft");
        timeBarLeft         = Resources.Load<Texture2D>("Images/timeBarLeft");
        crosshairBarLeft    = Resources.Load<Texture2D>("Images/crosshairBarLeft");
        targetsBarLeft      = Resources.Load<Texture2D>("Images/targetsBarLeft");
        interfaceBarLeft    = Resources.Load<Texture2D>("Images/interfaceBarLeft");
        saveBarLeft         = Resources.Load<Texture2D>("Images/saveBarLeft");
        skyboxBarLeft       = Resources.Load<Texture2D>("Images/skyboxBarLeft");
        languageBarLeft     = Resources.Load<Texture2D>("Images/languageBarLeft");
        keybindBarLeft      = Resources.Load<Texture2D>("Images/keybindBarLeft");
        soundBarLeft        = Resources.Load<Texture2D>("Images/soundBarLeft");
        notificationBarLeft = Resources.Load<Texture2D>("Images/notificationBarLeft");
        statsBarLeft        = Resources.Load<Texture2D>("Images/statsBarLeft");
        defaultBarLeft      = Resources.Load<Texture2D>("Images/defaultBarLeft");

        // 
        resizerStyle = new GUIStyle();
        resizerStyle.normal.background = EditorGUIUtility.Load("icons/d_AvatarBlendBackground.png") as Texture2D;

        boxStyle = new GUIStyle();
        boxStyle.normal.textColor = new Color(0.8f, 0.8f, 0.8f);

        // Load panel background resources
        boxBgOdd      = EditorGUIUtility.Load("builtin skins/darkskin/images/cn entrybackodd.png") as Texture2D;
        boxBgEven     = EditorGUIUtility.Load("builtin skins/darkskin/images/cnentrybackeven.png") as Texture2D;
        boxBgSelected = EditorGUIUtility.Load("builtin skins/darkskin/images/menuitemhover.png") as Texture2D;

        textAreaStyle = new GUIStyle();
        textAreaStyle.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
        textAreaStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/projectbrowsericonareabg.png") as Texture2D;

        devEvents = new List<DevEvent>();
        selectedDevEvent = null;
    }

    private void OnGUI() {
        DrawMenuBar();
        DrawFilterBar();
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
        menuBar = new Rect(0, 0, position.width, menuBarHeight);

        GUILayout.BeginArea(menuBar, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();

        // 1.0
        GUILayout.Button(new GUIContent("Clear"), EditorStyles.toolbarButton);
        GUILayout.Space(5);

        // Left side toggle group
        collapse    = GUILayout.Toggle(collapse, new GUIContent("Collapse"), EditorStyles.toolbarButton); // 1.1
        clearOnPlay = GUILayout.Toggle(clearOnPlay, new GUIContent("Clear On Play"), EditorStyles.toolbarButton); // 1.2
        errorPause  = GUILayout.Toggle(errorPause, new GUIContent("Error Pause"), EditorStyles.toolbarButton); // 1.3

        // 1.4 Middle flexible space
        GUILayout.FlexibleSpace();

        // Right side toggle group
        showLog = GUILayout.Toggle(showLog, new GUIContent("L", infoIconSmall), EditorStyles.toolbarButton, GUILayout.Width(30)); // 1.5
        showWarnings = GUILayout.Toggle(showWarnings, new GUIContent("W", warningIconSmall), EditorStyles.toolbarButton, GUILayout.Width(30)); // 1.6
        showErrors = GUILayout.Toggle(showErrors, new GUIContent("E", errorIconSmall), EditorStyles.toolbarButton, GUILayout.Width(30)); // 1.7

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    /// <summary>
    /// Draws top filter bar holding toggles for showing/hiding different events in console.
    /// </summary>
    private void DrawFilterBar() {
        filterBar = new Rect(0, filterBarHeight, position.width, filterBarHeight);

        GUILayout.BeginArea(filterBar, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();

        showGamemode  = GUILayout.Toggle(showGamemode, new GUIContent("Show Gamemode", warningIconSmall), EditorStyles.toolbarButton);
        showTime      = GUILayout.Toggle(showTime, new GUIContent("Show Time", warningIconSmall), EditorStyles.toolbarButton);
        showCrosshair = GUILayout.Toggle(showCrosshair, new GUIContent("Show Crossahair", warningIconSmall), EditorStyles.toolbarButton);

        GUILayout.FlexibleSpace();
        
        showAll = GUILayout.Toggle(showAll, new GUIContent("Show All"), EditorStyles.toolbarButton, GUILayout.Width(60));

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    /// <summary>
    /// Draws upper panel holding console output content
    /// </summary>
    private void DrawUpperPanel() {
        upperPanel = new Rect(0, menuBarHeight * 2, position.width, (position.height * sizeRatio) - menuBarHeight * 2);

        // 2.0 upper panel
        GUILayout.BeginArea(upperPanel);
        //GUILayout.Label("Upper Panel");

        // 2.1 Upper panel right scroll bar
        upperPanelScroll = GUILayout.BeginScrollView(upperPanelScroll);

        // Loop events in dev events object list

        // Draw boxed test:
        //LoopDevEvents();
        DrawDevEventBox("[23:47:14]", DevEventType.Gamemode, "Hello, World!", "Hello, World EXTRA!", true, false);
        DrawDevEventBox("[23:47:14]", DevEventType.Time, "Time here!", "EXTRA", false, false);
        DrawDevEventBox("[23:47:14]", DevEventType.Crosshair, "Crossahair here!", "CROSSHAIR EXTRA", true, false);
        //DrawDevEventBox("ResizablePanels here!", LogType.Log, false, false);
        //DrawDevEventBox("How do I look?", LogType.Warning, true, false);
        //DrawDevEventBox("The lower panel doesn't seem to be working.", LogType.Error, false, false);
        //DrawDevEventBox("You should start working on that.", LogType.Warning, true, false);

        GUILayout.EndScrollView();

        GUILayout.EndArea();
    }

    /// <summary>
    /// Draws lower panel holding console item extended text content
    /// </summary>
    private void DrawLowerPanel() {
        lowerPanel = new Rect(0, (position.height * sizeRatio) + resizerHeight, position.width, (position.height * (1 - sizeRatio)) - resizerHeight);

        // 3.0 lower panel
        GUILayout.BeginArea(lowerPanel);
        //GUILayout.Label("Lower Panel");

        // Lower panel text content scroll view
        lowerPanelScroll = GUILayout.BeginScrollView(lowerPanelScroll);
        //GUILayout.TextArea("It is working now!", textAreaStyle);
        GUILayout.EndScrollView();

        GUILayout.EndArea();
    }

    /// <summary>
    /// Redraws panels if resized
    /// </summary>
    private void DrawResizer() {
        resizer = new Rect(0, (position.height * sizeRatio) - resizerHeight, position.width, resizerHeight * 2);

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

        // DevEvent type color:
        //var boxStyleCopy = boxStyle;
        string eventTypeColor = ReturnDevEventTypeColor(devEventType);
        string eventTypeText = ReturnDevEventTypeText(devEventType);
        Texture2D eventTypeBarLeft = ReturnDevEventTypeBarLeft(devEventType);

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
            Repaint();
        }
    }

    // Util:

    /// <summary>
    /// Loop dev events object and draw boxes for events.
    /// </summary>
    private void LoopDevEvents() {
        for (int i = 0; i < devEvents.Count; i++) {
            if (DrawDevEventBox(devEvents[i].eventTime, devEvents[i].eventType, devEvents[i].eventContent, devEvents[i].eventExtra, i % 2 == 0, devEvents[i].isSelected)) {
                if (selectedDevEvent != null) {
                    selectedDevEvent.isSelected = false;
                }

                devEvents[i].isSelected = true;
                selectedDevEvent = devEvents[i];
                GUI.changed = true;
            }
        }
    }

    private string ReturnDevEventTypeColor(DevEventType devEventType) {
        switch (devEventType) {
            case DevEventType.Gamemode:     return "#FF9100";
            case DevEventType.Time:         return "#FFB700";
            case DevEventType.Crosshair:    return "#00B8FF";
            case DevEventType.Targets:      return "#B100FF";
            case DevEventType.Interface:    return "#00FF00";
            case DevEventType.Save:         return "#FF0000";
            case DevEventType.Skybox:       return "#6E00FF";
            case DevEventType.Language:     return "#00FFE3";
            case DevEventType.Keybind:      return "#FF0088";
            case DevEventType.Sound:        return "#0055FF";
            case DevEventType.Notification: return "#C1FF00";
            case DevEventType.Stats:        return "#FFFFFF";
            default:                        return "#DBDBDB";
        }
    }

    private Texture2D ReturnDevEventTypeBarLeft(DevEventType devEventType) {
        switch (devEventType) {
            case DevEventType.Gamemode:     return gamemodeBarLeft;
            case DevEventType.Time:         return timeBarLeft;
            case DevEventType.Crosshair:    return crosshairBarLeft;
            case DevEventType.Targets:      return targetsBarLeft;
            case DevEventType.Interface:    return interfaceBarLeft;
            case DevEventType.Save:         return saveBarLeft;
            case DevEventType.Skybox:       return skyboxBarLeft;
            case DevEventType.Language:     return languageBarLeft;
            case DevEventType.Keybind:      return keybindBarLeft;
            case DevEventType.Sound:        return soundBarLeft;
            case DevEventType.Notification: return notificationBarLeft;
            case DevEventType.Stats:        return statsBarLeft;
            default:                        return defaultBarLeft;
        }
    }

    private string ReturnDevEventTypeText(DevEventType devEventType) {
        switch (devEventType) {
            case DevEventType.Gamemode:     return "GAMEMODE";
            case DevEventType.Time:         return "TIME             ";
            case DevEventType.Crosshair:    return "CROSSHAIR ";
            case DevEventType.Targets:      return "TARGETS";
            case DevEventType.Interface:    return "INTERFACE";
            case DevEventType.Save:         return "SAVE";
            case DevEventType.Skybox:       return "SKYBOX";
            case DevEventType.Language:     return "LANGUAGE";
            case DevEventType.Keybind:      return "KEYBIND";
            case DevEventType.Sound:        return "SOUND";
            case DevEventType.Notification: return "NOTIFICATION";
            case DevEventType.Stats:        return "STATS";
            default:                        return "DEFAULT";
        }
    }
}