using UnityEngine;

public abstract class AbilityAbs : ScriptableObject
{
    public string abilityName;
    public float cooldown;

    protected BehaviorFighter user;

    public virtual void Activate(BehaviorFighter fighter)
    {
        user = fighter;
    }

    public abstract void OnAnimationEvent(BehaviorFighter fighter);
}
