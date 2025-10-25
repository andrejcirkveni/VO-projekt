using UnityEngine;
using UnityEngine.InputSystem;

public class MoveFighter1 : MonoBehaviour
{
    public float step=1f;
    public float moveTime=0.1f;
    public Transform fighter2;

    private Vector3 targetPos;
    private bool moving;
    private float t;


    void Start()
    {
        targetPos=transform.position;
    }

    void Update()
    {
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

}
