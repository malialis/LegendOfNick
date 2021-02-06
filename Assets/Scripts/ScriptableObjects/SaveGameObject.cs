using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/SaveGameObject")]
public class SaveGameObject : ScriptableObject
{
    public List<string> OpenedChests = new List<string>();
    public FloatValue life;
    public FloatValue mana;
    public Inventory playerInventory;
    //public List<Ability> abilities = new List<Ability>();
    public IntValue keys; //something like this

}
