using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmour;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value; // to sell it at
    public Sprite itemSprite;
    public int amountToChange;

    [Header("Item Details")]
    public bool affectHP;
    public bool affectMP;
    public bool affectStrength;
    public bool affectDefence;

    [Header("Equipment Stuff")]
    public int weaponStrength;
    public int armourStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseItem(int charToUseOn)
    {
        PlayerStats selectedCharacter = GameManager.instance.playerStats[charToUseOn];

        if (isItem)
        {
            if (affectHP)
            {
                selectedCharacter.currentHP += amountToChange;
                if(selectedCharacter.currentHP > selectedCharacter.maxHP)
                {
                    selectedCharacter.currentHP = selectedCharacter.maxHP;
                }
            }
            if (affectMP)
            {
                selectedCharacter.currentMP += amountToChange;
                if (selectedCharacter.currentMP > selectedCharacter.maxMP)
                {
                    selectedCharacter.currentMP = selectedCharacter.maxMP;
                }
            }
            if (affectStrength)
            {
                selectedCharacter.strength += amountToChange;
            }
            if (affectDefence)
            {
                selectedCharacter.defence += amountToChange;
            }
        }

        if (isWeapon)
        {
            if(selectedCharacter.equippedWeapon != "")
            {
                GameManager.instance.AddItem(selectedCharacter.equippedWeapon);
            }
            selectedCharacter.equippedWeapon = itemName;
            selectedCharacter.weaponPower = weaponStrength;
        }

        if (isArmour)
        {
            if(selectedCharacter.equippedArmor != "")
            {
                GameManager.instance.AddItem(selectedCharacter.equippedArmor);
            }
            selectedCharacter.equippedArmor = itemName;
            selectedCharacter.armorPower = armourStrength;
        }

        GameManager.instance.RemoveItem(itemName);
    }


}
