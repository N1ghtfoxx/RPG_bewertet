using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    public static CharacterStatsManager Instance { get; private set; }
    public List<BattleEntityData> characterData;
    private Dictionary<string, int> characterExp;
    private Dictionary<string, Health> characterHP;
    public Dictionary<string, bool> equipment { get; private set; }
    public Dictionary<BaseItem, int> items { get; private set; }


    void Start()
    {
        if (Instance == null)
        {
            Instance = this;

            Load();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Load()
    {
        characterExp = new Dictionary<string, int>();
        characterHP = new Dictionary<string, Health>();

        foreach (var item in characterData)
        {
            characterExp.Add(item.entityName, 0);
            characterHP.Add(item.entityName, new Health (item.baseMaxHealth, item.baseMaxHealth));
        }

        equipment = new Dictionary<string, bool>();
        items = new Dictionary<BaseItem, int>();
    }

    public int GetPlayerExp(string playerName)
    {
        if (characterExp.ContainsKey(playerName))
        {
            return characterExp[playerName];
        }
        return 0;

    }

    public Health GetPlayerHP(string playerName)
    {
        if (characterHP.ContainsKey(playerName))
        {
            return characterHP[playerName];
        }
        return new Health(0, 0);
    }

    // Increase quantity if item already exists
    // Add new item with quantity if it does not exist
    public void AddItemsToInventory(List<ChestItemEntry> gainedItems)
    {
        // made with Claude.ai
        foreach (var addedItem in gainedItems)
        {
            Debug.Log($"Adding item: {addedItem.item.name} with quantity: {addedItem.quantity} to inventory.");
            if (this.items.ContainsKey(addedItem.item))
            {
                this.items[addedItem.item] += addedItem.quantity; 
            }
            else
            {
                this.items.Add(addedItem.item, addedItem.quantity); 
            }
        }
        // end Claude.ai snippet
    }
}

[Serializable]
public struct Health
{
    public int health;
    public int maxHealth;
    public Health(int health, int maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
    }   
}