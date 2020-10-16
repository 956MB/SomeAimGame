using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectWeapon : MonoBehaviour {
    public Animator animator;

    void Start() {
        animator = this.gameObject.GetComponent<Animator>();
    }

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.F)) {
            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("InspectWeapon")) {
            //    Debug.Log("InspectWeapon currently running!");
            //    animator.enabled = false;
            //    animator.enabled = true;
            //    animator.Play("InspectWeapon");
            //}
            //else {
            //    Debug.Log("Starting InspectWeapon!");
            //    animator.Play("InspectWeapon");
            //}
            animator.Play("InspectWeapon", -1, 0f);
        }
    }
}
