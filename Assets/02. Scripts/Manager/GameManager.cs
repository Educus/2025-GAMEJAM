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
        // ���� Player ����
        if (player != null)
        {
            Destroy(player.gameObject);
            player = null;
        }

        // �� Player ����
        GameObject newPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player = newPlayer.GetComponent<Player>();
    }
}
