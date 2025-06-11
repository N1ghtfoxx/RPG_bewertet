using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenueManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject itemPrefab; // definition of itemPrefab
    [SerializeField] private Transform itemContainer; // definition of itemContainer

    private BaseCharacterController baseCC;
    private List<GameObject> spawnedItemUIs = new List<GameObject>(); // definition of spawnedItemUIs

    private void Start()
    {
        baseCC = FindObjectOfType<BaseCharacterController>();
    }

    public void TogglePauseMenu()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);

        DoStuff(pauseMenuUI.activeSelf);

        baseCC.PausePlayer(pauseMenuUI.activeSelf);
    }

    private void DoStuff(bool isEnabled)
    {
        if (isEnabled)
        {
            PopulateInventory();
        }
    }

    private void PopulateInventory()
    {
        ClearInventory(); // delete all existing items in the inventory UI

// created with Claude.ai
        // 2. get the items from CharacterStatsManager
        // 3. for each item in the items dictionary
        foreach (var itemPair in CharacterStatsManager.Instance.items)
        {
            BaseItem currentItem = itemPair.Key;    // item
            int itemCount = itemPair.Value;         // count

            // 3a. Instantiate(itemPrefab, itemContainer) → new Item UI element
            GameObject newItemUI = Instantiate(itemPrefab, itemContainer);

            // 3b. add element to the spawnedItemUIs list
            spawnedItemUIs.Add(newItemUI);

            // 3c. place the item icon, name and count in the new UI element
            ItemDataHolder dataHolder = newItemUI.GetComponent<ItemDataHolder>();
            dataHolder.SetItemData(currentItem.ItemIcon, currentItem.ItemName, itemCount);
        }
// end Claude.ai
    }

    private void ClearInventory()
    {

// created with Claude.ai
        // 1. go through the spawnedItemUIs list
        foreach (GameObject itemUI in spawnedItemUIs)
        {
            // 2. destroy each item UI element
            Destroy(itemUI);
        }

        // 3. clear the list
        spawnedItemUIs.Clear();
    }
// end Claude.ai
}