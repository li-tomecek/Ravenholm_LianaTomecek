using UnityEngine;
using UnityEngine.Events;
public class Button : Interactable
{
    [SerializeField] private Material _buttonOnMaterial;
    [SerializeField] private Material _buttonOffMaterial;
    [SerializeField] bool _toggleable = false;              //can you press the button more than once
    private bool _pressed;

    public void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = _buttonOffMaterial;
        _onInteractEvent.AddListener(PressButton);
    }

    private void ToggleMaterial()
    {
        if(_pressed)
        {
            gameObject.GetComponent<MeshRenderer>().material = _buttonOnMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = _buttonOffMaterial;
        }
    }
    public void PressButton()
    {
        if (_pressed && !_toggleable)
            return;

        _pressed = !_pressed;
        ToggleMaterial();
        
    }
}
