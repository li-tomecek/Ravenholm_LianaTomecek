using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Turret : MonoBehaviour
{
    [Header("Gun")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _bulletOrigin;
    
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private float _fireVelocity = 10f;

    
    [Header("Accuracy and Spread")]
    [SerializeField] private float _maxVerticalSpread;
    [SerializeField] private float _maxHorizontalSpread;

    private bool _firing;

    void Start()
    {
        StartCoroutine(FiringCycle());
    }
    private IEnumerator FiringCycle()
    {
        Quaternion rotation;
       
        while (_firing)
        {
            //calculate random offset of next bullet
            rotation = Quaternion.Euler(
                Random.Range(-_maxVerticalSpread, _maxVerticalSpread), 
                Random.Range(-_maxHorizontalSpread, _maxHorizontalSpread), 
                0f);

            if(_bulletOrigin != null)
                FireBullet((rotation * (_target.position - _bulletOrigin.position)).normalized);

            yield return new WaitForSeconds(_fireRate);
        }
    }
    
    private void FireBullet(Vector3 direction)
    {
        Rigidbody rb = Instantiate(_bulletPrefab, _bulletOrigin.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(direction * _fireVelocity, ForceMode.Impulse);

    }

    public void ToggleFiring()
    {
        _firing = !_firing;
        
        if(_firing)
            StartCoroutine(FiringCycle());
    }
}
