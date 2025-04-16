using System.Collections;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    [SerializeField] GameObject _particleEffects;
    BoxCollider _boxCollider;
    
    [SerializeField] float _onTimer = 5;
    [SerializeField] float _offTimer = 3;
    [SerializeField] float _startOffset = 0f;    
    const float PARTICLE_FINISH_TIMER = 1.8f;       //how long it takes for the particles to fully dissapear when stopping the animation
    const float PARTICLE_STARTUP_TIMER = 1f;        //how long it takes for the particles to reach a point in which they would collider with the player on startup
    
    private bool _inCycle;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boxCollider = gameObject.GetComponent<BoxCollider>();
        _inCycle = true;
        _onTimer += _startOffset;
        StartCoroutine(PlayCycle());
        _onTimer -= _startOffset;       //include offset for first cycle only

    }

    // Update is called once per frame
    void Update()
    {
        if (!_inCycle)  //TODO: check for paused game here
        {
            StartCoroutine(PlayCycle());
        }
    }

    private IEnumerator PlayCycle()
    {
        _inCycle = true;
        yield return new WaitForSeconds(_onTimer);                  //waterfall is on

        //Turn off waterfall
        foreach(Transform child in _particleEffects.transform)
        {
            child.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        
        yield return new WaitForSeconds(PARTICLE_FINISH_TIMER);     //wait for particles to disappear
        _boxCollider.enabled = false;
        
        yield return new WaitForSeconds(_offTimer);                 

        //turn on waterfall
        foreach (Transform child in _particleEffects.transform)
        {
            child.GetComponent<ParticleSystem>().Play();
        }
        yield return new WaitForSeconds(PARTICLE_STARTUP_TIMER);    //wait for particles to re-appear (so player can walk under them)
        _boxCollider.enabled = true;                              
        _inCycle = false;

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //respawn player
            other.GetComponent<PlayerController>().Respawn();
        } 
        else if (other.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {
            Destroy(other.gameObject);  //objects get destroyed if thrown through
        }
    }
}
