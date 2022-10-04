using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : States {

    [Header("State Logic")]
    [SerializeField] StateManager stateManagerRef;
    [SerializeField] FollowState followState;
    [SerializeField] Collider col;
    bool playerInRange;


    [Header("Movement")]

    [SerializeField] Transform bookFriend;
    Vector3 InitPos;
    Quaternion InitRot;
    [SerializeField] float amp;
    [SerializeField] float freq;


    public override States RunCurrentState() {

        if (playerInRange) {

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


        if ( stateManagerRef.currentState == this ) {


            if ( !playerInRange ) {

                bookFriend.position = new Vector3(InitPos.x, Mathf.Sin(Time.time * freq) * amp + InitPos.y, InitPos.z);
                //bookFriend.localScale = new Vector3(5f, 5f, 5f);
                bookFriend.rotation = InitRot;
            }
        }
    }


    private void OnTriggerEnter(Collider other) {
        

        if (other.tag == "Player") {

            playerInRange = true;
        }
    }
}
