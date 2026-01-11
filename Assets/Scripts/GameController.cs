using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Character Prefabs (isti redoslijed kao u selectu)")]
    public GameObject[] characterPrefabs;

    [Header("Spawn Points")]
    public Transform player1Spawn;
    public Transform player2Spawn;

    public GameObject aiEnemyPrefab;

    void Start()
    {
        int p1Index = PlayerPrefs.GetInt("P1_Character", 0);
        int p2Index = PlayerPrefs.GetInt("P2_Character", 1);

        SpawnPlayer1(p1Index);

        if (GameMode.IsSingleplayer)
        {
            SpawnAIEnemy();
        }
        else
        {
            SpawnPlayer2(p2Index);
        }
    }

    void SpawnPlayer1(int index)
    {
        Instantiate(
            characterPrefabs[index],
            player1Spawn.position,
            player1Spawn.rotation
        );
    }

    void SpawnPlayer2(int index)
    {
        Instantiate(
            characterPrefabs[index],
            player2Spawn.position,
            player2Spawn.rotation
        );
    }

    void SpawnAIEnemy()
    {
        Instantiate(
            aiEnemyPrefab,
            player2Spawn.position,
            player2Spawn.rotation
        );
    }
}


