using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 6f;
    public int damage = 2;
    public float lifeTime = 3f;

    private int direction;
    private FighterHealth target;
    public KnockbackData knockback;
    public BehaviorFighter owner;

    public void Init(int dir, FighterHealth targetFighter, BehaviorFighter ownerFighter)
    {
        direction = dir;
        target = targetFighter;
        owner = ownerFighter;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        FighterHealth health = other.GetComponentInParent<FighterHealth>();
        if (health == null) return;

        if (health == target)
        {
            health.TakeDamage(damage, knockback, owner);
            Destroy(gameObject);
        }
    }
}

