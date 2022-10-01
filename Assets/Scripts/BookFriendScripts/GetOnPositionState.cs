using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnPositionState : States {

    public FollowState followState;

    public Transform bookFriend;
    public Transform desiredPos;

    public override States RunCurrentState() {

        followState.enabled = false;
        return this;
    }


    private void Update() {

        if (Vector3.Distance(desiredPos.position, this.transform.position) <= 5) {

            bookFriend.position = desiredPos.position;
            bookFriend.rotation = Quaternion.Euler(0, 0, 0);
            bookFriend.localScale = new Vector3(5f, 5f, 5f);
        }
    }
}