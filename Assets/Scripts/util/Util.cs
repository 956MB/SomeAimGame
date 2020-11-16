using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Util : MonoBehaviour {
    /// <summary>
    /// Prints all Vector3 list (list) elements in string.
    /// </summary>
    /// <param name="list"></param>
    public static void PrintVector3List(List<Vector3> list) {
        string targetList = "";
        for (int i = 0; i < list.Count; i++) {
            targetList += $" {list[i]}";
        }
        Debug.Log($"Vector3 List: {targetList}");
    }

    /// <summary>
    /// Performs forced refresh on supplied scrollview group (rootGroup).
    /// </summary>
    /// <param name="rootGroup"></param>
    public static void RefreshRootLayoutGroup(GameObject rootGroup) {
        LayoutRebuilder.ForceRebuildLayoutImmediate(rootGroup.gameObject.GetComponent<RectTransform>());
    }

    /// <summary>
    /// Checks if supplied string (str) contains only digits.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool DigitsOnly(string str) {
        foreach (char c in str) {
            if (c < '0' || c > '9') return false;
        }
        return true;
    }

    /// <summary>
    /// Copies supplied string (copyString) to clipboard.
    /// </summary>
    /// <param name="copyString"></param>
    public static void CopyToClipboard(string copyString) {
        GUIUtility.systemCopyBuffer = copyString;
    }
}
