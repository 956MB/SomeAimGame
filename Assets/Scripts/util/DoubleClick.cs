using UnityEngine;
using UnityEngine.EventSystems;

namespace SomeAimGame.Utilities {
    public class DoubleClick : MonoBehaviour, IPointerClickHandler {
        private static Vector3 AARStartVector        = new Vector3(960f, 540f, 0f);
        private static Vector3 extraStatsStartVector = new Vector3(1455.711f, 638.3904f, 0f);

        /// <summary>
        /// Calls "OnDoubleClick" when gameObject double clicked.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData) {
            int clickCount = eventData.clickCount;
            if (clickCount == 2) { OnDoubleClick(); }
        }

        /// <summary>
        /// Returns 'AfterActionReport' and 'ExtraStats' panels to original positions on double click.
        /// </summary>
        void OnDoubleClick() {
            switch (transform.name) {
                case "AfterActionReport":
                    transform.position = AARStartVector;
                    CosmeticsSettings.resetAfterActionReportPanelCenter();
                    break;
                case "ExtraStats":
                    transform.position = extraStatsStartVector;
                    CosmeticsSettings.resetExtraStatsPanelCenter();
                    break;
            }
        }
    }
}