using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [Header("Movement")]

    public float moveSpeed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    bool readyToJump;

    [Header("Keybinds")]

    public KeyCode jumpKey = KeyCode.Space;

    [Header("GroundCheck")]

    public float playerHeight;
    public LayerMask floor;

    bool grounded;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public GameObject Player;

    public Transform PuntoSpawn;


    private void Start() {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }


    private void Update() {

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, floor);

        MyInput();
        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;

        else
            rb.drag = 0;
    }


    private void FixedUpdate() {

        MovePlayer();
    }


    void MyInput() {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


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

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }


    void SpeedControl() {

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);


        //limit velocity if needed
        if (flatVel.magnitude > moveSpeed) {

            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }


    void Jump() {

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }


    void ResetJump() {

        readyToJump = true;
    }

}
