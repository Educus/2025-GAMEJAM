using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StageSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
