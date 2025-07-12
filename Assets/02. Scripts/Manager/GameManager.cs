using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject playerPrefab;

    [SerializeField] public Player player;

    protected override void Awake()
    {
        base.Awake();

        // �� �ε� �̺�Ʈ�� ���
        // SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        // �� �̺�Ʈ ���� ���� (�޸� ���� ����)
        // SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetPlayer();
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
}
