using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public bool isItem;
    public bool isWeapon;
    public bool isArmour;

    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    public int amountToChange;

    public bool affectHP;
    public bool affectMP;
    public bool affectStrength;
    public bool affectDefence;

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
}
