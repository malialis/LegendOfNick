using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/VectorValue")]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Value running in Game")]
    public Vector2 initialValue;
    [Header("Value when starting the game")]
    public Vector2 defaultValue;

    public void OnAfterDeserialize()
    {
        initialValue = defaultValue;
    }

    public void OnBeforeSerialize()
    {

    }

}
