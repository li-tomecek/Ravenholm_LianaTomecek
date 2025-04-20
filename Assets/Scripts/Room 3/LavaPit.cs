using UnityEngine;

public class LavaPit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //respawn player
            other.GetComponent<PlayerController>().Respawn();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {
            Destroy(other.gameObject);  //objects get destroyed if thrown through
        }
    }
}
