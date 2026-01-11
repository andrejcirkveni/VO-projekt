using UnityEngine;

[CreateAssetMenu(fileName = "Ability_Parry", menuName = "Scriptable Objects/Ability_Parry")]
public class Ability_Parry : AbilityAbs
{
    public override void Activate(BehaviorFighter user)
    {
        user.anim.SetTrigger("Parry");

    }
    public override void OnAnimationEvent(BehaviorFighter user)
    {
        return;
    }
}
