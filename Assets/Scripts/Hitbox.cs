using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public BehaviorFighter owner;
    public int damage = 1;


    void OnTriggerEnter(Collider other)
    {
        Hurtbox hurtbox = other.GetComponent<Hurtbox>();
        if (hurtbox == null) return;

        hurtbox.owner.TakeDamage(damage);

        GetComponent<Collider>().enabled = false;
        
    }
}
