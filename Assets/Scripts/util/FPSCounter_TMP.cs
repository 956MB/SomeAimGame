﻿using UnityEngine;
using TMPro;

namespace SomeAimGame.Utilities {
    public class FPSCounter_TMP : MonoBehaviour {
        const float fpsMeasurePeriod  = 0.5f;
        private int m_FpsAccumulator  = 0;
        private float m_FpsNextPeriod = 0f;
        private int interval          = 3;
        const string displayFPS       = "{0}";
        private int m_CurrentFps;
        public TMP_Text m_FPSText;

        private void Start() {
            m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
        }

        /// <summary>
        /// Updates FPS count to widget in UI if 'showFPS' attribute true.
        /// </summary>
        private void Update() {
            m_FpsAccumulator++;
            //if (WidgetSettings.showFPS && Time.frameCount % interval == 0) {
            if (WidgetSettings.showFPS) {
                if (Time.realtimeSinceStartup > m_FpsNextPeriod) {
                    m_CurrentFps     = (int)(m_FpsAccumulator / fpsMeasurePeriod);
                    m_FpsAccumulator = 0;
                    m_FpsNextPeriod  += fpsMeasurePeriod;
                    m_FPSText.SetText(string.Format(displayFPS, m_CurrentFps));
                }       
            }
        }
    }
}