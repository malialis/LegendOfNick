using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/FloatValue")]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
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
