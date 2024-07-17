using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> itemsSlots = new List<InventorySlot>();
    public int maxSlotsAmount = 9;
    
    public bool AddItemToInventory(Item item, int amount = 1)
    {
        foreach (var inventorySlot in itemsSlots)
        {
            if (inventorySlot.item != item || inventorySlot.itemAmount >= item.maxItemStack) continue;
            
            var finalItemAmount = inventorySlot.itemAmount + amount;

            if (finalItemAmount > item.maxItemStack)
            {
                var excess = finalItemAmount - item.maxItemStack;
                    
                inventorySlot.itemAmount += inventorySlot.itemAmount - excess;
                    
                return AddItemToInventory(item, excess);
            }

            inventorySlot.itemAmount += amount;
            return true;
        }

        var occupiedSlots = itemsSlots.Count(i => i.item != null);

        if (occupiedSlots >= maxSlotsAmount) return false;
        
        itemsSlots.Add(new InventorySlot { item = item, itemAmount = amount });
        return true;
    }

    public bool RemoveItemFromInventory(Item item, int amount = 1)
    {
        var slots = itemsSlots.Where(i => i.item == item).ToArray();

        foreach (var slot in slots)
        {
            if (slot.itemAmount <= amount)
            {
                amount -= slot.itemAmount;
                
                slot.itemAmount = 0;
                slot.item = null;

                return RemoveItemFromInventory(item, amount);
            }

            slot.itemAmount -= amount;
            amount = 0;
        }

        return amount <= 0;
    }

    public List<InventorySlot> GetInventoryItems() => itemsSlots;
}

[Serializable]
public class InventorySlot
{
    public Item item;
    public int itemAmount;
}