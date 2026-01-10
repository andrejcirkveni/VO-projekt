using UnityEngine;

[CreateAssetMenu(fileName = "Ability_Fireball", menuName = "Scriptable Objects/Ability_Fireball")]
public class Ability_Fireball : AbilityAbs
{
    public GameObject fireballPrefab;
    public float spawnOffset = 0.8f;

    public override void Activate(BehaviorFighter fighter)
    {
        base.Activate(fighter);
        fighter.anim.SetTrigger("Fireball");
    }

    public override void OnAnimationEvent(BehaviorFighter fighter)
    {
        int dir = user.transform.position.x < user.opponent.position.x ? 1 : -1;

        Vector3 spawnPos = user.rightHandHitbox.transform.position;

        GameObject fb = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);

        FighterHealth target = user.opponent.GetComponent<FighterHealth>();
        fb.GetComponent<Fireball>().Init(dir, target);
    }
}
