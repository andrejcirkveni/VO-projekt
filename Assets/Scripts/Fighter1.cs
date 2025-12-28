using System;
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

    private Animator anim;
    private int combo_step = 0;
    private bool canReceiveInput = true;

    public bool isAttacking = false;
    public bool blocking = false;




    void Start()
    {
        anim = GetComponent<Animator>();
        rightFootHitbox.enabled = false;
        leftFootHitbox.enabled = false;
        rightHandHitbox.enabled = false;
        leftHandHitbox.enabled = false;

    }

    void Update()
    {

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

            canReceiveInput = false;


        }

        if (Mouse.current.rightButton.wasPressedThisFrame && canReceiveInput) {
            anim.SetBool("Guard", true);
            blocking = true;
        }
        else if (Mouse.current.rightButton.wasReleasedThisFrame) {
            anim.SetBool("Guard", false);
            blocking = false;
        }

        if (isAttacking || blocking)
        {
            anim.SetTrigger("Idle");
        }
        else
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
                else
                {  
                    moveDir = -1f;
                    anim.SetTrigger("Walk_B");
                }
            }
            else if (Keyboard.current.dKey.isPressed &&
                     transform.position.x < (fighter2.position.x - 1))
            {
                if (Keyboard.current.aKey.isPressed)
                {
                    moveDir = 0f;
                }
                else
                {
                    moveDir = 1f;
                    anim.SetTrigger("Walk_F");
                }

            }

            if (moveDir == 0f)
            {
                anim.SetTrigger("Idle");
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

}
