using UnityEngine;

public class maincamera : MonoBehaviour
{
    public Transform player;     
    public Vector3 offset;        

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}


