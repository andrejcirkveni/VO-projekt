using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class BehaviorFighter : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction guardAction;
    private InputAction abilityAction;
    private InputAction quickstepAction;
    private InputAction heavyAction;

    public InputActionMap map;

    public Transform opponent;
    public AbilityAbs ability;

    public float moveSpeed = 5f;

    public CapsuleCollider rightHandHitbox;
    public CapsuleCollider leftHandHitbox;
    public CapsuleCollider rightFootHitbox;
    public CapsuleCollider leftFootHitbox;

    public Animator anim;
    public FighterHealth myHealth;

    private int comboStep = 0;
    private bool canReceiveInput = true;
    public bool isAttacking;
    public bool isBlocking;

    public float arenaLeft = -4f;
    public float arenaRight = 4f;
    public float minDistanceToOpponent = 1f;

    public bool canBeInterrupted = true;


    void Awake()
    {
        anim = GetComponent<Animator>();
        myHealth = GetComponent<FighterHealth>();

        rightHandHitbox.enabled = false;
        leftHandHitbox.enabled = false;
        rightFootHitbox.enabled = false;
        leftFootHitbox.enabled = false;
        rightHandHitbox.GetComponent<Hitbox>().owner = this;
        leftHandHitbox.GetComponent<Hitbox>().owner = this;
        rightFootHitbox.GetComponent<Hitbox>().owner = this;
        leftFootHitbox.GetComponent<Hitbox>().owner = this;
    }

    void OnEnable()
    {
        map?.Enable();
    }

    void OnDisable()
    {
        map?.Disable();
    }

    void Update()
    {
        if (moveAction == null) return;
        HandleAbility();
        HandleAttack();
        HandleGuard();
        HandleMovement();
    }
    void LateUpdate()
    {
        int facing = opponent.position.x > transform.position.x ? 1 : -1;
        transform.rotation = Quaternion.Euler(0f, facing*90f, 0f);
    }


    void HandleAbility()
    {
        if (!canReceiveInput) return;

        if (abilityAction.WasPressedThisFrame())
        {
            isAttacking = true;
            ability?.Activate(this);
            canReceiveInput = false;
        }
    }

    void HandleAttack()
    {
        if (!canReceiveInput) return;

            if (attackAction.WasPressedThisFrame())
        {
            isAttacking = true;
            comboStep = comboStep % 3 + 1;

            anim.SetInteger("combo_step", comboStep);

            if (heavyAction.IsPressed())
                anim.SetTrigger("Heavy_attack");
            else
                anim.SetTrigger("Light_attack");

            anim.SetBool("Walk_F", false);
            anim.SetBool("Walk_B", false);

            canReceiveInput = false;
        }
    }

    void HandleGuard()
    {
        isBlocking = guardAction.IsPressed();

        anim.SetBool("Guard", isBlocking);
        myHealth.isBlocking = isBlocking;
    }

    void HandleMovement()
    {
        if (!canReceiveInput || isAttacking || isBlocking)
        {
            anim.SetBool("Walk_F", false);
            anim.SetBool("Walk_B", false);
            return;
        }

        float moveDir = moveAction.ReadValue<float>();

        if (Mathf.Abs(moveDir) < 0.01f)
        {
            anim.SetBool("Walk_F", false);
            anim.SetBool("Walk_B", false);
            return;
        }

        if (quickstepAction.WasPressedThisFrame())
        {
            anim.SetTrigger(moveDir > 0 ? "F_Quickstep" : "B_Quickstep");
            canReceiveInput = false;
            return;
        }

        int facing = opponent.position.x > transform.position.x ? 1 : -1;
        float distance = Mathf.Abs(opponent.position.x - transform.position.x);

        if ((moveDir < 0 && transform.position.x <= arenaLeft) ||
            (moveDir > 0 && transform.position.x >= arenaRight) ||
            (moveDir == facing && distance < minDistanceToOpponent))
        {
            anim.SetBool("Walk_F", false);
            anim.SetBool("Walk_B", false);
            return;
        }

        anim.SetBool("Walk_F", moveDir == facing);
        anim.SetBool("Walk_B", moveDir == -facing);

        transform.position += Vector3.right * moveDir * moveSpeed * Time.deltaTime;
    }


    public void EnableNextInput()
    {
        canReceiveInput = true;
        isAttacking = false;
    }

    public void ResetCombo()
    {
        comboStep = 0;
        anim.SetInteger("combo_step", 0);
        EnableNextInput();
    }

    public void AbilityEvent()
    {
        ability?.OnAnimationEvent(this);
    }

    public void EnableHitbox(string name)
    {
        GetHitbox(name).enabled = true;
    }

    public void DisableHitbox(string name)
    {
        GetHitbox(name).enabled = false;
    }

    CapsuleCollider GetHitbox(string name)
    {
        return name switch
        {
            "RightHand" => rightHandHitbox,
            "LeftHand" => leftHandHitbox,
            "RightFoot" => rightFootHitbox,
            "LeftFoot" => leftFootHitbox,
            _ => null
        };
    }

    public void Quickstep(float dir)
    {
        StartCoroutine(QuickstepRoutine(dir));
    }
    IEnumerator QuickstepRoutine(float dir)
    {
        int frames = 15;
        float distance = 1f;

        int facing = opponent.position.x > transform.position.x ? 1 : -1;

        float startX = transform.position.x;
        float targetX = startX + dir * distance;

        float opponentLimit = opponent.position.x - facing * minDistanceToOpponent;

        if (facing == 1)
            targetX = Mathf.Min(targetX, opponentLimit);
        else
            targetX = Mathf.Max(targetX, opponentLimit);

        targetX = Mathf.Clamp(targetX, arenaLeft, arenaRight);

        float stepPerFrame = (targetX - startX) / frames;

        for (int i = 0; i < frames; i++)
        {
            transform.position += Vector3.right * stepPerFrame;
            yield return null;
        }
    }

    public void Snap_To_Groud()
    {
        Vector3 pos = transform.position;
        pos.y = 0f;
        transform.position = pos;
    }
    public void Attack_Startup() => canBeInterrupted = true;
    public void Attack_Active() => canBeInterrupted = false;
    public void Attack_Recovery() => canBeInterrupted = true;

    public void ApplyKnockback(BehaviorFighter attacker, KnockbackData data)
    {
        StopAllCoroutines();
        StartCoroutine(KnockbackRoutine(attacker, data));
    }
    IEnumerator KnockbackRoutine(BehaviorFighter attacker, KnockbackData data)
    {
        canReceiveInput = false;

        int dir = attacker.transform.position.x < transform.position.x ? 1 : -1;

        float time = 0f;
        while (time < data.duration)
        {
            transform.position += Vector3.right * dir * data.force * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }

        canReceiveInput = true;
    }

    public void InitializeInputActions()
    {
        if (inputActions == null)
        {
            Debug.LogError("InputActionAsset is null!");
            return;
        }

        moveAction = map?.FindAction("Move");
        attackAction = map?.FindAction("Attack");
        guardAction = map?.FindAction("Guard");
        abilityAction = map?.FindAction("Ability");
        quickstepAction = map?.FindAction("Quickstep");
        heavyAction = map?.FindAction("Heavy");
    }

    public void SetActionMap(string actionMapName)
    {
        map?.Disable();
        map = inputActions.FindActionMap(actionMapName);
        map?.Enable();

        InitializeInputActions();
    }
}