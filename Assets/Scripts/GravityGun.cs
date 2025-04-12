using Unity.VisualScripting;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    [SerializeField] private Transform _sightOrigin;            //origin of the raycast for interactable objects
    [SerializeField] private Transform _holdPosition;           //Where the object is "held" by the gravity gun

    [SerializeField] private float _launchVelocity = 15f;
    [SerializeField] private float _sightDistance = 7f;
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
 
        }
        else
        {
            //Change CrosshairCOlor here
            _objectInSights = null;

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
            
            if(_heldObject == null && _objectInSights != null && _objectInSights.GetComponent<Rigidbody>())     //pickup new object
            {
                _heldObject = _objectInSights;

                _heldObject.GetComponent<Rigidbody>().isKinematic = true;

            } 
            else if (_heldObject != null)                         //launch held object
            {
                _heldObject.GetComponent<Rigidbody>().isKinematic = false;

                _heldObject.GetComponent<Rigidbody>().AddForce(_launchVelocity * _holdPosition.forward, ForceMode.Impulse);
                _heldObject = null;
            }

        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && _heldObject)        //Drop the object
        {
            _heldObject.GetComponent<Rigidbody>().isKinematic = false;
            _heldObject = null;
        }
    }
}
