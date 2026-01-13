using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    [Header("Character Prefabs")]
    public GameObject[] characterPrefabs;

    [Header("Winner Settings")]
    public Transform spawnPoint;

    [Header("UI Elements")]
    public Button backButton;

    private GameObject spawnedWinner;

    void Start()
    {
        int winnerIndex = PlayerPrefs.GetInt("Winner_Character", 0);

        if (characterPrefabs != null && characterPrefabs.Length > winnerIndex && spawnPoint != null)
        {
            spawnedWinner = Instantiate(
                characterPrefabs[winnerIndex],
                spawnPoint.position,
                Quaternion.Euler(
                    spawnPoint.rotation.eulerAngles.x,
                    spawnPoint.rotation.eulerAngles.y + 90f,
                    spawnPoint.rotation.eulerAngles.z
                )
            );

            spawnedWinner.transform.localScale = new Vector3(85f, 85f, 85f);

            BehaviorFighter fighter = spawnedWinner.GetComponent<BehaviorFighter>();
            if (fighter != null)
            {
                fighter.enabled = false;
            }

            Rigidbody rb = spawnedWinner.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            Animator anim = spawnedWinner.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("Victory");
            }
        }
        else
        {
            Debug.LogWarning("Character prefabs ili SpawnPoint nisu postavljeni!");
        }

        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }
        else
        {
            Debug.LogWarning("BackButton nije postavljen!");
        }
    }

    void OnBackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}