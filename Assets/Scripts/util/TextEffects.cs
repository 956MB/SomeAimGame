using UnityEngine;
using TMPro;

public class TextEffects : MonoBehaviour {
    private static float startX, endX;

    /// <summary>
    /// Applies wiggle effect to supplied TMP_Text (text) with delay (delay) and wiggle distance (distance).
    /// </summary>
    /// <param name="text"></param>
    /// <param name="delay"></param>
    /// <param name="distance"></param>
    public static void WiggleText(TMP_Text text, float delay, int distance) {
        GameObject moveText = text.gameObject;
        startX = moveText.transform.position.x;
        endX = startX - distance;

        for (int i = 0; i < 3; i++) {
            LeanTween.moveX(moveText, endX, delay);
            LeanTween.moveX(moveText, startX, delay);
        }
    }
}
