using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class STARTSCREEN : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            STARTBUTTON();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EXITBUTTON();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            MMBUTTON();
        }
    }
    public void STARTBUTTON()
    {
        SceneManager.LoadScene("Diseño de Nivel II");
    }

   public void EXITBUTTON()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }

    public void MMBUTTON()
    {
        
        SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
    }

}
