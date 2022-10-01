using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : States {

    [Header("State Logic")]

    [SerializeField] IdleState idleState;
    [SerializeField] GetOnPositionState getOnPositionState;

    [SerializeField] Transform desiredPos;
    bool canGetToPosition;

    [Header("Movement")]

    [SerializeField] Transform bookFriend;
    [SerializeField] Transform followPos;


    public override States RunCurrentState() {

        if (canGetToPosition) {

            getOnPositionState.enabled = true;
            return getOnPositionState;
        }

        else {

            idleState.enabled = false;
            return this;
        }
    }

    private void Update() {

        if (Vector3.Distance(desiredPos.position, this.transform.position) <= 5) {

            canGetToPosition = true;
        }

        else {

            bookFriend.position = Vector3.Lerp(bookFriend.position, followPos.position, 1000f);
            bookFriend.rotation = Quaternion.Euler(90, followPos.rotation.y, followPos.rotation.z);
            bookFriend.localScale = new Vector3(.5f, .5f, .5f);
        }
    }
}