using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public delegate void Interact();
    //[SerializeField] public event Action Interact;
}
