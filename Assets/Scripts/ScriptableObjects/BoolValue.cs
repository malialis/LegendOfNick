using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/BoolValue")]
[System.Serializable]
public class BoolValue : ScriptableObject
{
    public bool initialValue;

    [HideInInspector]
    public bool RuntimeValue;


}
