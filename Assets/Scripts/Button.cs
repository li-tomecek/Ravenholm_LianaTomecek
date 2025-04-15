using UnityEngine;
using UnityEngine.Events;
public class Button : Interactable
{
    [SerializeField] private Material _buttonPressedMaterial;
    private bool _pressed;

    public void Start()
    {
        _onInteractEvent.AddListener(PressButton);
    }

    public void PressButton()
    {
        if (_pressed)
            return;
        gameObject.GetComponent<MeshRenderer>().material = _buttonPressedMaterial;
        _pressed = true;
    }
}
