using UnityEngine;

public class Switch : Interactable
{
    [SerializeField] private GameObject _handle;
    private Material _buttonOffMaterial;
    
    private bool _inOnPosition;

    public void Start()
    {
        _onInteractEvent.AddListener(FlipSwitch);
    }

    public void FlipSwitch()
    {
        _inOnPosition = !_inOnPosition;

        if (_inOnPosition)
        {
            _handle.transform.eulerAngles = new Vector3(0f, 0f, 45f);
        }
        else
        {
            _handle.transform.eulerAngles = new Vector3(0f, 0f, -45f);
        }
    }
}
