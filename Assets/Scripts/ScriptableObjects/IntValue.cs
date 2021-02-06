using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/IntValue")]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;

    [HideInInspector]
    public float RuntimeValue;


    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }
}
