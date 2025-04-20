using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ConfettiSequence : MonoBehaviour
{
    [SerializeField] float _burstRate = 1.5f;
    
    public void FireConfetti()
    {
        StartCoroutine(StartSequence());
    }
    
    public IEnumerator StartSequence()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(_burstRate);
        }
    }

}
