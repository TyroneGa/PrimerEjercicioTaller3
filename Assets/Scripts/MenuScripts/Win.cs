using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.tag == "Player" )
        {

            print("COLLISION");
            SceneManager.LoadScene("Win" , LoadSceneMode.Single);

        }
    }

}
