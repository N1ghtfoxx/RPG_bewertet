using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataHolder : MonoBehaviour
{
    [SerializeField] Image itemSprite;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemCount;

    public void SetItemData(Sprite sprite, string name, int count)
    {
        itemSprite.sprite = sprite;
        itemName.text = name;
        itemCount.text = count.ToString();
    }
}
