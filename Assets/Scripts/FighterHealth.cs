using UnityEngine;
using Ilumisoft.HealthSystem;

public class FighterHealth : MonoBehaviour
{
    public bool isBlocking = false;

    private Animator anim;
    private Health health; // Ilumisoft Health

    void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();

        if (health == null)
        {
            Debug.LogError("Ilumisoft Health component missing!");
            return;
        }

        // reagiraj na smrt
        health.OnHealthEmpty += OnKO;
    }

    public void TakeDamage(float dmg)
    {
        if (isBlocking)
        {
            Debug.Log($"{gameObject.name} BLOCKED attack");
            return;
        }

        health.ApplyDamage(dmg);  

        anim.SetTrigger("Hit");
        Debug.Log($"{gameObject.name} HP: {health.CurrentHealth}");
    }

    public void Heal(float amount)
    {
        health.AddHealth(amount);
    }

    private void OnKO()
    {
        Debug.Log("KO");
        anim.SetTrigger("KO");
    }

    private void OnDestroy()
    {
        if (health != null)
            health.OnHealthEmpty -= OnKO;
    }
}
