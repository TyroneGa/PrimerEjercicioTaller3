using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour {

    public Transform camPos;

    private void Start()
    {
        transform.position = camPos.position;
    }

    private void Update() {

        transform.position = Vector3.Lerp(this.transform.position, camPos.transform.position, .1f);
    }
}
