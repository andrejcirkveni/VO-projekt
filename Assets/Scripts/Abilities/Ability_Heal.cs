using UnityEngine;

[CreateAssetMenu(fileName = "Ability_Heal", menuName = "Scriptable Objects/Ability_Heal")]
public class Ability_Heal : AbilityAbs
{

    public GameObject healFXPrefab;

    public override void Activate(BehaviorFighter user)
    {
        base.Activate(user);
        user.anim.SetTrigger("Heal");
    }
    public override void OnAnimationEvent(BehaviorFighter user) {
        user.myHealth.Heal(3);
        if (healFXPrefab != null)
        {
            Vector3 pos = user.transform.position;
            pos.y = 0f;
            GameObject fx = Instantiate(
                healFXPrefab,
                pos,
                Quaternion.identity
            );

            
            Destroy(fx, 1f);
        }
    }
    
}
