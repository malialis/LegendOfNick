using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : Interactions
{
    private bool canOpen;
    public string[] itemsForSale = new string[40];

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.OnTalk += OnTalk;
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && PlayerMovement.instance.canMove && !ShopUI.instance.shopMenu.activeInHierarchy)
        {
            
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.isTrigger)
        {
            //canOpen = false;
            playerInRange = true;
            Debug.Log("Can you see me");
            
        }
    } */
   

    public override void DoOnTalk()
    {
        if (playerInRange && PlayerMovement.instance.canMove && !ShopUI.instance.shopMenu.activeInHierarchy)
        {
            GameManager.instance.isShopActive = true;
            ShopUI.instance.itemsForSale = itemsForSale;
            ShopUI.instance.OpenShop();
        }     

    }


}
