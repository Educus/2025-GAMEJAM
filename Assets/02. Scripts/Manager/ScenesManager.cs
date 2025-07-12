using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject setting;

    /// <summary>
    /// 씬 이름으로 씬을 로드합니다.
    /// </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 빌드 인덱스를 이용해 씬을 로드합니다.
    /// </summary>
    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    /// <summary>
    /// 씬을 비동기로 로드합니다.
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
            // 로딩 중 처리 (예: UI 업데이트)
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
        // 미구현
    }

    public void Back()
    {
        setting.SetActive(false);
    }

}