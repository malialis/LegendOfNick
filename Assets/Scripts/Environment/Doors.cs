using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public enum DoorType
{
    key,
    enemy,
    button
}


public class Doors : Interactions
{
    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool isDoorOpen = false;

    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public Sprite openDoorImage;
    public Sprite lockedDoorImage;
    public BoxCollider2D physicsCollider;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        // turn off the sprite renderer
        doorSprite.sprite = openDoorImage;
        //set open to true
        isDoorOpen = true;
        // turn off the collider
        physicsCollider.enabled = false;
    }
    public void CloseDoor()
    {

    }

    public void InteractWithDoors(InputAction.CallbackContext context)
    {
        if (playerInRange && thisDoorType == DoorType.key)
        {
            //does the player have a key?
            if (playerInventory.numberOfKeys > 0)
            {
                //remove a key
                playerInventory.numberOfKeys--;
                OpenDoor();
            }
            //if so call the openDoor

        }
    }

}
