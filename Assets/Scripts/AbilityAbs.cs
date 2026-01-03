using UnityEngine;

public abstract class AbilityAbs : ScriptableObject
{
    public string abilityName;
    public float cooldown;

    public abstract void Activate(BehaviorFighter1 user);
}
