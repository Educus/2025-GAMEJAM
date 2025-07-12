using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject setting;

    /// <summary>
    /// �� �̸����� ���� �ε��մϴ�.
    /// </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// ���� �ε����� �̿��� ���� �ε��մϴ�.
    /// </summary>
    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    /// <summary>
    /// ���� �񵿱�� �ε��մϴ�.
    /// </summary>
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncRoutine(sceneName));
    }

    private IEnumerator LoadSceneAsyncRoutine(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            // �ε� �� ó�� (��: UI ������Ʈ)
            yield return null;
        }
    }

    public void OnSetting()
    {
        setting.SetActive(true);
    }
    public void Home()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 1)
        {
            SceneManager.LoadScene(0);
        }
        else if (currentScene >= 2)
        {
            SceneManager.LoadScene(1);
        }

        Back();
    }

    public void Setting()
    {
        // �̱���
    }

    public void Back()
    {
        setting.SetActive(false);
    }

}