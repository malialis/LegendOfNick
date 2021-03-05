using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance;

    [Header("Panels")]
    public GameObject theMenu;
    public GameObject characterItemChoiceMenu;
    public Text[] characterItemChoiceNameText;
    public GameObject[] windows;
    public GameObject hud;

    [Header("Player Stats")]
    public Text[] playerName;
    public Text[] hpText;
    public Text[] mpText;
    public Text[] levelText;
    public Text[] expText;
    public Slider[] expSlider;
    public Image[] charImage;

    [Header("Status Menu")]
    public Text statusName;
    public Text statusHP;
    public Text statusMP;
    public Text statusStrength;
    public Text statusDefence;
    public Text statusWeaponEquipped;
    public Text statusWeaponPower;
    public Text statusArmorEquipped;
    public Text statusArmorPower;
    public Text statusLevel;
    public Text statusCurrentExp;
    public Text statusEXPToNextLevel;
    public Image statusImage;


    public GameObject[] charStatHolder;
    public GameObject[] playerStatusButton;
    private PlayerStats[] playerStats;
    private string mainMenu; // main menu to quit to

    [Header("Items Inventory")]
    public ItemButton[] itemButtons;
    public string selectedItem;
    public Items activeItem;
    public Text itemName;
    public Text itemDescription;
    public Text useButtonText;

    public Text goldText;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenuScreen()
    {
        if (theMenu.activeInHierarchy)
        {
            //theMenu.SetActive(false);
            //GameManager.instance.gameMenuOpen = false;
            hud.SetActive(true);
            CloseMenuWindow();
        }
        else
        {
            theMenu.SetActive(true);
            hud.SetActive(false);
            UpdateMainStats();
            GameManager.instance.gameMenuOpen = true;
            Time.timeScale = 0f;
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);
                playerName[i].text = playerStats[i].playerName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                levelText[i].text = "" + playerStats[i].playerLevel;
                expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].charImage;
                
            }
            else
            {
                charStatHolder[i].SetActive(false);
            }
        }
        goldText.text = "Gold: " + GameManager.instance.currentGold;
    }

    public void StatusMenuCharacterUpdate(int selected)
    {
        statusName.text = playerStats[selected].playerName;
        statusHP.text = "" + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        statusMP.text = "" + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        statusStrength.text = "" + playerStats[selected].strength;
        statusDefence.text = "" + playerStats[selected].defence;
        if(playerStats[selected].equippedWeapon != "")
        {
            statusWeaponEquipped.text = playerStats[selected].equippedWeapon;
        }
        statusWeaponPower.text = "" + playerStats[selected].weaponPower;
        if(playerStats[selected].equippedArmor != "")
        {
            statusArmorEquipped.text = playerStats[selected].equippedArmor;
        }
        statusArmorPower.text = "" + playerStats[selected].armorPower;
        statusCurrentExp.text = "" + playerStats[selected].currentEXP;
        statusEXPToNextLevel.text = "" + (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentEXP);
        statusLevel.text = "" + playerStats[selected].playerLevel;
        statusImage.sprite = playerStats[selected].charImage;
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;
            if(GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].ammountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].ammountText.text = "";
            }
        }
    }

    public void SelectItem(Items newItem)
    {
        activeItem = newItem;
        if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        }
        if(activeItem.isWeapon || activeItem.isArmour)
        {
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    public void UseItem(int selectCharacter)
    {
        activeItem.UseItem(selectCharacter);
        CloseItemCharacterChoiceMenu();
    }

    public void DiscardItem()
    {
        if(activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    #region Window and button open close

    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();

        for (int i = 0; i < windows.Length; i++)
        {
            if(i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }
        characterItemChoiceMenu.SetActive(false);
    }

    public void CloseMenuWindow()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
        characterItemChoiceMenu.SetActive(false);
        hud.SetActive(true);
        Time.timeScale = 1f;
    }

    public void QuitToDesktop()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void OpenStatusWindow()
    {
        UpdateMainStats();
        //update all the info for the player

        StatusMenuCharacterUpdate(0);

        for (int i = 0; i < playerStatusButton.Length; i++)
        {
            playerStatusButton[i].SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
            playerStatusButton[i].GetComponentInChildren<Text>().text = playerStats[i].playerName;
        }
    }

    public void OpenItemCharacterChoiceMenu()
    {
        characterItemChoiceMenu.SetActive(true);
        for (int i = 0; i < characterItemChoiceNameText.Length; i++)
        {
            characterItemChoiceNameText[i].text = GameManager.instance.playerStats[i].playerName;
            characterItemChoiceNameText[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void CloseItemCharacterChoiceMenu()
    {
        characterItemChoiceMenu.SetActive(false);
    }


    #endregion


}
