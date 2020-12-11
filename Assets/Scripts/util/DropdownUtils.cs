using UnityEngine;
using TMPro;

namespace SomeAimGame.Utilities {
    public class DropdownUtils {
        public static Vector3 arrowDownScale          = new Vector3(1f, 1f, 1f);
        public static Vector3 arrowupScale            = new Vector3(1f, -1f, 1f);
        public static Vector3 dropdownBodyOpenScale   = new Vector3(1f, 1f, 1f);
        public static Vector3 dropdownBodyClosedScale = new Vector3(1f, 0f, 1f);
        public static Color32 uiSoundBackgroundOn     = new Color32(255, 255, 255, 15);
        public static Color32 uiSoundBackgroundOff    = new Color32(255, 255, 255, 0);

        public static void SetDropdownState(bool dropdownOpen, GameObject dropdownBody, TMP_Text arrowText, ref bool dropdownOpenRef) {
            if (dropdownOpen) {
                DropdownAction_Open(dropdownBody, arrowText, ref dropdownOpenRef);
            } else {
                DropdownAction_Close(dropdownBody, arrowText, ref dropdownOpenRef);
            }
        }

        public static void DropdownAction_Open(GameObject dropdownBody, TMP_Text arrowText, ref bool dropdownOpenRef) {
            dropdownBody.transform.localScale = dropdownBodyOpenScale;
            arrowText.transform.localScale    = arrowupScale;
            dropdownOpenRef                   = true;
            Util.RefreshRootLayoutGroup(dropdownBody);
        }

        public static void DropdownAction_Close(GameObject dropdownBody, TMP_Text arrowText, ref bool dropdownOpenRef) {
            dropdownBody.transform.localScale = dropdownBodyClosedScale;
            arrowText.transform.localScale    = arrowDownScale;
            dropdownOpenRef                   = false;
        }
    }
}
