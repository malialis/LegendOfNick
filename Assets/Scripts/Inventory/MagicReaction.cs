using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicReaction : MonoBehaviour
{
    public FloatValue playerMagic;
    public SignalSender magicSignal;

    public void Use(int ammountToIncrease)
    {
        playerMagic.RuntimeValue += ammountToIncrease;
        magicSignal.Raise();
    }
}
