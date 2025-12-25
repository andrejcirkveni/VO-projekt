using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage = 1;


    void OnTriggerEnter(Collider other)
    {
        FighterHealth health = other.GetComponent<FighterHealth>();
        if (health == null) return;

        health.TakeDamage(damage);

        gameObject.GetComponent<Collider>().enabled = false;
    }
}
