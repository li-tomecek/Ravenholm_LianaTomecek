using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] float _magnetStrength = 100f;
    [SerializeField] Transform _attractionPoint;   // The Transform to which magnetic objects are attracted to 

    private bool _activated;
    private List<Rigidbody> _attractedRigidbodies = new List<Rigidbody>();     //list of the attached objects when the magnet is on

    void FixedUpdate()
    {
        if (_activated)
        {
            //pull all magnetic objects towards the magnet
            foreach (Rigidbody body in _attractedRigidbodies)
            {
                body.linearVelocity = (_attractionPoint.position - (body.transform.position + body.centerOfMass)) * _magnetStrength * Time.deltaTime;
            }
        }
        
    }

    public void ToggleActive()
    {
        _activated = !_activated;
        if (!_activated)
            _attractedRigidbodies.Clear();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_activated || _attractedRigidbodies.Contains(other.gameObject.GetComponent<Rigidbody>()))
            return;

        if (other.gameObject.CompareTag("Magnetic"))
        {
            _attractedRigidbodies.Add(other.gameObject.GetComponent<Rigidbody>());

        }
        else if (other.gameObject.CompareTag("Player"))
        {
            //if player is carrying a magnetic item, force them to drop it
            GameObject _heldObject = other.gameObject.GetComponent<PlayerController>().GetGravityGun().GetHeldObject();

            if (_heldObject != null && _heldObject.CompareTag("Magnetic"))
            {
                other.gameObject.GetComponent<PlayerController>().GetGravityGun().DropObject();
            }
        }
    }

}

