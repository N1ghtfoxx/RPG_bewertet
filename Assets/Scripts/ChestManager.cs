using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    [SerializeField] private GameObject openButton;
    [SerializeField] private List<ChestItemEntry> availableItems = new List<ChestItemEntry>();
    [SerializeField] private SpriteRenderer chestSpriteRenderer;
    [SerializeField] private Sprite closedChestSprite;
    [SerializeField] private Sprite openChestSprite;
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

        if (chestSpriteRenderer == null)
        {
            chestSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (chestSpriteRenderer != null && closedChestSprite != null)
        {
            chestSpriteRenderer.sprite = closedChestSprite;
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

    // open chest and dissmiss openButton
    // change sprite to open chest sprite
    // clear list of items
    public void OpenChest()
    {
       isChestOpen = true;
        openButton.SetActive(false); 

        if (chestSpriteRenderer != null && openChestSprite != null)
        {
            chestSpriteRenderer.sprite = openChestSprite;
        }
        
        CharacterStatsManager.Instance.AddItemsToInventory(availableItems);
        availableItems.Clear(); 
    }

}
// made with Claude.ai 
[System.Serializable]
public class ChestItemEntry
{
    public BaseItem item;       // ScriptableObject
    public int quantity;        // number of items contained in chest
}
// end Claude.ai snippet
