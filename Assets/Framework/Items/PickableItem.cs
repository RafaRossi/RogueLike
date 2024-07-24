using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickableItem : MonoBehaviour, IPickable
{
    [SerializeField] private Item item;
    [SerializeField] private int itemAmount = 1;

    public UnityEvent<Item> onPickUpitem = new UnityEvent<Item>();
    
    public void PickUp(CharacterInventory inventory)
    {
        inventory.AddItemToInventory(item, itemAmount);
        
        onPickUpitem?.Invoke(item);
    }
}
