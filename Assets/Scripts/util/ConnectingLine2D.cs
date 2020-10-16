using UnityEngine;
using System.Collections.Generic;

public class ConnectingLine2D : MonoBehaviour {
    public List<Transform> points = new List<Transform>();
    private LineRenderer line;

    private static ConnectingLine2D connectingLine;
    void Awake() { connectingLine = this; }

    void Start() {
        this.drawConnectingLine();
    }

    private void drawConnectingLine() {
        GameObject lineObj = new GameObject();
        this.line = lineObj.AddComponent<LineRenderer>();
        this.line.startWidth = 1f;
        this.line.endWidth = 1f;
        this.line.positionCount = 2;

        Vector3[] checkPointArray = new Vector3[this.points.Count];
        for (int i = 0; i < this.points.Count; i++) {
            Vector3 pointPos = this.points[i].position;
            checkPointArray[i] = new Vector3(pointPos.x, pointPos.y, 0f);
        }

        this.line.SetPositions(checkPointArray);
    }

    //void Update() {
    //    // Check if the GameObjects are not null
    //    if (gameObject1 != null && gameObject2 != null) {
    //        // Update position of the two vertex of the Line Renderer
    //        line.SetPosition(0, gameObject1.transform.position);
    //        line.SetPosition(1, gameObject2.transform.position);
    //    }
    //}
}