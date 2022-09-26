using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public States currentState;


    private void Update() {

        RunStateMAchine();
    }


    void RunStateMAchine() {

        States nextState = currentState ?.RunCurrentState();


        if (nextState != null)
            SwitchToNextState(nextState);
    }


    void SwitchToNextState(States nextState) {

        currentState = nextState;
    }
}
