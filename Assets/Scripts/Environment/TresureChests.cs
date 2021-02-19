using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TresureChests : Interactions
{
    [Header("Contents")]
    public Item contents;
    public bool isOpen;
    public BoolValue haveBeenOpen;

    [Header("Signals and Dialogues")]
    public SignalSender raiseItem;
    public GameObject dialogueBox;
    public Text dialogueText;
    private Animator animator;
    public Inventory playerInventory;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = haveBeenOpen.RuntimeValue;
        if (isOpen)
        {
            animator.SetBool("isOpened", true);
        }
        GameEvents.current.OnSubmit += OnSubmit;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            //Debug.Log("Player in Range yo");
            playerInRange = true;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            //Debug.Log("Player has left yo");
            playerInRange = false;
            dialogueBox.SetActive(false);
        }
    }

    public void OpenChest()
    {
        isOpen = true;
        //dialog window open
        dialogueBox.SetActive(true);
        //dialog text is what is in it
        dialogueText.text = contents.itemDescription;
        //add contents to inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        //raise signal to player to animate it
        raiseItem.Raise();
        //turn off context clue        
        animator.SetBool("isOpened", true);
        context.Raise();
        //saveGame.OpenedChests.Add(this.gameObject.name);
        haveBeenOpen.RuntimeValue = isOpen;
    }

    public void ChestAlreadyOpen()
    {
        //turn off dialog
            dialogueBox.SetActive(false);
            //set the current item to empty
            //playerInventory.currentItem = null;
            //raise the signal to play to stop animating
            raiseItem.Raise();
                    
    }

    public void InteractWithChest(InputAction.CallbackContext context)
    {
        Debug.Log("I am interacting");
        if (playerInRange)
        {
            Debug.Log("I am in Range");
            if (!isOpen)
            {
                Debug.Log("I am trying to open Chest");
                OpenChest();
            }
            else
            {
                Debug.Log("chest is already open");
                ChestAlreadyOpen();
            }
        }

    }

    public override void DoOnSubmit()
    {
        if (playerInRange)
        {
            Debug.Log("I am in Range to Do");
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                //chest is already open
                ChestAlreadyOpen();
            }
        }

    }



}
