using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Simple class that controls a moving agent
/// </summary>
/// 
//[RequireComponent(typeof(Rigidbody))]
public class AgentController : MonoBehaviour
{
    /// <summary>
    /// Base speed of this agent
    /// </summary>
    public float speed;

    //Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
      //  rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector3 target, float speed = 0)
    {
        // If the AI controller didn't provide speed, use the default
        if (speed == 0) speed = this.speed;

        transform.LookAt(target);
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
        // rb.velocity = transform.forward * speed;
    }
}
