using UnityEngine;
using System;
using System.Collections;

public class IceCube : MonoBehaviour
{
    public float riseHeight = 1.2f;
    public float riseDuration = 0.25f;
    public int parryDamage = 2;
    private BehaviorFighter owner;
    public KnockbackData knockback;
    private bool hasHit = false;

    public void Init(BehaviorFighter ownerFighter)
    {
        owner = ownerFighter;

        StartCoroutine(RiseRoutine());

    }

    void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        Hitbox hitbox = other.GetComponent<Hitbox>();
        if (hitbox != null)
        {
            BehaviorFighter attacker = hitbox.owner;

            if (attacker != owner && attacker.isAttacking)
            {
                attacker.myHealth.TakeDamage(parryDamage, knockback, owner);
                return;
            }
        }

        FighterHealth enemyHealth = other.GetComponentInParent<FighterHealth>();
        if (enemyHealth != null && enemyHealth != owner.myHealth)
        {
            enemyHealth.TakeDamage(parryDamage, knockback, owner);
        }
        hasHit = true;
    }
    IEnumerator RiseRoutine()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.up * riseHeight;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / riseDuration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
        transform.position = targetPos;
    }
    void OnDestroy()
    {
        if (owner != null)
        {
            owner.isBlocking = false;
        }
    }
}
