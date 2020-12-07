using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace SomeAimGame.Stats {
    public class StatsUtil : MonoBehaviour {
        public static string itemHighscore     = "▲▲";
        public static string itemHighscoreFlip = "▼▼";
        public static string itemUp            = "▲";
        public static string itemDown          = "▼";
        public static string itemNeutral       = "-";
    
        public static Color32 newHighscoreBackgroundColor = new Color32(255, 255, 0, 120);
        public static Color32 upBackgroundColor           = new Color32(0, 255, 0, 70);
        public static Color32 downBackgroundColor         = new Color32(255, 0, 0, 70);
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

        /// <summary>
        /// Sets all supplied TMP_Text stat text (statDiffs) text to supplied string (textValue).
        /// </summary>
        /// <param name="textValue"></param>
        /// <param name="statDiffs"></param>
        public static void ClearStatDiffsText(string textValue, params TMP_Text[] statDiffs) {
            foreach (TMP_Text diff in statDiffs) {
                diff.SetText(textValue);
            }
        }
        /// <summary>
        /// Sets all supplied TMP_Text items (items) text and color to supplied string (itemValue) and Color32 (itemColor).
        /// </summary>
        /// <param name="itemValue"></param>
        /// <param name="itemColor"></param>
        /// <param name="statItems"></param>
        public static void SetNeutralItems(string itemValue, Color32 itemColor, params TMP_Text[] statItems) {
            foreach (TMP_Text item in statItems) {
                item.SetText(itemValue);
                item.color = itemColor;
            }
        }
        /// <summary>
        /// Sets all supplied GameObject images (statBackgrounds) colors to supplied Color32 (colorValue).
        /// </summary>
        /// <param name="colorValue"></param>
        /// <param name="statBackgrounds"></param>
        public static void ClearStatBackgrounds(Color32 colorValue, params GameObject[] statBackgrounds) {
            foreach (GameObject background in statBackgrounds) {
                background.GetComponent<Image>().color = colorValue;
            }
        }
        /// <summary>
        /// Sets all supplied GameObject images (statBackgrounds) colors to supplied Color32 array items (colorArray).
        /// </summary>
        /// <param name="colorArray"></param>
        /// <param name="statBackgrounds"></param>
        public static void ClearStatBackgrounds(Color32[] colorArray, params GameObject[] statBackgrounds) {
            int i = 0;
            foreach (GameObject background in statBackgrounds) {
                background.GetComponent<Image>().color = colorArray[i];
                i++;
            }
        }

        /// <summary>
        /// Returns appropriate stat item string (up/down/highscore) from supplied double values (newValue) (oldValue) (highscoreValue).
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <param name="highscoreValue"></param>
        /// <returns></returns>
        public static string GetItemText(double newValue, double oldValue, double highscoreValue) {
            if (newValue < oldValue) return itemDown;
            else if (newValue > oldValue)
                if (newValue > highscoreValue) return itemHighscore;
                else return itemUp;
            else return itemNeutral;
        }
        /// <summary>
        /// Returns appropriate flipped stat item string (up/down/highscore) from supplied double values (newValue) (oldValue) (highscoreValue).
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <param name="highscoreValue"></param>
        /// <returns></returns>
        public static string GetItemText_Flip(double newValue, double oldValue, double highscoreValue) {
            if (newValue > oldValue) return itemUp;
            else if (newValue < oldValue)
                if (newValue < highscoreValue) return itemHighscoreFlip;
                else return itemDown;
            else return itemNeutral;
        }
        /// <summary>
        /// Returns appropriate item string color (green/red/yellow) from supplied double values (newValue) (oldValue) (highscoreValue).
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <param name="highscoreValue"></param>
        /// <returns></returns>
        public static Color32 GetItemColor(double newValue, double oldValue, double highscoreValue) {
            if (newValue < oldValue) return itemColorRed;
            else if (newValue > oldValue)
                if (newValue > highscoreValue) return itemColorHighscore;
                else return itemColorGreen;
            else return itemColorGrey;
        }
        /// <summary>
        /// Returns appropriate score line image color (green/red/yellow) from supplied double values (newValue) (oldValue) (highscoreValue).
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <param name="highscoreValue"></param>
        /// <returns></returns>
        public static Color32 GetLineColor(double newValue, double oldValue, double highscoreValue) {
            if (newValue < oldValue) return downLineColor;
            else if (newValue > oldValue)
                if (newValue > highscoreValue) return highscoreLineColor;
                else return upLineColor;
            else return neutralLineColor;
        }
        /// <summary>
        /// Returns appropriate flipped item string color (green/red/yellow) from supplied double values (newValue) (oldValue) (highscoreValue).
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <param name="highscoreValue"></param>
        /// <returns></returns>
        public static Color32 GetItemColor_Flip(double newValue, double oldValue, double highscoreValue) {
            if (newValue < oldValue)
                if (newValue < highscoreValue) return itemColorHighscore;
                else return itemColorGreen;
            else if (newValue > oldValue) return itemColorRed;
            else return itemColorGrey;
        }
        /// <summary>
        /// Returns appropriate stat container background color (green/red/yellow) from supplied double values (newValue) (oldValue) (highscoreValue).
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <param name="highscoreValue"></param>
        /// <returns></returns>
        public static Color32 GetItemBackgroundColor(double newValue, double oldValue, double highscoreValue) {
            if (newValue < oldValue) return downBackgroundColor;
            else if (newValue > oldValue)
                if (newValue > highscoreValue) return newHighscoreBackgroundColor;
                else return upBackgroundColor;
            else return neutralBackgroundColor;
        }
        /// <summary>
        /// Returns appropriate flipped stat container background color (green/red/yellow) from supplied double values (newValue) (oldValue) (highscoreValue).
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <param name="highscoreValue"></param>
        /// <returns></returns>
        public static Color32 GetItemBackgroundColor_Flip(int newValue, int oldValue, int highscoreValue) {
            if (newValue < oldValue)
                if (newValue < highscoreValue) return newHighscoreBackgroundColor;
                else return upBackgroundColor;
            else if (newValue > oldValue) return downBackgroundColor;
            else return neutralBackgroundColor;
        }
        /// <summary>
        /// Checks if supplied first double (first) is higher than second double (second).
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool CheckHigherStatValue(double first, double second) {
            if (first > second) return true;
            else return false;
        }
        /// <summary>
        /// Checks if supplied doubles match.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool CheckMatchingStatValue(double first, double second) {
            if (first == second) return true;
            else return false;
        }
        /// <summary>
        /// Checks if supplied first double (first) is higher than second double (second).
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="highestValue"></param>
        /// <returns></returns>
        public static bool CheckHigherStatValue_Flip(int newValue, int highestValue) {
            if (newValue > highestValue) return false;
            else if (newValue == 0) return false;
            else return true;
        }
        /// <summary>
        /// Returns abs difference between supplied new value (newValue) and supplied old value (oldValue).
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        public static double CheckDifference(double newValue, double oldValue) {
            if (newValue > oldValue) return newValue - oldValue;
            else if (newValue < oldValue) return oldValue - newValue;
            else return newValue - oldValue;
        }
        /// <summary>
        /// Returns percent difference between supplied new value (newValue) and supplied old value (oldValue).
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        public static double CheckDifference_Percent(double newValue, double oldValue) {
            if (newValue > oldValue) return (int)((newValue - oldValue) * 100) / oldValue;
            else if (newValue < oldValue) return (int)((oldValue - newValue) * 100) / newValue;
            else return 0;
        }
        /// <summary>
        /// Returns appropriate higher/lower symbol for (newValue) > (oldValue).
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        public static string CheckDifference_Symbol(double newValue, double oldValue) {
            if (newValue > oldValue) return "+";
            else if (newValue < oldValue) return "-";
            else return "";
        }
    }
}