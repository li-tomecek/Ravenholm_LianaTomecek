using UnityEngine;

public class GravityGun : MonoBehaviour
{
    [SerializeField] private Transform _sightOrigin;            //origin of the raycast for interactable objects
    [SerializeField] private Transform _holdPosition;           //Where the object is "held" by the gravity gun
    GameObject _heldObject;

    [SerializeField] private float _launchVelocity = 5f;
    [SerializeField] private float _sightDistance = 7f;
    [SerializeField] private LayerMask _objectMask;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(_sightOrigin.position, _sightOrigin.forward * _sightDistance, Color.red);

        if(Physics.Raycast(_sightOrigin.position, _sightOrigin.forward, out RaycastHit hit, _sightDistance, _objectMask))
        {
            GameObject grabbed = hit.collider.gameObject;
            if(grabbed != null)
            {
                _heldObject = grabbed;
                _heldObject.transform.position = _holdPosition.position;

                _heldObject.transform.parent = this.transform;
            }
        }


        
    }
}
