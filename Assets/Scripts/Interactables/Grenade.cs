using System.Collections;
using UnityEngine;

public class Grenade : Grabbable
{
    [Header("Explosion")]
    [SerializeField] private float _explosionTimer;
    [SerializeField] private float _explosionRadius = 1f;
   
    [Header("Materials")]
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _flashMaterial;

    const float FLASH_TIMER = 0.5f;

    private bool _flashing;
    private bool _flashOn;
    public override void OnPickup()
    {
        //Make the grenade flash constantly
        _flashing = true;
        StartCoroutine(PlayFlashCycle());
    }

    public override void OnThrow()
    {
        _flashing = false;
        StartCoroutine(StartCountdown());
    }

    public override void OnDrop()
    {
        //same as throw
        OnThrow();
    }

    private IEnumerator PlayFlashCycle()
    {
        while (_flashing)
        {
            ToggleMaterial();
            yield return new WaitForSeconds(FLASH_TIMER);
        }
    }

    private IEnumerator StartCountdown()
    {
        float timer = _explosionTimer;
        float flashTime;

        while(timer > 0.01f)
        {
            ToggleMaterial();
            flashTime = (timer / (_explosionTimer * 2f));
            yield return new WaitForSeconds(flashTime);
            timer -= flashTime;
        }
        Explode();
    }


    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player"))
            {
                hitCollider.gameObject.GetComponent<PlayerController>().Respawn();
            } 
            else if (hitCollider.gameObject.CompareTag("Enemy")) 
            {
                Destroy(hitCollider.gameObject);
            }
        }

        Destroy(gameObject);
    }

    private void ToggleMaterial()
    {
        if (_flashOn)
        {
            gameObject.GetComponent<MeshRenderer>().material = _defaultMaterial;
            _flashOn = false;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = _flashMaterial;
            _flashOn = true;
        }
    }
}
