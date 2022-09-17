using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 direction;
    private Vector3 jump;

    private float horizontal = 0f;
    private float vertical = 0f;

    public float speed = 1f;
    public float jumpForce = 2f;
    public float smoothRotation = 0.1f;

    private float angle = 0f;
    private float targetAngle = 0f;
    private float rotatingVelocity = 0;

    private bool OnGround = true;

    //SavePos playerPosData;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0f, 2f, 0f);

        //playerPosData = FindObjectOfType<SavePos>();
        //playerPosData.PlayerPosLoad();
    }



    private void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;



        if (direction.magnitude >= 0.1f)
        {

            transform.position = new Vector3(transform.position.x + vertical * speed * Time.deltaTime, transform.position.y, transform.position.z + horizontal * -speed * Time.deltaTime);

            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotatingVelocity, smoothRotation);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }


        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {

            OnGround = false;
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        }
    }



    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Floor")
        {

            OnGround = true;
        }
    }

}
