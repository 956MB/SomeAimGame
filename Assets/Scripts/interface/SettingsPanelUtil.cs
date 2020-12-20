using UnityEngine;

namespace SomeAimGame.Utilities {
    public class SettingsPanelUtil : MonoBehaviour {
        /// <summary>
        /// Loads thumbnail sprites and sets them to corresponding buttons in settings panel (general sub-section).
        /// </summary>
        public static void LoadThumbnails(GameObject[] thumbnailObjects, Sprite[] thumbnailSprites) {
            for (int i = 0; i < thumbnailObjects.Length; i++) {
                thumbnailObjects[i].transform.GetComponent<UnityEngine.UI.Image>().sprite = thumbnailSprites[i];
            }
        }
    }
}