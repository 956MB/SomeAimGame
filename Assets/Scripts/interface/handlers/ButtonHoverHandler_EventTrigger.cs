using UnityEngine;

public class ButtonHoverHandler_EventTrigger : MonoBehaviour {
    public GameObject childBorder;
    public GameObject parentOptionsObject;
    public static bool optionsObjectOpen = false;

    private void Start() {
        foreach (Transform child in parentOptionsObject.transform) {
            if (child.gameObject.tag == "OptionObjectItem") { child.gameObject.SetActive(false); }
        }
    }

    public void EnableBorder() { childBorder.SetActive(true); }

    public void DisableBorder() { childBorder.SetActive(false); }

    public void ToggleOptionsObject() {
        if (optionsObjectOpen) DisableBorder();
        else EnableBorder();

        LoopToggleOptions();
    }

    public void LoopToggleOptions() {
        foreach (Transform child in parentOptionsObject.transform) {
            if (child.gameObject.tag == "OptionObjectItem") { child.gameObject.SetActive(!optionsObjectOpen); }
        }

        optionsObjectOpen = !optionsObjectOpen;
    }
}
