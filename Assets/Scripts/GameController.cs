using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Character Prefabs")]
    public GameObject[] characterPrefabs;

    [Header("Spawn Points")]
    public Transform player1Spawn;
    public Transform player2Spawn;

    [Header("Input configs")]
    public InputActionAsset inputActions;

    [Header("Camera")]
    public CameraBehaviour fightCamera;

    [Header("UI")]
    public Image player1HealthBar;
    public Image player2HealthBar;

    public GameObject aiEnemyPrefab;

    private BehaviorFighter player1;
    private BehaviorFighter player2;


    void Start()
    {
        int p1Index = PlayerPrefs.GetInt("P1_Character", 0);
        int p2Index = PlayerPrefs.GetInt("P2_Character", 1);

        player1 = SpawnPlayer(p1Index, player1Spawn);
        player1.SetActionMap("Fighter1");
        player1.GetComponent<FighterHealth>().healthBar = player1HealthBar;

        if (GameMode.IsSingleplayer)
        {
            player2 = SpawnAI();
        }
        else
        {
            player2 = SpawnPlayer(p2Index, player2Spawn);
            player2.SetActionMap("Fighter2");
        }
        player2.GetComponent<FighterHealth>().healthBar = player2HealthBar;

        player1.opponent = player2.transform;
        player2.opponent = player1.transform;

        fightCamera.fighter1 = player1.transform;
        fightCamera.fighter2 = player2.transform;



    }

    BehaviorFighter SpawnPlayer(int index, Transform spawn)
    {
        GameObject go = Instantiate(
        characterPrefabs[index],
        spawn.position,
        spawn.rotation
    );

        BehaviorFighter fighter = go.GetComponent<BehaviorFighter>();

        fighter.inputActions = Instantiate(inputActions);

        return fighter;
    }

    BehaviorFighter SpawnAI()
    {
        GameObject go = Instantiate(
        aiEnemyPrefab,
        player2Spawn.position,
        player2Spawn.rotation
    );

        BehaviorFighter fighter = go.GetComponent<BehaviorFighter>();
        fighter.inputActions = Instantiate(inputActions);

        return fighter;
    }
}
