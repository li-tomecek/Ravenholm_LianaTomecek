using UnityEngine;
using UnityEngine.Events;

public class TriggerPlate : MonoBehaviour
{
    [SerializeField] public UnityEvent _onActivatedEvent;
    [SerializeField] public UnityEvent _onDeactivatedEvent;

    [SerializeField] private Material _activatedMaterial;
    [SerializeField] private Material _deactivatedMaterial;

    private bool _isActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _onActivatedEvent.AddListener(TogglePlate);
        _onDeactivatedEvent.AddListener(TogglePlate);
    }

    // Update is called once per frame
    void TogglePlate()
    {
        _isActive = !_isActive;
        if (_isActive)  
            gameObject.GetComponent<MeshRenderer>().material = _activatedMaterial;
        else
            gameObject.GetComponent<MeshRenderer>().material = _deactivatedMaterial;

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TriggerPlateKey>())
            _onActivatedEvent.Invoke();
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<TriggerPlateKey>())
            _onDeactivatedEvent.Invoke();
    }
}
