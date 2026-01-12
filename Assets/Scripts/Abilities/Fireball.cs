using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 6f;
    public int damage = 2;
    public float lifeTime = 3f;

    private int direction;
    private FighterHealth ownerTarget;

    public void Init(int dir, FighterHealth target)
    {
        direction = dir;
        ownerTarget = target;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        FighterHealth health = other.GetComponent<FighterHealth>();
        if (health == null) return;

        if (health == ownerTarget)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

