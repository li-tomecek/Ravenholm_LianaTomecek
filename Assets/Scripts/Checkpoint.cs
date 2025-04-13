using System.Runtime.CompilerServices;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] Transform _respawnPoint;
    private BoxCollider _collider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _collider.includeLayers) != 0)
        {
            other.gameObject.GetComponent<PlayerController>().SetRespawnPoint(_respawnPoint);
        }
    }
}
