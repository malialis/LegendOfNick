using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [Header("UI related")]
    [SerializeField]
    private TextMeshProUGUI itemNumberText;
    [SerializeField]
    private Image itemImage;

    [Header("Variables from the item")]
    //public Sprite itemSprite;
    //public int numberHeld;
    //public string itemDescription;
    public InventoryItems thisItem;
    public InventoryManager thisManager;



    public void SetUp(InventoryItems newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        if(thisItem != null)
        {
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = "" + thisItem.numberHeld;
        }
    }

    public void ClickedOnSlot()
    {
        if(thisItem != null)
        {
            thisManager.SetUpDescriptionAndButton(thisItem.description, thisItem.usuable, thisItem);
        }
    }


}
