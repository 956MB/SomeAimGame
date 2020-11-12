using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBlock : MonoBehaviour {
    public Transform pickupDestination;

    void OnMouseDown() {
        //GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position              = pickupDestination.position;
        this.transform.parent                = GameObject.Find("BlockPickupDestination").transform;
    }

    void OnMouseUp() {
        this.transform.parent = null;
        //GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().mass       = 1;
    }
}
