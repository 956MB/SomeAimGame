using UnityEngine.UI;
using UnityEngine;

public class FixedLayoutSpacing : MonoBehaviour {
    public HorizontalLayoutGroup layoutGroup;
    public float dotWidth = 32f;
    public RectTransform lengthRectTransform;

    void Update() {
        if (transform.childCount > 0) {
            int dotCount = transform.childCount;
            layoutGroup.spacing = (lengthRectTransform.rect.width - (dotCount * dotWidth)) / (dotCount - 1);
            Debug.Log(layoutGroup.spacing);
        }

    }

    void OnValidate() {
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
        lengthRectTransform = GetComponent<RectTransform>();
    }
}