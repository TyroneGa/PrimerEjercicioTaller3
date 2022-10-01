using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {


    [Header("Movement")]

    [SerializeField] Transform orientation;

    [SerializeField] float moveSpeed;
    [SerializeField] float groundDrag;

    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;

    bool readyToJump;


    [Header("Keybinds")]

    [SerializeField] KeyCode jumpKey = KeyCode.Space;


    [Header("GroundCheck")]

    [SerializeField] float playerHeight;
    [SerializeField] LayerMask floor;

    bool grounded;


    [Header("Slope Handling")]

    [SerializeField] float maxSlopeAngle;

    RaycastHit slopeHit;
    bool exitingSlope;


    float horizontalInput;
    float verticalInput;
    float JumpK;

    Vector3 moveDirection;

    Rigidbody rb;

    Animator anim;

    


    



    private void Start() {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        anim = GetComponent<Animator>();
    }


    private void Update() {


        MovePlayer();

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, floor);

        MyInput();
        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;

        else
            rb.drag = 0;

        BackMainMenu();

        anim.SetFloat("VerY", verticalInput);


        anim.SetBool("Jump 0", readyToJump);



 
    }


    void MyInput() {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        JumpK = Input.GetAxisRaw("Jump");




        //when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded) {

            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }


    void MovePlayer() {

        //calculate movement direcction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //On Slope
        if (OnSlope() && !exitingSlope) {

            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        rb.useGravity = !OnSlope();
    }


    void SpeedControl() {

        //limiting speed on slope
        if (OnSlope() && !exitingSlope) {

            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        //limit velocity if needed
        else {

            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed) {

                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }


    void Jump() {

        exitingSlope = true;

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }


    void ResetJump() {

        readyToJump = true;

        exitingSlope = false;
    }


    bool OnSlope() {

        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f)) {

            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }


    Vector3 GetSlopeMoveDirection() {

        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "RESTART")
        {
            SceneManager.LoadScene("Diseño de Nivel II");
        }
    }

    void BackMainMenu()
    {
        if (Input.GetButtonDown("MainMenu"))
        {
            SceneManager.LoadScene("StartScreen");
            print("M PRESS");
        }
    }
}
