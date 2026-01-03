using UnityEngine;

public class Ability_Heal : AbilityAbs
{
    public override void Activate(BehaviorFighter1 user)
    {
        user.anim.SetTrigger("Heal");

        //možda stavit kao event u animaciju da se heala sa delayom, tako da se moze interruptat
        user.myHealth.Heal(3);
    }
}
