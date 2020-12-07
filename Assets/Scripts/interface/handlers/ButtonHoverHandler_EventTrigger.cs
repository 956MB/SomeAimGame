using UnityEngine;

public class ButtonHoverHandler_EventTrigger : MonoBehaviour {
    public GameObject childBorder;
    public GameObject parentOptionsObject;
    public static bool optionsObjectOpen = false;

    private static ButtonHoverHandler_EventTrigger hoverHandle;
    private void Awake() { hoverHandle = this; }

    public void EnableBorder() { childBorder.SetActive(true); }

    public void DisableBorder() { childBorder.SetActive(false); }

    public static void ToggleOptionsObject_Static() { hoverHandle.ToggleOptionsObject(); }

    public void ToggleOptionsObject() {
        if (optionsObjectOpen) {
            DisableBorder();
        } else {
            EnableBorder();
        }

        LoopToggleOptions();
    }

    public void LoopToggleOptions() {
        if (optionsObjectOpen) {
            QuitGame.CloseQuitButton();
        } else {
            QuitGame.OpenQuitButton();
        }

        optionsObjectOpen = !optionsObjectOpen;
    }
}
