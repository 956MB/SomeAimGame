using UnityEngine;
using UnityEngine.UI;

namespace SomeAimGame.Utilities {
    public class ScrollRectExtension : MonoBehaviour {
        /// <summary>
        /// Scrolls supplied scrollrect to top.
        /// </summary>
        /// <param name="scrollRect"></param>
        public static void ScrollToTop(ScrollRect scrollRect) { scrollRect.normalizedPosition = new Vector2(0, 1); }
        /// <summary>
        /// Scrolls supplied scrollrect to bottom.
        /// </summary>
        /// <param name="scrollRect"></param>
        public static void ScrollToBottom(ScrollRect scrollRect) { scrollRect.normalizedPosition = new Vector2(0, 0); }
    }
}
