using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability_Parry", menuName = "Scriptable Objects/Ability_Parry")]
public class Ability_Parry : AbilityAbs
{
    public GameObject iceCubePrefab;
    public float duration = 0.5f;

    public override void Activate(BehaviorFighter user)
    {
        user.anim.SetTrigger("Parry");

    }
    public override void OnAnimationEvent(BehaviorFighter user)
    {
        int facing = user.opponent.position.x > user.transform.position.x ? 1 : -1;
        Vector3 pos = user.transform.position + Vector3.right * facing * 1f+ Vector3.down*0.4f;
        user.isBlocking = true;

        GameObject ice = Object.Instantiate(
            iceCubePrefab,
            pos,
            Quaternion.identity
        );

        ice.GetComponent<IceCube>().Init(user);
        Object.Destroy(ice, duration);
        
    }
}
