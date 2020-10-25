using UnityEngine;

public class ButtonHoverHandler_EventTrigger : MonoBehaviour {
    public GameObject childBorder;
    public GameObject parentOptionsObject;
    public static bool optionsObjectOpen = false;

    private static ButtonHoverHandler_EventTrigger hoverHandle;
    private void Awake() { hoverHandle = this; }

    private void Start() {
        foreach (Transform child in parentOptionsObject.transform) {
            if (child.CompareTag("OptionObjectItem")) { child.gameObject.SetActive(false); }
        }
    }

    public void EnableBorder() { childBorder.SetActive(true); }

    public void DisableBorder() { childBorder.SetActive(false); }

    public static void ToggleOptionsObject_Static() { hoverHandle.ToggleOptionsObject(); }

    public void ToggleOptionsObject() {
        if (optionsObjectOpen) DisableBorder();
        else EnableBorder();

        LoopToggleOptions();
    }

    public void LoopToggleOptions() {
        foreach (Transform child in parentOptionsObject.transform) {
            if (child.CompareTag("OptionObjectItem")) { child.gameObject.SetActive(!optionsObjectOpen); }
        }

        optionsObjectOpen = !optionsObjectOpen;
    }
}
