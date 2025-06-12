using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseItem", menuName = "Items/BaseItems")]

public class BaseItem : ScriptableObject
{
    [SerializeField] private ItemType itemType; // Type of the item, e.g., Consumable, Equipment, etc.
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private int itemPrice;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private bool isConsumable;
    [SerializeField] private bool isEquipped;
    [SerializeField] private bool isUsableInBattle;
    [SerializeField] private bool isUsableOutsideBattle;
    [SerializeField] private bool isStackable;
    [SerializeField] private bool isSellable;
    [SerializeField] private bool isCraftable;
    [SerializeField] private bool isTradable;
    [SerializeField] private bool isDestroyable;
    [SerializeField] private bool isQuestItem;

    public string ItemName => itemName;
    public Sprite ItemIcon => itemIcon;
}

[Serializable]
// This class is used to save the state of a BaseItem
public class BaseItemSaveData
{
    public string itemName;
    public int itemCount;
}

[Serializable]
// This class is used to save a list of BaseItemSaveData
public class BaseItemListSaveData
{
    public List<BaseItemSaveData> items;
}

// This enum defines the different types of items that can exist in the game.   
public enum ItemType
{
    Consumable,
    Weapon,
    Material,
    Tool,
    Money,
}
