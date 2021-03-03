using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Inventory/Items", fileName = "New Item")]
public class InventoryItems : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite itemImage;
    public int numberHeld;
    public bool usuable;
    public bool unique;
    public UnityEvent thisEvent;

    public void Use()
    {
        if(numberHeld > 0)
        {
            Debug.Log("We are using an item");
            thisEvent.Invoke();
            Debug.Log("using " + itemName);
        }
        
    }

    public void DecreaseAmmountOnUse(int ammountToDecrease)
    {
        numberHeld -= ammountToDecrease;
        if(numberHeld < 0)
        {
            numberHeld = 0;
        }
    }

}
