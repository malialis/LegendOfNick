using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Signs : Interactions
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    
        
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            context.Raise();
            Debug.Log("Player has left yo");
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }

    public override void DoOnSubmit()
    {
        if (playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                Debug.Log("I am reading the Sign");
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
        
    }

}
