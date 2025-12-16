using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class BehaviorFighter1 : MonoBehaviour
{
    public float step=1f;
    public float moveTime=0.1f;
    public Transform fighter2;

    private Vector3 targetPos;
    private bool moving;
    private float t;

    private Animator anim;
    private int combo_step = 0;
    private bool canReceiveInput = true;


    void Start()
    {
        targetPos=transform.position;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Debug.Log(canReceiveInput);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame && canReceiveInput)
        {
            
            combo_step++;
            if(combo_step>3) combo_step=1;

            anim.SetInteger("combo_step", combo_step);
            Debug.Log(combo_step);
            if (Keyboard.current.leftShiftKey.isPressed)
            {
                anim.SetTrigger("Heavy_attack");
                Debug.Log("Heavy attack");
            }
            else {
                anim.SetTrigger("Light_attack");
                Debug.Log("Light attack");
            }
                
            canReceiveInput = false;
            

        }

        if (!moving)
        {
            if (Keyboard.current.aKey.wasPressedThisFrame && ((transform.position.x>-3) && (Mathf.Abs(transform.position.x-fighter2.position.x)<3)))
            {
                targetPos+=Vector3.left*step;
                moving = true;
                t = 0f;
            }

            if (Keyboard.current.dKey.wasPressedThisFrame && (transform.position.x < (fighter2.position.x - 1)))
            {
                targetPos+=Vector3.right*step;
                moving = true;
                t = 0f;
            }
        }

        if (moving)
        {
            t+=Time.deltaTime;
            transform.position=Vector3.Lerp(transform.position, targetPos, t);

            if (t>=moveTime)
            {
                transform.position=targetPos;
                moving=false;
            }
        }
    }

    public void EnableNextInput()
    {
        canReceiveInput = true;
    }
    public void ResetCombo()
    {
        combo_step = 0;
        anim.SetInteger("combo_step", 0);
        EnableNextInput();
    }

}
