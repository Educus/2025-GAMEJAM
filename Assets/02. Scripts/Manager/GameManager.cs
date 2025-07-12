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

        // 씬 로드 이벤트에 등록
        // SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        // 씬 이벤트 연결 해제 (메모리 누수 방지)
        // SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetPlayer();
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
}
