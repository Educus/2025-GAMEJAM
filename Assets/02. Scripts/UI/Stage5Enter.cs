using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage5Enter : MonoBehaviour
{
    public void EnterStage5()
    {
        SceneManager.LoadScene("Stage 5");
    }

    void OnMouseUp()
    {
        Color color = GetComponent<Renderer>().material.color;
        color.a = 0.5f; // Set alpha to 50% 
    }
}
