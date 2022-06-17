﻿using UnityEngine;

namespace SomeAimGame.Utilities {
    /// <summary>
    /// Class containing various colors used in UI.
    /// </summary>
    public class InterfaceColors {
        // Gamemode difficulties
        public static Color32 gamemodeEasyColor       = new Color32(0, 255, 0, 255);
        public static Color32 gamemodeMediumColor     = new Color32(255, 209, 0, 255);
        public static Color32 gamemodeHardColor       = new Color32(255, 0, 0, 255);
        // General UI text
        public static Color32 selectedColor           = new Color32(255, 255, 255, 255);
        public static Color32 hoveredColor            = new Color32(255, 255, 255, 190);
        public static Color32 unselectedColor         = new Color32(255, 255, 255, 120);
        public static Color32 disabledColor           = new Color32(255, 255, 255, 80);
        public static Color32 inactiveColor           = new Color32(255, 255, 255, 0);
        // Widgets
        public static Color32 widgetsHitColor         = new Color32(0, 255, 0, 255);
        public static Color32 widgetsMissColor        = new Color32(255, 0, 0, 255);
        public static Color32 widgetsBonusColor       = new Color32(255, 209, 0, 255);
        public static Color32 widgetsNeutralColor     = new Color32(255, 255, 255, 140);
        // Notifications
        public static Color32 notificationColorGreen  = new Color32(0, 255, 0, 255);
        public static Color32 notificationColorYellow = new Color32(255, 209, 0, 255);
        public static Color32 notificationColorRed    = new Color32(255, 0, 0, 255);
        public static Color32 notificationColorWhite  = new Color32(255, 255, 255, 255);

        // Backgrounds
        public static Color32 buttonBackgroundDisabled        = new Color32(255, 255, 255, 0);
        public static Color32 buttonBackgroundLight           = new Color32(49, 49, 49, 160);
        public static Color32 buttonBackgroundLight_Dropdown  = new Color32(75, 75, 75, 160);
        public static Color32 buttonBackgroundLight_hovered   = new Color32(100, 100, 100, 160);
        public static Color32 buttonBackgroundLight_GameTimer = new Color32(255, 255, 255, 10);
    }
}