using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/VectorValue")]
[System.Serializable]
public class VectorValue : ScriptableObject
{
    [Header("Value running in Game")]
    public Vector2 initialValue;
    [Header("Value when starting the game")]
    public Vector2 defaultValue;

   
}
