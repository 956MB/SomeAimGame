using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour {
    private TMP_Text tooltipTextContent;
    private RectTransform tooltipBackgroundRectTransform;
    private Vector2 localPoint;
    private float textPadding = 6;
    private bool tooltipShown = false;

    public static Tooltip tooltipInstance;
    private void Awake() {
        tooltipInstance                = this;
        tooltipBackgroundRectTransform = transform.Find("TooltipBackground").GetComponent<RectTransform>();
        tooltipTextContent             = transform.Find("TooltipText").GetComponent<TMP_Text>();

        gameObject.SetActive(false);
    }

    void Update() {
        if (tooltipShown) {
            // Set tooltip localposition to mouse position every frame if tooltip shown.
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
            transform.localPosition = localPoint;
        }
    }

    /// <summary>
    /// Shows tooltip with supplied string as text content.
    /// </summary>
    /// <param name="tooltipText"></param>
    private void ShowTooltip(string tooltipText) {
        if (!tooltipShown) {
            gameObject.SetActive(true);

            // Set supplied string (tooltipText) as text content, then calculate tooltip size based on text.
            tooltipTextContent.SetText(SpliceText(tooltipText, 35));

            Vector2 tooltipBackgroundSize            = new Vector2(tooltipTextContent.preferredWidth + 12f, tooltipTextContent.preferredHeight + 11f);
            tooltipBackgroundRectTransform.sizeDelta = tooltipBackgroundSize;
            tooltipShown                             = true;
        }
    }

    private static string SpliceText(string inputText, int lineLength) {
        string[] stringSplit = inputText.Split(' ');
        int charCounter = 0;
        string finalString = "";

        for (int i = 0; i < stringSplit.Length; i++) {
            finalString += stringSplit[i] + " ";
            charCounter += stringSplit[i].Length;

            if (charCounter > lineLength) {
                finalString += "\n";
                charCounter = 0;
            }
        }
        return finalString;
    }

    /// <summary>
    /// Hides tooltip if shown.
    /// </summary>
    private void HideTooltip() {
        if (tooltipShown) {
            gameObject.SetActive(false);
            tooltipShown = false;
        }
    }

    /// <summary>
    /// Shows tooltip with supplied string as text content (static).
    /// </summary>
    /// <param name="tooltipText"></param>
    public static void ShowTooltip_Static(string tooltipText) { tooltipInstance.ShowTooltip(tooltipText); }
    /// <summary>
    /// Hides tooltip.
    /// </summary>
    public static void HideTooltip_Static() { tooltipInstance.HideTooltip(); }
}
