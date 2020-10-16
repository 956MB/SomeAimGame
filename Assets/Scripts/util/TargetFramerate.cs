using UnityEngine;

public class TargetFramerate : MonoBehaviour {
    /// <summary>
    /// Sets target framerate to -1 (no vsync).
    /// </summary>
    void Start() { Application.targetFrameRate = -1; }
}
