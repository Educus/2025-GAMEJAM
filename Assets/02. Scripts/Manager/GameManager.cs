using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject playerPrefab;

    [SerializeField] public Player player;
    [SerializeField] private GameObject gameOver;

    public int sceneNum { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        // �� �ε� �̺�Ʈ�� ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        // �� �̺�Ʈ ���� ���� (�޸� ���� ����)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetPlayer();
        sceneNum = SceneManager.GetActiveScene().buildIndex;
    }

    public void ResetPlayer()
    {
        // ���� Player ����
        if (player != null)
        {
            Destroy(player.gameObject);
            player = null;
        }

        // �� Player ����
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
        // 5�� ���
        yield return new WaitForSeconds(5f);

        // �� 0���� �̵�
        gameOver.SetActive(false);
        SceneManager.LoadScene(0);
    }

}
