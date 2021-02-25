using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/FloatValue")]
[System.Serializable]
public class FloatValue : ScriptableObject
{

    public float initialValue;

    [HideInInspector]
    public float RuntimeValue;


}
