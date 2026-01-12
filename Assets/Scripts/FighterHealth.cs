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

    public void TakeDamage(int dmg, KnockbackData? knockback, BehaviorFighter? attacker)
    {
        if (isBlocking)
        {
            Debug.Log($"{name} BLOCKED");
            return;
        }

        BehaviorFighter fighter = GetComponent<BehaviorFighter>();
        if (health-dmg <= 0)
        {
            health = 0;
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
                rb.isKinematic = false;
            }

            anim.applyRootMotion = true;
            anim.SetTrigger("End");
            Debug.Log("KO");
            return;
        }

        health -= dmg;
        Debug.Log($"{name} HP: {health}");

        if (knockback.HasValue && fighter != null)
        {
            fighter.ApplyKnockback(attacker, knockback.Value);
        }

        if (fighter.canBeInterrupted)
        {
            anim.SetTrigger("Hit");

            fighter.isAttacking = false;
            fighter.EnableNextInput();
        }

        
            
    }
    public void Heal(int amount)
    {
        
        health += amount;
        if (health < 10) health = 10;
        Debug.Log($"{gameObject.name} Healed. HP: {health}");
    }
}
