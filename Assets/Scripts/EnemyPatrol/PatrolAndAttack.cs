using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircularPatrol))]
public class PatrolAndAttack : MonoBehaviour
{
    public float minPlayerDistance;

    AgentController controller;
    CircularPatrol patrol;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<AgentController>();
        patrol = GetComponent<CircularPatrol>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // First calculate the distance between the Player and the Agent
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Is the player near the Agent
        if (distance < minPlayerDistance)
        {
            // turn off the patrol component
            patrol.enabled = false;

            // calculate double speed for the chase
            float speed = controller.speed * 2;

            // move towards the player
            controller.Move(player.transform.position, speed);
        }
        // if the player is not near, keep patrolling
        else
        {
            patrol.enabled = true;
        }
    }
}
