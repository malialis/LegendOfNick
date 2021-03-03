using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("inventory Info")]
    [SerializeField]
    private GameObject blankInventorySlot;
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private GameObject useButton;
    public PlayerInventory playerInventory;
    public InventoryItems currentItem;


    // Start is called before the first frame update
    void Start()
    {
        SetTextAndButton("", false);
        MakeInventorySlots();
    }


    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    private void MakeInventorySlots()
    {
        if(playerInventory != null)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                GameObject tempInventorySlotItem = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                tempInventorySlotItem.transform.SetParent(inventoryPanel.transform);
                InventorySlot newSlot = tempInventorySlotItem.GetComponent<InventorySlot>();

                if(newSlot != null)
                {                    
                    newSlot.SetUp(playerInventory.myInventory[i], this);
                }
                
            }
        }
    }

    public void SetUpDescriptionAndButton(string newDescription, bool isButtonUseable, InventoryItems newItem)
    {
        descriptionText.text = newDescription;
        useButton.SetActive(isButtonUseable);
        currentItem = newItem;
    }

    public void UseButtonPressed()
    {
        if (currentItem)
        {
            currentItem.Use();
            //clear all the inventory slots
            //refill the slots
            ClearInventorySlots();
            MakeInventorySlots();
            if(currentItem.numberHeld == 0)
            {
                SetTextAndButton("", false);
            }
            

        }
    }

    private void ClearInventorySlots()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }


}
