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
            dmg = 0;
        }

        BehaviorFighter fighter = GetComponent<BehaviorFighter>();

        health -= dmg;
        Debug.Log($"{name} HP: {health}");

        if (knockback.HasValue && fighter != null && !isBlocking)
        {
            fighter.ApplyKnockback(attacker, knockback.Value);
        }

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

        if (fighter.canBeInterrupted && !isBlocking)
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
