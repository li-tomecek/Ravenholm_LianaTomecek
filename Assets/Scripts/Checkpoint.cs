using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] Transform _respawnPoint;
    [SerializeField] public UnityEvent _onRespawnEvent;

    private BoxCollider _collider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    public Transform GetRespawnPoint()
    {
        return _respawnPoint;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _collider.includeLayers) != 0)
        {
            other.gameObject.GetComponent<PlayerController>().SetActiveCheckpoint(this);
        }
    }
}
