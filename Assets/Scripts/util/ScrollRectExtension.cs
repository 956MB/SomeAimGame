using UnityEngine;
using UnityEngine.UI;

namespace SomeAimGame.Utilities {
    public class ScrollRectExtension : MonoBehaviour {
        private static Vector2 scrollViewTop = new Vector2(0, 1);
        private static Vector2 scrollViewBottom = new Vector2(0, 0);

        /// <summary>
        /// Scrolls supplied scrollrect to top.
        /// </summary>
        /// <param name="scrollRect"></param>
        public static void ScrollToTop(ScrollRect scrollRect) { scrollRect.normalizedPosition = scrollViewTop; }
        /// <summary>
        /// Scrolls supplied scrollrect to bottom.
        /// </summary>
        /// <param name="scrollRect"></param>
        public static void ScrollToBottom(ScrollRect scrollRect) { scrollRect.normalizedPosition = scrollViewBottom; }
    }
}
