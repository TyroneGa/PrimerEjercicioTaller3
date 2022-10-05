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

        BackMainMenu();

        anim.SetBool("Jump 0", readyToJump);
        anim.SetFloat("VerY", running ? verticalInput * 2 : verticalInput);
    }


    private void FixedUpdate() {

        MovePlayer();

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, floor);
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RESTART")
        {
            SceneManager.LoadScene("Nivel");
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
