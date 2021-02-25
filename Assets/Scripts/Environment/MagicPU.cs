using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPU : PowerUP
{
    public float magicAmmount;
    public Inventory playerInventory;
    public MagicManager magicManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            //magicManager.AddMagic(magicAmmount);
            playerInventory.currentMagic += magicAmmount;
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }


}
