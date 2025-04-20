using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float _timeToDespawn = 5f;
    protected float _timeAlive;
    void Update()
    {
        _timeAlive += Time.deltaTime;
        if (_timeAlive >= _timeToDespawn)
        {
            Destroy(gameObject);
        }          
    }
}
