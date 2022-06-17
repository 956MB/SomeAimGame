using UnityEngine;
using TMPro;

using SomeAimGame.Core.Video;

namespace SomeAimGame.Utilities {
    public class DropdownUtils : MonoBehaviour {
        public static Vector3 arrowDownScale          = new Vector3(1f, 1f, 1f);
        public static Vector3 arrowupScale            = new Vector3(1f, -1f, 1f);
        public static Vector3 dropdownBodyOpenScale   = new Vector3(1f, 1f, 1f);
        public static Vector3 dropdownBodyClosedScale = new Vector3(1f, 0f, 1f);
        public static Color32 uiSoundBackgroundOn     = new Color32(255, 255, 255, 15);
        public static Color32 uiSoundBackgroundOff    = new Color32(255, 255, 255, 0);

        /// <summary>
        /// Calls 'DropdownAction_Open' OR 'DropdownAction_Close' based on supplied dropdown state bool (dropdownOpen).
        /// </summary>
        /// <param name="dropdownOpen"></param>
        /// <param name="dropdownBody"></param>
        /// <param name="arrowText"></param>
        /// <param name="dropdownOpenRef"></param>
        public static void SetDropdownState(bool dropdownOpen, GameObject dropdownBody, TMP_Text arrowText, ref bool dropdownOpenRef) {
            if (dropdownOpen) { DropdownAction_Open(dropdownBody, arrowText, ref dropdownOpenRef); } else { DropdownAction_Close(dropdownBody, arrowText, ref dropdownOpenRef); }
        }

        /// <summary>
        /// Sets supplied dropdown body GameObject (dropdownBody) state opened.
        /// </summary>
        /// <param name="dropdownBody"></param>
        /// <param name="arrowText"></param>
        /// <param name="dropdownOpenRef"></param>
        public static void DropdownAction_Open(GameObject dropdownBody, TMP_Text arrowText, ref bool dropdownOpenRef) {
            dropdownBody.transform.localScale = dropdownBodyOpenScale;
            arrowText.transform.localScale    = arrowupScale;
            dropdownOpenRef                   = true;
            Util.RefreshRootLayoutGroup(dropdownBody);
        }
        /// <summary>
        /// Sets supplied dropdown body GameObject (dropdownBody) state closed.
        /// </summary>
        /// <param name="dropdownBody"></param>
        /// <param name="arrowText"></param>
        /// <param name="dropdownOpenRef"></param>
        public static void DropdownAction_Close(GameObject dropdownBody, TMP_Text arrowText, ref bool dropdownOpenRef) {
            dropdownBody.transform.localScale = dropdownBodyClosedScale;
            arrowText.transform.localScale    = arrowDownScale;
            dropdownOpenRef                   = false;
        }

        /// <summary>
        /// Calls 'CreateDropdownItem' in loop to create dropdown items with supplied dropdown item prefab (dropdownItemPrefab).
        /// </summary>
        /// <param name="videoSettingType"></param>
        /// <param name="dropdownItemPrefab"></param>
        /// <param name="parentDropdownPrefab"></param>
        /// <param name="stringsArray"></param>
        public static void CreateDropdownItems_Loop(VideoDropdowns videoSettingType, GameObject dropdownItemPrefab, GameObject parentDropdownPrefab, params string[] stringsArray) {
            for (int i = 0; i < stringsArray.Length; i++) {
                CreateDropdownItem((int)videoSettingType, i, stringsArray[i], dropdownItemPrefab, parentDropdownPrefab);
            }
        }
        /// <summary>
        /// Calls 'CreateDropdownItem' in loop to create dropdown items with supplied dropdown item prefab (dropdownItemPrefab).
        /// </summary>
        /// <param name="videoSettingType"></param>
        /// <param name="dropdownItemPrefab"></param>
        /// <param name="parentDropdownPrefab"></param>
        /// <param name="enumsArray"></param>
        public static void CreateDropdownItems_Loop(VideoDropdowns videoSettingType, GameObject dropdownItemPrefab, GameObject parentDropdownPrefab, params FullScreenMode[] enumsArray) {
            foreach (FullScreenMode itemType in enumsArray) { CreateDropdownItem((int)videoSettingType, (int)itemType, dropdownItemPrefab, parentDropdownPrefab); }
        }
        /// <summary>
        /// Calls 'CreateDropdownItem' in loop to create dropdown items with supplied dropdown item prefab (dropdownItemPrefab).
        /// </summary>
        /// <param name="videoSettingType"></param>
        /// <param name="dropdownItemPrefab"></param>
        /// <param name="parentDropdownPrefab"></param>
        /// <param name="enumsArray"></param>
        public static void CreateDropdownItems_Loop(VideoDropdowns videoSettingType, GameObject dropdownItemPrefab, GameObject parentDropdownPrefab, params AntiAliasType[] enumsArray) {
            foreach (AntiAliasType itemType in enumsArray) { CreateDropdownItem((int)videoSettingType, (int)itemType, dropdownItemPrefab, parentDropdownPrefab); }
        }

        /// <summary>
        /// Creates dropdown item inside parent dropdown body (parentDropdownPrefab), and sets dropdown values from supplied ints (setDropdownInt/setSettingInt).
        /// </summary>
        /// <param name="setDropdownInt"></param>
        /// <param name="setSettingInt"></param>
        /// <param name="dropdownItemPrefab"></param>
        /// <param name="parentDropdownPrefab"></param>
        public static void CreateDropdownItem(int setDropdownInt, int setSettingInt, GameObject dropdownItemPrefab, GameObject parentDropdownPrefab) {
            GameObject dropdownItem = Instantiate(dropdownItemPrefab);

            TMP_Text itemText = dropdownItem.GetComponentsInChildren<TMP_Text>()[0];

            if ((VideoDropdowns)setDropdownInt == VideoDropdowns.DISPLAY_MODE) {
                itemText.SetText($"{VideoSettingUtil.ReturnTypeString((FullScreenMode)setSettingInt)}");
                dropdownItem.GetComponent<VideoSettingChange>().SetSettingValues(setDropdownInt, setSettingInt, VideoSettingUtil.ReturnTypeString((FullScreenMode)setSettingInt));
            }
            if ((VideoDropdowns)setDropdownInt == VideoDropdowns.ANTI_ALIASING) {
                itemText.SetText($"{VideoSettingUtil.ReturnTypeString((AntiAliasType)setSettingInt)}");
                dropdownItem.GetComponent<VideoSettingChange>().SetSettingValues(setDropdownInt, setSettingInt, VideoSettingUtil.ReturnTypeString((AntiAliasType)setSettingInt));
            }

            //Debug.Log(dropdownItem.transform.GetComponent<RectTransform>().sizeDelta);
            dropdownItem.transform.SetParent(parentDropdownPrefab.transform, false);
        }
        /// <summary>
        /// Creates dropdown item inside parent dropdown body (parentDropdownPrefab), and sets dropdown values from supplied ints (setDropdownInt/setSettingInt).
        /// </summary>
        /// <param name="setDropdownInt"></param>
        /// <param name="setSettingInt"></param>
        /// <param name="setDropdownText"></param>
        /// <param name="dropdownItemPrefab"></param>
        /// <param name="parentDropdownPrefab"></param>
        public static void CreateDropdownItem(int setDropdownInt, int setSettingInt, string setDropdownText, GameObject dropdownItemPrefab, GameObject parentDropdownPrefab) {
            GameObject dropdownItem = Instantiate(dropdownItemPrefab);
            dropdownItem.GetComponent<VideoSettingChange>().SetSettingValues(setDropdownInt, setSettingInt, setDropdownText);

            TMP_Text itemText = dropdownItem.GetComponentsInChildren<TMP_Text>()[0];
            itemText.SetText($"{setDropdownText}");

            dropdownItem.transform.SetParent(parentDropdownPrefab.transform, false);
        }
    }
}
