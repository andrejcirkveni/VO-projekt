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
if (winnerPrefab != null && spawnPoint != null)
    {
        // Instanciraj model sa rotacijom 180° po Y
        spawnedWinner = Instantiate(
            winnerPrefab, 
            spawnPoint.position, 
            Quaternion.Euler(spawnPoint.rotation.eulerAngles.x, spawnPoint.rotation.eulerAngles.y + 90f, spawnPoint.rotation.eulerAngles.z)
        );

        // Postavi scale na 85,85,85
        spawnedWinner.transform.localScale = new Vector3(85f, 85f, 85f);
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

