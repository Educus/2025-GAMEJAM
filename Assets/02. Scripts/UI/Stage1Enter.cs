using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Enter : MonoBehaviour
{
    public Texture2D cursorTexture; 
    public Vector2 hotspot = Vector2.zero;

    public void EnterStage1()
    {
        SceneManager.LoadScene("Stage 1");
    }
    }
