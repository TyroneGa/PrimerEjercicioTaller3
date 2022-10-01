using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : States {

    [Header("State Logic")]

    public FollowState followState;
    public Collider col;
    bool playerInRange;


    [Header("Movement")]

    public Transform bookFriend;
    Vector3 InitPos;
    Quaternion InitRot;
    public float amp;
    public float freq;


    public override States RunCurrentState() {

        if (playerInRange) {

            followState.enabled = true;
            return followState;
        }

        else
            return this;
    }

    private void Start() {

        InitPos = bookFriend.position;
        InitRot = bookFriend.rotation;
    }


    private void Update() {

        if (!playerInRange) {

            bookFriend.position = new Vector3(InitPos.x, Mathf.Sin(Time.time * freq) * amp + InitPos.y, InitPos.z);
            //bookFriend.localScale = new Vector3(5f, 5f, 5f);
            //bookFriend.rotation = InitRot;
        }
    }


    private void OnTriggerEnter(Collider other) {
        
        if (other.tag == "Player") {

            playerInRange = true;
        }
    }
}
