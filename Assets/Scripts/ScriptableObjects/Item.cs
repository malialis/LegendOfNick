﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable/Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public string itemDescription;
    public bool isKey;



}
