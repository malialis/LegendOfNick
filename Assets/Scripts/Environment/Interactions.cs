using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Interactions : MonoBehaviour
{

    public SignalSender context;

    public bool playerInRange;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            //Debug.Log("Player in Range yo");
            playerInRange = true;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            //Debug.Log("Player has left yo");
            playerInRange = false;
            //dialogBox.SetActive(false);
        }
    }

}
