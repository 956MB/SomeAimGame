using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private float f_axisX, f_axisZ;
    private float groundDistance = 0.2f;
    private float jumpHeight = 2.6f;
    public float movementSpeed = 13.0f;
    public float gravityStrength = 85.0f;

    private bool isGrounded;
    private Vector3 velocity;
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    //public bool movementLock;

    /*
    void FixedUpdate()
    {
        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (controller.isGrounded)
        {
            f_axisX = Input.GetAxis("Horizontal");
            f_axisZ = Input.GetAxis("Vertical");

            moveDirection = new Vector3(f_axisX, 0, f_axisZ);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= movementSpeed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpHeight;

        }
        moveDirection.y -= gravityStrength * Time.fixedDeltaTime;
        controller.Move(moveDirection * Time.fixedDeltaTime);
    }*/

    /*
    void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -60f;

        //Debug.Log("movementLock on? : " + ToggleHandler.MovementLockOn());
        if (!ToggleHandler.MovementLockOn()) {
            f_axisX = Input.GetAxis("Horizontal");
            f_axisZ = Input.GetAxis("Vertical");

            Vector3 movement = transform.right * f_axisX + transform.forward * f_axisZ;
            controller.Move(movement * movementSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Space) && isGrounded)
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * -gravityStrength);
                //AnimateCharacter.animateJump();
        }

        velocity.y += -gravityStrength * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //AnimateCharacter.animateCharacterMovement(f_axisZ, f_axisX, velocity.y);
    }
    */
}
