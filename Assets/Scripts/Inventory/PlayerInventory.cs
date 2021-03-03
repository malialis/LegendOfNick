using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Inventory/PlayerInventory", fileName = "New Inventory")]
public class PlayerInventory : ScriptableObject
{
    public List<InventoryItems> myInventory = new List<InventoryItems>();




}
