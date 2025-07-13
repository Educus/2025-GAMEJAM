using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject playerPrefab;

    [SerializeField] public Player player;
    [SerializeField] private GameObject gameClear;
    [SerializeField] private GameObject gameOver;

    public bool[] clear { get; private set; } = new bool[5];
    public int sceneNum { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        clear = new bool[] { false, false, false, false, false };
        gameClear.SetActive(false);
        gameOver.SetActive(false);

        // 씬 로드 이벤트에 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        // 씬 이벤트 연결 해제 (메모리 누수 방지)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetPlayer();
        sceneNum = SceneManager.GetActiveScene().buildIndex;
    }

    public void ResetPlayer()
    {
        // 기존 Player 제거
        if (player != null)
        {
            Destroy(player.gameObject);
            player = null;
        }

        // 새 Player 생성
        GameObject playerObj = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player = playerObj.GetComponent<Player>();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        FindObjectOfType<GameOverUI>().ShowGameOver();

        StartCoroutine(DropAndBounceAndLoadScene());
    }
    private IEnumerator DropAndBounceAndLoadScene()
    {
        // 5초 대기
        yield return new WaitForSeconds(5f);

        // 씬 0으로 이동
        gameOver.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void StageClear()
    {
        clear[SceneManager.GetActiveScene().buildIndex - 2] = true;

        StartCoroutine(IEClear());

        gameClear.SetActive(true);
    }

    IEnumerator IEClear()
    {
        yield return new WaitForSeconds(3f);

        gameClear.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
