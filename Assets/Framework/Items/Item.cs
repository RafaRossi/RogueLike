using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item")]
public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public int maxItemStack = 1;
}

public interface IPickable
{
    void PickUp(CharacterInventory inventory);
}

public interface ICollectable : IPickable
{
    
}
