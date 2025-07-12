using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject playerPrefab;

    [SerializeField] public Player player;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetPlayer();
    }

    public void ResetPlayer()
    {
        // 扁粮 Player 力芭
        if (player != null)
        {
            Destroy(player.gameObject);
            player = null;
        }

        // 货 Player 积己
        GameObject newPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player = newPlayer.GetComponent<Player>();
    }
}
