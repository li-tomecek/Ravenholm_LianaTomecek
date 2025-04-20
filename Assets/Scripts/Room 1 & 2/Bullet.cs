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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Respawn();
            Destroy(gameObject);
        }
    }
}
