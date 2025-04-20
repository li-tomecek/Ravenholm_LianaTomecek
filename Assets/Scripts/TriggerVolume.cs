using UnityEngine;
using UnityEngine.Events;

public class TriggerVolume : MonoBehaviour
{
    [SerializeField] bool _retriggerable = false;

    [SerializeField] public UnityEvent _onPlayerEnterEvent;
    //[SerializeField] public UnityEvent _onOtherEnterEvent;
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            _onPlayerEnterEvent.Invoke();               //player enters trigger volume
            if (!_retriggerable)
                Destroy(gameObject);                    //so it cannot be triggered again
        }

    }
}
