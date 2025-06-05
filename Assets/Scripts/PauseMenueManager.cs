using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenueManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject itemPrefab; // Fügen Sie dies hinzu, um itemPrefab zu definieren
    [SerializeField] private Transform itemContainer; // Fügen Sie dies hinzu, um itemContainer zu definieren

    private BaseCharacterController baseCC;
    private List<GameObject> spawnedItemUIs = new List<GameObject>(); // Fügen Sie dies hinzu, um spawnedItemUIs zu definieren

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
            PopulateInventory();// populate
        }
    }

    private void PopulateInventory()
    {
        ClearInventory(); // delete all existing items in the inventory UI

        // 2. Items holen → Aus dem CharacterStatsManager
        // 3. Für jedes Item:
        foreach (var itemPair in CharacterStatsManager.Instance.items)
        {
            // Das Item und die Anzahl herausbekommen
            BaseItem currentItem = itemPair.Key;    // Das Item selbst
            int itemCount = itemPair.Value;         // Wie viele Stück

            // 3a. Instantiate(itemPrefab, itemContainer) → Neues UI-Element erstellen
            GameObject newItemUI = Instantiate(itemPrefab, itemContainer);

            // 3b. Element zur spawnedItemUIs Liste hinzufügen
            spawnedItemUIs.Add(newItemUI);

            // 3c. Item-Daten ins UI-Element einsetzen
            ItemDataHolder dataHolder = newItemUI.GetComponent<ItemDataHolder>();
            dataHolder.SetItemData(currentItem.ItemIcon, currentItem.ItemName, itemCount);
        }
    }

    private void ClearInventory()
    {
        // 1. Geht durch alle Items in der spawnedItemUIs Liste
        foreach (GameObject itemUI in spawnedItemUIs)
        {
            // 2. Löscht jedes UI-Element mit Destroy()
            Destroy(itemUI);
        }

        // 3. Leert die Liste mit Clear()
        spawnedItemUIs.Clear();
    }
}