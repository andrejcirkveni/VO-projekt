using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    [Header("Winner Settings")]
    public GameObject winnerPrefab; // Prefab pobjednika
    public Transform spawnPoint;    // Mjesto gdje će model biti prikazan

    [Header("UI Elements")]
    public Button backButton;       // Back gumb

    private GameObject spawnedWinner;

    void Start()
    {
        // Prikaz pobjedničkog modela
        if (winnerPrefab != null && spawnPoint != null)
        {
            spawnedWinner = Instantiate(winnerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("WinnerPrefab ili SpawnPoint nisu postavljeni!");
        }

        // Dodaj listener na Back gumb
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
        // Povratak na MainMenu scenu
        SceneManager.LoadScene("MainMenu");
    }
}

