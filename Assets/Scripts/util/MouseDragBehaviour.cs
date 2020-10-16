using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDragBehaviour : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
    private Vector2 lastMousePosition;
    private RectTransform windowsRectTransform;
    /// <summary>
    /// This method will be called on the start of the mouse drag
    /// </summary>
    /// <param name="eventData">mouse pointer event data</param>
    public void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log("Begin Drag");
        lastMousePosition = eventData.position;
        windowsRectTransform = GetComponent<RectTransform>();
        //Debug.Log($"START windwosRectTransform: {windwosRectTransform.position.x}");
        //Debug.Log($"START windwosRectTransform: {windwosRectTransform.position.y}");
        //Debug.Log($"START windwosRectTransform: {windwosRectTransform.position.z}");
    }

    /// <summary>
    /// This method will be called during the mouse drag
    /// </summary>
    /// <param name="eventData">mouse pointer event data</param>
    public void OnDrag(PointerEventData eventData) {
        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - lastMousePosition;
        windowsRectTransform = GetComponent<RectTransform>();

        Vector3 newPosition = windowsRectTransform.position + new Vector3(diff.x, diff.y, transform.position.z);
        //Debug.Log($"{diff.x}, {diff.y}, {transform.position.z}");
        Vector3 oldPos = windowsRectTransform.position;
        windowsRectTransform.position = newPosition;
        if (!IsRectTransformInsideSreen(windowsRectTransform)) {
            windowsRectTransform.position = oldPos;
        }
        lastMousePosition = currentMousePosition;
    }

    /// <summary>
    /// This method will be called at the end of mouse drag.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData) {
        CosmeticsSettings.SavePanelLocationXY(windowsRectTransform.name, transform.position.x, transform.position.y);
    }

    /// <summary>
    /// This methods will check is the rect transform is inside the screen or not
    /// </summary>
    /// <param name="rectTransform">Rect Trasform</param>
    /// <returns></returns>
    private bool IsRectTransformInsideSreen(RectTransform rectTransform) {
        bool isInside = false;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        int visibleCorners = 0;
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        foreach (Vector3 corner in corners) {
            if (rect.Contains(corner)) {
                visibleCorners++;
            }
        }
        if (visibleCorners == 4) {
            isInside = true;
        }
        return isInside;
    }
}