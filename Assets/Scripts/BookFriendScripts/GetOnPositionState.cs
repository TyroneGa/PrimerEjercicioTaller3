using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnPositionState : States {

    [SerializeField] StateManager stateManagerRef;
    [SerializeField] FollowState followState;

    [SerializeField] Transform bookFriend;
    [SerializeField] Transform desiredPos;

    public override States RunCurrentState() {

        return this;
    }


    private void Update() {

        if ( stateManagerRef.currentState == this ) {

            if ( Vector3.Distance(desiredPos.position, this.transform.position) <= 5 ) {

                bookFriend.position = desiredPos.position;
                bookFriend.rotation = Quaternion.Euler(0, 0, 90);
                bookFriend.localScale = new Vector3(150f, 150f, 150f);
            }
        }
    }
}