using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindsHandler : MonoBehaviour {
    void Update() {
        
    }

    public static string returnNewKeybind() {
        while (true) {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyUp(vKey)) {
                    //Debug.Log($"New keypressed: {vKey.ToString()}");
                    return vKey.ToString();
                }
            }
        }
    }
}
