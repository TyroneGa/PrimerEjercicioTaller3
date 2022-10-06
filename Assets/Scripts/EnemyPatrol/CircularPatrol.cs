using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class that controls an Agent in a circular patrol circuit
/// </summary>
[RequireComponent(typeof(AgentController))]
public class CircularPatrol : MonoBehaviour
{
    /// <summary>
    /// Objects that represent the target points in the map
    /// This can be changed for Vector3
    /// </summary>
    public GameObject[] patrolTargets;

    /// <summary>
    /// Distance required to switch target
    /// The Agent will only switch targets 
    /// when its distance to the current target is less than this number
    /// </summary>
    public float minDistance;

    /// <summary>
    /// Controller that moves this agent
    /// </summary>
    AgentController controller;

    /// <summary>
    /// Current index in the target list
    /// The Agent will try to move towards this target
    /// </summary>
    int currentTarget;


    void Start()
    {
        // Get the required agent controller
        controller = GetComponent<AgentController>();
    }


    void Update()
    {
        // First: Calculate the distance to the current target
        float distance = Vector3.Distance(transform.position,
            patrolTargets[currentTarget].transform.position);

        // Check if the distance is less than the current minDistance
        if (distance < minDistance) // If so: change the target
        {
            currentTarget = (currentTarget + 1) % patrolTargets.Length;
        }

        // Move towards the current selected target
        controller.Move(patrolTargets[currentTarget].transform.position);
    }
}
