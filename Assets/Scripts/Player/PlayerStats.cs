using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public string playerName;

    [Header("Level and Exp")]
    public int playerLevel = 1;
    public int maxLevel = 100;
    public int currentEXP;
    public int[] expToNextLevel;
    public int baseEXP = 1000;

    [Header("HP and MP")]
    public int currentHP;
    public int maxHP = 100;
    public int[] hpLvlBonus;
    public int currentMP;
    public int maxMP = 30;
    public int[] mpLvlBonus;

    [Header("Strength and Defence")]
    public int strength;
    public int defence;
    public int weaponPower;
    public int armorPower;

    [Header("Equipped Items")]
    public string equippedWeapon;
    public string equippedArmor;

    public Sprite charImage;



    // Start is called before the first frame update
    void Start()
    {
        StartingEXPSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartingEXPSetup()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.07f);
        }
    }

    public void AddExp(int expToAdd)
    {
        currentEXP += expToAdd;

        if(playerLevel < maxLevel)
        {
            if (currentEXP >= expToNextLevel[playerLevel] && playerLevel < maxLevel)
            {
                currentEXP -= expToNextLevel[playerLevel];
                playerLevel++;
                OnLevelUp();
            }
        }
        
        if(playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }
    }

    private void OnLevelUp()
    {
        maxHP = Mathf.FloorToInt(maxHP * 1.07f) + hpLvlBonus[playerLevel];
        currentHP = maxHP;
        maxMP = Mathf.FloorToInt(maxMP * 1.05f) + mpLvlBonus[playerLevel];
        currentMP = maxMP;

        //determine to add strength or defence on odd or even
        if (playerLevel % 2 == 0)
        {
            strength += 2;
        }
        else
        {
            defence += 2;
        }
    }

}
