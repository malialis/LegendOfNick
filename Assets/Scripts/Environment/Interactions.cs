using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Interactions : MonoBehaviour
{

    public SignalSender context;

    public bool playerInRange;

    private void Start()
    {
        GameEvents.current.OnSubmit += OnSubmit;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = true;
            Debug.Log("Player has entered range yo");

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

    public void OnSubmit()
    {
        if (this.playerInRange) DoOnSubmit(); //Interact with it (open door, etc)
    }

    public virtual void DoOnSubmit()
    {

    }

    public void DoSubmit()
    {
         GameEvents.current.OnSubmit += OnSubmit;
    }

}
