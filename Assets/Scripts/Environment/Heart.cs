using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUP
{
    public FloatValue playerHeath;
    public FloatValue heartContainers;
    public float ammountToIncrease;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            playerHeath.RuntimeValue += ammountToIncrease;
            Debug.Log("I got a heart yo");
            if(playerHeath.initialValue > heartContainers.RuntimeValue * 2f)
            {
                playerHeath.initialValue = heartContainers.RuntimeValue * 2f;
            }
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }


}
