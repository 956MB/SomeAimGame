using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseSensitivitySlider : MonoBehaviour {
    public Slider mouseSensitivitySlider;
    public TMP_Text mouseSensitivityValueText;
    public TMP_Text mouseSensitivityPlaceholderText;
    private static float mouseSensValue;

    private static MouseSensitivitySlider mouseSens;
    void Awake() { mouseSens = this; }

    // Init mouse sensitivity value slider with listener.
    void Start() { mouseSensitivitySlider.onValueChanged.AddListener(delegate { SetMouseSensitivity(); }); }

    /// <summary>
    /// Sets mouse sensitivity with slider value. Also sets corresponding text, placeholder and slider with set value, then saves value with 'ExtraSettings.saveMouseSensItem(mouseSensValue)'.
    /// </summary>
    public static void SetMouseSensitivity() {
        mouseSensValue = mouseSens.mouseSensitivitySlider.value;
        SetMouseSensitivityValueText(mouseSensValue);
        ExtraSettings.SaveMouseSensItem(mouseSensValue);
        MouseLook.mouseSensitivity = mouseSensValue;
    }

    /// <summary>
    /// Sets mouse sensivitiy text and placeholder to supplied value (sens).
    /// </summary>
    /// <param name="sens"></param>
    public static void SetMouseSensitivityValueText(float sens) {
        mouseSens.mouseSensitivityValueText.SetText($"{sens:0.00}");
        mouseSens.mouseSensitivityPlaceholderText.SetText($"{sens:0.00}");
        //mouseSensInput.SetTextWithoutNotify($"{sens:0.00}");
    }

    /// <summary>
    /// Sets mouse sensivitiy slider to supplied value (sens).
    /// </summary>
    /// <param name="sens"></param>
    public static void SetMouseSensitivitySlider(float sens) { mouseSens.mouseSensitivitySlider.value = sens; }

    /// <summary>
    /// Sets mouse sensitivity value with supplied input string (sensText).
    /// </summary>
    /// <param name="sensText"></param>
    public void SetSaveMouseSens_Input(string sensText) {
        if (sensText != "" && sensText != " " && sensText != "  " && sensText != "   " && sensText != "    " && sensText != "     ") {
            float newMouseSensValue = float.Parse(sensText);

            if (newMouseSensValue >= 0.1f && newMouseSensValue <= 10f) {
                mouseSens.mouseSensitivityValueText.SetText($"{sensText}");
                mouseSens.mouseSensitivityPlaceholderText.SetText($"{sensText}");
                MouseLook.mouseSensitivity = newMouseSensValue;
                SetMouseSensitivitySlider(newMouseSensValue);
                ExtraSettings.SaveMouseSensItem(newMouseSensValue);
            } else {
                mouseSens.mouseSensitivityValueText.SetText($"{ExtraSettings.mouseSensitivity:0.00}");
                mouseSens.mouseSensitivityPlaceholderText.SetText($"{ExtraSettings.mouseSensitivity:0.00}");
            }
        } else {
            mouseSens.mouseSensitivityValueText.SetText($"{ExtraSettings.mouseSensitivity:0.00}");
            mouseSens.mouseSensitivityPlaceholderText.SetText($"{ExtraSettings.mouseSensitivity:0.00}");
        }
    }
}
