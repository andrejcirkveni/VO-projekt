using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DirectKeyCharacterSelect : MonoBehaviour
{
    public GameObject[] characters;
    public Text p1Text;
    public Text p2Text;

    private int p1Index = -1;
    private int p2Index = -1;
    private bool[] taken;
    private bool gameStarted = false;

    void Start()
    {
        taken = new bool[characters.Length];
        UpdateVisuals();
    }

    void Update()
    {
        if (p2Index == -1)
        {
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
                TryPick(2, 0);
            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
                TryPick(2, 1);
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
                TryPick(2, 2);
        }

        if (p1Index == -1)
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
                TryPick(1, 0);
            if (Keyboard.current.sKey.wasPressedThisFrame)
                TryPick(1, 1);
            if (Keyboard.current.dKey.wasPressedThisFrame)
                TryPick(1, 2);
        }

        if (!gameStarted && p1Index != -1 && p2Index != -1)
        {
            gameStarted = true;

            PlayerPrefs.SetInt("P1_Character", p1Index);
            PlayerPrefs.SetInt("P2_Character", p2Index);

            SceneManager.LoadScene("GameScene");
        }
    }

    void TryPick(int player, int index)
    {
        Debug.Log("Pokušaj biranja: P" + player + " lik " + index);
        if (taken[index])
            return;

        taken[index] = true;

        if (player == 1)
            p1Index = index;
        else
            p2Index = index;

        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        for (int i = 0; i < characters.Length; i++)
            characters[i].transform.localScale = Vector3.one;

        if (p1Index != -1)
            characters[p1Index].transform.localScale = Vector3.one * 1.2f;

        if (p2Index != -1)
            characters[p2Index].transform.localScale = Vector3.one * 1.2f;

        p1Text.text = p1Index == -1 ? "P1: ← ↓ →" : "P1 READY";
        p2Text.text = p2Index == -1 ? "P2: A S D" : "P2 READY";
    }
}

