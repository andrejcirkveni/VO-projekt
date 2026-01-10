using UnityEngine;
using UnityEngine.UI;

public class DirectKeyCharacterSelect : MonoBehaviour
{
    public GameObject[] characters; // 3 modela

    public Text p1Text;
    public Text p2Text;

    private int p1Index = -1;
    private int p2Index = -1;

    private bool[] taken;

    void Start()
    {
        taken = new bool[characters.Length];
        UpdateVisuals();
    }

    void Update()
    {
        // -------- PLAYER 1 --------
        if (p1Index == -1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                TryPick(1, 0);

            if (Input.GetKeyDown(KeyCode.DownArrow))
                TryPick(1, 1);

            if (Input.GetKeyDown(KeyCode.RightArrow))
                TryPick(1, 2);
        }

        // -------- PLAYER 2 --------
        if (p2Index == -1)
        {
            if (Input.GetKeyDown(KeyCode.A))
                TryPick(2, 0);

            if (Input.GetKeyDown(KeyCode.S))
                TryPick(2, 1);

            if (Input.GetKeyDown(KeyCode.D))
                TryPick(2, 2);
        }

        if (p1Index != -1 && p2Index != -1)
        {
            Debug.Log("Oba igrača su odabrala likove");
            // LoadGameplayScene();
        }
    }

    void TryPick(int player, int index)
    {
        if (taken[index])
            return;

        taken[index] = true;

        if (player == 1)
        {
            p1Index = index;
            Debug.Log("P1 uzeo: " + characters[index].name);
        }
        else
        {
            p2Index = index;
            Debug.Log("P2 uzeo: " + characters[index].name);
        }

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
