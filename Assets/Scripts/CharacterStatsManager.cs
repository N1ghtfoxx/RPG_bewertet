using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    public static CharacterStatsManager Instance { get; private set; }
    public List<BattleEntityData> characterData;
    private Dictionary<string, int> characterExp;
    private Dictionary<string, Health> characterHP;
    public Dictionary<string, bool> equipment { get; private set; }
    public Dictionary<BaseItem, int> items { get; private set; }

    // define the file name and path for the inventory
    private string inventoryFileName = "inventory.json";
    private string inventoryFilePath;


    void Start()
    {
        if (Instance == null)
        {
            Instance = this;

// created with Claude.ai
            // path to the inventory file
            inventoryFilePath = Path.Combine(Application.persistentDataPath, inventoryFileName);
// end Claude.ai 

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
            characterHP.Add(item.entityName, new Health(item.baseMaxHealth, item.baseMaxHealth));
        }

        equipment = new Dictionary<string, bool>();
        items = new Dictionary<BaseItem, int>();

        LoadInventory();
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

// created with Claude.ai
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
// end Claude.ai

        // save the inventory after adding items
        SaveInventory();
    }
    
// created with Claude.ai and Osman ;)
    private void SaveInventory()
    {
        // Ensure the directory exists
        List<BaseItemSaveData> saveData = new List<BaseItemSaveData>();
        // Check if the directory exists, if not create it
        foreach (var item in items)
        {
            var data = new BaseItemSaveData
            {
                itemName = item.Key.ItemName,
                itemCount = item.Value
            };
            saveData.Add(data);
        }
        // Serialize the inventory data to JSON
        BaseItemListSaveData saveDataWrapper = new BaseItemListSaveData { items = saveData };
        // Create the JSON string from the save data
        string jsonData = JsonUtility.ToJson(saveDataWrapper, true);
        // Ensure the directory exists
        File.WriteAllText(inventoryFilePath, jsonData);
    }

    private void LoadInventory()
    {
        // Check if the directory exists, if not create it
        if (File.Exists(inventoryFilePath))
        {
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(inventoryFilePath);
            // Deserialize the JSON data into a BaseItemListSaveData object
            BaseItemListSaveData saveDataWrapper = JsonUtility.FromJson<BaseItemListSaveData>(jsonData);

            items.Clear(); // be sure to clear the existing items before loading new ones

            // Populate the items dictionary with the loaded data
            foreach (var item in saveDataWrapper.items)
            {
                // Load the BaseItem from resources using the itemName
                BaseItem baseItem = Resources.Load<BaseItem>("Items/" + item.itemName);
                // If the item was found, add it to the items dictionary
                if (baseItem != null)
                {
                    items[baseItem] = item.itemCount; // add or update the item countS
                }
                else
                {
                    Debug.LogWarning($"Item '{item.itemName}' konnte nicht geladen werden. Überprüfen Sie den Pfad oder die Ressource.");
                }
            }
        }
        else
        {
            Debug.LogWarning("Inventory-Datei nicht gefunden: " + inventoryFilePath);
        }
    }
// end Claude.ai and Osman-Tutorial ;)
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