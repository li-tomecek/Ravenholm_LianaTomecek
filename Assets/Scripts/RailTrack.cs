using System.Collections;
using UnityEngine;

public class RailTrack : MonoBehaviour
{
    [SerializeField] Transform _pointA;
    [SerializeField] Transform _pointB;
    [SerializeField] GameObject _attachment;
    [SerializeField] float _trackSpeed = 5f;

    const float TARGET_BUFFER = 0.1f;
    Transform _targetPoint;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _targetPoint = _pointA;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((_targetPoint.position - _attachment.transform.position).magnitude > TARGET_BUFFER)
            _attachment.transform.position += (_targetPoint.position - _attachment.transform.position).normalized * _trackSpeed * Time.deltaTime;
    }

    public void ToggleTargetPoint()
    {
        _targetPoint = (_targetPoint == _pointA ? _pointB : _pointA);
    }

}
