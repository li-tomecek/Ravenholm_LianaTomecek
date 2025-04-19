using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _objectPrefab;                               //The type of object that will be spawned
    [SerializeField] private GameObject _spawnedObject;                      //reference to the spawned pbject so we can see if it has been destroyed
    private void Update()
    {
        if (_spawnedObject == null)
            SpawnObject();
    }

    public void SpawnObject()
    {
        _spawnedObject = Instantiate(_objectPrefab, transform.position, transform.rotation);
    }

}
