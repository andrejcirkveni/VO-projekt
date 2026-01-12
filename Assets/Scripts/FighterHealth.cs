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
            Debug.Log($"{name} BLOCKED");
            return;
        }

        BehaviorFighter fighter = GetComponent<BehaviorFighter>();

        health -= dmg;
        Debug.Log($"{name} HP: {health}");

        if (fighter != null && fighter.canBeInterrupted)
        {
            anim.SetTrigger("Hit");

            fighter.isAttacking = false;
            fighter.EnableNextInput();
        }

        if (health <= 0)
            Debug.Log("KO");
    }
    public void Heal(int amount)
    {
        health += amount;
        Debug.Log($"{gameObject.name} Healed. HP: {health}");
    }
}
