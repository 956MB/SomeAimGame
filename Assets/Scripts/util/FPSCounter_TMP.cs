using UnityEngine;
using TMPro;

public class FPSCounter_TMP : MonoBehaviour {
    const float fpsMeasurePeriod = 0.5f;
    private int m_FpsAccumulator = 0;
    private float m_FpsNextPeriod = 0f;
    private int m_CurrentFps;
    const string displayFPS = "{0}";
    public TMP_Text m_FPSText;

    private void Start() {
        m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
    }

    private void Update() {
        if (WidgetSettings.showFPS) {
            m_FpsAccumulator++;
            if (Time.realtimeSinceStartup > m_FpsNextPeriod) {
                m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
                m_FpsAccumulator = 0;
                m_FpsNextPeriod += fpsMeasurePeriod;
                m_FPSText.SetText(string.Format(displayFPS, m_CurrentFps));
            }
        }
    }
}