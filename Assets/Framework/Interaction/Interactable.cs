using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [field:SerializeField] public bool CanInteract { get; set; }

    [SerializeField] private UnityEvent<CharacterInteraction> onInteract = new UnityEvent<CharacterInteraction>();
    
    public void Interact(CharacterInteraction interaction)
    {
        if(CanInteract) onInteract?.Invoke(interaction);
    }

    public void ChangeCanInteract(bool canInteract)
    {
        CanInteract = canInteract;
    }

    public void AddInteractionEvent(UnityAction<CharacterInteraction> interactionEvent)
    {
        onInteract.AddListener(interactionEvent);
    }
    
    public void RemoveInteractionEvent(UnityAction<CharacterInteraction> interactionEvent)
    {
        onInteract.RemoveListener(interactionEvent);
    }
}
