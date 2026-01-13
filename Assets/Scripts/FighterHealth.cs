using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FighterHealth : MonoBehaviour
{
    public int health = 30;
    public int maxHealth = 30;
    public bool isBlocking = false;
    private Animator anim;
    private bool isDead = false;
    public Image healthBar;

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
        health = Mathf.Max(health, 0);
        UpdateHealthBar();
        Debug.Log($"{name} HP: {health}");

        if (knockback.HasValue && fighter != null && !isBlocking)
        {
            fighter.ApplyKnockback(attacker, knockback.Value);
        }

        if (health <= 0)
        {
            health = 0;
            isDead = true;

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
                rb.isKinematic = false;
            }

            anim.applyRootMotion = true;
            anim.SetTrigger("End");
            Debug.Log("KO");

            DisableAllInputs();

            return;
        }

        if (fighter.canBeInterrupted && !isBlocking)
        {
            anim.SetTrigger("Hit");
            fighter.isAttacking = false;
            fighter.EnableNextInput();
        }
    }

    public void EndGame()
    {
        if (!isDead) return;

        BehaviorFighter thisFighter = GetComponent<BehaviorFighter>();
        BehaviorFighter winner = thisFighter.opponent.GetComponent<BehaviorFighter>();

        if (winner != null)
        {
            int winnerIndex = GetCharacterIndexFromGameObject(winner.gameObject);
            PlayerPrefs.SetInt("Winner_Character", winnerIndex);
            PlayerPrefs.Save();

            Debug.Log($"Winner: Character index {winnerIndex}");
        }

        SceneManager.LoadScene("EndScene");
    }

    void DisableAllInputs()
    {
        BehaviorFighter[] allFighters = FindObjectsOfType<BehaviorFighter>();
        foreach (var fighter in allFighters)
        {
            fighter.enabled = false;
        }
    }

    int GetCharacterIndexFromGameObject(GameObject character)
    {
        string charName = character.name.Replace("(Clone)", "").Trim();
        if (charName.Contains("Blue") || charName.Contains("Ice"))
            return 0;
        else if (charName.Contains("Green") || charName.Contains("Nature"))
            return 1;
        else if (charName.Contains("Red") || charName.Contains("Fire"))
            return 2;

        return 0;
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthBar();
        Debug.Log($"{gameObject.name} Healed. HP: {health}");
    }

    void UpdateHealthBar()
    {
        Debug.Log(healthBar != null);
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)health / maxHealth;
        }
    }
}