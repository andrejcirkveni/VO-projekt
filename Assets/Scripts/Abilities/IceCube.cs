using UnityEngine;
using System;
using System.Collections;

public class IceCube : MonoBehaviour
{
    public float riseHeight = 1.2f;
    public float riseDuration = 0.25f;
    public int parryDamage = 2;
    private BehaviorFighter owner;

    public void Init(BehaviorFighter ownerFighter)
    {
        owner = ownerFighter;
        StartCoroutine(RiseRoutine());
    }

    void OnTriggerEnter(Collider other)
    {
        Hitbox hitbox = other.GetComponent<Hitbox>();
        if (hitbox != null)
        {
            BehaviorFighter attacker = hitbox.owner;

            if (attacker != owner && attacker.isAttacking)
            {
                attacker.myHealth.TakeDamage(parryDamage);
                return;
            }
        }

        FighterHealth enemyHealth = other.GetComponent<FighterHealth>();
        if (enemyHealth != null && enemyHealth != owner.myHealth)
        {
            enemyHealth.TakeDamage(parryDamage);
        }
    }
    IEnumerator RiseRoutine()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.up * riseHeight;
        owner.isBlocking = true;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / riseDuration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
        owner.isBlocking = false;
        transform.position = targetPos;
    }
}
