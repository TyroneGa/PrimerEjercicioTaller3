using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    [Header("Movement")]

    Vector3 moveDirection;

    [SerializeField] Transform orientation;

    [SerializeField] float moveSpeed;
    [SerializeField] float groundDrag;

    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    bool readyToJump;

        
    [Header("GroundCheck")]

    [SerializeField] float playerHeight;
    [SerializeField] LayerMask floor;
    bool grounded;


    [Header("Slope Handling")]

    [SerializeField] float maxSlopeAngle;
    RaycastHit slopeHit;
    //bool exitingSlope;


    //Inputs
    float horizontalInput;
    float verticalInput;
    bool running;


    //Otros
    Rigidbody rb;
    Animator anim;

   
    private void Start() {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        anim = GetComponent<Animator>();
    }


    private void Update() {

        MyInput();
        MovePlayer();

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, floor);

        BackMainMenu();

        anim.SetFloat("VerY", running ? verticalInput * 2 : verticalInput);


        anim.SetBool("Jump 0", readyToJump);
    }


    void MyInput() {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        running = Input.GetButton("Run");


        //when to jump
        if (Input.GetButton("Jump") && readyToJump && grounded) {

            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    //Usar Velocity
    void MovePlayer() {

        //calculate movement direcction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (moveDirection.magnitude > 0) {

            moveDirection = moveDirection.normalized;

            if (running) {

                moveDirection = moveDirection * moveSpeed * 2f;
            }

            else {

                moveDirection = moveDirection * moveSpeed;
            }
        }

        rb.velocity = moveDirection + Vector3.up * rb.velocity.y;
    }



    void Jump() {

        //exitingSlope = true;

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }


    void ResetJump() {

        readyToJump = true;
    }


    bool OnSlope() {

        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f)) {

            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle > 0;
        }

        return false;
    }


    Vector3 GetSlopeMoveDirection() {

        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RESTART")
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
