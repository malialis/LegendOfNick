using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactions : MonoBehaviour
{

    public SignalSender context;

    public bool playerInRange;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
