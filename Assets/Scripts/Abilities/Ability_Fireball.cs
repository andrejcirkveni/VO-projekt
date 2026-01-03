using UnityEngine;

public class Ability_Fireball : AbilityAbs
{
    public override void Activate(BehaviorFighter1 user) { 
        user.anim.SetTrigger("Fireball");
        //nekako spawnat fireball
    }
}
