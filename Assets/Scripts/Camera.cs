using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform fighter1;
    public Transform fighter2;
    public float offsetZ = -1f;
    public float smooth = 5f;

    void LateUpdate()
    {
        Vector3 mid=(fighter1.position+fighter2.position)*0.5f;

        float dist=Mathf.Abs(fighter1.position.x-fighter2.position.x)*0.5f+0.5f;
        float z=offsetZ-dist; 

        Vector3 target = new Vector3(mid.x, transform.position.y, z);

        transform.position = Vector3.Lerp(transform.position, target, smooth*Time.deltaTime);
    }
}
