using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class STARTSCREEN : MonoBehaviour
{

   public void STARTBUTTON()
    {
        SceneManager.LoadScene("Diseño de Nivel");
    }

   public void EXITBUTTON()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }

    public void MMBUTTON()
    {
        SceneManager.LoadScene("StartScreen");
    }

}
