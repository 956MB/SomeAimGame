using UnityEngine;

public class StatsUtil : MonoBehaviour {
    public static string itemHighscore     = "▲▲";
    public static string itemHighscoreFlip = "▼▼";
    public static string itemUp            = "▲";
    public static string itemDown          = "▼";
    public static string itemNeutral       = "-";
    
    // Brighter:
    public static Color32 newHighscoreBackgroundColor = new Color32(255, 255, 0, 100);
    public static Color32 upBackgroundColor           = new Color32(0, 255, 0, 70);
    public static Color32 downBackgroundColor         = new Color32(255, 0, 0, 70);

    //public static Color32 newHighscoreBackgroundColor = new Color32(255, 209, 0, 65);
    //public static Color32 upBackgroundColor           = new Color32(0, 255, 0, 20);
    //public static Color32 downBackgroundColor         = new Color32(255, 0, 0, 20);
    public static Color32 neutralBackgroundColor      = new Color32(255, 255, 255, 15);
    public static Color32 itemColorRed                = new Color32(255, 0, 0, 255);
    public static Color32 itemColorGreen              = new Color32(0, 255, 0, 255);
    public static Color32 itemColorGrey               = new Color32(255, 255, 255, 85);
    public static Color32 itemColorHighscore          = new Color32(255, 209, 0, 255);
    public static Color32 upLineColor                 = new Color32(0, 255, 0, 150);
    public static Color32 downLineColor               = new Color32(255, 0, 0, 150);
    public static Color32 neutralLineColor            = new Color32(255, 255, 255, 35);
    public static Color32 highscoreLineColor          = new Color32(255, 209, 0, 255);
    public static Color32 clearBackgroundLight        = new Color32(255, 255, 255, 15);
    public static Color32 clearBackgroundDark         = new Color32(0, 0, 0, 0);

    public static string GetItemText(double newValue, double oldValue, double highscoreValue) {
        if (newValue < oldValue) return itemDown;
        else if (newValue > oldValue)
            if (newValue > highscoreValue) return itemHighscore;
            else return itemUp;
        else return itemNeutral;
    }

    public static string GetItemText_Flip(double newValue, double oldValue, double highscoreValue) {
        if (newValue > oldValue) return itemUp;
        else if (newValue < oldValue)
            if (newValue < highscoreValue) return itemHighscoreFlip;
            else return itemDown;
        else return itemNeutral;
    }

    public static Color32 GetItemColor(double newValue, double oldValue, double highscoreValue) {
        if (newValue < oldValue) return itemColorRed;
        else if (newValue > oldValue)
            if (newValue > highscoreValue) return itemColorHighscore;
            else return itemColorGreen;
        else return itemColorGrey;
    }

    public static Color32 GetLineColor(double newValue, double oldValue, double highscoreValue) {
        if (newValue < oldValue) return downLineColor;
        else if (newValue > oldValue)
            if (newValue > highscoreValue) return highscoreLineColor;
            else return upLineColor;
        else return neutralLineColor;
    }

    public static Color32 GetItemColor_Flip(double newValue, double oldValue, double highscoreValue) {
        if (newValue < oldValue)
            if (newValue < highscoreValue) return itemColorHighscore;
            else return itemColorGreen;
        else if (newValue > oldValue) return itemColorRed;
        else return itemColorGrey;
    }

    public static Color32 GetItemBackgroundColor(double newValue, double oldValue, double highscoreValue) {
        if (newValue < oldValue) return downBackgroundColor;
        else if (newValue > oldValue)
            if (newValue > highscoreValue) return newHighscoreBackgroundColor;
            else return upBackgroundColor;
        else return neutralBackgroundColor;
    }

    public static Color32 GetItemBackgroundColor_Flip(int newValue, int oldValue, int highscoreValue) {
        if (newValue < oldValue)
            if (newValue < highscoreValue) return newHighscoreBackgroundColor;
            else return upBackgroundColor;
        else if (newValue > oldValue) return downBackgroundColor;
        else return neutralBackgroundColor;
    }

    public static bool CheckHighestStatValue(double newValue, double highestValue) {
        if (newValue > highestValue) return true;
        else return false;
    }

    public static bool CheckHighestStatValue_Flip(int newValue, int highestValue) {
        if (newValue > highestValue) return false;
        else if (newValue == 0) return false;
        else return true;
    }

    public static double CheckDifference(double newValue, double oldValue) {
        if (newValue > oldValue) return newValue - oldValue;
        else if (newValue < oldValue) return oldValue - newValue;
        else return newValue - oldValue;
    }

    public static double CheckDifference_Percent(double newValue, double oldValue) {
        if (newValue > oldValue) return (int)((newValue - oldValue) * 100) / oldValue;
        else if (newValue < oldValue) return (int)((oldValue - newValue) * 100) / newValue;
        else return 0;
    }

    public static string CheckDifference_Symbol(double newValue, double oldValue) {
        if (newValue > oldValue) return "+ ";
        else if (newValue < oldValue) return "- ";
        else return "";
    }
}
