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

    private string inventoryFileName = "inventory.json";
    private string inventoryFilePath;


    void Start()
    {
        if (Instance == null)
        {
            Instance = this;

            // made with Claude.ai
            // Dateipfad für Inventar festlegen
            inventoryFilePath = Path.Combine(Application.persistentDataPath, inventoryFileName);
            // end Claude.ai snippet

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

        // save the inventory after adding items
        SaveInventory();
    }

    private void SaveInventory()
    {
        List<BaseItemSaveData> saveData = new List<BaseItemSaveData>();
        foreach (var item in items)
        {
            var data = new BaseItemSaveData
            {
                itemName = item.Key.ItemName,
                itemCount = item.Value
            };
            saveData.Add(data);
        }
        BaseItemListSaveData saveDataWrapper = new BaseItemListSaveData { items = saveData };
        string jsonData = JsonUtility.ToJson(saveDataWrapper, true);

        File.WriteAllText(inventoryFilePath, jsonData);
    }

    private void LoadInventory()
    {
        if (File.Exists(inventoryFilePath))
        {
            string jsonData = File.ReadAllText(inventoryFilePath);
            BaseItemListSaveData saveDataWrapper = JsonUtility.FromJson<BaseItemListSaveData>(jsonData);

            items.Clear(); // Sicherstellen, dass das Dictionary leer ist, bevor es befüllt wird

            foreach (var item in saveDataWrapper.items)
            {
                BaseItem baseItem = Resources.Load<BaseItem>("Items/" + item.itemName);
                if (baseItem != null)
                {
                    items[baseItem] = item.itemCount; // Hinzufügen oder Aktualisieren der Menge
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