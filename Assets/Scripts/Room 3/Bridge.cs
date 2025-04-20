using System.Collections;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] float _raisedHeight = 10f;
    [SerializeField] float _raiseSpeed = 5f;

    float _distanceRaised = 0f;
    private bool _raised;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_raised && _distanceRaised < _raisedHeight)     //go up
        {
            _distanceRaised += (_raisedHeight / _raiseSpeed) * Time.fixedDeltaTime;
            transform.Translate(0f, (_raisedHeight / _raiseSpeed) * Time.fixedDeltaTime, 0f);
        }
        else if(!_raised && _distanceRaised > 0f)           //go down
        {
            _distanceRaised -= (_raisedHeight / _raiseSpeed) * Time.fixedDeltaTime;
            transform.Translate(0f, -(_raisedHeight / _raiseSpeed) * Time.fixedDeltaTime, 0f);
        }
    }

    public void ToggleRaise()
    {
        _raised  = !_raised;
    }

}
