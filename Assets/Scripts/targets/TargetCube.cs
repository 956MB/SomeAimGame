using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCube : MonoBehaviour {
    public float targetUpForce = 1.0f;
    public float targetSideForce = 0.1f;

    void Start() {
        float xForce = Random.Range(-targetSideForce, targetSideForce);
        float yForce = Random.Range(targetUpForce / 2f, targetUpForce);
        float zForce = Random.Range(-targetSideForce, targetSideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity = force;
    }
}
