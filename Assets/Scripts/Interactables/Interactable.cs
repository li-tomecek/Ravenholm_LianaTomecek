using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] public UnityEvent _onInteractEvent; 
}
