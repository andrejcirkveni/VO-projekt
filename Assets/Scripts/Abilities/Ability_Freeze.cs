using UnityEngine;

public class Ability_Freeze : AbilityAbs
{
    public override void Activate(BehaviorFighter1 user)
    {
        user.anim.SetTrigger("Freeze");
        //ne znam što bi ovaj ability bio
    }
}
