using UnityEngine;

[CreateAssetMenu(fileName = "Ability_Heal", menuName = "Scriptable Objects/Ability_Heal")]
public class Ability_Heal : AbilityAbs
{
    public override void Activate(BehaviorFighter user)
    {
        user.anim.SetTrigger("Heal");
        base.Activate(user);
    }
    public override void OnAnimationEvent(BehaviorFighter user) {
        user.myHealth.Heal(3);
    }
    
}
