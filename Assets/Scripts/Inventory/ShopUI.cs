using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI instance;

    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;
    public GameObject hud;

    public Text goldText;

    public string[] itemsForSale;
    public ItemButton[] buyItemButton;
    public ItemButton[] sellItemButton;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        hud.SetActive(false);
        OpenBuyMenu();
        GameManager.instance.isShopActive = true;
        goldText.text = "Gold: " + GameManager.instance.currentGold;
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        hud.SetActive(true);
        GameManager.instance.isShopActive = false;
    }

    public void OpenBuyMenu()
    {
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        GameManager.instance.SortItems();

        for (int i = 0; i < buyItemButton.Length; i++)
        {
            buyItemButton[i].buttonValue = i;
            if (itemsForSale[i] != "")
            {
                buyItemButton[i].buttonImage.gameObject.SetActive(true);
                buyItemButton[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyItemButton[i].ammountText.text = "";
            }
            else
            {
                buyItemButton[i].buttonImage.gameObject.SetActive(false);
                buyItemButton[i].ammountText.text = ""; 
            }
        }

    }

    public void OpenSellMenu()
    {
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);

        GameManager.instance.SortItems();

        for (int i = 0; i < sellItemButton.Length; i++)
        {
            sellItemButton[i].buttonValue = i;
            if (GameManager.instance.itemsHeld[i] != "")
            {
                sellItemButton[i].buttonImage.gameObject.SetActive(true);
                sellItemButton[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                sellItemButton[i].ammountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                sellItemButton[i].buttonImage.gameObject.SetActive(false);
                sellItemButton[i].ammountText.text = "";
            }
        }
    }




}
