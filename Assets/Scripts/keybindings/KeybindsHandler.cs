using System.Collections;
using UnityEngine;

public class KeybindsHandler : MonoBehaviour {
    public static bool getNewKeybind = false;
    public static string newKeybind = "";

    void Update() {
        if (getNewKeybind) { ReturnNewKeybind(); }
    }

    public static void ReturnNewKeybind() {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyUp(vKey)) {
                //Debug.Log($"New keypressed: {vKey.ToString()}");
                newKeybind = vKey.ToString();
                getNewKeybind = false;
            }
        }
    }
}
