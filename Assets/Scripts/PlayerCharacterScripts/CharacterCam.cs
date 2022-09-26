using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCam : MonoBehaviour{

    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;


    private void Start() {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        // Get Mouse Input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate cam and orientation

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(xRotation, yRotation, 0), Time.deltaTime * 8f);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
