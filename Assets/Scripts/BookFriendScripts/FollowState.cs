using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : States {

    [Header("State Logic")]
    public GetOnPositionState getOnPositionState;
    public Transform desiredPos;
    public bool canGetToPosition;

    [Header("Movement")]
    public Transform bookFriend;
    public Transform followPos;


    public override States RunCurrentState() {

        if (canGetToPosition)
            return getOnPositionState;

        else
            return this;
    }

    private void Update() {

        if (Vector3.Distance(desiredPos.position, this.transform.position) <= 5) {

            canGetToPosition = true;
        }

        else {
            bookFriend.position = followPos.position;

            if (bookFriend.position == followPos.position) {

                bookFriend.rotation = Quaternion.Euler(90, followPos.rotation.y, followPos.rotation.z);
                bookFriend.localScale = new Vector3(.5f, .5f, .5f);
            }
        }
    }
}