using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCharacter : MonoBehaviour {
    private static AnimateCharacter animateC;

    Animator animator;
    public float InputX, InputY, InputZ;

    private float groundDistance = 0.4f;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundMask;
    public bool movementLock;

    void Awake() { animateC = this; }

    void Start() {
        animateC.animator = this.gameObject.GetComponent<Animator>();
    }

    void FixedUpdate() {
        if (!movementLock) {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (Input.GetKey(KeyCode.Space) && isGrounded) {
                //animateC.animator.SetTrigger("toJump");
                animateC.animator.Play("AnimateJump");
            }
        }
    }

    public static void animateCharacterMovement(float zInput, float xInput, float yInput) {
        //animateC.InputX = xInput;
        //animateC.InputY = yInput;
        animateC.InputZ = zInput;
        //animateC.animator.SetFloat("InputX", animateC.InputX);
        //animateC.animator.SetFloat("InputY", animateC.InputY);
        animateC.animator.SetFloat("InputZ", animateC.InputZ);
    }
}
