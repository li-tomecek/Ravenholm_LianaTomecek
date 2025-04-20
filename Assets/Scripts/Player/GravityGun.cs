using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GravityGun : MonoBehaviour
{
    [SerializeField] private Transform _sightOrigin;            //origin of the raycast for interactable objects
    [SerializeField] private float _sightDistance = 7f;
    [SerializeField] private Image _crosshair;

    [Header("Object Interaction")]
    [SerializeField] private float _launchVelocity = 15f;
    [SerializeField] private Transform _holdPosition;           //Where the object is "held" by the gravity gun
    [SerializeField] private LayerMask _objectMask;

    private GameObject _heldObject;
    private GameObject _objectInSights;


    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.DrawRay(_sightOrigin.position, _sightOrigin.forward * _sightDistance, Color.red);

        if(_heldObject == null && Physics.Raycast(_sightOrigin.position, _sightOrigin.forward, out RaycastHit hit, _sightDistance, _objectMask))
        {
            _objectInSights = hit.collider.gameObject;
            _crosshair.color = Color.yellow;
 
        }
        else
        {
            _objectInSights = null;
            _crosshair.color = Color.black;

        }

    }

    public void Update()
    {
        if (_heldObject)
        {
            _heldObject.transform.position = _holdPosition.position;
            _heldObject.transform.forward = _holdPosition.forward;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if(_objectInSights)
            {
                if (_objectInSights.layer == LayerMask.NameToLayer("Grabbable"))
                {
                    if (_heldObject == null && _objectInSights.GetComponent<Rigidbody>())       //Pick up object
                    {
                        _heldObject = _objectInSights;
                        _heldObject.GetComponent<Collider>().enabled = false;
                        _heldObject.GetComponent<Rigidbody>().isKinematic = true;

                        if (_heldObject.gameObject.GetComponent<Grabbable>())
                        {
                            _heldObject.gameObject.GetComponent<Grabbable>().OnPickup();
                        }

                        _crosshair.enabled = false;
                    }
                }
                else if (_objectInSights.layer == LayerMask.NameToLayer("Interactable"))        //Interact with object (ex. Press a Button)
                {
                    _objectInSights.GetComponent<Interactable>()._onInteractEvent.Invoke();
                }

            }
            else if(_heldObject)                                                                //Launch held object
            {
                if (_heldObject.gameObject.GetComponent<Grabbable>())
                {
                    _heldObject.gameObject.GetComponent<Grabbable>().OnThrow();
                }
                
                LaunchObject();

            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && _heldObject)                                    //Drop Object
        {
            if (_heldObject.gameObject.GetComponent<Grabbable>())
            {
                _heldObject.gameObject.GetComponent<Grabbable>().OnThrow();
            }

            DropObject();
        }                               
    
    }

    
    
    public void DropObject()
    {
        if(_heldObject != null)
        {
            _heldObject.GetComponent<Rigidbody>().isKinematic = false;
            _heldObject.GetComponent<Collider>().enabled = true;

            _heldObject = null;
            _crosshair.enabled = true;
        }
    }

    public void LaunchObject()
    {
        if(_heldObject != null)
        {
            _heldObject.GetComponent<Rigidbody>().isKinematic = false;
            _heldObject.GetComponent<Collider>().enabled = true;


            _heldObject.GetComponent<Rigidbody>().AddForce(_launchVelocity * _holdPosition.forward, ForceMode.Impulse);
            _heldObject = null;
            _crosshair.enabled = true;
        }
    }

    public GameObject GetHeldObject()
    {
        return _heldObject;
    }
}
