using System.Collections.Generic;
using UnityEngine;

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
}
