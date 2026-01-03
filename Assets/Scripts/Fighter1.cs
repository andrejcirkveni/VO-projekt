using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class BehaviorFighter1 : MonoBehaviour
{
    public Transform fighter2;

    public float moveSpeed = 5f;

    public BoxCollider rightHandHitbox;
    public BoxCollider leftHandHitbox;
    public BoxCollider rightFootHitbox;
    public BoxCollider leftFootHitbox;

    public Collider fighter2_hitbox;

    public Animator anim;
    private int combo_step = 0;
    private bool canReceiveInput = true;

    public bool isAttacking = false;
    public bool blocking = false;

    public FighterHealth myHealth;

    public AbilityAbs ability;



    void Start()
    {
        anim = GetComponent<Animator>();
        rightFootHitbox.enabled = false;
        leftFootHitbox.enabled = false;
        rightHandHitbox.enabled = false;
        leftHandHitbox.enabled = false;
        myHealth = GetComponent<FighterHealth>();

    }

    void Update()
    {
        //temp
        if (Keyboard.current.rKey.wasPressedThisFrame && canReceiveInput)
        {
            ability?.Activate(this);
            isAttacking = true;
        }
        //


        if (Mouse.current.leftButton.wasPressedThisFrame && canReceiveInput)
        {
            isAttacking = true;
            combo_step++;
            if (combo_step > 3) combo_step = 1;

            anim.SetInteger("combo_step", combo_step);
            if (Keyboard.current.leftShiftKey.isPressed)
            {
                anim.SetTrigger("Heavy_attack");
            }
            else
            {
                anim.SetTrigger("Light_attack");
            }
            anim.SetBool("Walk_F", false);
            anim.SetBool("Walk_B", false);

            canReceiveInput = false;


        }

        if (Mouse.current.rightButton.wasPressedThisFrame && canReceiveInput) {
            anim.SetBool("Guard", true);
            blocking = true;
            myHealth.isBlocking = true;
        }
        else if (Mouse.current.rightButton.wasReleasedThisFrame) {
            anim.SetBool("Guard", false);
            blocking = false;
            myHealth.isBlocking = false;
        }

        if (isAttacking || blocking)
        {
            anim.SetBool("Walk_F", false);
            anim.SetBool("Walk_B", false);
        }
        else if(canReceiveInput)
        {
            float moveDir = 0f;

            if (Keyboard.current.aKey.isPressed &&
                transform.position.x > -4 &&
                Mathf.Abs(transform.position.x - fighter2.position.x) < 4)
            {
                if (Keyboard.current.dKey.isPressed)
                {
                    moveDir = 0f;
                }
                else if (Keyboard.current.spaceKey.isPressed)
                {
                    moveDir = 0f;
                    Debug.Log("Quickstep");
                    anim.SetTrigger("B_Quickstep");
                    canReceiveInput = false;
                }
                else
                {
                    moveDir = -1f;
                    anim.SetBool("Walk_B", true);
                }
            }
            else if (Keyboard.current.dKey.isPressed &&
                     transform.position.x < (fighter2.position.x - 1))
            {
                if (Keyboard.current.aKey.isPressed)
                {
                    moveDir = 0f;
                }
                else if (Keyboard.current.spaceKey.isPressed)
                {
                    moveDir = 0f;
                    Debug.Log("Quickstep");
                    anim.SetTrigger("F_Quickstep");
                    canReceiveInput = false;
                }
                else
                {
                    moveDir = 1f;
                    anim.SetBool("Walk_F", true);
                }

            }

            if (moveDir == 0f)
            {
                anim.SetBool("Walk_B", false);
                anim.SetBool("Walk_F", false);
            }

            if (moveDir != 0f)
            {
                transform.position += Vector3.right * moveDir * moveSpeed * Time.deltaTime;
            }
        }


    }

    public void EnableNextInput()
    {
        canReceiveInput = true;
        isAttacking = false;
    }
    public void ResetCombo()
    {
        combo_step = 0;
        anim.SetInteger("combo_step", 0);
        EnableNextInput();
    }

    public void EnableHitbox(string hitboxName)
    {
        if (hitboxName == "RightHand")
        {
            rightHandHitbox.enabled = true;
        }
        else if (hitboxName == "LeftHand")
        {
            leftHandHitbox.enabled = true;
        }
        else if (hitboxName == "RightFoot")
        {
            rightFootHitbox.enabled = true;
        }
        else if (hitboxName == "LeftFoot")
        {
            leftFootHitbox.enabled = true;
        }
    }

    public void DisableHitbox(string hitboxName)
    {
        if (hitboxName == "RightHand")
        {
            rightHandHitbox.enabled = false;
        }
        else if (hitboxName == "LeftHand")
        {
            leftHandHitbox.enabled = false;
        }
        else if (hitboxName == "RightFoot")
        {
            rightFootHitbox.enabled = false;
        }
        else if (hitboxName == "LeftFoot")
        {
            leftFootHitbox.enabled = false;
        }
    }

    public void Quickstep(float dir) {
        StartCoroutine(QuickstepRoutine(dir));
    }

    IEnumerator QuickstepRoutine(float dir)
    {
        int frames = 15;
        float distance = 1f;

        float startX = transform.position.x;
        float targetX = startX + dir * distance;

        if (dir > 0)
        {
            float limit = fighter2.position.x - 1f;
            if (targetX > limit)
                targetX = limit;
        }
        else
        {
            float limit = -4f;
            if (targetX < limit)
                targetX = limit;
        }

        float stepPerFrame = (targetX - startX) / frames;

        for (int i = 0; i < frames; i++)
        {
            transform.position += Vector3.right * stepPerFrame;
            yield return null;
        }
    }


}
