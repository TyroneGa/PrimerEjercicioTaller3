using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : States {


    [Header("State Logic")]

    [SerializeField] StateManager stateManagerRef;
    [SerializeField] IdleState idleState;
    [SerializeField] GetOnPositionState getOnPositionState;

    [SerializeField] Transform desiredPos;
    bool canGetToPosition;


    [Header("Movement")]

    [SerializeField] Transform bookFriend;
    [SerializeField] Transform followPos;


    [Header("Other")]
    [SerializeField] Collider col;


    public override States RunCurrentState() {


        if (canGetToPosition) {

            col.enabled = true;
            return getOnPositionState;
        }


        else {

            col.enabled = false;
            return this;
        }
    }

    private void Update() {


        if ( stateManagerRef.currentState == this ) {


            if ( Vector3.Distance(desiredPos.position, this.transform.position) <= 5 ) {

                canGetToPosition = true;
            }


            else {

                bookFriend.position = Vector3.Lerp(bookFriend.position, followPos.position, Time.deltaTime * 2.5f);
                bookFriend.rotation = Quaternion.Euler(0, 0, -90);
                bookFriend.localScale = new Vector3(10f, 10f, 10f);
            }
        }
    }
}