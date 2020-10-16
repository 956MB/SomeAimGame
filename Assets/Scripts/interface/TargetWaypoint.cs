using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetWaypoint : MonoBehaviour {
    public Image waypointImage;
    public Transform targetArea;
    public Vector3 yOffset;
    private float minX, maxX, minY, maxY;
    private Vector2 position;

    void FixedUpdate() {
        minX = waypointImage.GetPixelAdjustedRect().width / 2;
        maxX = Screen.width / minX;
        minY = waypointImage.GetPixelAdjustedRect().height / 2;
        maxY = Screen.height / minY;

        position = Camera.main.WorldToScreenPoint(targetArea.position + yOffset);

        if (Vector3.Dot((targetArea.position - transform.position), transform.forward) < 0) {
            if (position.x < Screen.width / 2) {
                position.x = maxX;
            } else {
                position.x = minX;
            }
        }

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        waypointImage.transform.position = position;
    }
}
