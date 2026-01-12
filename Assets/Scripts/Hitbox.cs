using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public BehaviorFighter owner;
    public int damage = 1;
    public KnockbackData knockback;

    void OnTriggerEnter(Collider other)
    {
        Hurtbox hurtbox = other.GetComponent<Hurtbox>();
        if (hurtbox == null) return;

        hurtbox.owner.TakeDamage(damage, knockback, owner);

        GetComponent<Collider>().enabled = false;
        
    }
}
