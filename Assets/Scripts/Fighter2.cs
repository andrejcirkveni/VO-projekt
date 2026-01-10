using UnityEngine;
using UnityEngine.InputSystem;

public class Fighter2 : MonoBehaviour
{
    private Animator anim;
    public bool blocking = false;

    private FighterHealth myHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        myHealth = GetComponent<FighterHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            anim.SetBool("Guard", true);
            blocking = true;
            myHealth.isBlocking = true;
            Debug.Log("Guarding");
        }
        else if (Keyboard.current.kKey.wasReleasedThisFrame)
        {
            anim.SetBool("Guard", false);
            blocking = false;
            myHealth.isBlocking = false;
        }

    }
}
