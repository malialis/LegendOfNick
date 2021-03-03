using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject theMenu;
    public GameObject[] windows;

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

    // Start is called before the first frame update
    void Start()
    {
        
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
            CloseMenuWindow();
        }
        else
        {
            theMenu.SetActive(true);
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
    }

    public void CloseMenuWindow()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
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
            playerStatusButton[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            playerStatusButton[i].GetComponentInChildren<Text>().text = playerStats[i].playerName;
        }
    }


    #endregion


}
