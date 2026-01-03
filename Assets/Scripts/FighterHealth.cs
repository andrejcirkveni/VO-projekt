using UnityEngine;

public class FighterHealth : MonoBehaviour
{
    public int health = 10;
    public bool isBlocking = false;

    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int dmg)
    {
        if (isBlocking)
        {
            Debug.Log($"{gameObject.name} BLOCKED attack");
            return;
        }

        health -= dmg;
        Debug.Log($"{gameObject.name} HP: {health}");
        anim.SetTrigger("Hit");

        if (health <= 0)
        {
            Debug.Log("KO");
        }
    }
}
