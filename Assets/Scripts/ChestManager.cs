using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    [SerializeField] private GameObject openButton;

    private void OnTriggerEnter2D(Collider2D col)
    {
       openButton.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        openButton.SetActive(false);
    }

}
