using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    private Interactable _currentInteraction;
    
    public void Interact()
    {
        if(_currentInteraction) _currentInteraction.Interact(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out var interactable))
        {
            _currentInteraction = interactable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Interactable>(out var interactable)) return;
        
        if(_currentInteraction == interactable) _currentInteraction = null;
    }
}
