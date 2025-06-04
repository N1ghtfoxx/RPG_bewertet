using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    [SerializeField] private GameObject openButton;
    [SerializeField] private List<ChestItemEntry> availableItems = new List<ChestItemEntry>();
    private bool isChestOpen = false;

    private void Start()
    {
        if (openButton == null)
        {
            Debug.LogError("Open Button is not assigned in the ChestManager script.");
        }
        else
        {
            openButton.SetActive(false); // Hide the button initially
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isChestOpen) return;
       openButton.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        openButton.SetActive(false);
    }

    public void OpenChest()
    {
       isChestOpen = true;
        openButton.SetActive(false); // Button ausblenden
        CharacterStatsManager.Instance.AddItemsToInventory(availableItems);
        availableItems.Clear(); // Leere die Liste der verfügbaren Items
    }

}

[System.Serializable]
public class ChestItemEntry
{
    public BaseItem item;       // Dein ScriptableObject
    public int quantity;        // Anzahl Items, die in der Truhe enthalten sind
}
