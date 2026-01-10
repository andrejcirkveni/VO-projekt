using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject aiEnemyPrefab;

    void Start()
    {
        SpawnPlayer();

        if (GameMode.IsSingleplayer)
        {
            SpawnAIEnemy();
        }
        else
        {
            // multiplayer inicijalizacija 
        }
    }

    void SpawnPlayer()
    {
        Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void SpawnAIEnemy()
    {
        Instantiate(aiEnemyPrefab, new Vector3(5, 0, 5), Quaternion.identity);
    }
}

