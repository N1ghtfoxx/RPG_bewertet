using System;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenueManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    private BaseCharacterController baseCC;

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
        else
        {
            ClearInventory();// clear
        }
    }

    private void PopulateInventory()
    {
        ClearInventory(); // delete all existing items in the inventory UI
    }

    private void ClearInventory()
    {
        // Destroy all existing ItemDataHolder objects in the pause menu UI
        foreach (Transform child in pauseMenuUI.transform)
        {
            if (child.GetComponent<ItemDataHolder>() != null)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
